﻿namespace Sightseer.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Interfaces;
    using Models.BindingModels;
    using Models.EntityModels;
    using Models.ViewModels.Attractions;
    using PagedList;
    using Utils;

    public class AttractionsService : Service, IAttractionsService
    {
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

        public Attraction GetAttraction(int id)
        {
            var attr = this.Context.Attractions.Find(id);

            return attr;
        }

        public IEnumerable<AttractionVm> GetAllAttractions(int? page, string searchValue)
        {
            IEnumerable<Attraction> attractions;

            if (searchValue != null)
            {
                attractions = this.Context.Attractions.Where(a => a.Name.Contains(searchValue)).OrderByDescending(a => a.Rating);
                if (!attractions.Any())
                {
                    attractions =
                        this.Context.Attractions.Where(a => a.Address.Town.Name.Contains(searchValue))
                            .OrderByDescending(a => a.Rating);

                    if (!attractions.Any())
                    {
                        attractions =
                            this.Context.Attractions.Where(a => a.Address.Town.Country.Name.Contains(searchValue))
                                .OrderByDescending(a => a.Rating);
                    }
                }
            }
            else
            {
                attractions = this.Context.Attractions.OrderByDescending(a => a.Rating);
            }

            var pageNumber = page ?? 1;
            var onePageOfAttractions = attractions.ToPagedList(pageNumber, 4).ToMappedPagedList<Attraction, AttractionVm>();

            return onePageOfAttractions;
        }

        public void CreateAttraction(CreateAttractionBm bind, HttpPostedFileBase file)
        {
            var attraction = new Attraction()
            {
                Name = bind.Name,
                Description = bind.Description,
                Image = this.GetImageFromBind(file),
                Latitude = bind.Latitude,
                Longitude = bind.Longitude
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

        public EditAttractionVm GetEditAttractionVm(int id)
        {
            var attraction = this.Context.Attractions.Find(id);
            if (attraction != null)
            {
                var vm = Mapper.Map<Attraction, EditAttractionVm>(attraction);
                return vm;
            }

            return null;
        }

        public void EditAttraction(EditAttractionBm bind, HttpPostedFileBase file)
        {
            var attraction = this.Context.Attractions.Find(bind.Id);
            attraction.Name = bind.Name;
            attraction.Description = bind.Description;

            if (bind.Town != null)
            {
                var address = this.GetAddress(bind.AddressFirstLine, bind.Postcode, bind.Town, bind.Country);
                if (address == null)
                {
                    attraction.Address = new Address()
                    {
                        FirstLine = bind.AddressFirstLine,
                        Postcode = bind.Postcode,
                        Town = this.GetAttractionLocation(bind.Town, bind.Country)
                    };
                }
                else
                {
                    attraction.Address = address;
                }

            }

            if (file != null)
            {
                attraction.Image = this.GetImageFromBind(file);
            }

            this.Context.SaveChanges();
        }

        public Attraction GetAttractionByName(string name)
        {
            return this.Context.Attractions.FirstOrDefault(a => a.Name == name);
        }


        public void DeleteAttraction(Attraction attraction)
        {

            this.Context.Attractions.Remove(attraction);
            this.Context.SaveChanges();
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }

        private Address GetAddress(string addressFirstLine, string postcode, string town, string country)
        {
            var address = this.Context.Addresses.FirstOrDefault(
                a =>
                    a.FirstLine == addressFirstLine
                    && a.Postcode == postcode
                    && a.Town.Name == town
                    && a.Town.Country.Name == country);

            return address;
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
    }
}
