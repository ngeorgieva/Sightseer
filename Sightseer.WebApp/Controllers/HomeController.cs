namespace Sightseer.WebApp.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Models.ViewModels.Attractions;
    using Services;

    public class HomeController : Controller
    {
        private HomeService service;

        public HomeController()
        {
            this.service = new HomeService();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            IEnumerable<AttractionVM> attractionVMs = this.service.GetTopAttractions();
            return this.View(attractionVMs);
        }

        public ActionResult About()
        {
            this.ViewBag.Message = "Your application description page.";

            return this.View();
        }

        public ActionResult Contact()
        {
            this.ViewBag.Message = "Your contact page.";

            return this.View();
        }
    }
}