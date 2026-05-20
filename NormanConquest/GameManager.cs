using System;
using System.Collections.Generic;
using System.Text;

namespace NormanConquest
{
    public class GameManager
    {
        // 玩家 
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public Player CurrentPlayer { get; private set; }
        public Player OpponentPlayer { get; private set; }

        // 回合相关
        public int TurnNumber { get; private set; }
        public bool IsFirstTurn { get; private set; }

        // 当前正在进行的战斗连锁（同时只能有一个）
        public BattleContext ActiveBattle { get; private set; }

        // UI 回调接口
        public IGameUI UI { get; set; }

        // 初始 HP
        private int initialHP;

        public GameManager(int hp = 10)
        {
            initialHP = hp;
        }

        // 初始化并开始游戏
        public void StartGame()
        {
            Player1 = new Player("玩家1", initialHP);
            Player2 = new Player("玩家2", initialHP);

            // 初始抽牌：各抽等同于 HP 数量的牌
            for (int i = 0; i < initialHP; i++)
            {
                Player1.DrawCard();
                Player2.DrawCard();
            }

            // 处理牌堆为空的情况
            CheckDeckEmpty(Player1);
            CheckDeckEmpty(Player2);

            CurrentPlayer = Player1;
            OpponentPlayer = Player2;
            TurnNumber = 1;
            IsFirstTurn = true;

            // 先手第一回合不抽牌，直接进入行动阶段
            CurrentPlayer.RemainingNormalAttacks = GetMaxNormalAttacks(CurrentPlayer);
            UI?.RefreshAll();// 刷新界面显示初始状态, ?表示如果UI不为null才调用
            UI?.PromptPlayerAction(CurrentPlayer);
        }

        // 结束当前玩家的回合
        public void EndTurn()
        {
            // 切换玩家
            Player temp = CurrentPlayer;
            CurrentPlayer = OpponentPlayer;
            OpponentPlayer = temp;

            TurnNumber++;
            IsFirstTurn = false;

            // 新回合开始
            BeginTurn();
        }

        private void BeginTurn()
        {
            // 1. 处理延迟效果
            ProcessPendingEffects(CurrentPlayer);

            // 2. 回合开始抽牌
            if (!IsFirstTurn)
            {
                CurrentPlayer.DrawCard();
                CheckDeckEmpty(CurrentPlayer);
            }

            // 3. 触发建筑回合开始效果
            TriggerBuildingTurnStart(CurrentPlayer);

            // 4. 检查手牌上限，溢出则强制弃牌
            CheckHandOverflow(CurrentPlayer);

            // 5. 重置通常进攻次数
            CurrentPlayer.RemainingNormalAttacks = GetMaxNormalAttacks(CurrentPlayer);

            // 6. 检查HP
            CheckBuildingOverflow(CurrentPlayer);

            UI?.RefreshAll();
            UI?.PromptPlayerAction(CurrentPlayer);
        }

        // 进攻入口 

        // 玩家尝试发起通常进攻
        public bool TryNormalAttack(UnitCard unitCard)
        {
            if (ActiveBattle != null) return false;
            if (CurrentPlayer.RemainingNormalAttacks <= 0) return false;
            if (!CurrentPlayer.Hand.Contains(unitCard)) return false;

            CurrentPlayer.RemainingNormalAttacks--;
            StartBattle(CurrentPlayer, OpponentPlayer, unitCard);
            return true;
        }

        // 特殊进攻入口
        public void StartSpecialAttack(Player attacker, Player defender, UnitCard unitCard, bool disablePursuit = false)
        {
            // 特殊进攻不消耗 RemainingNormalAttacks
            StartBattle(attacker, defender, unitCard, disablePursuit);
        }

        private void StartBattle(Player attacker, Player defender, UnitCard unitCard, bool disablePursuit = false)
        {
            attacker.Hand.Remove(unitCard);

            ActiveBattle = new BattleContext(attacker, defender, unitCard);
            ActiveBattle.PursuitDisabled = disablePursuit;
            ActiveBattle.OnStateChanged = OnBattleStateChanged;
            ActiveBattle.OnResolved = OnBattleResolved;

            UI?.RefreshAll();
            UI?.PromptDefense(ActiveBattle);
        }

        // 战斗回调

        private void OnBattleStateChanged(BattleContext context)
        {
            UI?.RefreshAll();

            if (context.Phase == BattlePhase.WaitingForDefense)
            {
                UI?.PromptDefense(context);
            }
            else if (context.Phase == BattlePhase.WaitingForPursuit)
            {
                UI?.PromptPursuit(context);
            }
        }

        private void OnBattleResolved(BattleContext context)
        {
            // 所有部队牌进各自拥有者的弃牌堆
            foreach (var (card, owner) in context.AllUnitsPlayed)
            {
                owner.DiscardPile.Add(card);
            }

            // 马厩效果：骑兵进攻成功回手
            ProcessStableEffect(context);

            // 清空当前连锁
            ActiveBattle = null;

            // 检查双方HP
            if (CheckGameOver()) return;

            UI?.RefreshAll();
            UI?.PromptPlayerAction(CurrentPlayer);
        }

        // 建筑效果

        private int GetMaxNormalAttacks(Player player)
        {
            int baseAttacks = 1;
            foreach (var building in player.BuildingZone)
            {
                if (building.EffectName == "Castle")
                    baseAttacks = 2;
            }
            return baseAttacks;
        }

        private void TriggerBuildingTurnStart(Player player)
        {
            foreach (var building in player.BuildingZone)
            {
                if (building.EffectName == "Manor")
                {
                    player.DrawCard();
                    CheckDeckEmpty(player);
                }
            }
        }

        private void ProcessStableEffect(BattleContext context)
        {
            if (!context.DamageDealt) return;
            if (context.SuccessfulAttackOwner == null) return;

            Player owner = context.SuccessfulAttackOwner;
            bool hasStable = false;
            foreach (var building in owner.BuildingZone)
            {
                if (building.EffectName == "Stable")
                {
                    hasStable = true;
                    break;
                }
            }
            if (!hasStable) return;

            UnitType unitType = context.SuccessfulAttackUnit.UnitType;
            if (unitType == UnitType.LightCavalry || unitType == UnitType.HeavyCavalry)
            {
                // 从弃牌堆移回手牌
                owner.DiscardPile.Remove(context.SuccessfulAttackUnit);
                owner.Hand.Add(context.SuccessfulAttackUnit);
            }
        }

        //牌堆管理

        private void CheckDeckEmpty(Player player)
        {
            if (player.Deck.Count > 0) return;
            if (player.DiscardPile.Count == 0) return; // 弃牌堆也空，无牌可抽，暂时不处理

            player.HP -= 1;
            player.ReshuffleDiscardIntoDeck();
        }

        // 手牌上限

        public void CheckHandOverflow(Player player)
        {
            bool hasBarracks = false;
            foreach (var building in player.BuildingZone)
            {
                if (building.EffectName == "Barracks")
                {
                    hasBarracks = true;
                    break;
                }
            }

            int limit = player.HandLimit;
            while (true)
            {
                // 计算有效手牌数量：如果有兵营，部队牌不计入上限
                int effectiveCount = hasBarracks
                    ? player.Hand.Count(c => c.CardType != CardType.Unit)
                    : player.Hand.Count;

                if (effectiveCount <= limit) break;

                // 需要弃牌
                // 简化：自动弃最后一张非部队牌（或第一张牌）
                Card toDiscard = player.Hand[player.Hand.Count - 1];
                player.DiscardFromHand(toDiscard);
            }
        }

        //建筑区上限

        public void CheckBuildingOverflow(Player player)
        {
            int limit = player.BuildingLimit;
            while (player.BuildingZone.Count > limit)
            {
                // 需要弃建筑
                // 简化：弃最后一个
                BuildingCard toRemove = player.BuildingZone[player.BuildingZone.Count - 1];
                player.BuildingZone.Remove(toRemove);
                player.DiscardPile.Add(toRemove);
            }
        }

        //指令牌处理

        public void PlayOrderCard(OrderCard card)
        {
            if (!CurrentPlayer.Hand.Contains(card)) return;
            CurrentPlayer.Hand.Remove(card);

            switch (card.EffectName)
            {
                case "Levy":
                    ExecuteLevy(CurrentPlayer);
                    break;
                case "LandDevelopment":
                    ExecuteLandDevelopment(CurrentPlayer);
                    break;
                case "Taxation":
                    ExecuteTaxation(CurrentPlayer);
                    break;
                case "Enfeoffment":
                    ExecuteEnfeoffment(CurrentPlayer);
                    break;
                case "AllOutAttack":
                    ExecuteAllOutAttack(CurrentPlayer, OpponentPlayer);
                    break;
                case "FakeRetreat":
                    ExecuteFakeRetreat(CurrentPlayer);
                    break;
                case "Anathema":
                    ExecuteAnathema(CurrentPlayer, OpponentPlayer);
                    break;
            }

            CurrentPlayer.DiscardPile.Add(card);
            if (CheckGameOver()) return;
            UI?.RefreshAll();
            UI?.PromptPlayerAction(CurrentPlayer);
        }
        // 征召：抽2张部队牌
        private void ExecuteLevy(Player player)
        {
            // 抽2张部队牌：从牌堆依次翻牌直到找到2张部队牌
            int found = 0;
            List<Card> nonUnitCards = new List<Card>();
            while (found < 2 && player.Deck.Count > 0)
            {
                Card card = player.Deck[0];
                player.Deck.RemoveAt(0);
                if (card is UnitCard)
                {
                    player.Hand.Add(card);
                    found++;
                }
                else
                {
                    nonUnitCards.Add(card);
                }
            }
            // 非部队牌放回牌堆顶部
            for (int i = nonUnitCards.Count - 1; i >= 0; i--)
            {
                player.Deck.Insert(0, nonUnitCards[i]);
            }
        }
        // 土地开发：抽2张牌
        private void ExecuteLandDevelopment(Player player)
        {
            player.DrawCard();
            CheckDeckEmpty(player);
            player.DrawCard();
            CheckDeckEmpty(player);
        }
        // 税收：HP回复2点
        private void ExecuteTaxation(Player player)
        {
            player.HP += 2;
        }
        // 分封：HP扣1，抽3张牌
        private void ExecuteEnfeoffment(Player player)
        {
            player.HP -= 1;
            player.DrawCard();
            CheckDeckEmpty(player);
            player.DrawCard();
            CheckDeckEmpty(player);
            player.DrawCard();
            CheckDeckEmpty(player);
        }
        // 倾巢出动：打出手牌中所有部队牌，每张视为特殊进攻，禁止追击
        private void ExecuteAllOutAttack(Player attacker, Player defender)
        {
            // 收集手牌中所有部队牌
            List<UnitCard> units = new List<UnitCard>();
            foreach (var card in attacker.Hand)
            {
                if (card is UnitCard unit)
                    units.Add(unit);
            }
            // 依次打出，每张视为特殊进攻，禁止追击
            foreach (var unit in units)
            {
                if (!attacker.Hand.Contains(unit)) continue; // 可能在连锁中被弃掉
                if (ActiveBattle != null)
                {
                    // 等当前连锁结束再打下一次？这里简化：直接打
                    // 实际上需要排队，但先这样
                }
                StartSpecialAttack(attacker, defender, unit, disablePursuit: true);
            }
        }
        // 假装撤退：弃一张部队牌，下一次抽牌阶段额外抽2张牌
        private void ExecuteFakeRetreat(Player player)
        {
            // 需要玩家选择弃哪张部队牌，这里简化：弃手牌中第一张部队牌
            UnitCard toDiscard = null;
            foreach (var card in player.Hand)
            {
                if (card is UnitCard unit)
                {
                    toDiscard = unit;
                    break;
                }
            }
            if (toDiscard != null)
            {
                player.DiscardFromHand(toDiscard);
                player.PendingEffects.Add("FakeRetreat");
            }
        }
        //绝罚：目标玩家弃一张手牌
        private void ExecuteAnathema(Player caster, Player target)
        {
            //随机弃一张手牌
            if (target.Hand.Count > 0)
            {
                Random rng = new Random();
                int index = rng.Next(target.Hand.Count);
                Card card = target.Hand[index];
                target.DiscardFromHand(index);
            }
        }
        // 处理回合开始时的延迟效果
        private void ProcessPendingEffects(Player player)
        {
            foreach (var effect in player.PendingEffects)
            {
                // 假装撤退的效果：抽2张牌
                if (effect == "FakeRetreat")
                {
                    player.DrawCard();
                    CheckDeckEmpty(player);
                    player.DrawCard();
                    CheckDeckEmpty(player);
                }
            }
            player.PendingEffects.Clear();
        }

        //建筑牌处理

        public void PlayBuildingCard(BuildingCard card)
        {
            if (!CurrentPlayer.Hand.Contains(card)) return;

            // 检查建筑区是否已满
            if (CurrentPlayer.BuildingZone.Count >= CurrentPlayer.BuildingLimit)
            {
                // 需要先弃一张建筑，通知UI选择
                // 简化：自动弃第一个建筑
                if (CurrentPlayer.BuildingZone.Count > 0)
                {
                    BuildingCard toRemove = CurrentPlayer.BuildingZone[0];
                    CurrentPlayer.BuildingZone.Remove(toRemove);
                    CurrentPlayer.DiscardPile.Add(toRemove);
                }
            }

            CurrentPlayer.Hand.Remove(card);
            CurrentPlayer.BuildingZone.Add(card);
            // 教堂和市场的效果暂不实现

            UI?.RefreshAll();
            UI?.PromptPlayerAction(CurrentPlayer);
        }

        //胜负判定

        private bool CheckGameOver()
        {
            if (Player1.HP <= 0)
            {
                UI?.OnGameOver(Player2);
                return true;
            }
            if (Player2.HP <= 0)
            {
                UI?.OnGameOver(Player1);
                return true;
            }
            return false;
        }
    }
    /// 这是一个接口，定义了游戏UI需要实现的方法，GameManager通过这个接口与UI交互，通知UI刷新界面、提示玩家操作等
    public interface IGameUI
    {
        void RefreshAll();
        void PromptPlayerAction(Player player);
        void PromptDefense(BattleContext context);
        void PromptPursuit(BattleContext context);
        void OnGameOver(Player winner);
    }
}
