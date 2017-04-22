namespace Sightseer.Services.Interfaces
{
    using System.Collections.Generic;
    using Models.BindingModels;
    using Models.ViewModels.Reviews;

    public interface IReviewsService
    {
        IEnumerable<ReviewVm> GetAllReviewsForAttraction(int attractionId);
        void CreateReview(CreateReviewBm bind, int attractionId, string username);
        ReviewVm GetDeleteReviewVm(int id);
        int DeleteReview(int id);
        void Dispose();
    }
}