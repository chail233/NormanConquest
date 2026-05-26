using System;
using System.Collections.Generic;
using System.Text;

namespace NormanConquest
{
    public class AIPlayer
    {
        private Random rng = new Random();
        public TaskCompletionSource<bool> waiter;

        // AI 选择行动：进攻、打指令、放建筑、或结束回合
        public int TakeAction(GameManager game)
        {
            Player self = game.opponent;
            Player opponent = game.player;

            // 优先级1：有部队牌且能进攻 → 通常进攻
            if (self.RemainingNormalAttacks > 0 || self.PendingEffects.Contains("AllOutAttack"))
            {
                int attackUnitIdx = ChooseAttackUnit(self, opponent);
                if (attackUnitIdx != -1)
                {
                    return attackUnitIdx;
                }
            }

            // 优先级2：打指令牌
            int orderidx = ChooseOrderCard(self);
            if (orderidx!=-1)
            {
                return orderidx;
            }

            // 优先级3：放建筑牌
            int buildingIdx = ChooseBuildingCard(self);
            if (buildingIdx != -1)
            {
                return buildingIdx;
            }

            // 无事可做，结束回合
            return -1;
        }

        // AI 选择抵御或不出牌
        public int DecideDefense(Attack attack)
        {
            Player defender = attack.Defender;

            // 找一张能克制进攻方的部队牌
            UnitCard attackerUnit = attack.AttackUnit;
            int bestDefenderidx = -1;

            for (int i = 0; i < defender.Hand.Count; i++)
            {
                var card = defender.Hand[i];
                if (card is UnitCard unit)
                {
                    // 优先选能克制进攻方的
                    if (TypeCounterSystem.IsCounter(unit.UnitType, attackerUnit.UnitType))
                    {
                        bestDefenderidx = i;
                        break; // 找到克制的直接选
                    }
                    // 其次选不被克制的
                    if (!TypeCounterSystem.IsCounter(attackerUnit.UnitType, unit.UnitType))
                    {
                        if (bestDefenderidx == -1)
                            bestDefenderidx = i;
                    }
                }
            }
            return bestDefenderidx;
        }

        // 选择一张用于进攻的部队牌
        private int ChooseAttackUnit(Player self, Player opponent)
        {
            List<int> units = new List<int>();
            for (int i=0;i<self.Hand.Count;++i)
            {
                var card = self.Hand[i];
                if (card is UnitCard unit)
                    units.Add(i);
            }

            if (units.Count == 0) return -1;

            // 简单策略：随机选一张
            return units[rng.Next(units.Count)];
        }

        // 选择一张指令牌打出
        private int ChooseOrderCard(Player self)
        {
            List<int> oreders = new List<int>();
            for (int i = 0; i < self.Hand.Count; i++)
            {
                var card = self.Hand[i];
                if (card is OrderCard order)
                {
                    oreders.Add(i);
                    // 优先打有益效果
                    if (order.EffectName == "Taxation" ||
                        order.EffectName == "LandDevelopment" ||
                        order.EffectName == "Levy" ||
                        order.EffectName == "Enfeoffment")
                    {
                        return i;
                    }
                }
            }
            if (oreders.Count > 0){
                return oreders[rng.Next(oreders.Count)];
            }
            return -1;
        }

        // 选择一张建筑牌放置
        private int ChooseBuildingCard(Player self)
        {
            // 建筑区没满才放
            if (self.BuildingZone.Count >= self.BuildingLimit) return -1;

            for (int i = 0; i < self.Hand.Count; i++)
            {
                var card = self.Hand[i];
                if (card is BuildingCard building)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
