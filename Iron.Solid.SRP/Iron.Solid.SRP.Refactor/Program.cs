using System;
using System.Collections.Generic;

namespace Iron.Solid.SRP.Refactor
{
    class Program
    {
        static void Main(string[] args)
        {
            var countryService = new CountryService();
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
