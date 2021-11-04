using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Numerics;
using System.Collections;

namespace Guild
{
    public class Guild
    {
        public Guild(string name, int capacity)
        {
            Roaster = new List<Player>();
             Name = name;
            Capacity = capacity;
        }

        
        public string Name { get; set; }
        public int Capacity { get; set; }
        public List<Player> Roaster { get; set; }
        public int Count => Roaster.Count;

        public void AddPlayer(Player player)
        {
            if (Roaster.Count < Capacity)
            {
                Roaster.Add(player);
            }
        }

        public bool RemovePlayer(string name)
        {
            bool playerExists = Roaster.Exists(x => x.Name == name);
            if (playerExists)
            {
                Roaster.Remove(Roaster.First(x => x.Name == name));
                return true;
            }
            return true;
        }
        public void PromotePlayer(string name)
        {
            var player = Roaster.First(x => x.Name == name);
            if (player.Rank != "Member")
            {
                player.Rank = "Member";
            }
        }

        public void DemotePlayer(string name)
        {
            var player = Roaster.First(x => x.Name == name);
            if (player.Rank != "Trial")
            {
                player.Rank = "Trial";
            }
            
        }
        public Player[] KickPlayersByClass(string className)
        {
            Player[] listPlayers = Roaster.Where(x => x.Class == className).ToArray();
            Roaster.RemoveAll(x => x.Class == className);
            return listPlayers;
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Players in the guild: {this.Name}");
            foreach (Player player in Roaster)
            {
                sb.AppendLine(player.ToString());
            }
            return sb.ToString().TrimEnd();
        }
    }
}

