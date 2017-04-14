namespace Sightseer.WebApp
{
    using AutoMapper;
    using Models.EntityModels;
    using Models.ViewModels.Attractions;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            this.ConfigureMappings();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void ConfigureMappings()
        {
            Mapper.Initialize(expression =>
            {
                expression.CreateMap<Attraction, AttractionVM>()
                .ForMember(dest => dest.Town, opts => opts.MapFrom(src => src.Address.Town.Name))
                .ForMember(dest => dest.Country, opts => opts.MapFrom(src => src.Address.Town.Country.Name));
            });
        }
    }
}
