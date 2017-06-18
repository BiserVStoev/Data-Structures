namespace BunnyWars.Core
{
    using System.Linq;
    using System;
    using System.Collections.Generic;
    using Wintellect.PowerCollections;

    public class BunnyWarsStructure : IBunnyWarsStructure
    {
        private const int IdMinValue = 0;
        private const int IdMaxValue = 4;
        private readonly Dictionary<int, SortedSet<Bunny>[]> bunniesByRoomId;
        private readonly Dictionary<string, Bunny> bunnyByName;
        private readonly Dictionary<int, SortedSet<Bunny>> bunniesByTeamId;
        private readonly OrderedSet<int> rooms;
        private readonly OrderedBag<Bunny> bunniesBySuffix;

        public BunnyWarsStructure()
        {
            this.bunniesByRoomId = new Dictionary<int, SortedSet<Bunny>[]>();
            this.bunnyByName = new Dictionary<string, Bunny>();
            this.bunniesByTeamId = new Dictionary<int, SortedSet<Bunny>>();
            this.rooms = new OrderedSet<int>();
            this.bunniesBySuffix = new OrderedBag<Bunny>(new SuffixComparer());
        }

        public int BunnyCount { get { return this.bunniesBySuffix.Count; } }

        public int RoomCount { get { return this.rooms.Count; } }

        public void AddRoom(int roomId)
        {
            if (this.bunniesByRoomId.ContainsKey(roomId))
            {
                throw new ArgumentException(string.Format("Room with {0} Id already exists.", roomId));
            }
            
            this.bunniesByRoomId.Add(roomId, new SortedSet<Bunny>[5]);
            this.rooms.Add(roomId);
        }

        public void AddBunny(string name, int team, int roomId)
        {
            if (team < IdMinValue)
            {
                throw new IndexOutOfRangeException("Team Id cannot be negative.");
            }

            if (team > IdMaxValue)
            {
                throw new IndexOutOfRangeException("Team Id cannot be above 4.");
            }

            if (this.bunnyByName.ContainsKey(name))
            {
                throw new ArgumentException(string.Format("Bunny with name {0} already exists.", name));
            }

            if (!this.bunniesByRoomId.ContainsKey(roomId))
            {
                throw new ArgumentException(string.Format("Room with {0} Id does not exist.", roomId));
            }

            var bunny = new Bunny(name, team, roomId);

            // Add bunny by roomID
            if (this.bunniesByRoomId[roomId][team] == null)
            {
                this.bunniesByRoomId[roomId][team] = new SortedSet<Bunny>();
            }
            this.bunniesByRoomId[roomId][team].Add(bunny);

            // Add bunny by name
            this.bunnyByName.Add(name, bunny);

            // Add bunny by teamId
            if (!this.bunniesByTeamId.ContainsKey(team))
            {
                this.bunniesByTeamId.Add(team, new SortedSet<Bunny>());
            }

            this.bunniesByTeamId[team].Add(bunny);
            
            // Add room for suffix
            this.bunniesBySuffix.Add(bunny);
        }

        public void Remove(int roomId)
        {
            if (!this.bunniesByRoomId.ContainsKey(roomId))
            {
                throw new ArgumentException(string.Format("Room with {0} Id does not exist.", roomId));
            }

            foreach (var bunnySet in this.bunniesByRoomId[roomId])
            {
                if (bunnySet == null)
                {
                    continue;
                }

                foreach (var bunny in bunnySet.ToList())
                {
                    // Remove bunny by name
                    this.bunnyByName.Remove(bunny.Name);

                    // Remove bunny by teamId
                    this.bunniesByTeamId[bunny.Team].Remove(bunny);
                    
                    // Remove bunny by suffix
                    this.bunniesBySuffix.Remove(bunny);
                }
            }

            // Remove bunnies by Id
            this.bunniesByRoomId.Remove(roomId);

            // Remove room
            this.rooms.Remove(roomId);
        }

        public void Next(string bunnyName)
        {
            if (!this.bunnyByName.ContainsKey(bunnyName))
            {
                throw new ArgumentException(string.Format("Bunny with the name {0} does not exist.", bunnyName));
            }

            var bunny = this.bunnyByName[bunnyName];
            var currentRoomId = bunny.RoomId;
            var currentRoomIndex = this.rooms.IndexOf(currentRoomId);
            var nextIndex = currentRoomIndex + 1;
            if (nextIndex > this.rooms.Count - 1)
            {
                nextIndex = 0;
            }

            var nextRoomId = this.rooms[nextIndex];

            this.bunniesByRoomId[currentRoomId][bunny.Team].Remove(bunny);

            bunny.RoomId = nextRoomId;
            if (this.bunniesByRoomId[nextRoomId][bunny.Team] == null)
            {
                this.bunniesByRoomId[nextRoomId][bunny.Team] = new SortedSet<Bunny>();
            }

            this.bunniesByRoomId[nextRoomId][bunny.Team].Add(bunny);
        }

        public void Previous(string bunnyName)
        {
            if (!this.bunnyByName.ContainsKey(bunnyName))
            {
                throw new ArgumentException(string.Format("Bunny with the name {0} does not exist.", bunnyName));
            }

            var bunny = this.bunnyByName[bunnyName];
            var currentRoomId = bunny.RoomId;
            var currentRoomIndex = this.rooms.IndexOf(currentRoomId);
            var previousIndex = currentRoomIndex - 1;
            if (previousIndex < 0)
            {
                previousIndex = this.rooms.Count - 1;
            }

            var previousRoomId = this.rooms[previousIndex];

            this.bunniesByRoomId[currentRoomId][bunny.Team].Remove(bunny);

            bunny.RoomId = previousRoomId;
            if (this.bunniesByRoomId[previousRoomId][bunny.Team] == null)
            {
                this.bunniesByRoomId[previousRoomId][bunny.Team] = new SortedSet<Bunny>();
            }

            this.bunniesByRoomId[previousRoomId][bunny.Team].Add(bunny);
        }

        public void Detonate(string bunnyName)
        {
            if (!this.bunnyByName.ContainsKey(bunnyName))
            {
                throw new ArgumentException(string.Format("Bunny with the name {0} does not exist.", bunnyName));
            }

            if (this.bunniesByTeamId.Count < 2)
            {
                return;
            }

            var bunny = this.bunnyByName[bunnyName];
            var bunnyRoomId = bunny.RoomId;
            var room = this.bunniesByRoomId[bunnyRoomId];

            for (int i = 0; i < 5; i++)
            {
                if (bunny.Team == i || room[i] == null)
                {
                    continue;
                }

                foreach (var bunn in room[i].ToList())
                {
                    bunn.Health -= 30;
                    if (bunn.Health <= 0)
                    {
                        this.bunniesByTeamId[bunn.Team].Remove(bunn);
                        this.bunnyByName.Remove(bunn.Name);
                        this.bunniesBySuffix.Remove(bunn);
                        this.bunniesByRoomId[bunnyRoomId][bunn.Team].Remove(bunn);
                        bunny.Score++;
                    }
                }
            }
        }

        public IEnumerable<Bunny> ListBunniesByTeam(int team)
        {
            if (team < 0 || team > 4)
            {
                throw new IndexOutOfRangeException(string.Format("Team Id must be between {0} and {1}.", IdMinValue, IdMaxValue));
            }

            return this.bunniesByTeamId.GetValuesForKey(team);
        }

        public IEnumerable<Bunny> ListBunniesBySuffix(string suffix)
        {
            var minBunny = new Bunny(suffix, 0, 0);
            var maxBunny = new Bunny(char.MaxValue + suffix, 0, 0);
            var bunniesBySufix = this.bunniesBySuffix.Range(minBunny, true, maxBunny, true);

            return bunniesBySufix;
        }
    }
}