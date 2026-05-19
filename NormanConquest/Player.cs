using System;
using System.Collections.Generic;
using System.Text;

namespace NormanConquest
{
    // 这是玩家类，包含玩家的基本属性和操作方法
    public class Player
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public List<Card> Deck { get; private set; }
        public List<Card> Hand { get; private set; }
        public List<Card> DiscardPile { get; private set; }
        public List<BuildingCard> BuildingZone { get; private set; }
        public int RemainingNormalAttacks { get; set; }
        public List<string> PendingEffects { get; private set; }

        public Player(string name, int initialHP)
        {
            Name = name;
            HP = initialHP;
            Deck = DeckTemplate.GenerateDeck();
            Hand = new List<Card>();
            DiscardPile = new List<Card>();
            BuildingZone = new List<BuildingCard>();
            RemainingNormalAttacks = 1;
            PendingEffects = new List<string>();
        }

        // 从牌堆顶部抽一张牌加入手牌。牌堆为空时返回 null（由 GameManager 处理扣血和洗牌）
        public Card DrawCard()
        {
            if (Deck.Count == 0) return null;
            Card card = Deck[0];
            Deck.RemoveAt(0);
            Hand.Add(card);
            return card;
        }

        // 将一张手牌弃入弃牌堆
        public void DiscardFromHand(Card card)
        {
            if (Hand.Remove(card))
            {
                DiscardPile.Add(card);
            }
        }

        // 将一张手牌弃入弃牌堆，按索引
        public void DiscardFromHand(int index)
        {
            if (index < 0 || index >= Hand.Count) return;
            Card card = Hand[index];
            Hand.RemoveAt(index);
            DiscardPile.Add(card);
        }

        // 将弃牌堆所有牌洗入牌堆，并清空弃牌堆
        public void ReshuffleDiscardIntoDeck()
        {
            Deck.AddRange(DiscardPile);
            DiscardPile.Clear();
            CardUtils.Shuffle(Deck);
        }

        // 手牌上限 = HP + 2
        public int HandLimit => HP + 2;//=>表示这是一个只读属性，计算方式是 HP + 2，每当访问 HandLimit 时都会计算这个值，而不是存储一个固定的值

        // 建筑区上限 = max(0, HP - 2)
        public int BuildingLimit => Math.Max(0, HP - 2);
    }
}
