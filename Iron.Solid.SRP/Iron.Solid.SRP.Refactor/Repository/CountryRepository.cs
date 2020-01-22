using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iron.Solid.SRP.Refactor.Repository
{
    class CountryRepository : ICountryRepository
    {
        public IList<Country> GetAll()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Countries.ToList();
            };
        }

        public void Insert(IList<Country> countries)
        {
            using (var context = new ApplicationDbContext())
            {
                foreach (var country in countries)
                {
                    context.Add(country);
                }

                context.SaveChanges();
            };
        }
    }
}
