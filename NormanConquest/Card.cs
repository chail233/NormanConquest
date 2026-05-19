using System;
using System.Collections.Generic;
using System.Text;

namespace NormanConquest
{
    //卡牌抽象基类
    public abstract class Card
    {
        public string Name { get; set; }            // 卡牌名称
        public CardType CardType { get; set; }      // 卡牌大类
        public string Description { get; set; }     // 效果描述

        protected Card(string name, CardType cardType, string description)
        {
            Name = name;
            CardType = cardType;
            Description = description;
        }

        // 克隆一张卡牌，用于从原型生成牌堆
        public abstract Card Clone();
    }
}
