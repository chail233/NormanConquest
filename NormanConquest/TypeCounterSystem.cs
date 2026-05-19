using System;
using System.Collections.Generic;
using System.Text;

namespace NormanConquest
{
    //这是一个静态类，用于处理兵种克制关系。
    public static class TypeCounterSystem
    {
        private static Dictionary<UnitType, HashSet<UnitType>> counterMap;

        static TypeCounterSystem()
        {
            counterMap = new Dictionary<UnitType, HashSet<UnitType>>// 定义克制关系的字典，键是克制者，值是被克制的兵种集合
        {
            { UnitType.HeavyInfantry, new HashSet<UnitType> { UnitType.LightInfantry, UnitType.Spearman } },// 重步兵克制轻步兵和长枪兵
            { UnitType.Archer,        new HashSet<UnitType> { UnitType.LightInfantry, UnitType.Spearman } },// 弓箭手克制轻步兵和长枪兵
            { UnitType.LightCavalry,  new HashSet<UnitType> { UnitType.Archer } },// 轻骑兵克制弓箭手
            { UnitType.HeavyCavalry,  new HashSet<UnitType> { UnitType.LightInfantry, UnitType.HeavyInfantry, UnitType.Archer } },// 重骑兵克制轻步兵、重步兵和弓箭手
            { UnitType.Spearman,      new HashSet<UnitType> { UnitType.LightCavalry, UnitType.HeavyCavalry } }// 长枪兵克制轻骑兵和重骑兵
        };
            // 轻步兵不克制任何兵种，所以不在字典里
        }

        // 判断 attacker 是否克制 defender
        public static bool IsCounter(UnitType attacker, UnitType defender)
        {
            if (counterMap.TryGetValue(attacker, out var counteredTypes))// 如果 attacker 在字典中，获取它克制的兵种集合
            {
                return counteredTypes.Contains(defender);// 判断 defender 是否在克制集合中
            }
            return false;
        }
    }
}
