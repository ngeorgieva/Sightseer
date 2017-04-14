namespace Sightseer.WebApp.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Net;
    using System.Web.Mvc;
    using Models.EntityModels;
    using Models.ViewModels.Attractions;
    using Services;
    using SightSeer.Data;

    [RoutePrefix("attractions")]
    public class AttractionsController : Controller
    {
        private AttractionsService service;
        private SightseerContext db = new SightseerContext();

        public AttractionsController()
        {
            this.service = new AttractionsService();
        }

        // GET: Attractions
        [AllowAnonymous]
        public ActionResult Index()
        {
            IEnumerable<AttractionVM> avm = this.service.GetAllAttractions();
            return this.View(avm);
        }

        // GET: Attractions/Details/5
        [AllowAnonymous]
        [HttpGet, Route("attractions/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AttractionDetailsVm vm = this.service.GetDetails((int) id);

            if (vm == null)
            {
                return this.HttpNotFound();
            }
            return this.View(vm);
        }

        // GET: Attractions/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Attractions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Image,Description")] Attraction attraction)
        {
            if (this.ModelState.IsValid)
            {
                db.Attractions.Add(attraction);
                db.SaveChanges();
                return this.RedirectToAction("Index");
            }

            return this.View(attraction);
        }

        // GET: Attractions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attraction attraction = db.Attractions.Find(id);
            if (attraction == null)
            {
                return HttpNotFound();
            }
            return View(attraction);
        }

        // POST: Attractions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Image,Description")] Attraction attraction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attraction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(attraction);
        }

        // GET: Attractions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attraction attraction = db.Attractions.Find(id);
            if (attraction == null)
            {
                return HttpNotFound();
            }
            return View(attraction);
        }

        // POST: Attractions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attraction attraction = db.Attractions.Find(id);
            db.Attractions.Remove(attraction);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: attractions/attractionImage/{id}
        public ActionResult AttractionImage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Attraction attraction = this.service.GetAttractionImage((int)id);

            return this.PartialView(attraction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
