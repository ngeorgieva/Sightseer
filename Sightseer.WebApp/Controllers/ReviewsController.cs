namespace Sightseer.WebApp.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Net;
    using System.Web.Mvc;
    using Models.BindingModels;
    using Models.EntityModels;
    using Models.ViewModels.Reviews;
    using Services;
    using SightSeer.Data;

    public class ReviewsController : Controller
    {
        private SightseerContext db = new SightseerContext();
        private ReviewsService service = new ReviewsService();
        
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
        public ActionResult Create(CreateReviewBm bind, int attractionId)
        {
            string userName = this.User.Identity.Name;

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
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
            db.SaveChanges();
            return RedirectToAction("Reviews");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
