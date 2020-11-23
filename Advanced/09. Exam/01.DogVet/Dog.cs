namespace _01.DogVet
{
    using System;

    public class Dog : IComparable<Dog>
    {
        public Dog(string id, string name, Breed breed, int age, int vaccines)
        {
            this.Id = id;
            this.Name = name;
            this.Breed = breed;
            this.Age = age;
            this.Vaccines = vaccines;
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public Breed Breed { get; set; }

        public int Age { get; set; }

        public int Vaccines { get; set; }

        public Owner Owner { get; set; }

        public int CompareTo(Dog other)
        {
            if (this.Name == other.Name)
            {
                return 0;
            }

            return 1;
        }

        public override bool Equals(object obj)
        {
            Dog other = obj as Dog;
            if (other == null)
            {
                return false;
            }

            return this.Name == other.Name;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}