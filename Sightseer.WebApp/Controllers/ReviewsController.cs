namespace Sightseer.WebApp.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Mvc;
    using Models.BindingModels;
    using Models.ViewModels.Reviews;
    using Services.Interfaces;

    public class ReviewsController : Controller
    {
        private IReviewsService service;

        public ReviewsController(IReviewsService service)
        {
            this.service = service;
        }
        
        public ActionResult Reviews(int? attractionId)
        {
            if (attractionId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IEnumerable<ReviewVm> rvms = this.service.GetAllReviewsForAttraction((int)attractionId);

            if (rvms == null)
            {
                return this.HttpNotFound();
            }

            return this.PartialView(rvms);
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            return this.PartialView();
        }

        // POST: Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateReviewBm bind, int attractionId, string userName)
        {
            if (this.ModelState.IsValid)
            {
                this.service.CreateReview(bind, attractionId, userName);
                return this.RedirectToAction("Details", "Attractions", new { id = bind.AttractionId });
            }

            return this.View();
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ReviewVm rvm = this.service.GetDeleteReviewVm((int) id);

            if (rvm == null)
            {
                return this.HttpNotFound();
            }

            return this.View(rvm);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //TODO: Make this one better!
            int attractionId = this.service.DeleteReview(id);
            return this.RedirectToAction("Details", "Attractions", new { id = attractionId });
        }

        protected override void Dispose(bool disposing)
        {
            this.service.Dispose();
            base.Dispose(disposing);
        }
    }
}
