namespace _02.LegionSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using _02.LegionSystem.Interfaces;
    using Wintellect.PowerCollections;

    public class Legion : IArmy
    {
        private OrderedSet<IEnemy> _elements;

        public Legion()
        {
            this._elements = new OrderedSet<IEnemy>();
        }
        public int Size => this._elements.Count;

        public bool Contains(IEnemy enemy)
        {
            return this._elements.Contains(enemy);
        }

        public void Create(IEnemy enemy)
        {
            this._elements.Add(enemy);
        }

        public IEnemy GetByAttackSpeed(int speed)
        {
            if (this._elements.GetFirst().AttackSpeed > speed || this._elements.GetLast().AttackSpeed < speed)
            {
                return null;
            }

            return this._elements.First(e => e.AttackSpeed == speed);
        }

        public List<IEnemy> GetFaster(int speed)
        {
            List<IEnemy> result = this._elements.FindAll(e => e.AttackSpeed > speed).ToList();

            return result;
        }

        public IEnemy GetFastest()
        {
            this.EnsureNotEmpty();

            return this._elements.GetLast();
        }

        public IEnemy[] GetOrderedByHealth()
        {
            //TODO: create new ordered set with Healt
            return this._elements.OrderByDescending(e => e.Health).ToArray();
        }

        public List<IEnemy> GetSlower(int speed)
        {
            List<IEnemy> result = this._elements.FindAll(e => e.AttackSpeed < speed).ToList();

            return result;
        }

        public IEnemy GetSlowest()
        {
            this.EnsureNotEmpty();

            return this._elements.GetFirst();
        }

        public void ShootFastest()
        {
            this.EnsureNotEmpty();
            this._elements.RemoveLast();
        }

        public void ShootSlowest()
        {
            this.EnsureNotEmpty();
            this._elements.RemoveFirst();
        }

        private void EnsureNotEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }
        }
    }
}
