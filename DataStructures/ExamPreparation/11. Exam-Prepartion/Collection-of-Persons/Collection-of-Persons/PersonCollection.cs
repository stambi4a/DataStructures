using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Collection_of_Persons;

using Wintellect.PowerCollections;

public class PersonCollection : IPersonCollection
{
    private Dictionary<string, Person> personsByEmail;
    private Dictionary<Tuple<string, string>, SortedSet<Person>> personsByNameAndTown;
    private SortedDictionary<int, SortedSet<Person>> personsByAge;
    private SortedDictionary<Tuple<int, string>, SortedSet<Person>> personsByAgeAndTown;
    private Dictionary<string, SortedSet<Person>> personsByEmailDomain;

    public PersonCollection()
    {
        this.personsByEmail = new Dictionary<string, Person>();
        this.personsByNameAndTown = new Dictionary<Tuple<string, string>, SortedSet<Person>>();
        this.personsByAge = new SortedDictionary<int, SortedSet<Person>>();
        this.personsByAgeAndTown = new SortedDictionary<Tuple<int, string>, SortedSet<Person>>(new TupleComparer());
        this.personsByEmailDomain = new Dictionary<string, SortedSet<Person>>();
    }

    public bool AddPerson(string email, string name, int age, string town)
    {
        if (this.personsByEmail.ContainsKey(email))
        {
            return false;
        }

        string emailDomain = Regex.Match(email, "(?<=@).+").ToString();
        Person person = new Person(email, name, age, town, emailDomain);
        this.personsByEmail.Add(email, person);
        if (!this.personsByAge.ContainsKey(age))
        {
            this.personsByAge.Add(age, new SortedSet<Person>());
        }

        this.personsByAge[age].Add(person);

        var tupleAgeTown = new Tuple<int, string>(age, town);
        if (!this.personsByAgeAndTown.ContainsKey(tupleAgeTown))
        {
            this.personsByAgeAndTown.Add(tupleAgeTown, new SortedSet<Person>());
        }

        this.personsByAgeAndTown[tupleAgeTown].Add(person);

        var tupleNameAndTown = new Tuple<string, string>(name, town);
        if (!this.personsByNameAndTown.ContainsKey(tupleNameAndTown))
        {
            this.personsByNameAndTown.Add(tupleNameAndTown, new SortedSet<Person>());
        }

        this.personsByNameAndTown[tupleNameAndTown].Add(person);

        if (!this.personsByEmailDomain.ContainsKey(emailDomain))
        {
            this.personsByEmailDomain.Add(emailDomain, new SortedSet<Person>());
        }

        this.personsByEmailDomain[emailDomain].Add(person);

        return true;
    }

    public int Count => this.personsByEmail.Count;

    public Person FindPerson(string email)
    {
        if (!this.personsByEmail.ContainsKey(email))
        {
            return null;
        }

        return this.personsByEmail[email];
    }

    public bool DeletePerson(string email)
    {
        if (!this.personsByEmail.ContainsKey(email))
        {
            return false;
        }

        Person person = this.personsByEmail[email];
        this.personsByEmail.Remove(email);
        this.personsByAge.Remove(person.Age);
        var tupleAgeTown = new Tuple<int, string>(person.Age, person.Town);
        this.personsByAgeAndTown[tupleAgeTown].Remove(person);
        var tupleNameTown = new Tuple<string, string>(person.Name, person.Town);
        this.personsByNameAndTown[tupleNameTown].Remove(person);
        this.personsByEmailDomain[person.EmailDomain].Remove(person);

        return true;

    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        if (!this.personsByEmailDomain.ContainsKey(emailDomain))
        {
            return new List<Person>();
        }

        return this.personsByEmailDomain[emailDomain];
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        var tupleNameAndTown = new Tuple<string, string>(name, town);
        if (!this.personsByNameAndTown.ContainsKey(tupleNameAndTown))
        {
            return new List<Person>();
        }

        return this.personsByNameAndTown[tupleNameAndTown];
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        return this.personsByAge.Where(x => x.Key >= startAge && x.Key <= endAge).SelectMany(x => x.Value);
    }

    public IEnumerable<Person> FindPersons(
        int startAge, int endAge, string town)
    {
        return this.personsByAgeAndTown.Where(
                x => x.Key.Item2.Equals(town) && x.Key.Item1 >= startAge && x.Key.Item1 <= endAge)
                .SelectMany(x => x.Value);
    }
}
