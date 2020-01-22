using Iron.Solid.SRP.Refactor.Logger;
using System;
using System.Collections.Generic;
using System.Text;

namespace Iron.Solid.SRP.Refactor.Parser
{
    class CountryParser : ICountryParser
    {
        private readonly ILogger _logger;

        public CountryParser(ILogger logger)
        {
            _logger = logger;
        }
        public IList<Country> Parse(IList<string> countryFileLines)
        {
            IList <Country> countries = new List<Country>();
            var lineCount = 0;
            foreach (var line in countryFileLines)
            {
                lineCount++;

                var fields = line.Split("-");

                if (fields.Length != 3)
                {
                    _logger.LogWarning($"WARN: Line { lineCount } Invalid. Only { fields.Length } fields found.");
                    continue;
                }

                string countryName = fields[0];
                if (string.IsNullOrWhiteSpace(countryName))
                {
                    _logger.LogWarning($"WARN: Line { lineCount } - Invalid Country Name.");
                    continue;
                }

                string countryLanguage = fields[1];
                if (string.IsNullOrWhiteSpace(countryLanguage))
                {
                    _logger.LogWarning($"WARN: Line { lineCount } - Invalid Country Language.");
                    continue;
                }

                string countryContinent = fields[2];
                if (string.IsNullOrWhiteSpace(countryContinent))
                {
                    _logger.LogWarning($"WARN: Line { lineCount } - Invalid Country Continent.");
                    continue;
                }

                countries.Add(new Country
                {
                    Name = countryName.Trim(),
                    Continent = countryContinent.Trim(),
                    Language = countryLanguage.Trim()
                });

            }

            return countries;
        }
    }
}
