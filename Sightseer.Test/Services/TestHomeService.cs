namespace Sightseer.Test.Services
{
    using System.Linq;
    using AutoMapper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.EntityModels;
    using Models.ViewModels.Attractions;
    using Sightseer.Services;

    [TestClass]
    public class TestHomeService
    {
        [TestMethod]
        public void GetTopAttractions()
        {
            var service = new HomeService();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Attraction, AttractionVm>();
            });

            var result = service.GetTopAttractions();

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(true, result.All(a => a.Rating >= 4));
        }
    }
}
