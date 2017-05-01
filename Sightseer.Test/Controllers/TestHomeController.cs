namespace Sightseer.Test.Controllers
{
    using System.Collections.Generic;
    using AutoMapper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services;
    using Services.Interfaces;
    using TestStack.FluentMVCTesting;
    using WebApp.Controllers;
    using Models.EntityModels;
    using Models.ViewModels.Attractions;

    [TestClass]
    public class TestHomeController
    {
        private HomeController controller;

        [TestInitialize]
        public void Setup()
        {
            var service = new HomeService();
            this.controller = new HomeController(service);
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Attraction, AttractionVm>();
            });
        }

        [TestMethod]
        public void Index_AllOk()
        {
            this.controller.WithCallTo(homeController => homeController.Index()).ShouldRenderView("Index").WithModel<IEnumerable<AttractionVm>>();
        }

        [TestMethod]
        public void Contact_ShouldRenderCorrectView()
        {
            this.controller.WithCallTo(homeController => homeController.Contact()).ShouldRenderDefaultView();
        }
    }
}
