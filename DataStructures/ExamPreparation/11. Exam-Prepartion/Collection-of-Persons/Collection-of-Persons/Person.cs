using System;
using System.Security.Policy;

public class Person : IComparable<Person>
{
    public Person(string email, string name, int age, string town, string emailDomain)
    {
        this.Email = email;
        this.Name = name;
        this.Age = age;
        this.Town = town;
        this.EmailDomain = emailDomain;
    }

    public string Email { get; set; }
    public string EmailDomain { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Town { get; set; }

    public int CompareTo(Person other)
    {
        return this.Email.CompareTo(other.Email);
    }
}
