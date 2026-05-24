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
        public Player currentPlayer { get; set; }
        public bool isFirstTurn { get; set; }
        private int initialHP = 5;
        public Attack currentAttack { get; set; }
        public GameManager()
        {
            // 初始化游戏状态
            player = new Player("玩家", initialHP);
            opponent = new Player("敌人", initialHP);
            currentPlayer = player;
        }
        void log(string message)
        {
            UI.Logout(message);
        }
        public void StartGame()
        {
            log("游戏开始！");
            player = new Player("玩家", initialHP);
            opponent = new Player("敌人", initialHP);
            currentPlayer = player; // 玩家先手
            isFirstTurn = true;
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
            log($"{currentPlayer.Name}结束回合。");
            // 切换当前玩家
            currentPlayer = (currentPlayer == player) ? opponent : player;
            isFirstTurn = false;
            UI.Refresh();
            StartTurn();
        }
        //回合开始
        public void StartTurn()
        {
            log($"{currentPlayer.Name}的回合开始。");
            // 回合开始时抽一张牌
            if (!isFirstTurn) DrawCard(currentPlayer);
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
            UI.Refresh();
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
        //处理回合开始效果
        private void ProcessPendingEffects(Player player)
        {
            if (player.PendingEffects.Count > 0)
            {
                log($"{player.Name}有以下待处理效果：");
                foreach (var effect in player.PendingEffects)
                {
                    log($"- {effect}");
                    Effect(player, effect);
                }
                // 处理完毕后清空待处理效果列表
                player.PendingEffects.Clear();
            }
        }
        //抽牌
        private void DrawCard(Player player)
        {
            if (player.Deck.Count == 0)
            {
                //牌堆空了，扣血洗牌
                TakeDamage(player, 1);
                ResetDeck(player);
                Card drawnCard = player.DrawCard();
                log($"{player.Name}抽到了 {drawnCard.Name}。");
            }
            else
            {
                if (player.Hand.Count >= player.HandLimit)
                {
                    Card discardedCard = player.DiscardTopCard();
                    log($"{player.Name}的手牌已满，抽牌时丢弃了 {discardedCard.Name}。");
                }
                else
                {
                    Card drawnCard = player.DrawCard();
                    log($"{player.Name}抽到了 {drawnCard.Name}。");
                }
            }
        }
        //重置牌堆
        private void ResetDeck(Player player)
        {
            player.ReshuffleDiscardIntoDeck();
            log($"{player.Name}的牌堆已重置。");
        }
        //扣血
        private void TakeDamage(Player player, int damage)
        {
            player.HP -= damage;
            log($"{player.Name}受到 {damage} 点伤害，剩余 HP：{player.HP}。");
            if (player.HP <= 0)
            {
                log($"{player.Name}被击败了！");
                // 游戏结束处理
            }
        }
        //处理效果
        private void Effect(Player player, string effectName)
        {
            //实现
        }
        //进攻
        public void TryAttack(Player Attacker, Player Defender, UnitCard AttackUnit, int attackerUnitIndex)
        {
            if (Attacker.RemainingNormalAttacks <= 0)
            {
                log($"{Attacker.Name}没有剩余的通常攻击次数了，无法发动攻击。");
                return;
            }
            UI.Logout($"{Attacker.Name}尝试用{AttackUnit.Name}攻击{Defender.Name}。");
            Attacker.RemainingNormalAttacks -= 1;
            currentAttack = new Attack(Attacker, Defender, AttackUnit, null, this, attackerUnitIndex, -1);
            UI.PromptDefense(currentAttack);
        }
        public void TrySpecialAttack(Player Attacker, Player Defender, UnitCard AttackUnit, int attackerUnitIndex)
        {
            UI.Logout($"{Attacker.Name}尝试用{AttackUnit.Name}进行特殊攻击{Defender.Name}。");
            currentAttack = new Attack(Attacker, Defender, AttackUnit, null, this, attackerUnitIndex, -1);
            UI.PromptDefense(currentAttack);
        }
        public void PursuitAttack()
        {
            UI.Logout($"{currentAttack.Defender.Name}发动了追击！");
            UI.PromptDefense(currentAttack);
        }
        public void TakeAttack(UnitCard DefenseUnit)
        {
            currentAttack.DefenderUnit = DefenseUnit;
            currentAttack.Execute();
            if (haveBuilding(currentAttack.Attacker, "马厩") &&
                (currentAttack.AttackUnit.UnitType == UnitType.LightCavalry || currentAttack.AttackUnit.UnitType == UnitType.HeavyCavalry))
            {
                UI.Logout("由于攻击单位是骑兵且攻击方拥有马厩，攻击方不丢牌");
                currentAttack.Defender.DiscardFromHand(currentAttack.DefenderUnitIndex);
            }
            else
            {
                currentAttack.Attacker.DiscardFromHand(currentAttack.AttackerUnitIndex);
                currentAttack.Defender.DiscardFromHand(currentAttack.DefenderUnitIndex);
            }
            if (currentAttack.DamageDealt)
            {
                UI.Logout("攻击成功，造成了伤害！");
                TakeDamage(currentAttack.Defender, 1);
            }
            if (currentAttack.NeedPursuit)
            {
                currentAttack = currentAttack.PursuitAttack;
                PursuitAttack();
            }
        }
        public void TakeAttackWithoutDefense()
        {
            if (haveBuilding(currentAttack.Attacker, "马厩") &&
                (currentAttack.AttackUnit.UnitType == UnitType.LightCavalry || currentAttack.AttackUnit.UnitType == UnitType.HeavyCavalry))
            {
                UI.Logout("由于攻击单位是骑兵且攻击方拥有马厩，攻击方不丢牌");
                currentAttack.Defender.DiscardFromHand(currentAttack.DefenderUnitIndex);
            }
            else
            {
                currentAttack.Attacker.DiscardFromHand(currentAttack.AttackerUnitIndex);
                currentAttack.Defender.DiscardFromHand(currentAttack.DefenderUnitIndex);
            }
            UI.Logout("攻击成功，造成了伤害！");
            TakeDamage(currentAttack.Defender, 1);
        }
        public void TakeOrder(Player player, OrderCard order)
        {
            log($"{player.Name}使用了指令牌：{order.Name}。");
            //根据命令效果进行处理
        }
        public void TakeBuilding(Player player, BuildingCard building)
        {
            log($"{player.Name}建造了建筑：{building.Name}。");
            //根据建筑效果进行处理
        }

        public interface IGameUI
        {
            void Refresh();
            void Logout(string message);
            void PromptDefense(Attack attack);
        }
    }
}
