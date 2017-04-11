namespace SightSeer.Data.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Sightseer.Models.EntityModels;

    internal sealed class Configuration : DbMigrationsConfiguration<SightSeer.Data.SightseerContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SightSeer.Data.SightseerContext context)
        {
            if (!context.Roles.Any(role => role.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole("Admin");
                manager.Create(role);
            }

            if (!context.Roles.Any(role => role.Name == "User"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole("User");
                manager.Create(role);
            }

            if (!context.Roles.Any(role => role.Name == "AttractionAdministrator"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole("AttractionAdministrator");
                manager.Create(role);
            }

            this.SeedContinents(context);
            this.SeedCountries(context);
        }

        private void SeedContinents(SightseerContext context)
        {
            context.Continents.AddOrUpdate(c => c.ContinentCode,
                new Continent() {ContinentCode = "AF", Name = "Africa"},
                new Continent() {ContinentCode = "AN", Name = "Antarctica"},
                new Continent() {ContinentCode = "AS", Name = "Asia"},
                new Continent() {ContinentCode = "EU", Name = "Europe"},
                new Continent() {ContinentCode = "NA", Name = "North America"},
                new Continent() {ContinentCode = "OC", Name = "Oceania"},
                new Continent() {ContinentCode = "SA", Name = "South America"});

            this.SaveChanges(context);
        }

        private void SeedCountries(SightseerContext context)
        {
            List<Country> countries = new List<Country>();

            // TODO: Change this to relative path
            using (var fs = File.OpenRead(@"D:\SoftUni\C-Sharp Web\C# MVC Frameworks - ASP.NET\Sightseer\SightSeer.Data\resources\countries.csv"))
            {
                using (var reader = new StreamReader(fs))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        var country = new Country()
                        {
                            CountryCode = values[0].Trim(),
                            Name = values[1].Trim(),
                            ContinentCode = values[2].Trim()  
                        };

                        countries.Add(country);
                    }
                }
            }

            context.Countries.AddOrUpdate(c => c.CountryCode, countries.ToArray());
            this.SaveChanges(context);
        }

        private void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
        }
    }
}
