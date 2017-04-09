namespace SightSeer.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Sightseer.Models.EntityModels;

    public class SightseerContext : IdentityDbContext<ApplicationUser>
    {
        public SightseerContext()
            : base("name=SightseerContext")
        {
        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Continent> Continents { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Attraction> Attractions { get; set; }

        public DbSet<Town> Towns { get; set; }

        public static SightseerContext Create()
        {
            return new SightseerContext();
        }
    }
}