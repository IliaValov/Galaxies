using Galaxies.Models;

namespace Galaxies.Service.Contracts
{
    public interface IGalaxyEngine : IEngine
    {
        void Stats();

        void Print(string galaxyName);

        void AddGalaxy(Galaxy galaxy);

        void AddStar(string galaxyName, Star star);

        void AddPlanet(string starName, Planet planet);

        void AddMoon(string planetName, Moon moon);

        void PrintSpaceObjectsOf(string spaceObjectType);

        void PrintAllGalaxies();

        void PrintAllStars();

        void PrintAllPlanets();

        void PrintAllMoons();

    }
}
