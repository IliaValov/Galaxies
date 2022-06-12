using Galaxies.Models.Contracts;
using Galaxies.Models.Enums;
using System.Collections.Generic;
using System.Text;

namespace Galaxies.Models
{
    public class Galaxy : SpaceObject
    {
        public Galaxy(string name, GalaxyType type, double age, AgeType ageType) : base(name)
        {
            this.Type = type;
            this.Age = age;
            this.AgeType = ageType;
            this.Stars = new HashSet<Star>();
        }

        public GalaxyType Type { get; set; }

        public double Age { get; set; }

        public AgeType AgeType { get; set; }

        public ICollection<Star> Stars { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Data for {this.Name} galaxy");
            sb.AppendLine($"Type: {this.Type}");
            sb.AppendLine($"Age: {this.Age}{this.AgeType}");
            sb.AppendLine("Stars: ");
            foreach (var star in this.Stars)
            {
                sb.AppendLine(star.ToString());
            }
            sb.AppendLine($"End of data for {this.Name} galaxy");

            return sb.ToString();
        }
    }
}
