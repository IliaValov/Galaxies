namespace Galaxies.Exceptions
{
    public static class ErrorMessages
    {
        public const string INVALID_PARAMETERS_FOR_STAR = "The parameters are wrong and doesn't apply for any star class";

        public static string GalaxyMissingMessageError(string galaxyName) { return $"Galaxy with name [{galaxyName}] doesn't exists"; }

        public static string PlanetMissingMessageError(string starName) { return $"Star with name [{starName}] doesn't exists"; }

        public static string StarMissingMessageError(string planetName) { return $"Planet with name [{planetName}] doesn't exists"; }

        public static string MoonMissingMessageError(string moonName) { return $"Moon with name [{moonName}] doesn't exists"; }

        public static string GalaxyAlreadyExistsMessageError(string galaxyName) { return $"Galaxy with name [{galaxyName}] already exists"; }

        public static string StarAlreadyExistsMessageError(string starName) { return $"Star with name [{starName}] already exists"; }

        public static string PlanetAlreadyExistsMessageError(string planetName) { return $"Planet with name [{planetName}] already exists"; }

        public static string MoonAlreadyExistsMessageError(string moonName) { return $"Moon with name [{moonName}] already exists"; }
    }
}
