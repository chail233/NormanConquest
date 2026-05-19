using System;
using System.Collections.Generic;
using System.Text;

namespace NormanConquest
{
    //建筑牌
    public class BuildingCard : Card
    {
        public string EffectName { get; set; }

        public BuildingCard(string name, string effectName, string description)
            : base(name, CardType.Building, description)
        {
            EffectName = effectName;
        }

        public override Card Clone()
        {
            return (BuildingCard)MemberwiseClone();
        }
    }
}
