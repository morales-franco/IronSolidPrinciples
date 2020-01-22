using System.Collections.Generic;

namespace Iron.Solid.SRP.Refactor.ExternalDataProvider
{
    interface ICountryDataProvider
    {
        IList<string> GetCountryData();
    }
}
