namespace _02.Data
{
    using _02.Data.Interfaces;
    using _02.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class Data : IRepository
    {
        private OrderedBag<IEntity> _elements;

        public Data()
        {
            this._elements = new OrderedBag<IEntity>();
        }

        public int Size => this._elements.Count;

        public void Add(IEntity entity)
        {
            this._elements.Add(entity);

            var parent = this.GetById((int)entity.ParentId);

            if (parent != null)
            {
                parent.Children.Add(entity);
            }
        }

        public IRepository Copy()
        {
            return (IRepository)this.MemberwiseClone();
        }

        public IEntity DequeueMostRecent()
        {
            this.EnsureNotEmpty();

            return this._elements.RemoveFirst();
        }

        public List<IEntity> GetAll()
        {
            return new List<IEntity>(this._elements);
        }

        public List<IEntity> GetAllByType(string type)
        {
            if (type != typeof(User).Name
                && type != typeof(Invoice).Name 
                && type != typeof(StoreClient).Name)
            {
                throw new InvalidOperationException("Invalid type: " + type);
            }

            List<IEntity> result = new List<IEntity>(this.Size);

            for (int i = 0; i < this.Size; i++)
            {
                var current = this._elements[i];

                if (current.GetType().Name == type)
                {
                    result.Add(current);
                }
            }

            return result;
        }

        public IEntity GetById(int id)
        {
            if (id <= 0 || id >= this.Size)
            {
                return null;
            }

            return this._elements[this.Size - 1 - id];
        }

        public List<IEntity> GetByParentId(int parentId)
        {
            var parent = this.GetById(parentId);

            if (parent == null)
            {
                return new List<IEntity>();
            }

            return parent.Children;
        }

        public IEntity PeekMostRecent()
        {
            this.EnsureNotEmpty();

            return this._elements.First();
        }

        private void EnsureNotEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Operation on empty Data");
            }
        }
    }
}
