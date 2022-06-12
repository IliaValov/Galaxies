using Galaxies.Exceptions;
using Galaxies.Models;
using Galaxies.Models.Enums;
using Galaxies.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Galaxies
{
    public class Engine : IGalaxyEngine
    {
        private ICollection<Galaxy> galaxies;
        private int galaxiesCount = 0;
        private int starsCount = 0;
        private int planetsCount = 0;
        private int moonsCount = 0;
        private const string REGEXPATTERN = @"\[(\w*\s*\w*)\]";
        public Engine()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            this.galaxies = new List<Galaxy>();
        }

        public void Start()
        {
            Regex r = new Regex(REGEXPATTERN);
            while (true)
            {
                try
                {
                    var rawInput = Console.ReadLine();
                    var match = r.Match(rawInput);
                    var input = rawInput.Split();
                    var command = input[0].ToLower();
                    if (command == "add")
                    {
                        var addType = input[1].ToLower();

                        var spaceObjectName = this.trimBrackets(match.Groups[1].Value);
                        var spaceObjectNameToBeAdded = this.trimBrackets(match.NextMatch().Groups[1].Value);
                        rawInput = rawInput.Replace(spaceObjectName, "");
                        if (spaceObjectNameToBeAdded.Length > 0)
                            rawInput.Replace(spaceObjectNameToBeAdded, "");
                        input = rawInput.Split();
                        if (addType == "galaxy")
                        {
                            var galaxyType = Enum.Parse<GalaxyType>(input[3]);
                            var rawAge = input[4];
                            var age = double.Parse(rawAge.Remove(rawAge.Length - 1, 1), CultureInfo.InvariantCulture);
                            var ageType = Enum.Parse<AgeType>(rawAge[rawAge.Length - 1].ToString());
                            this.AddGalaxy(new Galaxy(spaceObjectName, galaxyType, age, ageType));
                        }
                        else if (addType == "star")
                        {
                            var mass = double.Parse(input[4], CultureInfo.InvariantCulture);
                            var size = double.Parse(input[5], CultureInfo.InvariantCulture);
                            var temp = int.Parse(input[6], CultureInfo.InvariantCulture);
                            var luminosity = double.Parse(input[7], CultureInfo.InvariantCulture);
                            this.AddStar(spaceObjectName, new Star(spaceObjectNameToBeAdded, mass, size, luminosity, temp));
                        }
                        else if (addType == "planet")
                        {
                            this.AddPlanet(spaceObjectName, new Planet(spaceObjectNameToBeAdded, input[4], input[5].ToLower() == "yes"));
                        }
                        else if (addType == "moon")
                        {
                            this.AddMoon(spaceObjectName, new Moon(spaceObjectNameToBeAdded));
                        }
                        else
                        {
                            Console.WriteLine("Wrong add type.\r\nAvaible add types are: galaxy, star, planet and moon.");
                            Console.WriteLine();
                            continue;
                        }
                    }
                    else if (command == "stats")
                    {
                        this.Stats();
                    }

                    else if (command == "list")
                    {
                        var secondCommand = input[1];
                        this.PrintSpaceObjectsOf(secondCommand);
                    }
                    else if (command == "print")
                    {
                        var spaceObjectName = this.trimBrackets(match.Groups[1].Value);
                        this.Print(spaceObjectName);
                    }
                    else if (command == "exit")
                    {
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Wrong command.\r\nAvaible commands are: add [spaceObjectType], stats, list and exit.");
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void Print(string galaxyName)
        {
            Console.WriteLine(this.galaxies.FirstOrDefault(g => g.Name == galaxyName).ToString());
        }

        public void Stats()
        {
            Console.WriteLine("--- Stats ---");
            Console.WriteLine($"Galaxies: {this.galaxiesCount}");
            Console.WriteLine($"Stars: {this.starsCount}");
            Console.WriteLine($"Planets: {this.planetsCount}");
            Console.WriteLine($"Moons: {this.moonsCount}");
            Console.WriteLine("--- End of stats ---");
        }

        public void AddGalaxy(Galaxy galaxy)
        {
            if (galaxies.Any(g => g.Name == galaxy.Name))
            {
                throw new ArgumentException(ErrorMessages.GalaxyAlreadyExistsMessageError(galaxy.Name));
            }

            this.galaxies.Add(galaxy);
            this.galaxiesCount++;
        }

        public void AddStar(string galaxyName, Star star)
        {
            var galaxy = this.galaxies.FirstOrDefault(g => g.Name == galaxyName);
            if (galaxy == null)
            {
                throw new ArgumentException(ErrorMessages.GalaxyMissingMessageError(galaxyName));
            }

            if (galaxy.Stars.Any(s => s.Name == star.Name))
            {
                throw new ArgumentException(ErrorMessages.StarAlreadyExistsMessageError(star.Name));
            }

            galaxy.Stars.Add(star);
            this.starsCount++;
        }

        public void AddPlanet(string starName, Planet planet)
        {
            var star = this.galaxies.SelectMany(g => g.Stars).FirstOrDefault(s => s.Name == starName);
            if (star == null)
            {
                throw new ArgumentException(ErrorMessages.StarMissingMessageError(starName));
            }

            if (star.Planets.Any(p => p.Name == planet.Name))
            {
                throw new ArgumentException(ErrorMessages.PlanetAlreadyExistsMessageError(planet.Name));
            }

            star.Planets.Add(planet);
            this.planetsCount++;
        }

        public void AddMoon(string planetName, Moon moon)
        {
            var planet = this.galaxies.SelectMany(g => g.Stars).SelectMany(s => s.Planets).FirstOrDefault(p => p.Name == planetName);

            if (planet == null)
            {
                throw new ArgumentException(ErrorMessages.PlanetMissingMessageError(planetName));
            }

            if (planet.Moons.Any(m => m.Name == moon.Name))
            {
                throw new ArgumentException(ErrorMessages.MoonAlreadyExistsMessageError(moon.Name));
            }

            planet.Moons.Add(moon);
            this.moonsCount++;
        }


        public void PrintAllGalaxies()
        {
            Console.WriteLine("--- List of all researched galaxies ---");
            foreach (var galaxy in this.galaxies)
            {
                Console.WriteLine(galaxy.Name);
            }
            Console.WriteLine("--- End of galaxies list ---");
        }

        public void PrintAllStars()
        {
            Console.WriteLine("--- List of all researched stars ---");
            foreach (var star in this.galaxies.SelectMany(g => g.Stars))
            {
                Console.WriteLine(star.Name);
            }
            Console.WriteLine("--- End of stars list ---");
        }

        public void PrintAllPlanets()
        {
            Console.WriteLine("--- List of all researched planets ---");
            foreach (var star in this.galaxies.SelectMany(g => g.Stars).SelectMany(s => s.Planets))
            {
                Console.WriteLine(star.Name);
            }
            Console.WriteLine("--- End of planets list ---");
        }

        public void PrintAllMoons()
        {
            Console.WriteLine("--- List of all researched moons ---");
            foreach (var star in this.galaxies.SelectMany(g => g.Stars).SelectMany(s => s.Planets).SelectMany(m => m.Moons))
            {
                Console.WriteLine(star.Name);
            }
            Console.WriteLine("--- End of moons list ---");
        }

        public void PrintSpaceObjectsOf(string spaceObjectType)
        {
            if (spaceObjectType == "galaxies")
            {
                this.PrintAllGalaxies();
            }
            else if (spaceObjectType == "stars")
            {
                this.PrintAllStars();
            }
            else if (spaceObjectType == "planets")
            {
                this.PrintAllPlanets();
            }
            else if (spaceObjectType == "moons")
            {
                this.PrintAllMoons();
            }
        }

        private string trimBrackets(string input)
        {
            return input.Trim('[', ']');
        }
    }
}
