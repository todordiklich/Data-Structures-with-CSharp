namespace Collection_of_Persons
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PersonCollectionSlow : IPersonCollection
    {
        List<Person> people = new List<Person>();

        public bool AddPerson(string email, string name, int age, string town)
        {
            int index = GetPersonIndex(email);
            if (index == -1)
            {
                Person person = new Person()
                {
                    Email = email,
                    Name = name,
                    Age = age,
                    Town = town
                };

                people.Add(person);
                return true;
            }

            return false;
        }

        public int Count => people.Count;
        public Person FindPerson(string email)
        {
            int index = GetPersonIndex(email);

            if (index == -1)
            {
                return null;
            }

            return people[index];
        }

        public bool DeletePerson(string email)
        {
            int index = GetPersonIndex(email);
            if (index == -1)
            {
                return false;
            }

            people.RemoveAt(index);
            return true;
        }

        public IEnumerable<Person> FindPersons(string emailDomain)
        {
            return people
                .Where(p => p.Email.Split('@')[1] == emailDomain)
                .OrderBy(p => p.Email);
        }

        public IEnumerable<Person> FindPersons(string name, string town)
        {
            return people
                .Where(p => p.Name == name && p.Town == town)
                .OrderBy(p => p.Email);
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge)
        {
            return people
                .Where(p => p.Age >= startAge && p.Age <= endAge)
                .OrderBy(p => p.Age)
                .ThenBy(p => p.Email);
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
        {
            return people
                .Where(p => p.Age >= startAge && p.Age <= endAge && p.Town == town)
                .OrderBy(p => p.Age)
                .ThenBy(p => p.Email);
        }

        private int GetPersonIndex(string email)
        {
            int index = -1;
            for (int i = 0; i < people.Count; i++)
            {
                if (people[i].Email == email)
                {
                    index = i;
                    return index;
                }
            }

            return index;
        }
    }
}
