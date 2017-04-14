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

        // TODO: Contact form if I have time
        public ActionResult Contact()
        {
            return this.View();
        }
    }
}