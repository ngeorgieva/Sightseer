namespace Sightseer.Test.Controllers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Net;
    using System.Web;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.BindingModels;
    using Models.ViewModels.Reviews;
    using Moq;
    using Sightseer.Services.Interfaces;
    using TestStack.FluentMVCTesting;
    using WebApp.Controllers;

    [TestClass]
    public class TestReviewsController
    {
        private Mock<IReviewsService> serviceMock;
        private ReviewsController controller;

        [TestInitialize]
        public void Setup()
        {
            this.serviceMock = new Mock<IReviewsService>();
            this.controller = new ReviewsController(this.serviceMock.Object);     
        }

        [TestMethod]
        public void Reviews_AllOk_ShouldReturnExpectedView()
        {
            ReviewVm vm1 = new ReviewVm();
            ReviewVm vm2 = new ReviewVm();
            IEnumerable<ReviewVm> rvms = new ReviewVm[2] { vm1, vm2 };
            this.serviceMock.Setup(s => s.GetAllReviewsForAttraction(3)).Returns(rvms);

            this.controller.WithCallTo(reviewsController => reviewsController.Reviews(3))
                .ShouldRenderPartialView("Reviews").WithModel<IEnumerable<ReviewVm>>();
        }

        [TestMethod]
        public void Reviews_IdNull_ShouldReturnBadRequest()
        {
            this.controller.WithCallTo(reviewsController => reviewsController.Reviews(null))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void Reviews_ViewModeNull_ShouldReturnHttpNotFound()
        {
            IEnumerable<ReviewVm> rvms = null;
            this.serviceMock.Setup(s => s.GetAllReviewsForAttraction(3)).Returns(rvms);

            this.controller.WithCallTo(reviewsController => reviewsController.Reviews(3))
                .ShouldGiveHttpStatus(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void CreateReview_AllOk_ShouldReturnCorrectView()
        {
            this.controller.WithCallTo(reviewsController => reviewsController.Create())
                .ShouldRenderDefaultPartialView();
        }

        [TestMethod]
        public void CreateReview_AllOk_ShouldRedirect()
        {
            CreateReviewBm bm = new CreateReviewBm();

            this.serviceMock.Setup(s => s.CreateReview(bm, 3, "lilly"));
            this.controller.WithCallTo(reviewsController => reviewsController.Create(bm, 3, "lilly"))
                .ShouldRedirectToRoute("");
        }

        [TestMethod]
        public void CreateReview_ModelStateNotValid_ShouldReturnTheDefaultView()
        {
            this.controller.ModelState.AddModelError("name", "Invalid name");
            this.controller.WithCallTo(reviewsController => reviewsController.Create(null, 3, "lilly"))
                .ShouldRenderDefaultView();

            //Teardown
            this.controller.ModelState["name"].Errors.Clear();
        }

        [TestMethod]
        public void DeleteReview_AllOk_ShouldReturnExpectedView()
        {
            ReviewVm vm = new ReviewVm();
            this.serviceMock.Setup(s => s.GetDeleteReviewVm(3)).Returns(vm);

            this.controller.WithCallTo(attractionController => attractionController.Delete(3))
                .ShouldRenderDefaultView().WithModel<ReviewVm>();
        }

        [TestMethod]
        public void DeleteReview_IdNull_ShouldReturnBadRequest()
        {
            this.controller.WithCallTo(reviewsController => reviewsController.Delete(null))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void DeleteReview_ViewModeNull_ShouldReturnHttpNotFound()
        {
            ReviewVm vm = null;
            this.serviceMock.Setup(s => s.GetDeleteReviewVm(3)).Returns(vm);

            this.controller.WithCallTo(reviewsController => reviewsController.Delete(3))
                .ShouldGiveHttpStatus(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void DeleteConfirmed_AllOk_ShouldRedirect()
        {
            this.controller.WithCallTo(reviewsController => reviewsController.DeleteConfirmed(3))
                .ShouldRedirectToRoute("");
        }
    }
}
