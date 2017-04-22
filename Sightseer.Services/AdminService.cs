namespace Sightseer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Interfaces;
    using Models.EntityModels;
    using Models.ViewModels.Admin;
    using Models.ViewModels.Attractions;

    public class AdminService : Service, IAdminService
    {
        public AdminPageVm GetAdminPageVm()
        {
            IEnumerable<ApplicationUser> users = this.Context.Users;
            IEnumerable<Attraction> attractions = this.Context.Attractions;

            IEnumerable<AdminPageUserVm> userVms = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<AdminPageUserVm>>(users);
            IEnumerable<AttractionVm> attrVms = Mapper.Map<IEnumerable<Attraction>, IEnumerable<AttractionVm>>(attractions);

            AdminPageVm page = new AdminPageVm()
            {
                Attractions = attrVms,
                Users = userVms
            };

            return page;
        }

        public AttractionDetailsVm GetAttractionDetailsVm(int id)
        {
            Attraction attraction = this.Context.Attractions.Find(id);

            if (attraction == null)
            {
                return null;
            }

            AttractionDetailsVm avm = Mapper.Map<Attraction, AttractionDetailsVm>(attraction);
            return avm;
        }

        public void DeleteAttraction(int id)
        {
            var attraction = this.Context.Attractions.Find(id);

            if (attraction != null)
            {
                var reviews = new List<Review>(attraction.Reviews);
                foreach (var review in reviews)
                {
                    this.Context.Reviews.Remove(review);
                }

                this.Context.Attractions.Remove(attraction);
                this.Context.SaveChanges();
            }
        }

        // TODO: This is not working. Fix it!!!
        public void DeleteUser(string username)
        {
            var user = this.Context.Users.FirstOrDefault(u => u.UserName == username);

            if (user != null)
            {
                this.Context.Users.Remove(user);
                this.Context.SaveChanges();
            }
        }

        public AdminPageUserVm GetUserDetailsVm(string username)
        {
            var user = this.Context.Users.FirstOrDefault(u => u.UserName == username);

            if (user != null)
            {
                var userVm = Mapper.Map<ApplicationUser, AdminPageUserVm>(user);
                return userVm;
            }

            return null;
        }
    }
}
