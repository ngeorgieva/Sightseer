namespace Sightseer.Test.Services
{
    using System.Linq;
    using AutoMapper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.BindingModels;
    using Models.EntityModels;
    using Models.ViewModels.Attractions;
    using Sightseer.Services;
    using Sightseer.Services.Interfaces;

    [TestClass]
    public class TestAttractionService
    {
        private IAttractionsService service;

        [TestInitialize]
        public void Setup()
        {
            this.service = new AttractionsService();
        }

        [TestMethod]
        public void GetAttractionDetailsVm_ValidCase_ShouldReturnCorrectVm()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Attraction, AttractionDetailsVm>();
            });
            var result = this.service.GetAttractionDetailsVm(1);

            Assert.AreEqual("Aleksandar Nevski Cathedral", result.Name);
        }

        [TestMethod]
        public void GetAttractionDetailsVm_InvalidCase_ShouldReturnNull()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Attraction, AttractionDetailsVm>();
            });
            var result = this.service.GetAttractionDetailsVm(100);

            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void GetAttraction_ValidCase_ShoudlReturnCorrectAttr()
        {
            var result = this.service.GetAttraction(1);

            Assert.AreEqual("Aleksandar Nevski Cathedral", result.Name);
        }

        [TestMethod]
        public void GetAttraction_InvalidCase_ShoudlReturnNull()
        {
            var result = this.service.GetAttraction(100);

            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void GetAllAttractions_ValidCase_ShoudlReturnCorrectVm()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Attraction, AttractionVm>();
            });

            var result = this.service.GetAllAttractions(1, null);

            Assert.AreEqual(4, result.Count());
        }

        [TestMethod]
        public void GetAllAttractions_ValidCaseSearch_ShoudlReturnCorrectVm()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Attraction, AttractionVm>();
            });

            var result = this.service.GetAllAttractions(1, "Nevski");

            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void CreateAttraction_ValidCase_AttrCreated()
        {
            var bind = new CreateAttractionBm()
            {
                AddressFirstLine = "BlaBLa",
                Town = "Sofia",
                Country = "Bulgaria",
                Postcode = "GGGGG",
                Description = "Description",
                Latitude = "42.6953468",
                Longitude = "23.183863",
                Name = "Test Attraction"
            };

            this.service.CreateAttraction(bind, null);

            var result = this.service.GetAttractionByName("Test Attraction");

            Assert.AreEqual("Test Attraction", result.Name);
            Assert.AreEqual("Sofia", result.Address.Town.Name);
            Assert.AreEqual("Bulgaria", result.Address.Town.Country.Name);
            Assert.AreEqual("Description", result.Description);
        }

        [TestMethod]
        public void EditAttraction_ValidCase_AttrEdited()
        {
            var attrToEdit = this.service.GetAttractionByName("Test Attraction");
            EditAttractionBm bind = new EditAttractionBm()
            {
                Id = attrToEdit.Id,
                Name = "Test Attraction2",
                Town = "Paris",
                Country = "France",
                AddressFirstLine = "new address",
                Description = attrToEdit.Description,
                Postcode = "new postcode"
            };

            this.service.EditAttraction(bind, null);

            var editedAttr = this.service.GetAttractionByName("Test Attraction2");
            Assert.AreEqual("Test Attraction2", editedAttr.Name);
            Assert.AreEqual("Paris", editedAttr.Address.Town.Name);
            Assert.AreEqual("France", editedAttr.Address.Town.Country.Name);
            Assert.AreEqual("Description", editedAttr.Description);
            this.service.DeleteAttraction(editedAttr);
        }
    }
}
