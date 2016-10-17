namespace Scoreboard
{
    public class Game
    {
        public Game(string name, string password)
        {
            this.Name = name;
            this.Password = password;
        }

        public string Name { get; private set; }
        public string Password { get; set; }
    }
}
