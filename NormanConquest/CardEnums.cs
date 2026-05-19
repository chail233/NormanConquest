using System;
using System.Collections.Generic;
using System.Text;

namespace NormanConquest
{
    //牌类型枚举
    public enum CardType
    {
        Unit,      // 部队牌
        Order,     // 指令牌
        Building   // 建筑牌
    }

    public enum UnitType
    {
        LightInfantry,   // 轻步兵
        HeavyInfantry,   // 重步兵
        Archer,          // 弓弩兵
        LightCavalry,    // 轻骑兵
        HeavyCavalry,    // 重骑兵
        Spearman         // 枪兵
    }
}
