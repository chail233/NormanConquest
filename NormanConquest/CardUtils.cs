using System;
using System.Collections.Generic;
using System.Text;

namespace NormanConquest
{
    // 这是一个静态类，提供各种卡牌相关的工具方法。
    public static class CardUtils
    {
        private static Random rng = new Random();

        /// Fisher-Yates 洗牌，直接修改原列表
        public static void Shuffle(List<Card> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);// 生成一个随机索引，范围是 [0, i]
                (list[i], list[j]) = (list[j], list[i]);// 交换元素
            }
        }
    }
}
