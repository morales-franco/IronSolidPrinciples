using System;
using System.Collections.Generic;
using System.Text;

namespace Iron.Solid.SRP.Refactor.Parser
{
    interface ICountryParser
    {
        IList<Country> Parse(IList<string> countryFileLines);
    }
}
