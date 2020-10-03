namespace _01.Inventory
{
    using _01.Inventory.Interfaces;
    using _01.Inventory.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class Inventory : IHolder
    {
        private List<IWeapon> _elements;

        public Inventory()
        {
            this._elements = new List<IWeapon>();
        }

        public int Capacity => this._elements.Count;

        public void Add(IWeapon weapon)
        {
            this._elements.Add(weapon);
        }

        public void Clear()
        {
            this._elements.Clear();
        }

        public bool Contains(IWeapon weapon)
        {
            int elementIndex = this.GetIndexById(weapon.Id);

            if (elementIndex == -1)
            {
                return false;
            }

            return true;
        }

        public void EmptyArsenal(Category category)
        {
            for (int i = 0; i < this.Capacity; i++)
            {
                IWeapon current = this._elements[i];

                if (current.Category == category)
                {
                    current.Ammunition = 0;
                }
            }
        }

        public bool Fire(IWeapon weapon, int ammunition)
        {
            int index = this.GetIndexById(weapon.Id);
            this.EnsureIndexIsCorrect(index);

            if (this._elements[index].Ammunition - ammunition >= 0)
            {
                this._elements[index].Ammunition -= ammunition;
                return true;
            }

            return false;
        }

        public IWeapon GetById(int id)
        {
            for (int i = 0; i < this.Capacity; i++)
            {
                IWeapon current = this._elements[i];

                if (current.Id == id)
                {
                    return current;
                }
            }

            return null;
        }

        public int Refill(IWeapon weapon, int ammunition)
        {
            int index = this.GetIndexById(weapon.Id);
            this.EnsureIndexIsCorrect(index);

            if (this._elements[index].Ammunition + ammunition >= this._elements[index].MaxCapacity)
            {
                this._elements[index].Ammunition = this._elements[index].MaxCapacity;
            }
            else
            {
                this._elements[index].Ammunition += ammunition;
            }

            return this._elements[index].Ammunition;
        }

        public IWeapon RemoveById(int id)
        {
            int index = this.GetIndexById(id);
            this.EnsureIndexIsCorrect(index);
            IWeapon weapon = this._elements[index];
            this._elements.RemoveAt(index);

            return weapon;
        }

        public int RemoveHeavy()
        {
            int count = this._elements.RemoveAll(e => e.Category == Category.Heavy);

            return count;
        }

        public List<IWeapon> RetrieveAll()
        {
            List<IWeapon> result = new List<IWeapon>(this._elements);

            return result;
        }

        public List<IWeapon> RetriveInRange(Category lower, Category upper)
        {
            List<IWeapon> result = new List<IWeapon>();

            for (int i = 0; i < this.Capacity; i++)
            {
                if (this._elements[i].Category >= lower && this._elements[i].Category <= upper)
                {
                    result.Add(this._elements[i]);
                }
            }

            return result;
        }

        public void Swap(IWeapon firstWeapon, IWeapon secondWeapon)
        {
            int firstWeaponIndex = this.GetIndexById(firstWeapon.Id);
            int secondWeaponIndex = this.GetIndexById(secondWeapon.Id);

            this.EnsureIndexIsCorrect(firstWeaponIndex);
            this.EnsureIndexIsCorrect(secondWeaponIndex);

            if (firstWeapon.Category == secondWeapon.Category)
            {
                this._elements[firstWeaponIndex] = secondWeapon;
                this._elements[secondWeaponIndex] = firstWeapon;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this._elements.GetEnumerator();
        }

        private void EnsureWeaponExist(IWeapon weapon)
        {
            if (!this.Contains(weapon))
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }
        }

        private int GetIndexById(int id)
        {
            for (int i = 0; i < this.Capacity; i++)
            {
                if (this._elements[i].Id == id)
                {
                    return i;
                }
            }

            return -1;
        }

        private void EnsureIndexIsCorrect(int id)
        {
            if (id < 0 || id >= this.Capacity)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }
        }
    }
}
