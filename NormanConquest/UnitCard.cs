using System;
using System.Collections.Generic;
using System.Text;

namespace NormanConquest
{
    //部队牌
    public class UnitCard : Card
    {
        public UnitType UnitType { get; set; }

        public UnitCard(string name, UnitType unitType, string description)
            : base(name, CardType.Unit, description)   //调用父类构造函数，设置卡牌名称、类型和描述
        {
            UnitType = unitType;
        }

        public override Card Clone()
        {
            return (UnitCard)MemberwiseClone();
        }
    }
}
