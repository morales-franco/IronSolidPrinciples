using System;
using System.Collections.Generic;
using System.Text;

namespace Iron.Solid.SRP.Refactor.Repository
{
    interface ICountryRepository
    {
        void Insert(IList<Country> countries);
        IList<Country> GetAll();
    }
}
