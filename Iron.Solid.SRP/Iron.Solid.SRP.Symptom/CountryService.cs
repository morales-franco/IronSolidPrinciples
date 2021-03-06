﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Iron.Solid.SRP.Symptom
{
    /*
     * This class has many reasons to change because this class has many responsiblities
     * Connect to database
     * Read file text
     * Parse File text
     * Apply business rules
     * Logging
     * ==> This class violates the Single Responsibility Principle
     */
    class CountryService
    {
        const string countryFilePath = @"C:\Users\franc\Documents\Desarrollo\GitRepo\IronSolidPrinciples\Iron.Solid.SRP\FileStorage\Countries.txt";

        private readonly IList<string> _validLanguages;

        public CountryService()
        {
            _validLanguages = new List<string> { "spanish", "english" };
        }

        public void ProcessCountries()
        {
            //Read data from file
            List<string> countryFileLines = new List<string>();

            using (StreamReader reader = new StreamReader(countryFilePath))
            {
                try
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        countryFileLines.Add(line);
                    }
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine($"ERROR: File { countryFilePath } Not Found - { ex.Message }");
                }
            }

            //Validate countries and instance countries
            List<Country> countries = new List<Country>();
            var lineCount = 0;
            foreach (var line in countryFileLines)
            {
                lineCount++;

                var fields = line.Split("-");

                if(fields.Length != 3)
                {
                    Console.WriteLine($"WARN: Line { lineCount } Invalid. Only { fields.Length } fields found.");
                    continue;
                }

                string countryName = fields[0];
                if (string.IsNullOrWhiteSpace(countryName))
                {
                    Console.WriteLine($"WARN: Line { lineCount } - Invalid Country Name.");
                    continue;
                }

                string countryLanguage = fields[1];
                if (string.IsNullOrWhiteSpace(countryLanguage))
                {
                    Console.WriteLine($"WARN: Line { lineCount } - Invalid Country Language.");
                    continue;
                }

                string countryContinent = fields[2];
                if (string.IsNullOrWhiteSpace(countryContinent))
                {
                    Console.WriteLine($"WARN: Line { lineCount } - Invalid Country Continent.");
                    continue;
                }

                //Business rule
                if (!_validLanguages.Contains(countryLanguage.Trim().ToLower()))
                {
                    Console.WriteLine($"WARN: Line { lineCount } - Invalid Language {countryLanguage} - Country { countryName }.");
                    continue;
                }

                countries.Add(new Country
                {
                    Name = countryName.Trim(),
                    Continent = countryContinent.Trim(),
                    Language = countryLanguage.Trim()
                });

            }

            //Insert countries in BD
            using (var context = new ApplicationDbContext())
            {
                countries.ForEach(country => context.Add(country));
                context.SaveChanges();
            };
        }

        public List<Country> GetCountries()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Countries.ToList();
            };
        }
    }
}
