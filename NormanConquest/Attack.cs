using System;
using System.Collections.Generic;
using System.Text;

namespace NormanConquest
{
    public class Attack
    {
        public Player Attacker {  get; set; }
        public Player Defender { get; set; }
        public UnitCard AttackUnit { get; set; }
        public UnitCard DefenderUnit { get; set; }
        public bool PursuitDisabled { get; set; }
        public GameManager gameManager { get; set; }
        public bool DamageDealt { get; set; }
        public bool NeedPursuit { get; set; }
        public Attack PursuitAttack { get; set; }
        public int AttackerUnitIndex { get; set; }
        public int DefenderUnitIndex { get; set; }
        public bool finish {  get; set; } = false;
        public Attack(Player attacker, Player defender, UnitCard attackUnit, UnitCard defenderUnit, GameManager gameManager, int attackerUnitIndex, int defenderUnitIndex   )
        {
            Attacker = attacker;
            Defender = defender;
            AttackUnit = attackUnit;
            DefenderUnit = defenderUnit;
            PursuitDisabled = false;
            this.gameManager = gameManager;
            DamageDealt = false;
            NeedPursuit = false;
            AttackerUnitIndex = attackerUnitIndex;
            DefenderUnitIndex = defenderUnitIndex;
        }
        //执行进攻的逻辑，仅仅更新数据，具体的伤害计算和状态更新由GameManager处理
        public void Execute()
        {
            bool AttackerCounter = TypeCounterSystem.IsCounter(AttackUnit.UnitType, DefenderUnit.UnitType);
            bool DefenderCounter = TypeCounterSystem.IsCounter(DefenderUnit.UnitType, AttackUnit.UnitType);
            if (AttackerCounter)
            {
                DamageDealt = true;
            }
            if (DefenderCounter) 
            { 
                if(!PursuitDisabled)
                {
                    NeedPursuit = true;
                    CreatePursuit();
                }
            }
            finish = true;
        }
        public void CreatePursuit()
        {
            PursuitAttack = new Attack(Defender, Attacker, DefenderUnit, null, gameManager, DefenderUnitIndex, -1);
        }
    }
}
