namespace _01.RoyaleArena
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class RoyaleArena : IArena
    {
        private Dictionary<int, BattleCard> cards = new Dictionary<int, BattleCard>();

        public void Add(BattleCard card)
        {
            if (this.Contains(card))
            {
                throw new InvalidOperationException();
            }

            cards[card.Id] = card;
        }

        public bool Contains(BattleCard card)
        {
            return this.cards.ContainsKey(card.Id);
        }

        public int Count => this.cards.Count;

        public void ChangeCardType(int id, CardType type)
        {
            if (this.cards.ContainsKey(id) && Enum.IsDefined(typeof(CardType), type))
            {
                this.cards[id].Type = type;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public BattleCard GetById(int id)
        {
            if (this.cards.ContainsKey(id))
            {
                return this.cards[id];
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void RemoveById(int id)
        {
            var card = this.GetById(id);
            this.cards.Remove(card.Id);
        }

        public IEnumerable<BattleCard> GetByCardType(CardType type)
        {
            var toReturn = this.cards.Values
                .Where(c => c.Type == type)
                .OrderBy(c => c.Damage)
                .ThenBy(c => c.Id);

            this.CkechifCollectionIsEmpty(toReturn);

            return toReturn;
        }

        public IEnumerable<BattleCard> GetByTypeAndDamageRangeOrderedByDamageThenById(CardType type, int lo, int hi)
        {
            var toReturn = this.cards.Values
                .Where(c => c.Type == type)
                .Where(c => c.Damage > lo)
                .Where(c => c.Damage < hi)
                .OrderByDescending(c => c.Damage)
                .ThenBy(c => c.Id);

            this.CkechifCollectionIsEmpty(toReturn);

            return toReturn;
        }

        public IEnumerable<BattleCard> GetByCardTypeAndMaximumDamage(CardType type, double damage)
        {
            var toReturn = this.cards.Values
                .Where(c => c.Type == type)
                .Where(c => c.Damage <= damage)
                .OrderByDescending(c => c.Damage)
                .ThenBy(c => c.Id);

            this.CkechifCollectionIsEmpty(toReturn);

            return toReturn;
        }

        public IEnumerable<BattleCard> GetByNameOrderedBySwagDescending(string name)
        {
            var toReturn = this.cards.Values
                .Where(c => c.Name == name)
                .OrderByDescending(c => c.Swag)
                .ThenBy(c => c.Id);

            if (toReturn.Count() == 0)
            {
                throw new InvalidOperationException();
            }

            return toReturn;
        }

        public IEnumerable<BattleCard> GetByNameAndSwagRange(string name, double lo, double hi)
        {
            var toReturn = this.cards.Values
                .Where(c => c.Name == name)
                .Where(c => c.Swag >= lo)
                .Where(c => c.Swag < hi)
                .OrderByDescending(c => c.Swag)
                .ThenBy(c => c.Id);

            this.CkechifCollectionIsEmpty(toReturn);

            return toReturn;
        }

        public IEnumerable<BattleCard> FindFirstLeastSwag(int n)
        {
            if (n > this.Count())
            {
                throw new InvalidOperationException();
            }

            double minSwag = this.cards.Min(d => d.Value.Swag);

            var toReturn = this.cards.Values
                .Where(c => c.Swag >= minSwag)
                .OrderBy(c => c.Swag)
                .ThenBy(c => c.Id);

            return toReturn.Take(n);
        }

        public IEnumerable<BattleCard> GetAllInSwagRange(double lo, double hi)
        {
            var toReturn = this.cards.Values
                .Where(c => c.Swag >= lo)
                .Where(c => c.Swag <= hi)
                .OrderBy(c => c.Swag);

            return toReturn;
        }


        public IEnumerator<BattleCard> GetEnumerator()
        {
            return this.cards.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void CkechifCollectionIsEmpty(IEnumerable<BattleCard> toReturn)
        {
            if (toReturn.Count() == 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}