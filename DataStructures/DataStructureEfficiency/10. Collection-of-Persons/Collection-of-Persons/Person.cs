﻿using System;

public class Person : IComparable
{
    public Person(string email, string name, int age, string town)
    {
        this.Email = email;
        this.Name = name;
        this.Age = age;
        this.Town = town;
    }

    public string Email { get; set; }

    public string Name { get; set; }

    public int Age { get; set; }

    public string Town { get; set; }

    public int CompareTo(object obj)
    {
        var otherPerson = obj as Person;
        if (otherPerson == null)
        {
            return 1;
        }

        return this.Email.CompareTo(otherPerson.Email);
    }
}
