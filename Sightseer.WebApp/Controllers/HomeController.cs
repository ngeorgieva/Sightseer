namespace Sightseer.WebApp.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Models.ViewModels.Attractions;
    using Services;
    using Services.Interfaces;

    [RequireHttps]
    public class HomeController : Controller
    {
        private IHomeService service;

        public HomeController(IHomeService service)
        {
            this.service = service;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            IEnumerable<AttractionVm> attractionVMs = this.service.GetTopAttractions();
            return this.View(attractionVMs);
        }

        // TODO: Contact form if I have time
        public ActionResult Contact()
        {
            return this.View();
        }
    }
}