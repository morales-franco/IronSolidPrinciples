using Iron.Solid.SRP.Refactor.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Iron.Solid.SRP.Refactor.ExternalDataProvider
{
    class TextFileCountryDataProvider: ICountryDataProvider
    {
        private readonly ILogger _logger;
        private readonly string FilePath;

        public TextFileCountryDataProvider(ILogger logger,
            string path)
        {
            _logger = logger;
            FilePath = path;
        }

        public IList<string> GetCountryData()
        {
            List<string> countryFileLines = new List<string>();

            using (StreamReader reader = new StreamReader(FilePath))
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
                    _logger.LogError($"ERROR: File { FilePath } Not Found - { ex.Message }");
                }
            }

            return countryFileLines;
        }
    }
}
