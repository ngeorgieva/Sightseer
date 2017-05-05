namespace Sightseer.Test.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Web;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.BindingModels;
    using Models.EntityModels;
    using Models.ViewModels.Attractions;
    using Moq;
    using Sightseer.Services.Interfaces;
    using TestStack.FluentMVCTesting;
    using WebApp.Controllers;

    [TestClass]
    public class TestAttractionsController
    {
        private Mock<IAttractionsService> serviceMock;
        private AttractionsController controller;

        [TestInitialize]
        public void Setup()
        {
            this.serviceMock = new Mock<IAttractionsService>();
            this.controller = new AttractionsController(this.serviceMock.Object);
        }

        [TestMethod]
        public void Index_ShouldReturnExpectedView()
        {
            AttractionVm vm1 = new AttractionVm();
            AttractionVm vm2 = new AttractionVm();
            IEnumerable<AttractionVm> aVms = new AttractionVm[2] { vm1, vm2 };
            
            this.serviceMock.Setup(s => s.GetAllAttractions(3, null)).Returns(aVms);

            this.controller.WithCallTo(attractionController => attractionController.Index(4, null))
                .ShouldRenderDefaultView().WithModel<IEnumerable<AttractionVm>>();
        }

        [TestMethod]
        public void Details_AllOk_ShouldReturnExpectedView()
        {
            AttractionDetailsVm vm = new AttractionDetailsVm();
            this.serviceMock.Setup(s => s.GetAttractionDetailsVm(3)).Returns(vm);

            this.controller.WithCallTo(attractionController => attractionController.Details(3))
                .ShouldRenderDefaultView().WithModel<AttractionDetailsVm>();
        }

        [TestMethod]
        public void Details_IdNull_ShouldReturnBadRequest()
        {
            this.controller.WithCallTo(attractionController => attractionController.Details(null))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void Details_ViewModeNull_ShouldReturnHttpNotFound()
        {
            AttractionDetailsVm vm = null;
            this.serviceMock.Setup(s => s.GetAttractionDetailsVm(3)).Returns(vm);

            this.controller.WithCallTo(attractionController => attractionController.Details(3))
                .ShouldGiveHttpStatus(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void Create_AllOk_ShouldReturnCorrectView()
        {
            this.controller.WithCallTo(attractionController => attractionController.Create())
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void Create_AllOk_ShouldRedirect()
        {
            CreateAttractionBm bm = new CreateAttractionBm();
            HttpPostedFileBase file = null;

            this.serviceMock.Setup(s => s.CreateAttraction(bm, file));
            this.controller.WithCallTo(attrController => attrController.Create(bm, file))
                .ShouldRedirectToRoute("");
        }

        [TestMethod]
        public void Create_ModelStateNotValid_ShouldReturnTheDefaultView()
        {
            this.controller.ModelState.AddModelError("username", "Invalid username");
            this.controller.WithCallTo(attrController => attrController.Create(null, null))
                .ShouldRenderDefaultView();
            
            //Teardown
            this.controller.ModelState["username"].Errors.Clear();
        }

        [TestMethod]
        public void EditAttraction_ShouldReturnCorrectView()
        {
            EditAttractionVm vm = new EditAttractionVm()
            {
                Name = "Eiffel Tower"
            };
            this.serviceMock.Setup(s => s.GetEditAttractionVm(5)).Returns(vm);

            this.controller.WithCallTo(attrController => attrController.Edit(5))
                .ShouldRenderView("Edit")
                .WithModel<EditAttractionVm>();
        }

        [TestMethod]
        public void EditProfile_IdNull_ShouldReturnBadRequest()
        {
            this.controller.WithCallTo(attrController => attrController.Edit(null))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void EditProfile_ViewModelNull_ShouldReturnHttpNotFound()
        {
            EditAttractionVm vm = null;
            this.serviceMock.Setup(s => s.GetEditAttractionVm(3)).Returns(vm);

            this.controller.WithCallTo(attrController => attrController.Edit(3))
                .ShouldGiveHttpStatus(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void EditAttr_AllOk_ShouldRedirect()
        {
            EditAttractionBm bm = new EditAttractionBm()
            {
                Id = 3
            };
            HttpPostedFileBase file = null;

            this.serviceMock.Setup(s => s.EditAttraction(bm, file));
            this.controller.WithCallTo(attrController => attrController.Edit(bm, file))
                .ShouldRedirectToRoute("");
        }

        [TestMethod]
        public void EditAttr_ModelStateNotValid_ShouldReturnTheDefaultView()
        {
            this.controller.ModelState.AddModelError("name", "Invalid name");
            this.serviceMock.Setup(s => s.GetEditAttractionVm(3)).Returns(new EditAttractionVm());
            this.controller.WithCallTo(attrController => attrController.Edit(new EditAttractionBm() { Id = 3 }, null))
                .ShouldRenderDefaultView().WithModel<EditAttractionVm>();

            //Teardown
            this.controller.ModelState["name"].Errors.Clear();
        }

        [TestMethod]
        public void AttractionImage_AllOk_ShouldReturnCorrectpartilaView()
        {
            this.serviceMock.Setup(s => s.GetAttraction(3)).Returns(new Attraction());

            this.controller.WithCallTo(attrController => attrController.AttractionImage(3))
                .ShouldRenderPartialView("AttractionImage")
                .WithModel<Attraction>();
        }

        [TestMethod]
        public void AttractionImage_NullId_ShouldReturnBadRequest()
        {
            this.controller.WithCallTo(attrController => attrController.AttractionImage(null))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }
    }
}
