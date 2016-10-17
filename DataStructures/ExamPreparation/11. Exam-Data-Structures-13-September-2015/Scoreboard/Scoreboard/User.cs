namespace Scoreboard
{
    using System;

    public class User : IComparable<User>
    {
        public User(string name, string password)
        {
            this.Name = name;
            this.Password = password;
        }

        public string Name { get; private set; }
        public string Password { get; set; }

        public int CompareTo(User other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }
}
