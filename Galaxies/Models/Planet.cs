using Galaxies.Models.Contracts;
using Galaxies.Models.Enums;
using System.Collections.Generic;
using System.Text;

namespace Galaxies.Models
{
    public class Planet : SpaceObject
    {
        public Planet(string name, string type, bool isHabitable) : base(name)
        {
            this.Type = type;
            this.IsHabitable = isHabitable;
            this.Moons = new HashSet<Moon>();
        }

        public string Type { get; set; }

        public bool IsHabitable { get; set; }

        public ICollection<Moon> Moons { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\t\t o Name: {this.Name}");
            sb.AppendLine($"\t\t   Type: {this.Type}");
            sb.AppendLine($"\t\t   Moons:");
            foreach (var moon in this.Moons)
            {         
                sb.AppendLine(moon.ToString());
            }

            return sb.ToString();
        }
    }
}
