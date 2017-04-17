namespace Sightseer.Services
{
    using System.Collections.Generic;
    using AutoMapper;
    using Models.EntityModels;
    using Models.ViewModels.Reviews;

    public class ReviewsService : Service
    {
        public IEnumerable<ReviewVm> GetAllReviewsForAttraction(int attractionId)
        {
            var attraction = this.Context.Attractions.Find(attractionId);
            var reviews = attraction.Reviews;
            IEnumerable<ReviewVm> rvms = Mapper.Map<IEnumerable<Review>, IEnumerable<ReviewVm>>(reviews);

            return rvms;
        }
    }
}
