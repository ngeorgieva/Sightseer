namespace Sightseer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Models.BindingModels;
    using Models.EntityModels;
    using Models.ViewModels.Reviews;

    public class ReviewsService : Service
    {
        public IEnumerable<ReviewVm> GetAllReviewsForAttraction(int attractionId)
        {
            var attraction = this.Context.Attractions.Find(attractionId);

            if (attraction == null)
            {
                return null;
            }

            var reviews = attraction.Reviews;
            IEnumerable<ReviewVm> rvms = Mapper.Map<IEnumerable<Review>, IEnumerable<ReviewVm>>(reviews);

            return rvms;
        }

        public void CreateReview(CreateReviewBm bind, int attractionId, string username)
        {
            ApplicationUser user = this.Context.Users.FirstOrDefault(u => u.UserName == username);
            Attraction attraction = this.Context.Attractions.Find(attractionId);
            var review = new Review()
            {
                Title = bind.Title,
                ReviewText = bind.ReviewText,
                StarRating = bind.StarRating,
                WorthVisiting = bind.WorthVisiting,
                Attraction = attraction,
                Author = user
            };

            this.Context.Reviews.Add(review);
            this.Context.SaveChanges();
        }
    }
}
