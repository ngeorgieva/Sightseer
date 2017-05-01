namespace Sightseer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Interfaces;
    using Models.BindingModels;
    using Models.EntityModels;
    using Models.ViewModels.Attractions;
    using Models.ViewModels.Users;

    public class UserService : Service, IUserService
    {
        public UserProfileVm GetUserProfile(string userName)
        {
            ApplicationUser user = this.Context.Users.FirstOrDefault(u => u.UserName == userName);
            UserProfileVm vm = new UserProfileVm()
            {
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
            var attrVm = Mapper.Map<IEnumerable<Attraction>, IEnumerable<AttractionVm>>(attractions);
            vm.ReviewedAttractions = attrVm;

            return vm;
        }

        public EditUserProfiveVm GetEditProfileVm(string username)
        {
            ApplicationUser user = this.Context.Users.FirstOrDefault(u => u.UserName == username);
            EditUserProfiveVm vm = Mapper.Map<ApplicationUser, EditUserProfiveVm>(user);
            return new EditUserProfiveVm();
        }

        public void EditUser(EditUserBm bind, string username)
        {
            ApplicationUser user = this.Context.Users.FirstOrDefault(u => u.UserName == username);
            user.UserName = bind.Email;
            user.FirstName = bind.FirstName;
            user.LastName = bind.LastName;
            user.Email = bind.Email;
            user.Town = this.GetUserLocation(bind.Town, bind.Country);
            this.Context.SaveChanges();
        }

        public bool IsEmailUnique(string email, string username)
        {
            ApplicationUser user = this.Context.Users.FirstOrDefault(u => u.UserName == username);

            if (user.Email == email)
            {
                return true;
            }

            return !this.Context.Users.Any(u => u.Email == email);
        }

        private Town GetUserLocation(string townName, string countryName)
        {
            var country = this.Context.Countries.FirstOrDefault(c => c.Name == countryName);

            if (country != null)
            {
                var town = this.Context.Towns.FirstOrDefault(t => t.Name == townName && t.Country.Name == countryName);

                if (town == null)
                {
                    Town newTown = new Town() { Name = townName, Country = country };
                    this.Context.Towns.Add(newTown);
                    this.Context.SaveChanges();
                }

                return town;
            }

            return null;
        }
    }
}
