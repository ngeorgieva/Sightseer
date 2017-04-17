namespace Sightseer.WebApp.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Models.BindingModels;
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
            IEnumerable<AttractionVm> avm = this.service.GetAllAttractions();
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

            AttractionDetailsVm vm = this.service.GetAttractionDetailsVm((int) id);

            if (vm == null)
            {
                return this.HttpNotFound();
            }
            return this.View(vm);
        }

        // GET: Attractions/Create
        [Authorize(Roles = "Admin,Traveller")]
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Attractions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Traveller")]
        public ActionResult Create(CreateAttractionBm bind, HttpPostedFileBase file)
        {
            if (this.ModelState.IsValid)
            {
                this.service.CreateAttraction(bind, file);
                return this.RedirectToAction("Index", "Attractions");
            }

            return this.View();
        }

        // GET: Attractions/Edit/5
        [Authorize(Roles = "Admin,Traveller")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EditAttractionVm vm = this.service.GetEditAttractionVm((int)id);
            if (vm == null)
            {
                return this.HttpNotFound();
            }

            return this.View(vm);
        }

        // POST: Attractions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Traveller")]
        public ActionResult Edit(EditAttractionBm bind, HttpPostedFileBase file)
        {
            if (this.ModelState.IsValid)
            {
                this.service.EditAttraction(bind, file);
                return this.RedirectToAction("Details", new { id = bind.Id });
            }

            return this.View(this.service.GetEditAttractionVm(bind.Id));
        }

        // GET: Attractions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vm = this.service.GetAttractionDetailsVm((int)id);

            if (vm == null)
            {
                return this.HttpNotFound();
            }

            return this.View(vm);
        }

        // POST: Attractions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            this.service.DeleteAttraction(id);
            return this.RedirectToAction("Index");
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
