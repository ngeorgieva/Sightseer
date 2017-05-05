namespace Sightseer.Test.Services
{
    using AutoMapper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.EntityModels;
    using Models.ViewModels.Attractions;
    using Models.ViewModels.Users;
    using Sightseer.Services;
    using Sightseer.Services.Interfaces;

    [TestClass]
    public class TestUserService
    {
        private IUserService service;

        [TestInitialize]
        public void Setup()
        {
            this.service = new UserService();
        }

        [TestMethod]
        public void TestGetUserProfile_ValidCase_ShouldRerunCorrectVm()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Attraction, AttractionVm>();
            });
            var result = this.service.GetUserProfile("admin@gmail.com");

            Assert.AreEqual(result.GetType(), typeof(UserProfileVm));
            Assert.AreEqual(result.LastName, "admin");
            Assert.AreEqual(result.LastName, "admin");
            Assert.AreEqual(result.Town, "Sofia");
            Assert.AreEqual(result.Country, "Bulgaria");
        }

        [TestMethod]
        public void TestGetUserProfile_InvalidCase_ShouldRerunNull()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Attraction, AttractionVm>();
            });

            var result = this.service.GetEditProfileVm("blablabla");

            Assert.AreEqual(result, null);
        }

        [TestMethod]
        public void GetEditProfileVm_ValidCase_ShouldRerunCorrectVm()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ApplicationUser, EditUserProfiveVm>();
            });

            var result = this.service.GetEditProfileVm("admin@gmail.com");

            Assert.AreEqual(result.GetType(), typeof(EditUserProfiveVm));
            Assert.AreEqual(result.FirstName, "admin");
            Assert.AreEqual(result.LastName, "admin");
        }

        [TestMethod]
        public void GetEditProfileVm_InvalidCase_ShouldRerunNull()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ApplicationUser, EditUserProfiveVm>();
            });

            var result = this.service.GetEditProfileVm("blablabla");

            Assert.AreEqual(result, null);
        }

        [TestMethod]
        public void TestEditUser_ValidCase_ShouldReturnCorrectView()
        {
            //: TODO
        }

        [TestMethod]
        public void TestEditUser_InvalidCase_ShouldThrow()
        {
            //: TODO
        }

        [TestMethod]
        public void IsEmailUnique_ValidCase_ShoudlReturnTrue()
        {
            var result = this.service.IsEmailUnique("admin@gmail.com", "admin@gmail.com");

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsEmailUnique_ValidCase_ShoudlReturnFalse()
        {
            var result = this.service.IsEmailUnique("pesho@gmail.com", "admin@gmail.com");

            Assert.AreEqual(false, result);
        }
    }
}
