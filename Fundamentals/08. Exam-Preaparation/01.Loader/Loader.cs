namespace _01.Loader
{
    using _01.Loader.Interfaces;
    using _01.Loader.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Loader : IBuffer
    {
        private List<IEntity> _elements;

        public Loader()
        {
            this._elements = new List<IEntity>();
        }

        public int EntitiesCount => this._elements.Count;

        public void Add(IEntity entity)
        {
            this._elements.Add(entity);
        }

        public void Clear()
        {
            this._elements.Clear();
        }

        public bool Contains(IEntity entity)
        {
            int elementIndex = this.GetById(entity.Id);

            if (elementIndex == -1)
            {
                return false;
            }

            return true;
        }

        public IEntity Extract(int id)
        {
            int elementIndex = this.GetById(id);

            if (elementIndex == -1)
            {
                return null;
            }

            IEntity entity = this._elements[elementIndex];
            this._elements.RemoveAt(elementIndex);

            return entity;
        }        

        public IEntity Find(IEntity entity)
        {
            int elementIndex = this.GetById(entity.Id);

            if (elementIndex == -1)
            {
                return null;
            }

            return this._elements[elementIndex];
        }

        public List<IEntity> GetAll()
        {
            return new List<IEntity>(this._elements);
        }

        
        public void RemoveSold()
        {
            this._elements.RemoveAll(e => e.Status == BaseEntityStatus.Sold);
        }

        public void Replace(IEntity oldEntity, IEntity newEntity)
        {
            int oldEntityIndex = this.GetById(oldEntity.Id);

            if (oldEntityIndex == -1)
            {
                throw new InvalidOperationException("Entity not found");
            }

            this._elements[oldEntityIndex] = newEntity;
        }

        public List<IEntity> RetainAllFromTo(BaseEntityStatus lowerBound, BaseEntityStatus upperBound)
        {
            var result = new List<IEntity>();

            for (int i = 0; i < this.EntitiesCount; i++)
            {
                if (this._elements[i].Status >= lowerBound
                    && this._elements[i].Status <= upperBound)
                {
                    result.Add(this._elements[i]);
                }
            }

            return result;
        }

        public void Swap(IEntity first, IEntity second)
        {
            int firstIndex = this.GetById(first.Id);
            if (firstIndex == -1)
            {
                throw new InvalidOperationException("Entity not found");
            }

            int secondIndex = this.GetById(second.Id);
            if (secondIndex == -1)
            {
                throw new InvalidOperationException("Entity not found");
            }

            this._elements[firstIndex] = second;
            this._elements[secondIndex] = first;
        }

        public IEntity[] ToArray()
        {
            IEntity[] result = new IEntity[this.EntitiesCount];

            for (int i = 0; i < this.EntitiesCount; i++)
            {
                result[i] = this._elements[i];
            }

            return result;
        }

        public void UpdateAll(BaseEntityStatus oldStatus, BaseEntityStatus newStatus)
        {

            for (int i = 0; i < this.EntitiesCount; i++)
            {
                if (this._elements[i].Status == oldStatus)
                {
                    this._elements[i].Status = newStatus;
                }
            }
        }

        public IEnumerator<IEntity> GetEnumerator()
        {
            return this._elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private int GetById(int id)
        {
            for (int i = 0; i < this.EntitiesCount; i++)
            {
                if (this._elements[i].Id == id)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
