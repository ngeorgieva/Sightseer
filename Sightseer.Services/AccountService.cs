namespace Sightseer.Services
{
    using System.Linq;
    using Models.EntityModels;

    public class AccountService : Service
    {
        public Town GetUserLocation(string townName, string countryCode)
        {
            var country = this.Context.Countries.Find(countryCode);

            var town = this.Context.Towns.FirstOrDefault(t => t.Name == townName && t.Country.CountryCode == countryCode);

            if (town == null)
            {
                Town newTown = new Town() { Name = townName, Country = country };
                this.Context.Towns.Add(newTown);
                this.Context.SaveChanges();
            }

            return town;
        }
    }
}
