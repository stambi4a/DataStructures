namespace BunnyWars.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BunnyWarsStructure : IBunnyWarsStructure
    {
        private const int BunnyDetonateDamage = 30;
        public BunnyWarsStructure()
        {
            this.Bunnies = new SortedDictionary<string, Bunny>(new StringComparer());
            this.BunniesByTeamId = new Dictionary<int, SortedSet<Bunny>>();
            this.BunniesByRoom = new SortedList<int, Dictionary<int, HashSet<Bunny>>>();
        }

        public int BunnyCount => this.Bunnies.Count;
        public int RoomCount => this.BunniesByRoom.Count;
        public Dictionary<int, SortedSet<Bunny>> BunniesByTeamId { get; set; }
        public IDictionary<string, Bunny> Bunnies { get; set; }
        public SortedList<int, Dictionary<int, HashSet<Bunny>>> BunniesByRoom { get; set; }

        public void AddRoom(int roomId)
        {
            if (this.BunniesByRoom.ContainsKey(roomId))
            {
                throw new ArgumentException("Room with this Id already exists.");
            }

            this.BunniesByRoom.Add(roomId, new Dictionary<int, HashSet<Bunny>>());
        }

        public void AddBunny(string name, int team, int roomId)
        {
            if (this.Bunnies.ContainsKey(name))
            {
                throw new ArgumentException("A bunny with the same name already exists.");
            }

            if (!this.BunniesByRoom.ContainsKey(roomId))
            {
                throw new ArgumentException("A room with this id is not yet created.");
            }

            if (team < 0 || team > 4)
            {
                throw new IndexOutOfRangeException("Team id Should be in the range [0..4]");
            }

            Bunny bunny = new Bunny(name, team, roomId);
            if (!this.BunniesByRoom[roomId].ContainsKey(team))
            {
                this.BunniesByRoom[roomId].Add(team, new HashSet<Bunny>());
            }

            this.BunniesByRoom[roomId][team].Add(bunny);
            this.Bunnies.Add(name, bunny);
            if (!this.BunniesByTeamId.ContainsKey(team))
            {
                this.BunniesByTeamId.Add(team, new SortedSet<Bunny>());
            }

            this.BunniesByTeamId[team].Add(bunny);
        }

        public void Remove(int roomId)
        {
            if (!this.BunniesByRoom.ContainsKey(roomId))
            {
                throw new ArgumentException("A room with this id is not yet created.");
            }

            foreach (var team in this.BunniesByRoom[roomId].Keys)
            {
                foreach (var bunny in this.BunniesByRoom[roomId][team])
                {
                    this.Bunnies.Remove(bunny.Name);
                }

                this.BunniesByTeamId.Remove(team);
            }

            this.BunniesByRoom.Remove(roomId);
        }

        public void Next(string bunnyName)
        {
            if (!this.Bunnies.ContainsKey(bunnyName))
            {
                throw new ArgumentException("This bunny does not exist.");
            }

            Bunny bunny = this.Bunnies[bunnyName];
            int oldRoom = bunny.RoomId;
            int newRoom = 0;

            newRoom = this.BunniesByRoom.Count - 1 == this.BunniesByRoom.IndexOfKey(oldRoom) 
                ? this.BunniesByRoom.Keys.First() 
                : this.BunniesByRoom.Keys.First(x=>x > oldRoom);

            int team = bunny.Team;
            this.BunniesByRoom[oldRoom][bunny.Team].Remove(bunny);
            if (!this.BunniesByRoom[newRoom].ContainsKey(team))
            {
                this.BunniesByRoom[newRoom].Add(team, new HashSet<Bunny>());
            }

            this.BunniesByRoom[newRoom][team].Add(bunny);
            bunny.RoomId = newRoom;
        }

        public void Previous(string bunnyName)
        {
            if (!this.Bunnies.ContainsKey(bunnyName))
            {
                throw new ArgumentException("This bunny does not exist.");
            }

            Bunny bunny = this.Bunnies[bunnyName];
            int oldRoom = bunny.RoomId;
            int newRoom = 0;

            newRoom = 0 == this.BunniesByRoom.IndexOfKey(oldRoom) 
                ? this.BunniesByRoom.Keys.Last() 
                : this.BunniesByRoom.Keys.First(x => x < oldRoom);

            int team = bunny.Team;
            this.BunniesByRoom[oldRoom][bunny.Team].Remove(bunny);
            if (!this.BunniesByRoom[newRoom].ContainsKey(team))
            {
                this.BunniesByRoom[newRoom].Add(team, new HashSet<Bunny>());
            }

            this.BunniesByRoom[newRoom][team].Add(bunny);
            bunny.RoomId = newRoom;
        }

        public void Detonate(string bunnyName)
        {
            if (!this.Bunnies.ContainsKey(bunnyName))
            {
                throw new ArgumentException("This bunny does not exist.");
            }

            Bunny bunny = this.Bunnies[bunnyName];
            var otherTeams = this.BunniesByRoom[bunny.RoomId].Keys.Where(x => x != bunny.Team);
            foreach(var otherTeam in otherTeams)
            {
                List<Bunny> otherTeamDeadBunnies = new List<Bunny>();
                var bunniesByRoomOtherTeam = this.BunniesByRoom[bunny.RoomId][otherTeam];
                foreach (var other in bunniesByRoomOtherTeam)
                {
                    other.Health -= BunnyDetonateDamage;
                    if (other.Health <= 0)
                    {
                        otherTeamDeadBunnies.Add(other);                     
                    }

                    bunny.Score = bunny.Score + otherTeamDeadBunnies.Count;
                }

                var bunniesByTeamIdOtherTeam = this.BunniesByTeamId[otherTeam];
                foreach (var other in otherTeamDeadBunnies)
                {
                    bunniesByTeamIdOtherTeam.Remove(other);
                    this.Bunnies.Remove(other.Name);
                    bunniesByRoomOtherTeam.Remove(other);                   
                }
            }
        }

        public IEnumerable<Bunny> ListBunniesByTeam(int team)
        {
            if (team < 0 || team > 4)
            {
                throw new IndexOutOfRangeException("Team id Should be in the range [0..4]");
            }

            var bunniesByTeam = this.BunniesByTeamId[team];

            return bunniesByTeam;
        }

        public IEnumerable<Bunny> ListBunniesBySuffix(string suffix)
        {
            var bunniesWithGivenSuffix = this.Bunnies.Values.Where(x => x.Name.EndsWith(suffix, StringComparison.Ordinal));
            return bunniesWithGivenSuffix;
        }
    }
}
