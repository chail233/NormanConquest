using System;
using System.Collections.Generic;
using System.Text;

namespace NormanConquest
{
    //指令牌
    public class OrderCard : Card
    {
        public string EffectName { get; set; }

        public OrderCard(string name, string effectName, string description)
            : base(name, CardType.Order, description)
        {
            EffectName = effectName;
        }

        public override Card Clone()
        {
            return (OrderCard)MemberwiseClone();
        }
    }
}
