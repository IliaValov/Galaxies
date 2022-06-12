using Galaxies.Service.Contracts;
using System;
using System.Globalization;

namespace Galaxies
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IEngine engine = new Engine();
            engine.Start();
        }
    }
}
