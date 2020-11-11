namespace Collection_of_Persons
{
    using System;
    using System.Collections.Generic;
    using Wintellect.PowerCollections;

    public class PersonCollection : IPersonCollection
    {
        private Dictionary<string, Person> personByEmail = new Dictionary<string, Person>();

        private Dictionary<string, SortedSet<Person>> personByDomain = 
            new Dictionary<string, SortedSet<Person>>();

        private Dictionary<string, SortedSet<Person>> personByNameAndTown =
            new Dictionary<string, SortedSet<Person>>();

        private OrderedDictionary<int, SortedSet<Person>> personByAge =
            new OrderedDictionary<int, SortedSet<Person>>();

        private Dictionary<string, OrderedDictionary<int, SortedSet<Person>>> personByTownAge =
            new Dictionary<string, OrderedDictionary<int, SortedSet<Person>>>();

        public bool AddPerson(string email, string name, int age, string town)
        {
            if (this.FindPerson(email) != null)
            {
                return false;
            }

            Person person = new Person() { Email = email, Name = name, Age = age, Town = town };
            this.personByEmail.Add(email, person);

            var emailDomain = this.ExtractDomain(email);
            this.personByDomain.AppendValueToKey(emailDomain, person);

            var nameAndTown = this.CombineNameAndTown(name, town);
            this.personByNameAndTown.AppendValueToKey(nameAndTown, person);

            this.personByAge.AppendValueToKey(age, person);

            this.personByTownAge.EnsureKeyExists(town);
            this.personByTownAge[town].AppendValueToKey(age, person);

            return true;
        }

        public int Count { get => this.personByEmail.Count; }
        public Person FindPerson(string email)
        {
            Person person = null;
            bool ersonExist = this.personByEmail.TryGetValue(email, out person);

            return person;
        }

        public bool DeletePerson(string email)
        {
            var person = this.FindPerson(email);
            if (person == null)
            {
                return false;
            }

            var personDeleted = this.personByEmail.Remove(email);

            var emailDomain = this.ExtractDomain(email);
            this.personByDomain[emailDomain].Remove(person);

            var nameAndTown = this.CombineNameAndTown(person.Name, person.Town);
            this.personByNameAndTown[nameAndTown].Remove(person);

            this.personByAge[person.Age].Remove(person);

            this.personByTownAge[person.Town][person.Age].Remove(person);

            return true;
        }

        public IEnumerable<Person> FindPersons(string emailDomain)
        {
            return this.personByDomain.GetValuesForKey(emailDomain);
        }

        public IEnumerable<Person> FindPersons(string name, string town)
        {
            var nameAndTown = this.CombineNameAndTown(name, town);
            return this.personByNameAndTown.GetValuesForKey(nameAndTown);
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge)
        {
            var personInRange = this.personByAge.Range(startAge, true, endAge, true);

            foreach (var personByAge in personInRange)
            {
                foreach (var person in personByAge.Value)
                {
                    yield return person;
                }
            }
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
        {
            if (!this.personByTownAge.ContainsKey(town))
            {
                yield break;
            }

            var personInRange = this.personByTownAge[town].Range(startAge, true, endAge, true);

            foreach (var personByTown in personInRange)
            {
                foreach (var person in personByTown.Value)
                {
                    yield return person;
                }
            }
        }

        private string ExtractDomain(string email)
        {
            string domain = email.Split('@')[1];
            return domain;
        }

        private string CombineNameAndTown(string name, string town)
        {
            const string separator = "|!|";
            return name + separator + town;
        }
    }
}
