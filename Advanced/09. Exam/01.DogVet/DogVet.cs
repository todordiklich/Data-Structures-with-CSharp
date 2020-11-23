namespace _01.DogVet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DogVet : IDogVet
    {
        private Dictionary<string, SortedSet<Dog>> byOwner = new Dictionary<string, SortedSet<Dog>>();
        private HashSet<Dog> dogs = new HashSet<Dog>();
        private SortedDictionary<int, List<Dog>> byAge = new SortedDictionary<int, List<Dog>>();

        public int Size => dogs.Count;
        public void AddDog(Dog dog, Owner owner)
        {
            if (dogs.Contains(dog))
            {
                throw new ArgumentException();
            }
            if (!byOwner.ContainsKey(owner.Id))
            {
                byOwner[owner.Id] = new SortedSet<Dog>();
            }
           
            if (byOwner[owner.Id].Contains(dog))
            {
                throw new ArgumentException();
            }

            dog.Owner = owner;


            dogs.Add(dog);
            byOwner[owner.Id].Add(dog);
            // new sets
            if (!byAge.ContainsKey(dog.Age))
            {
                byAge[dog.Age] = new List<Dog>();
            }
            byAge[dog.Age].Add(dog);
        }

        public bool Contains(Dog dog)
        {
            return dogs.Contains(dog);
        }

        public Dog GetDog(string name, string ownerId)
        {
            if (!byOwner.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }

            Dog dog = byOwner[ownerId].FirstOrDefault(d => d.Name == name);
            if (dog == null)
            {
                throw new ArgumentException();
            }

            return dog;
        }

        public Dog RemoveDog(string name, string ownerId)
        {
            Dog dog = this.GetDog(name, ownerId);

            byOwner[ownerId].Remove(dog);
            if (byOwner[ownerId].Count == 0)
            {
                byOwner.Remove(ownerId);
            }
            dogs.Remove(dog);

            byAge[dog.Age].Remove(dog);
            if (byAge[dog.Age].Count == 0)
            {
                byAge.Remove(dog.Age);
            }

            return dog;
        }

        public IEnumerable<Dog> GetDogsByOwner(string ownerId)
        {
            if (!byOwner.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }

            return byOwner[ownerId];
        }

        public IEnumerable<Dog> GetDogsByBreed(Breed breed)
        {
            var dogsToReturn = dogs.Where(d => d.Breed == breed);
            if (dogsToReturn.Count() == 0)
            {
                throw new ArgumentException();
            }

            return dogsToReturn;
        }

        public void Vaccinate(string name, string ownerId)
        {
            Dog dog = this.GetDog(name, ownerId);
            dog.Vaccines++;
        }

        public void Rename(string oldName, string newName, string ownerId)
        {
            Dog dog = this.GetDog(oldName, ownerId);
            dog.Name = newName;
        }

        public IEnumerable<Dog> GetAllDogsByAge(int age)
        {
            if (!byAge.ContainsKey(age))
            {
                throw new ArgumentException();
            }

            var dogsToReturn = byAge[age];

            return dogsToReturn;
        }

        public IEnumerable<Dog> GetDogsInAgeRange(int lo, int hi)
        {
            var dogsToReturn = byAge.Where(d => d.Key >= lo && d.Key <= hi).SelectMany(d => d.Value);
            return dogsToReturn;
        }

        public IEnumerable<Dog> GetAllOrderedByAgeThenByNameThenByOwnerNameAscending()
        {
            var dogsToReturn = dogs.OrderBy(d => d.Age).ThenBy(d => d.Name).ThenBy(d => d.Owner.Name);

            return dogsToReturn;
        }
    }
}