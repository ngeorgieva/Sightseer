namespace Sightseer.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Sightseer.Data;
    using Sightseer.Models.EntityModels;

    internal sealed class Configuration : DbMigrationsConfiguration<SightseerContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SightseerContext context)
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
                var role = new IdentityRole("Traveller");
                manager.Create(role);
            }

            this.SeedContinents(context);
            this.SeedCountries(context);
            this.SeedAttractions(context);
            this.SeedUsers(context);
            this.SeedReviews(context);
        }

        private void SeedContinents(SightseerContext context)
        {
            context.Continents.AddOrUpdate(c => c.ContinentCode,
                new Continent() { ContinentCode = "AF", Name = "Africa" },
                new Continent() { ContinentCode = "AN", Name = "Antarctica" },
                new Continent() { ContinentCode = "AS", Name = "Asia" },
                new Continent() { ContinentCode = "EU", Name = "Europe" },
                new Continent() { ContinentCode = "NA", Name = "North America" },
                new Continent() { ContinentCode = "OC", Name = "Oceania" },
                new Continent() { ContinentCode = "SA", Name = "South America" });

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

        private void SeedAttractions(SightseerContext context)
        {
            var townSofia = GetTown(context, "Bulgaria", "Sofia");
            context.Attractions.AddOrUpdate(a => a.Name,
                new Attraction()
                {
                    Name = "Aleksandar Nevski Cathedral",
                    Address = new Address() { FirstLine = "Aleksander Nevski Square", Postcode = "1000", Town = townSofia },
                    Description = "The St. Alexander Nevsky Cathedral is a Bulgarian Orthodox cathedral in Sofia, the capital of Bulgaria. Built in Neo-Byzantine style, it serves as the cathedral church of the Patriarch of Bulgaria and it is one of the largest Eastern Orthodox cathedrals in the world, as well as one of Sofia\'s symbols and primary tourist attractions. The St. Alexander Nevsky Cathedral in Sofia occupies an area of 3,170 square metres (34,100 sq ft) and can hold 10,000 people inside. It is the second-largest cathedral located on the Balkan Peninsula, after the Cathedral of Saint Sava in Belgrade.",
                    Image = this.DownloadImage("https://media-cdn.tripadvisor.com/media/photo-s/0b/46/82/bd/aleksandar-nevski-cathedral.jpg"),
                    Rating = 4.5,
                    Longitude = "23.3456486",
                    Latitude = "42.690326"
                });

            var townRio = GetTown(context, "Brazil", "Rio de Janeiro");
            context.Attractions.AddOrUpdate(a => a.Name,
            new Attraction()
            {
                Name = "Christ the Redeemer",
                Address = new Address() { FirstLine = "Corcovado mountain", Postcode = "1000", Town = townRio },
                Description = @"Christ the Redeemer is an Art Deco statue of Jesus Christ in Rio de Janeiro, Brazil, created by French sculptor Paul Landowski and built by the Brazilian engineer Heitor da Silva Costa, in collaboration with the French engineer Albert Caquot. Romanian sculptor Gheorghe Leonida fashioned the face. The statue is 30 metres (98 ft) tall, not including its 8-metre (26 ft) pedestal, and its arms stretch 28 metres (92 ft) wide.The statue weighs 635 metric tons(625 long, 700 short tons), and is located at the peak of the 700 - metre(2, 300 ft) Corcovado mountain in the Tijuca Forest National Park overlooking the city of Rio.A symbol of Christianity across the world, the statue has also become a cultural icon of both Rio de Janeiro and Brazil, and is listed as one of the New Seven Wonders of the World. It is made of reinforced concrete and soapstone, and was constructed between 1922 and 1931.",
                Image = this.DownloadImage("http://kingofwallpapers.com/christ-the-redeemer-wallpaper/christ-the-redeemer-wallpaper-010.jpg"),
                Rating = 5,
                Latitude = "-22.951916",
                Longitude = "-43.2126759"
            });

            var townAmesbury = GetTown(context, "United Kingdom", "Amesbury");
            context.Attractions.AddOrUpdate(a => a.Name,
            new Attraction()
            {
                Name = "Stonehenge",
                Address = new Address() { Postcode = "SP4 7DE", Town = townAmesbury },
                Description = @"Stonehenge is a prehistoric monument in Wiltshire, England, 2 miles (3 km) west of Amesbury and 8 miles (13 km) north of Salisbury. Stonehenge consists of ring of standing stones, with each standing stone around 4.1 metres (13 ft) high, 2.1 metres (6 ft 11 in) wide and weighing around 25 tons. The stones are set within earthworks in the middle of the most dense complex of Neolithic and Bronze Age monuments in England, including several hundred burial mounds. Archaeologists believe it was constructed from 3000 BC to 2000 BC. The surrounding circular earth bank and ditch, which constitute the earliest phase of the monument, have been dated to about 3100 BC. Radiocarbon dating suggests that the first bluestones were raised between 2400 and 2200 BC, although they may have been at the site as early as 3000 BC. One of the most famous landmarks in the UK, Stonehenge is regarded as a British cultural icon.",
                Image = this.DownloadImage("http://www.english-heritage.org.uk/remote/www.english-heritage.org.uk/content/properties/stonehenge/portico/2670999/stonehenge-sunrise?w=640&mode=none&scale=downscale&quality=60&anchor=middlecenter"),
                Rating = 4.33,
                Latitude = "51.178882",
                Longitude = "-1.8284037"
            });

            var townParis = GetTown(context, "France", "Paris");
            context.Attractions.AddOrUpdate(a => a.Name,
            new Attraction()
            {
                Name = "Eiffel Tower",
                Address = new Address() { FirstLine = "Champ de Mars, 5 Avenue Anatole France", Postcode = "75007", Town = townParis },
                Description = @"The Eiffel Tower is a wrought iron lattice tower on the Champ de Mars in Paris, France. It is named after the engineer Gustave Eiffel, whose company designed and built the tower. Constructed from 1887–89 as the entrance to the 1889 World's Fair, it was initially criticized by some of France's leading artists and intellectuals for its design, but it has become a global cultural icon of France and one of the most recognisable structures in the world.[3] The Eiffel Tower is the most-visited paid monument in the world; 6.91 million people ascended it in 2015. The tower is 324 metres (1,063 ft) tall, about the same height as an 81-storey building, and the tallest structure in Paris.",
                Image = this.DownloadImage("https://cdn.getyourguide.com/img/tour_img-376513-70.jpg"),
                Latitude = "48.8583701",
                Longitude = "2.2922926"
            });

            var townRome = GetTown(context, "Italy", "Rome");
            context.Attractions.AddOrUpdate(a => a.Name,
            new Attraction()
            {
                Name = "Colosseum",
                Address = new Address() { FirstLine = "Piazza del Colosseo, 1", Postcode = "00184", Town = townRome },
                Description = @"The Colosseum is an oval amphitheatre in the centre of the city of Rome, Italy. Built of concrete and sand,[1] it is the largest amphitheatre ever built. The Colosseum is situated just east of the Roman Forum. Construction began under the emperor Vespasian in AD 72, and was completed in AD 80 under his successor and heir Titus.[3] Further modifications were made during the reign of Domitian (81–96). These three emperors are known as the Flavian dynasty, and the amphitheatre was named in Latin for its association with their family name (Flavius).

The Colosseum could hold, it is estimated, between 50,000 and 80,000 spectators,] having an average audience of some 65,000; it was used for gladiatorial contests and public spectacles such as mock sea battles (for only a short time as the hypogeum was soon filled in with mechanisms to support the other activities), animal hunts, executions, re-enactments of famous battles, and dramas based on Classical mythology. The building ceased to be used for entertainment in the early medieval era. It was later reused for such purposes as housing, workshops, quarters for a religious order, a fortress, a quarry, and a Christian shrine.

Although partially ruined because of damage caused by earthquakes and stone-robbers, the Colosseum is still an iconic symbol of Imperial Rome. It is one of Rome's most popular tourist attractions and also has links to the Roman Catholic Church, as each Good Friday the Pope leads a torchlit ""Way of the Cross"" procession that starts in the area around the Colosseum.

The Colosseum is also depicted on the Italian version of the five-cent euro coin.",
                Image = this.DownloadImage("http://saveti.bg/wp-content/uploads/2015/06/colosseum1.jpg"),
                Latitude = "41.8902102",
                Longitude = "12.4900422"
            });

            var townLondon = GetTown(context, "United Kingdom", "London");
            context.Attractions.AddOrUpdate(a => a.Name,
            new Attraction()
            {
                Name = "The British Museum",
                Address = new Address() { FirstLine = "Great Russell St, Bloomsbury", Postcode = "WC1B 3DG", Town = townLondon },
                Description = @"The British Museum is dedicated to human history, art and culture, and is located in the Bloomsbury area of London. Its permanent collection, numbering some 8 million works, is among the largest and most comprehensive in existence and originates from all continents, illustrating and documenting the story of human culture from its beginnings to the present.

The British Museum was established in 1753, largely based on the collections of the physician and scientist Sir Hans Sloane. The museum first opened to the public on 15 January 1759, in Montagu House, on the site of the current building. Its expansion over the following two and a half centuries was largely a result of an expanding British colonial footprint and has resulted in the creation of several branch institutions, the first being the British Museum of Natural History in South Kensington in 1881 (it is nowadays simply called the Natural History Museum).

In 1973, the British Library Act 1972 detached the library department from the British Museum, but it continued to host the now separated British Library in the same Reading Room and building as the museum until 1997. The museum is a non-departmental public body sponsored by the Department for Culture, Media and Sport, and as with all other national museums in the United Kingdom it charges no admission fee, except for loan exhibitions.",
                Image = this.DownloadImage("http://www.fosterandpartners.com/media/1705103/img0.jpg"),
                Latitude = "51.5194133",
                Longitude = "-0.1269566"
            });

            var townAgra = GetTown(context, "India", "Agra");
            context.Attractions.AddOrUpdate(a => a.Name,
            new Attraction()
            {
                Name = "Taj Mahal",
                Address = new Address() { FirstLine = "Dharmapuri, Forest Colony, Tajganj", Postcode = "282001", Town = townAgra },
                Description = @"The Taj Mahal meaning Crown of the Palace) is an ivory-white marble mausoleum on the south bank of the Yamuna river in the Indian city of Agra. It was commissioned in 1632 by the Mughal emperor, Shah Jahan (reigned 1628–1658), to house the tomb of his favourite wife, Mumtaz Mahal. The tomb is the centrepiece of a 17-hectare (42-acre) complex, which includes a mosque and a guest house, and is set in formal gardens bounded on three sides by a crenellated wall.

Construction of the mausoleum was essentially completed in 1643 but work continued on other phases of the project for another 10 years. The Taj Mahal complex is believed to have been completed in its entirety in 1653 at a cost estimated at the time to be around 32 million rupees, which in 2015 would be approximately 52.8 billion rupees (US$827 million). The construction project employed some 20,000 artisans under the guidance of a board of architects led by the court architect to the emperor, Ustad Ahmad Lahauri.

The Taj Mahal was designated as a UNESCO World Heritage Site in 1983 for being ""the jewel of Muslim art in India and one of the universally admired masterpieces of the world's heritage"". Described by Nobel laureate Rabindranath Tagore as ""the tear-drop on the cheek of time"", it is regarded by many as the best example of Mughal architecture and a symbol of India's rich history. The Taj Mahal attracts 7–8 million visitors a year. In 2007, it was declared a winner of the New7Wonders of the World (2000–2007) initiative.",
                Image = this.DownloadImage("http://webneel.com/daily/sites/default/files/images/daily/04-2014/6-taj-mahal-hdr.preview.jpg"),
                Latitude = "27.1750151",
                Longitude = "78.0421552"
            });

            var townSydney = GetTown(context, "Australia", "Sydney");
            context.Attractions.AddOrUpdate(a => a.Name,
            new Attraction()
            {
                Name = "Sydney Opera House",
                Address = new Address() { FirstLine = "Bennelong Point", Postcode = "NSW 2000", Town = townSydney },
                Description = @"The Sydney Opera House is a multi-venue performing arts centre in Sydney, Australia. It is one of the 20th century's most famous and distinctive buildings. Designed by Danish architect Jørn Utzon, the building was formally opened on 20 October 1973[4] after a gestation beginning with Utzon's 1957 selection as winner of an international design competition. The government of New South Wales, led by the premier, Joseph Cahill, authorised work to begin in 1958 with Utzon directing construction. The government's decision to build Utzon's design is often overshadowed by circumstances that followed, including cost and scheduling overruns as well as the architect's ultimate resignation. The building and its surrounds occupy the whole of Bennelong Point on Sydney Harbour, between Sydney Cove and Farm Cove, adjacent to the Sydney central business district and the Royal Botanic Gardens, and close by the Sydney Harbour Bridge. Though its name suggests a single venue, the building comprises multiple performance venues which together are among the busiest[citation needed] performing arts centres – hosting well over 1, 500 performances annually, attended by more than 1.2 million people.[6] Performances are presented by numerous performing artists, including four resident companies: Opera Australia, The Australian Ballet, the Sydney Theatre Company and the Sydney Symphony Orchestra.As one of the most popular visitor attractions in Australia, more than eight million people visit the site annually, and approximately 350,000 visitors take a guided tour of the building each year. The building is managed by the Sydney Opera House Trust, an agency of the New South Wales State Government.",
                Image = this.DownloadImage("https://userscontent2.emaze.com/images/3627301d-f3ae-4fb0-99f2-1855979c3096/b4b55a561e2232b9d3b115a8d1f27c14.jpg"),
                Latitude = "-33.8567844",
                Longitude = "151.2152967"
            });

            this.SaveChanges(context);
        }

        private void SeedUsers(SightseerContext context)
        {
            CreateUser(context, "admin", "admin", "admin@gmail.com", "123Abc!", new DateTime(1986, 9, 4), "Admin");
            CreateUser(context, "Pesho", "Petrov", "pesho@gmail.com", "BlaBlaBla3£", new DateTime(1990, 6, 1), "Traveller");
            CreateUser(context, "Gosho", "Ivanov", "gosho@gmail.com", "BleBleBle3£", new DateTime(1988, 7, 2), "Traveller");
            CreateUser(context, "Maria", "Borisova", "maria@gmail.com", "BluBluBlu3£", new DateTime(1992, 8, 3), "Traveller");
        }

        private void SeedReviews(SightseerContext context)
        {
            var userPesho = GetUser(context, "pesho@gmail.com");
            var userGosho = GetUser(context, "gosho@gmail.com");
            var userMaria = GetUser(context, "maria@gmail.com");
            var admin = GetUser(context, "admin@gmail.com");

            var eiffelTower = GetAttraction(context, "Eiffel Tower");
            var alNevskiCathedral = GetAttraction(context, "Aleksandar Nevski Cathedral");
            var stonehendge = GetAttraction(context, "Stonehenge");
            var rioStatue = GetAttraction(context, "Christ the Redeemer");

            context.Reviews.AddOrUpdate(r => r.Title, new Review()
            {
                Title = "Iconic place",
                Attraction = alNevskiCathedral,
                Author = userPesho,
                ReviewText = "One of the most iconic places you can visit. It is actually a fully working church. Astonishing and breathtaking architectural achievement. It literally makes you feel humble while standing inside and staring at the ceiling. Prepare to be amazed!",
                StarRating = 5,
                WorthVisiting = true
            });

            context.Reviews.AddOrUpdate(r => r.Title, new Review()
            {
                Title = "Magnificent",
                Attraction = alNevskiCathedral,
                Author = userGosho,
                ReviewText = "This is one of the most magnificent buildings I\'ve ever seen. I stayed in Sofia for 11 days and made sure to always walk by because it\'s unlike anything I\'ve ever witnessed. The inside is also great, but they are very strict about noise and taking pictures. ",
                StarRating = 5,
                WorthVisiting = true
            });

            context.Reviews.AddOrUpdate(r => r.Title, new Review()
            {
                Title = "Would visit again",
                Attraction = alNevskiCathedral,
                Author = userMaria,
                ReviewText = "Nice architecture in general. You can see its golden cupule from most parts of Sofia, unbelievable stain glass windows that projects the light in a very special way.",
                StarRating = 4,
                WorthVisiting = true
            });

            context.Reviews.AddOrUpdate(r => r.Title, new Review()
            {
                Title = "Awesome from the outside",
                Attraction = alNevskiCathedral,
                Author = admin,
                ReviewText = "Awesome from the outside, from the inside there is a lack of caring. I do recommend the visit because it is an important landmark. Be advised that they will try to charge 5euros for a photo you might take.",
                StarRating = 4,
                WorthVisiting = true
            });

            context.Reviews.AddOrUpdate(r => r.Title, new Review()
            {
                Title = "Stunning statue and views",
                Attraction = rioStatue,
                Author = userPesho,
                ReviewText = "I recommend buying the tram ticket online to get the times you want, the early ones run out fast! There\'s obviously tons of people trying to take the typical photo from the front so you\'ll have to be a little patient to get a good shot, but other than directly in front of the statue it\'s much more relaxed on the sides and the back where the shade is. Also, the views from the sides and coming up the steps are just as good as the one where everyone is trying to take a photo.",
                StarRating = 5,
                WorthVisiting = true
            });

            context.Reviews.AddOrUpdate(r => r.Title, new Review()
            {
                Title = "Killer views",
                Attraction = rioStatue,
                Author = userMaria,
                ReviewText = "Amazing statue with killer views. Come ready with cash in hand and lots of patience. Make a day out of it, there will be a lot of people and it will be crowded at the top, but remember to relax and enjoy the view.",
                StarRating = 5,
                WorthVisiting = true
            });

            context.Reviews.AddOrUpdate(r => r.Title, new Review()
            {
                Title = "A must see",
                Attraction = rioStatue,
                Author = userGosho,
                ReviewText = "A must see when in Rio, give yourself some time for a little waiting around as it is a busy tourist hot spot. There are seating area's, funiculars and elevators to assist with the final ascent. All well worth this spectacular attraction!",
                StarRating = 5,
                WorthVisiting = true
            });

            context.Reviews.AddOrUpdate(r => r.Title, new Review()
            {
                Title = "Excellent for history lovers",
                Attraction = stonehendge,
                Author = userGosho,
                ReviewText = "Excellent for history lovers, spiritual seekers and site seeing. Museum is well curated and really enjoyable to look at the artifacts. I took  a Stonehenge only tour from London. Traffic was crazy both way. Don't be in a rush because the traffic is unpredictable. At least 2 hours one way. You will see Stonehenge on your right in a feild but the visit center is still 30 minutes with traffic down the road. Once you are at the visit center there is a decent overpriced cafeteria, bathroom, gift shop and  museum. They have example of houses people who built Stonehenge. Then you can choose between walking to the Stonehenge site or taking the buses that are included in your admission price. 30 minutes to walk, 15 minutes to wait and ride. Try to get there early and during middle of the week. Crowds will ruin your experience.",
                StarRating = 4,
                WorthVisiting = true
            });

            context.Reviews.AddOrUpdate(r => r.Title, new Review()
            {
                Title = "Crowdy",
                Attraction = stonehendge,
                Author = userMaria,
                ReviewText = "With quite the huge fame accompanying it, expectations are always high when visiting such a landmark. It is, without a doubt, worthy of said reputation and, as a destination to a pleasant walk, it more than delivers. The downside? With all that fame come neverending crowds of people who, unless your able to completely abstract yourself from them, can negatively impact the experience.",
                StarRating = 4,
                WorthVisiting = true
            });

            context.Reviews.AddOrUpdate(r => r.Title, new Review()
            {
                Title = "History",
                Attraction = stonehendge,
                Author = userPesho,
                ReviewText = "History  history   history.   Can\'t get too close anymore, but still worth the visit.  Audio guides were very informative.",
                StarRating = 5,
                WorthVisiting = true
            });

            this.SaveChanges(context);
        }

        private static Town GetTown(SightseerContext context, string countryName, string townName)
        {
            var country = context.Countries.FirstOrDefault(c => c.Name == countryName);
            var town = context.Towns.FirstOrDefault(t => t.Name == townName && t.Country.Name == countryName) ??
                       new Town()
                       {
                           Name = townName,
                           Country = country
                       };

            return town;
        }

        private static ApplicationUser GetUser(SightseerContext context, string email)
        {
            return context.Users.FirstOrDefault(u => u.Email == email);
        }

        private static Attraction GetAttraction(SightseerContext context, string name)
        {
            return context.Attractions.FirstOrDefault(u => u.Name == name);
        }

        private static void CreateUser(SightseerContext context, string firstName, string lastName, string email, string password, DateTime dateOfBirth, string role)
        {
            if (!context.Users.Any(u => u.Email == email))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser()
                {
                    UserName = email,
                    AccessFailedCount = 0,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    DateOfBirth = dateOfBirth,
                    LockoutEnabled = true
                };

                var result = manager.Create(user, password);

                if (result.Succeeded == false)
                {
                    throw new Exception(result.Errors.First());
                }

                manager.AddToRole(user.Id, role);
            }
        }

        private byte[] DownloadImage(string url)
        {
            byte[] image;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();

            Stream stream = response.GetResponseStream();

            using (BinaryReader br = new BinaryReader(stream))
            {
                image = br.ReadBytes(500000);
                br.Close();
            }
            response.Close();

            return image;
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
