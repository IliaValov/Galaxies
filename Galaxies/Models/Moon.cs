using Galaxies.Models.Contracts;
using System.Text;

namespace Galaxies.Models
{
    public class Moon : SpaceObject
    {
        public Moon(string name) : base(name)
        {

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"\t\t\t \u25AE {this.Name}");
            return sb.ToString();
        }
    }
}
