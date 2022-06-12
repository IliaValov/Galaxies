using Galaxies.Exceptions;
using Galaxies.Models.Contracts;
using Galaxies.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxies.Models
{
    public class Star : SpaceObject
    {
        public Star(string name, double mass, double size, double luminosity, int temperature) : base(name)
        {
            this.Mass = mass;
            this.Size = size;
            this.Temperature = temperature;
            this.Luminosity = luminosity;
            this.Class = this.FindStarClass(mass, Size, luminosity, temperature);
            this.Planets = new HashSet<Planet>();
        }

        public double Mass { get; set; }

        public double Size { get; set; }

        public double Luminosity { get; set; }

        public int Temperature { get; set; }

        public ICollection<Planet> Planets { get; set; }

        public StarClassType Class { get; set; }

        private StarClassType FindStarClass(double mass, double size, double luminosity, int temperature)
        {
            var sizeRadius = size / 2;
            if ((temperature >= 2400 && temperature <= 3700) &&
                luminosity <= 0.08 &&
                (mass >= 0.08 && mass <= 0.45) &&
                (sizeRadius <= 0.7)
             )
            {
                return StarClassType.M;
            }
            else if ((temperature >= 3700 && temperature <= 5200) &&
            (luminosity >= 0.08 && luminosity <= 0.6) &&
            (mass >= 0.45 && mass <= 0.8) &&
            (sizeRadius >= 0.7 && sizeRadius <= 0.96))
            {
                return StarClassType.K;
            }
            else if ((temperature >= 5200 && temperature <= 6000) &&
            (luminosity >= 0.6 && luminosity <= 1.5) &&
            (mass >= 0.8 && mass <= 1.04) &&
            (sizeRadius >= 0.96 && sizeRadius <= 1.15))
            {
                return StarClassType.G;
            }
            else if ((temperature >= 6000 && temperature <= 7500) &&
            (luminosity >= 1.5 && luminosity <= 5) &&
            (mass >= 1.04 && mass <= 1.4) &&
            (sizeRadius >= 1.15 && sizeRadius <= 1.4))
            {
                return StarClassType.F;
            }
            else if ((temperature >= 7500 && temperature <= 10000) &&
            (luminosity >= 5 && luminosity <= 25) &&
            (mass >= 1.4 && mass <= 2.1) &&
            (sizeRadius >= 1.4 && sizeRadius <= 1.8))
            {
                return StarClassType.A;
            }
            else if ((temperature >= 10000 && temperature <= 30000) &&
            (luminosity >= 25 && luminosity <= 30000) &&
            (mass >= 2.1 && mass <= 16) &&
            (sizeRadius >= 1.8 && sizeRadius <= 6.6))
            {
                return StarClassType.B;
            }
            else if (temperature >= 30000 && luminosity >= 30000 && mass >= 16 && sizeRadius >= 6.6)
            {
                return StarClassType.O;
            }

            throw new ArgumentException(ErrorMessages.INVALID_PARAMETERS_FOR_STAR);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\t - Name: {this.Name}");
            sb.AppendLine($"\t - Class: {this.Class}({this.Mass}, {this.Size}, {this.Temperature}, {this.Luminosity})");
            sb.AppendLine($"\t - Planets:");
            foreach (var planet in this.Planets)
            {
                sb.AppendLine(planet.ToString());
            }
            return sb.ToString();
        }
    }
}
