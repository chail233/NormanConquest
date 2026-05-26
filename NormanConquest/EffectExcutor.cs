using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace NormanConquest
{
    public class EffectExecutor
    {
        private GameManager game;
        private Random rng = new Random();

        public EffectExecutor(GameManager game)
        {
            this.game = game;
        }
        // 征召：抽2张部队牌
        public void Levy(Player player)
        {
            int found = 0;
            List<Card> nonUnitCards = new List<Card>();

            while (found < 2 && player.Deck.Count > 0)
            {
                Card card = player.Deck[0];
                player.Deck.RemoveAt(0);
                if (card is UnitCard)
                {
                    player.Hand.Add(card);
                    game.log($"{player.Name} 抽到了部队牌: {card.Name}");
                    found++;
                }
                else
                {
                    nonUnitCards.Add(card);
                }
            }

            for (int i = nonUnitCards.Count - 1; i >= 0; i--)
            {
                player.Deck.Insert(0, nonUnitCards[i]);
            }
            CardUtils.Shuffle(player.Deck);
        }

        // 土地开发：抽2张牌
        public void LandDevelopment(Player player)
        {
            game.DrawCard(player);
            game.DrawCard(player);
        }

        // 征税：加1HP
        public void Taxation(Player player)
        {
            player.HP += 1;
        }

        // 分封：减1HP，抽3张牌
        public void Enfeoffment(Player player)
        {
            game.TakeDamage(player, 1);
            game.DrawCard(player);
            game.DrawCard(player);
            game.DrawCard(player);
        }

        // 倾巢出动：本回合所有的进攻视为特殊进攻，且不会被追击
        public void AllOutAttack(Player attacker)
        {
            game.AddEffect(attacker, "AllOutAttack");
        }

        // 假装撤退：弃一张部队牌，下回合开始多抽2张
        public void FakeRetreat(Player player)
        {
            List<int> cards = new List<int>();
            for(int i = 0;i<player.Hand.Count; i++)
            {
                if (player.Hand[i] is UnitCard)
                    cards.Add(i);
            }
            if (cards.Count == 0)
            {
                game.log($"{player.Name} 没有部队牌可弃，假装撤退无效");
                return;
            }
            else
            {
                game.Discard(player, cards[rng.Next(cards.Count)]);
                player.PendingEffects.Add("FakeRetreat");
            }
        }

        // 绝罚：随机弃对方一张手牌
        public void Anathema(Player caster)
        {
            Player target = caster == game.player ? game.opponent : game.player;
            if (target.Hand.Count > 0)
            {
                int index = rng.Next(target.Hand.Count);
                game.Discard(target, index);
            }
        }
    }
}
