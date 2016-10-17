using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

using Collection_of_Persons;

using Wintellect.PowerCollections;

public class PersonCollection : IPersonCollection
{
    public PersonCollection()
    {
        this.PersonsByEmail = new Dictionary<string, Person>();
        this.PersonsByEmailDomain = new Dictionary<string, SortedSet<Person>>();
        this.PersonsByNameTown = new Dictionary<string, SortedSet<Person>>();
        this.PersonsByAge = new SortedDictionary<int, SortedSet<Person>>();
    }

    public Dictionary<string, Person> PersonsByEmail { get; }

    public Dictionary<string, SortedSet<Person>> PersonsByEmailDomain { get; } 

    public Dictionary<string, SortedSet<Person>> PersonsByNameTown { get; } 

    public SortedDictionary<int, SortedSet<Person>> PersonsByAge { get; }

    public bool AddPerson(string email, string name, int age, string town)
    {
        var person = new Person(email, name, age, town);
        if (!this.PersonsByEmail.ValidateKey(name, person))
        {
            return false;
        }
        else
        {
            var domain = email.GetEmailDomain();
            this.PersonsByEmailDomain.EnsureKeyExists(domain);
            this.PersonsByEmailDomain.AppendValueToKey(domain, person);

            string nameTown = name + town;
            this.PersonsByNameTown.EnsureKeyExists(nameTown);
            this.PersonsByNameTown.AppendValueToKey(nameTown, person);

            this.PersonsByAge.EnsureKeyExists(age);
            this.PersonsByAge.AppendValueToKey(age, person);

            return true;
        }
    }

    public int Count => this.PersonsByEmail.Count;
   

    public Person FindPerson(string email)
    {
        var person = this.PersonsByEmail.FindKey(email);

        return person;
    }

    public bool DeletePerson(string email)
    {
        if (this.PersonsByEmail.CheckKeyExists(email))
        {
            var person = this.PersonsByEmail[email];
            this.PersonsByEmail.Remove(email);

            var domain = email.GetEmailDomain();
            this.PersonsByEmailDomain[domain].Remove(person);
            if (this.PersonsByEmailDomain[domain].Count == 0)
            {
                this.PersonsByEmailDomain.Remove(domain);
            }

            string nameTown = person.Name + person.Town;
            this.PersonsByNameTown[nameTown].Remove(person);
            if (this.PersonsByNameTown[nameTown].Count == 0)
            {
                this.PersonsByNameTown.Remove(nameTown);
            }

            int age = person.Age;
            this.PersonsByAge[age].Remove(person);
            if (this.PersonsByAge[age].Count == 0)
            {
                this.PersonsByAge.Remove(age);
            }

            return true;
        }

        return false;
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        var personsByEmailDomain = this.PersonsByEmailDomain[emailDomain];

        return personsByEmailDomain;;
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        string nameTown = name + town;
        var personsByNameTown = this.PersonsByNameTown[nameTown];

        return personsByNameTown;;
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        var personsBetweenAges =
            this.PersonsByAge.Where(x => x.Key >= startAge && x.Key <= endAge).SelectMany(x => x.Value);

        return personsBetweenAges;
    }

    public IEnumerable<Person> FindPersons(
        int startAge, int endAge, string town)
    {
        // TODO: implement this
        throw new NotImplementedException();
    }
}
