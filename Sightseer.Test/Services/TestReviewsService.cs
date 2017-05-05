namespace Sightseer.Test.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.EntityModels;
    using Models.ViewModels.Reviews;
    using Sightseer.Services;
    using Sightseer.Services.Interfaces;

    [TestClass]
    public class TestReviewsService
    {
        private IReviewsService service;

        [TestInitialize]
        public void Setup()
        {
            this.service = new ReviewsService();
        }

        //[TestMethod]
        //public void GetAllReviewsForAttraction_ValidCase_ShouldReturnReviews()
        //{
        //    Mapper.Initialize(cfg =>
        //    {
        //        cfg.CreateMap<IEnumerable<Review>, IEnumerable<ReviewVm>>();
        //    });

        //    var result = this.service.GetAllReviewsForAttraction(1);

        //    Assert.AreEqual(4, result.Count());
        //    Assert.AreEqual(typeof(IEnumerable<ReviewVm>), result.GetType());
        //}

        [TestMethod]
        public void GetAllReviewsForAttraction_InvalidCase_ShouldReturnNull()
        {
            var result = this.service.GetAllReviewsForAttraction(100);

            Assert.AreEqual(null, result);
        }
    }
}
