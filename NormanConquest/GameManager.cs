using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace NormanConquest
{

    public class GameManager
    {
        public IGameUI UI { get; set; }
        public Player player { get; set; }
        public Player opponent { get; set; }
        public AIPlayer AI { get; set; } = new AIPlayer();
        public Player currentPlayer { get; set; }
        private int initialHP = 5;
        public bool processing = false;//当前是否正在处理某个效果，防止同时处理多个效果导致状态混乱
        private bool marketeffectused = false;//标记市场效果是否已经使用过了，确保每回合只触发一次
        public Attack currentAttack { get; set; }
        private EffectExecutor effectExecutor {  get; set; }
        public GameManager()
        {
            // 初始化游戏状态
            player = new Player("玩家", initialHP);
            opponent = new Player("敌人", initialHP);
            currentPlayer = player;
            effectExecutor = new EffectExecutor(this);
        }
        public void log(string message)
        {
            UI.Logout(message);
        }
        public void StartGame()
        {
            log("游戏开始！");
            if (player.CharacterIndex == 1)
            {
                log("玩家选择了英格兰国王，HP+2。");
                player.HP += 2;
            }
            else
            {
                log("敌人选择了英格兰国王，HP+2。");
                opponent.HP += 2;
            }
            currentPlayer = player; // 玩家先手
            // 游戏开始时双方各抽initialHP张牌
            for (int i = 0; i < initialHP; i++)
            {
                player.DrawCard();
                opponent.DrawCard();
            }
            UI.Refresh();
            StartTurn();
        }
        //结束回合
        public void EndTurn()
        {
            if(currentPlayer.PendingEffects.Contains("AllOutAttack"))
            {
                log($"{currentPlayer.Name}的倾巢出动效果结束。");
                currentPlayer.PendingEffects.Remove("AllOutAttack");
            }
            log($"{currentPlayer.Name}结束回合。");
            // 切换当前玩家
            currentPlayer = (currentPlayer == player) ? opponent : player;
            UI.Refresh();
            StartTurn();
            UI.Refresh();
        }
        //回合开始
        public void StartTurn()
        {
            log($"{currentPlayer.Name}的回合开始。");
            processing = true;
            marketeffectused = false;
            // 回合开始时抽牌
            while(currentPlayer.Hand.Count < currentPlayer.HP)
            {
                DrawCard(currentPlayer);
            }
            if (haveBuilding(currentPlayer, "庄园"))
            {
                log($"{currentPlayer.Name}拥有庄园。所以额外抽一张牌");
                DrawCard(currentPlayer);
            }
            //处理回合开始时的待处理效果
            ProcessPendingEffects(currentPlayer);
            currentPlayer.RemainingNormalAttacks = 1;
            if (haveBuilding(currentPlayer, "城堡"))
            {
                log($"由于{currentPlayer.Name}拥有城堡，通常攻击次数加一");
                currentPlayer.RemainingNormalAttacks += 1;
            }
            processing = false;
            UI.Refresh();
            if(currentPlayer == opponent)
            {
                AIAction();
            }
            else
            {
                UI.EnableEndTurnButton();
            }
            UI.Refresh();
        }
        //AI行动
        private async void AIAction()
        {
            log($"AI的行动回合");
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 800;
            AI.waiter = new TaskCompletionSource<bool>();
            timer.Tick += (s, e) =>
            {
                int ActIdx = AI.TakeAction(this);
                if (processing) return;
                if (currentAttack?.finish==false) return;
                if (ActIdx == -1)
                {
                    AI.waiter.SetResult(true);
                    timer.Stop();
                    return;
                }
                Card ActCard = opponent.Hand[ActIdx];
                if (ActCard.CardType == CardType.Unit)
                {
                    TryAttack(opponent, player, (UnitCard)ActCard, ActIdx);
                }
                else if (ActCard.CardType == CardType.Order)
                {
                    TakeOrder(opponent, ActIdx);
                }
                else if (ActCard.CardType == CardType.Building)
                {
                    TakeBuilding(opponent, ActIdx);
                }
                UI.Refresh();
            };
            timer.Start();
            await AI.waiter.Task;
            EndTurn();
        }
        //是否有某个建筑
        private bool haveBuilding(Player player, string name)
        {
            foreach (var card in player.BuildingZone)
            {
                if (card.Name == name) return true;
            }
            return false;
        }
        //找到某个建筑的索引
        private int findBuildingIndex(Player player, string name)
        {
            for (int i = 0; i < player.BuildingZone.Count; i++)
            {
                if (player.BuildingZone[i].Name == name) return i;
            }
            return -1;
        }
        //处理回合开始效果
        private void ProcessPendingEffects(Player player)
        {
            processing = true;
            if (player.PendingEffects.Count > 0)
            {
                log($"{player.Name}有以下待处理效果：");
                foreach (var effect in player.PendingEffects.ToList())
                {
                    log($"- {effect}");
                    Effect(player, effect);
                }
                // 处理完毕后清空待处理效果列表
                player.PendingEffects.Clear();
            }
            processing = false;
            UI.Refresh();
        }
        //抽牌
        public void DrawCard(Player _player)
        {
            processing = true;
            if (_player.Deck.Count == 0)
            {
                //牌堆空了，扣血洗牌
                TakeDamage(_player, 1);
                ResetDeck(_player);
                Card drawnCard = _player.DrawCard();
                if(_player.Name == "玩家")
                {
                    log($"{_player.Name}抽到了 {drawnCard.Name}。");
                }
                else log($"{_player.Name}抽了一张牌。");
            }
            else
            {
                Card drawnCard = _player.DrawCard();
                if (_player.Name == "玩家")
                {
                    log($"{_player.Name}抽到了 {drawnCard.Name}。");
                }
                else log($"{_player.Name}抽了一张牌。");
            }
            UI.Refresh();
            processing = false;
        }
        //重置牌堆
        private void ResetDeck(Player player)
        {
            processing = true;
            player.ReshuffleDiscardIntoDeck();
            log($"{player.Name}的牌堆已重置。");
            processing = false;
            UI.Refresh();
        }
        //弃牌
        public void Discard(Player player, int idx)
        {
            player.DiscardFromHand(idx);
            if(haveBuilding(player, "市场") && !marketeffectused)
            {
                log($"{player.Name}的市场！");
                DrawCard(player);
                marketeffectused = true;
            }
        }
        //弃建筑牌
        public void DiscardBuilding(Player player, int idx)
        {
            player.DiscardPile.Add(player.BuildingZone[idx]);
            player.BuildingZone.RemoveAt(idx);
            log($"{player.Name}失去了一个建筑！");
        }
        //扣血
        public void TakeDamage(Player player, int damage)
        {
            processing = true;
            player.HP -= damage;
            log($"{player.Name}受到 {damage} 点伤害，剩余 HP：{player.HP}。");
            if (player.HP <= 0)
            {
                log($"{player.Name}被击败了！");
                UI.GameOver((player == this.player) ? opponent : player);
            }
            processing = false;
            UI.Refresh();
        }
        //处理效果
        private void Effect(Player player, string effectName)
        {
            processing = true;
            switch (effectName)
            {
                case "AllOutAttack":
                    
                    effectExecutor.AllOutAttack(player);
                    break;
                case "Levy":
                    effectExecutor.Levy(player);
                    break;
                case "LandDevelopment":
                    effectExecutor.LandDevelopment(player);
                    break;
                case "Taxation":
                    effectExecutor.Taxation(player);
                    break;
                case "Enfeoffment":
                    effectExecutor.Enfeoffment(player);
                    break;
                case "FakeRetreat":
                    effectExecutor.FakeRetreat(player);
                    break;
                case "Anathema":
                    effectExecutor.Anathema(player);
                    break;
            }
            processing = false;
            UI.Refresh();
        }
        public void AddEffect(Player player, string effectName)
        {
            player.PendingEffects.Add(effectName);
            log($"{player.Name}获得了一个效果：{effectName}。");
        }
        //进攻
        public void TryAttack(Player Attacker, Player Defender, UnitCard AttackUnit, int attackerUnitIndex)
        {
            if(Attacker.PendingEffects.Contains("AllOutAttack"))
            {
                log($"{Attacker.Name}的倾巢出动！");
                TrySpecialAttack(Attacker, Defender, AttackUnit, attackerUnitIndex);
                currentAttack.PursuitDisabled = true;
                return;
            }
            if (Attacker.RemainingNormalAttacks <= 0)
            {
                log($"{Attacker.Name}没有剩余的通常攻击次数了，无法发动攻击。");
                return;
            }
            UI.Logout($"{Attacker.Name}尝试用{AttackUnit.Name}攻击{Defender.Name}。");
            processing = true;
            Attacker.RemainingNormalAttacks -= 1;
            currentAttack = new Attack(Attacker, Defender, AttackUnit, null, this, attackerUnitIndex, -1);
            if(currentAttack.Defender == player) UI.PromptDefense(currentAttack);
            else AIDefense(currentAttack);
        }
        public void TrySpecialAttack(Player Attacker, Player Defender, UnitCard AttackUnit, int attackerUnitIndex)
        {
            UI.Logout($"{Attacker.Name}尝试用{AttackUnit.Name}进行特殊攻击{Defender.Name}。");
            processing = true;
            currentAttack = new Attack(Attacker, Defender, AttackUnit, null, this, attackerUnitIndex, -1);
            if (currentAttack.Defender == player) UI.PromptDefense(currentAttack);
            else AIDefense(currentAttack);
        }
        public void PursuitAttack()
        {
            UI.Logout($"{currentAttack.Attacker.Name}发动了追击！");
            processing = true;
            if (currentAttack.Defender == player) UI.PromptDefense(currentAttack);
            else AIDefense(currentAttack);
        }
        public void TakeAttack(int DefenseUnitIndex)
        {
            UnitCard DefenseUnit = (UnitCard)currentAttack.Defender.Hand[DefenseUnitIndex];
            currentAttack.DefenderUnit = DefenseUnit;
            currentAttack.DefenderUnitIndex = DefenseUnitIndex;
            log($"{currentAttack.Defender.Name}使用{DefenseUnit.Name}防御");
            currentAttack.Execute();
            if(currentAttack.DamageDealt && haveBuilding(currentAttack.Defender, "城墙"))
            {
                log("由于防御方拥有城墙，此次进攻失败！");
                DiscardBuilding(currentAttack.Defender, findBuildingIndex(currentAttack.Defender, "城墙"));
                currentAttack.DamageDealt = false;
            }
            if (haveBuilding(currentAttack.Attacker, "马厩") &&
                (currentAttack.AttackUnit.UnitType == UnitType.LightCavalry || currentAttack.AttackUnit.UnitType == UnitType.HeavyCavalry))
            {
                UI.Logout("由于攻击单位是骑兵且攻击方拥有马厩，攻击方不丢牌");
                Discard(currentAttack.Defender, currentAttack.DefenderUnitIndex);
            }
            else
            {
                Discard(currentAttack.Attacker, currentAttack.AttackerUnitIndex);
                Discard(currentAttack.Defender, currentAttack.DefenderUnitIndex);
            }
            if (currentAttack.DamageDealt)
            {
                int demage = 1;
                if (haveBuilding(currentAttack.Attacker, "兵营"))
                {
                    log("由于攻击方拥有兵营，攻击造成的伤害增加1点！");
                    demage += 1;
                }
                if (currentAttack.Attacker.CharacterIndex == 0)
                {
                    log("由于攻击方是诺曼底公爵，攻击造成的伤害增加1点！");
                    demage += 1;
                }
                UI.Logout("攻击成功，造成了伤害！");
                TakeDamage(currentAttack.Defender, demage);
                if(haveBuilding(currentAttack.Attacker, "教堂"))
                {
                    log("由于攻击方拥有教堂，攻击方回复了1点HP！");
                    currentAttack.Attacker.HP += 1;
                }
            }
            if (currentAttack.NeedPursuit)
            {
                currentAttack = currentAttack.PursuitAttack;
                PursuitAttack();
            }
            processing = false;
            UI.Refresh();
        }
        public void TakeAttackWithoutDefense()
        {
            log($"{currentAttack.Defender.Name}选择不防御");
            currentAttack.finish = true;
            if (haveBuilding(currentAttack.Attacker, "马厩") &&
                (currentAttack.AttackUnit.UnitType == UnitType.LightCavalry || currentAttack.AttackUnit.UnitType == UnitType.HeavyCavalry))
            {
                UI.Logout("由于攻击单位是骑兵且攻击方拥有马厩，攻击方不丢牌");
                Discard(currentAttack.Defender, currentAttack.DefenderUnitIndex);
            }
            else
            {
                Discard(currentAttack.Attacker, currentAttack.AttackerUnitIndex);
                Discard(currentAttack.Defender, currentAttack.DefenderUnitIndex);
            }
            int demage = 1;
            if (haveBuilding(currentAttack.Attacker, "兵营"))
            {
                log("由于攻击方拥有兵营，攻击造成的伤害增加1点！");
                demage += 1;
            }
            if (currentAttack.Attacker.CharacterIndex == 0)
            {
                log("由于攻击方是诺曼底公爵，攻击造成的伤害增加1点！");
                demage += 1;
            }
            UI.Logout("攻击成功，造成了伤害！");
            TakeDamage(currentAttack.Defender, demage);
            processing = false;
        }
        private void AIDefense(Attack attack)
        {
            int CardIdx = AI.DecideDefense(attack);
            if (CardIdx == -1)
            {

                TakeAttackWithoutDefense();
            }
            else
            {
                TakeAttack(CardIdx);
            }
        }
        public void TakeOrder(Player player, int CardIdx)
        {
            OrderCard order = (OrderCard)player.Hand[CardIdx];
            Discard(player, CardIdx);
            log($"{player.Name}使用了指令牌：{order.Name}。");
            Effect(player, order.EffectName);
            UI.Refresh();
        }
        public void TakeBuilding(Player player, int CardIdx)
        {
            BuildingCard building = (BuildingCard)player.Hand[CardIdx];
            player.Hand.RemoveAt(CardIdx);
            AddBuilding(player, building);
            UI.Refresh();
        }
        public void AddBuilding(Player player, BuildingCard building)
        {
            if(player.BuildingZone.Count >= player.BuildingLimit)
            {
                log($"{player.Name}的建筑区已满，无法建造{building.Name}。");
                player.DiscardPile.Add(building);
                UI.Refresh();
                return;
            }
            player.BuildingZone.Add(building);
            log($"{player.Name}的建筑区新增了{building.Name}。");
            UI.Refresh();
        }
        
        public interface IGameUI
        {
            void Refresh();
            void Logout(string message);
            void PromptDefense(Attack attack);
            void EnableEndTurnButton();
            void GameOver(Player winner);
        }
    }
}
