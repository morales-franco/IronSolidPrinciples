using Iron.Solid.SRP.Refactor.ExternalDataProvider;
using Iron.Solid.SRP.Refactor.Logger;
using Iron.Solid.SRP.Refactor.Parser;
using Iron.Solid.SRP.Refactor.Repository;
using System;
using System.Collections.Generic;

namespace Iron.Solid.SRP.Refactor
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new CountryLogger();
            var countryService = new CountryService(
                logger,
                new TextFileCountryDataProvider(logger,@"C:\Users\franc\Documents\Desarrollo\GitRepo\IronSolidPrinciples\Iron.Solid.SRP\FileStorage\Countries.txt"),
                new CountryParser(logger),
                new CountryRepository()
            );

            countryService.ProcessCountries();

            Console.WriteLine("------ Results ------");
            var countries = countryService.GetCountries();
            ShowCountries(countries);
        }

        private static void ShowCountries(List<Country> countries)
        {
            countries.ForEach(c =>
                Console.WriteLine($"{ c.Name } - { c.Continent } - { c.Language }"));
        }
    }
}
