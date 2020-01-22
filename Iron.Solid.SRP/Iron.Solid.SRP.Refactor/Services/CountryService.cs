using Iron.Solid.SRP.Refactor.ExternalDataProvider;
using Iron.Solid.SRP.Refactor.Logger;
using Iron.Solid.SRP.Refactor.Parser;
using Iron.Solid.SRP.Refactor.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Iron.Solid.SRP.Refactor
{
    /*
     * This class only needs to change when business rules change!
     * Only one reason! Only one Responsibility!
     * 
     * What happen if we change to another database provider? Nothing! It's not our responsibility! Ask to Repository class!
     * What happen if the file location changes or now, we need to retrieve data from a webservice not from a file text? Nothing! Ask to the Data provider it is his resposibility!
     * What happen if the parse rules changes? Nothing! You have to ask to the Country Parser! I don't parse any informatin here!
     * What happen if business rules change? OK! That's is MY RESPONSIBILITY! Tell me more! Valid languages  change ? I have to update my code!
     * What happen if Logger provider changes? Hey I want to log in a cloud provider, any problem ? For me it is ok! You need to ask to the Logger Provider! I don't need to change the code if you change the log provider!
     * ==> This class complies Single Responsibility Principle :D
     */
    class CountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ICountryParser _countryParser;
        private readonly ICountryDataProvider _countryDataProvider;
        private readonly ILogger _logger;
        private readonly IList<string> _validLanguages;

        public CountryService(
            ILogger logger,
            ICountryDataProvider countryDataProvider, 
            ICountryParser countryParser, 
            ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
            _countryParser = countryParser;
            _countryDataProvider = countryDataProvider;
            _logger = logger;
            _validLanguages = new List<string> { "spanish", "english" };

        }

        public void ProcessCountries()
        {
            IList<string> countryFileLines = _countryDataProvider.GetCountryData();
            IList<Country> countries = _countryParser.Parse(countryFileLines);
            ApplyBusinessRules(countries);
            InsertCountries(countries);
        }

        private void ApplyBusinessRules(IList<Country> countries)
        {
            var countriesToDelete = new List<Country>();
            foreach (var country in countries)
            {
                //Business rule
                if (_validLanguages.Contains(country.Language.ToLower()))
                {
                    continue;
                }

                _logger.LogWarning($"WARN: Invalid Language { country.Language } - Country { country.Name }.");

                countriesToDelete.Add(country);
            }

            for (int i = countriesToDelete.Count; i > 0; i--)
            {
                countries.Remove(countriesToDelete[i - 1]);
            }

        }

        private void InsertCountries(IList<Country> countries)
        {
            _countryRepository.Insert(countries);
        }

        public List<Country> GetCountries()
        {
            return _countryRepository.GetAll().ToList();
        }
    }
}
