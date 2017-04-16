namespace Sightseer.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Models.BindingModels;
    using Models.EntityModels;
    using Models.ViewModels.Attractions;

    public class AttractionsService : Service
    {
        public AttractionDetailsVm GetDetails(int id)
        {
            Attraction attraction = this.Context.Attractions.Find(id);

            if (attraction == null)
            {
                return null;
            }

            AttractionDetailsVm avm = Mapper.Map<Attraction, AttractionDetailsVm>(attraction);
            return avm;
        }

        public Attraction GetAttractionImage(int id)
        {
            var attr = this.Context.Attractions.Find(id);

            return attr;
        }

        public IEnumerable<AttractionVm> GetAllAttractions()
        {
            IEnumerable<Attraction> attractions = this.Context.Attractions.OrderByDescending(a => a.Rating);
            IEnumerable<AttractionVm> avms = Mapper.Map<IEnumerable<Attraction>, IEnumerable<AttractionVm>>(attractions);

            return avms;
        }

        public void CreateAttraction(CreateAttractionBm bind, HttpPostedFileBase file)
        {
            var attraction = new Attraction()
            {
                Name = bind.Name,
                Description = bind.Description,
                Image = this.GetImageFromBind(file)
            };

            if (!string.IsNullOrEmpty(bind.Town) && !string.IsNullOrEmpty(bind.Country))
            {
                var address = new Address()
                {
                    Town = this.GetAttractionLocation(bind.Town, bind.Country),
                    FirstLine = bind.AddressFirstLine,
                    Postcode = bind.Postcode
                };

                this.Context.Addresses.Add(address);
                attraction.Address = address;
            }

            this.Context.Attractions.Add(attraction);
            this.Context.SaveChanges();
        }

        private byte[] GetImageFromBind(HttpPostedFileBase bindImage)
        {
            if (bindImage != null)
            {
                var fileName = Path.GetFileName(bindImage.FileName);
                byte[] bytes = new byte[bindImage.ContentLength];
                int bytesToRead = (int)bindImage.ContentLength;
                int bytesRead = 0;
                while (bytesToRead > 0)
                {
                    int n = bindImage.InputStream.Read(bytes, bytesRead, bytesToRead);
                    if (n == 0) break;
                    bytesRead += n;
                    bytesToRead -= n;
                }

                return bytes;
            }

            return null;
        }

        private Town GetAttractionLocation(string townName, string countryName)
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
