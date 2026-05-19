using System;
using System.Collections.Generic;
using System.Text;

namespace NormanConquest
{
    // 这是一个静态类，定义了游戏中所有牌的原型，并提供生成洗好的牌堆的方法。
    public static class DeckTemplate
    {
        private static List<Card> prototypes;

        static DeckTemplate()
        {
            prototypes = new List<Card>();

            // 部队牌 24张
            for (int i = 0; i < 8; i++)
                prototypes.Add(new UnitCard("轻步兵", UnitType.LightInfantry, ""));
            for (int i = 0; i < 3; i++)
                prototypes.Add(new UnitCard("重步兵", UnitType.HeavyInfantry, "克制轻步兵、枪兵"));
            for (int i = 0; i < 5; i++)
                prototypes.Add(new UnitCard("弓弩兵", UnitType.Archer, "克制轻步兵、枪兵"));
            for (int i = 0; i < 4; i++)
                prototypes.Add(new UnitCard("轻骑兵", UnitType.LightCavalry, "克制弓弩兵"));
            for (int i = 0; i < 2; i++)
                prototypes.Add(new UnitCard("重骑兵", UnitType.HeavyCavalry, "克制轻步兵、重步兵、弓弩兵"));
            for (int i = 0; i < 3; i++)
                prototypes.Add(new UnitCard("枪兵", UnitType.Spearman, "克制轻骑兵、重骑兵"));

            // 指令牌 9张
            prototypes.Add(new OrderCard("征召", "Levy", "抽2张部队牌"));
            prototypes.Add(new OrderCard("土地开发", "LandDevelopment", "抽2张牌"));
            prototypes.Add(new OrderCard("土地开发", "LandDevelopment", "抽2张牌"));
            prototypes.Add(new OrderCard("征税", "Taxation", "加2HP"));
            prototypes.Add(new OrderCard("征税", "Taxation", "加2HP"));
            prototypes.Add(new OrderCard("分封", "Enfeoffment", "减1HP，抽3张牌"));
            prototypes.Add(new OrderCard("倾巢出动", "AllOutAttack", "打出手牌中所有部队牌，每张视作一次特殊进攻，且不会被追击"));
            prototypes.Add(new OrderCard("假装撤退", "FakeRetreat", "弃掉一张部队牌，下回合开始时抽2张牌"));
            prototypes.Add(new OrderCard("绝罚", "Anathema", "选择对方一张手牌将其弃掉"));

            // 建筑牌 7张
            prototypes.Add(new BuildingCard("庄园", "Manor", "每回合开始额外抽1张牌"));
            prototypes.Add(new BuildingCard("城墙", "CityWall", "被进攻时可弃掉此牌，抵御此次进攻"));
            prototypes.Add(new BuildingCard("城堡", "Castle", "每回合可多进行一次通常进攻"));
            prototypes.Add(new BuildingCard("马厩", "Stable", "己方骑兵进攻成功时，将该骑兵返回手牌"));
            prototypes.Add(new BuildingCard("兵营", "Barracks", "部队牌不计入手牌上限"));
            prototypes.Add(new BuildingCard("教堂", "Church", "待设计"));
            prototypes.Add(new BuildingCard("市场", "Market", "待设计"));
        }

        // 生成一副洗好的牌堆
        public static List<Card> GenerateDeck()
        {
            List<Card> deck = new List<Card>();
            foreach (var proto in prototypes)
            {
                deck.Add(proto.Clone());
            }
            CardUtils.Shuffle(deck);
            return deck;
        }
    }
}
