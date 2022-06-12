namespace Galaxies.Models.Contracts
{
    public abstract class SpaceObject
    {
        public SpaceObject()
        {

        }

        public SpaceObject(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
