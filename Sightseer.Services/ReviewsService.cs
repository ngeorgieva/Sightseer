namespace Sightseer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Interfaces;
    using Models.BindingModels;
    using Models.EntityModels;
    using Models.ViewModels.Reviews;

    public class ReviewsService : Service, IReviewsService
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
            attraction.Rating = attraction.Reviews.Sum(r => r.StarRating) / (double)attraction.Reviews.Count;
            this.Context.SaveChanges();
        }

        public ReviewVm GetDeleteReviewVm(int id)
        {
            var review = this.Context.Reviews.Find(id);
            ReviewVm drvm = Mapper.Map<Review, ReviewVm>(review);

            return drvm;
        }

        public int DeleteReview(int id)
        {
            var review = this.Context.Reviews.Find(id);
            int attrId = 0;
            if (review != null)
            {
                attrId = review.Attraction.Id;
                this.Context.Reviews.Remove(review);
                this.Context.SaveChanges();
            }

            return attrId;
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }
    }
}

