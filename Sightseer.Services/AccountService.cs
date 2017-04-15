namespace Sightseer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Models.EntityModels;
    using Models.ViewModels.Account;
    using Models.ViewModels.Attractions;

    public class AccountService : Service
    {
        //public Town GetUserLocation(string townName, string countryCode)
        //{
        //    var country = this.Context.Countries.Find(countryCode);

        //    var town = this.Context.Towns.FirstOrDefault(t => t.Name == townName && t.Country.CountryCode == countryCode);

        //    if (town == null)
        //    {
        //        Town newTown = new Town() { Name = townName, Country = country };
        //        this.Context.Towns.Add(newTown);
        //        this.Context.SaveChanges();
        //    }

        //    return town;
        //}

        public UserProfileVm GetUserProfile(string userName)
        {
            ApplicationUser user = this.Context.Users.FirstOrDefault(u => u.UserName == userName);
            UserProfileVm vm = new UserProfileVm()
            {
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth
            };

            if (user.Town != null)
            {
                vm.Town = user.Town.Name;
                vm.Country = user.Town.Country.Name;
            }

            IEnumerable<Attraction> attractions =
                this.Context.Attractions.Where(a => a.Reviews.Any(r => r.Author.UserName == userName));
            var attrVm = Mapper.Map<IEnumerable<Attraction>, IEnumerable<AttractionVM>>(attractions);
            vm.ReviewedAttractions = attrVm;

            return vm;
        }
    }
}
