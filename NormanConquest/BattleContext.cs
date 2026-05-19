using System;
using System.Collections.Generic;
using System.Text;

namespace NormanConquest
{
    public enum BattlePhase
    {
        WaitingForDefense,   // 等待防守方选择（抵御/不抵御）
        WaitingForPursuit,   // 等待反制方选择（追击/不追击）
        Resolved             // 连锁结束
    }
    // 这是一个类，表示一次进攻连锁的上下文，包含参与方、当前状态、已打出的部队牌等信息，以及处理连锁逻辑的方法
    public class BattleContext
    {
        public Player Attacker { get; private set; }
        public Player Defender { get; private set; }

        //当前进攻的部队
        public UnitCard CurrentAttackUnit { get; private set; }

        //本次连锁是否禁止追击
        public bool PursuitDisabled { get; set; }

        //当前状态
        public BattlePhase Phase { get; private set; }

        //连锁中双方打出的所有部队牌，结束时统一进弃牌堆
        public List<(UnitCard Card, Player Owner)> AllUnitsPlayed { get; private set; }
        //本次连锁是否造成了扣血
        public bool DamageDealt { get; private set; }

        // 记录造成扣血的那张进攻牌和它的主人
        public UnitCard SuccessfulAttackUnit { get; private set; }
        public Player SuccessfulAttackOwner { get; private set; }

        //回调：需要玩家选择时，通知 GameManager更新UI
        public Action<BattleContext> OnStateChanged { get; set; }

        //回调：连锁结束时调用
        public Action<BattleContext> OnResolved { get; set; }

        public BattleContext(Player attacker, Player defender, UnitCard attackUnit)
        {
            Attacker = attacker;
            Defender = defender;
            CurrentAttackUnit = attackUnit;
            PursuitDisabled = false;
            DamageDealt = false;
            AllUnitsPlayed = new List<(UnitCard, Player)>
            {
                (attackUnit, attacker)
            };
            Phase = BattlePhase.WaitingForDefense;
        }

        // 防守方选择不抵御
        public void DefenderPass()
        {
            Defender.HP -= 1;
            DamageDealt = true;
            SuccessfulAttackUnit = CurrentAttackUnit;
            SuccessfulAttackOwner = Attacker;
            Resolve();
        }

        // 防守方打出一张部队牌尝试抵御
        public void DefenderPlayUnit(UnitCard defenderUnit)
        {
            // 从防守方手牌移除
            Defender.Hand.Remove(defenderUnit);
            AllUnitsPlayed.Add((defenderUnit, Defender));

            bool defenderCounters = TypeCounterSystem.IsCounter(defenderUnit.UnitType, CurrentAttackUnit.UnitType);
            bool attackerCounters = TypeCounterSystem.IsCounter(CurrentAttackUnit.UnitType, defenderUnit.UnitType);

            if (attackerCounters)
            {
                // 防守方被克制：抵御失败，防守方扣血，连锁结束
                Defender.HP -= 1;
                DamageDealt = true;
                SuccessfulAttackUnit = CurrentAttackUnit;
                SuccessfulAttackOwner = Attacker;
                Resolve();
            }
            else if (defenderCounters)
            {
                // 防守方克制进攻方：抵御成功，攻守互换，进入追击阶段
                if (PursuitDisabled)
                {
                    //禁止追击，直接结束
                    Resolve();
                }
                else
                {
                    // 攻守互换
                    Player oldAttacker = Attacker;
                    Attacker = Defender;
                    Defender = oldAttacker;
                    CurrentAttackUnit = defenderUnit;
                    Phase = BattlePhase.WaitingForPursuit;
                    // 通知 GameManager 更新UI，等待新的进攻方选择是否追击
                    if (OnStateChanged != null)
                    {
                        OnStateChanged.Invoke(this);
                    }
                }
            }
            else
            {
                // 无克制关系：抵御成功，连锁结束
                Resolve();
            }
        }

        // 追击阶段，新的防守方（原进攻方）选择不追击
        public void PursuerPass()
        {
            Resolve();
        }

        // 追击阶段，追击方打出一张部队牌（视为新的进攻）
        public void PursuerPlayUnit(UnitCard pursuitUnit)
        {
            // 追击方就是当前的 Attacker
            Attacker.Hand.Remove(pursuitUnit);
            AllUnitsPlayed.Add((pursuitUnit, Attacker));
            CurrentAttackUnit = pursuitUnit;
            Phase = BattlePhase.WaitingForDefense;
            if (OnStateChanged != null)
            {
                OnStateChanged.Invoke(this);
            }
        }

        // 连锁结束：所有部队牌进各自拥有者的弃牌堆
        private void Resolve()
        {
            Phase = BattlePhase.Resolved;
            // 这里不直接操作弃牌堆，由 GameManager 在 OnResolved 回调中统一处理
            if (OnStateChanged != null)
            {
                OnStateChanged.Invoke(this);
            }
        }
    }

    
}
