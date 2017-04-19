namespace Sightseer.WebApp
{
    using AutoMapper;
    using Models.EntityModels;
    using Models.ViewModels.Attractions;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Models.ViewModels;
    using Models.ViewModels.Account;
    using Models.ViewModels.Admin;
    using Models.ViewModels.Reviews;
    using Models.ViewModels.Users;

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
                expression.CreateMap<Attraction, AttractionVm>()
                .ForMember(dest => dest.Town, opts => opts.MapFrom(src => src.Address.Town.Name))
                .ForMember(dest => dest.Country, opts => opts.MapFrom(src => src.Address.Town.Country.Name));

                expression.CreateMap<Attraction, AttractionDetailsVm>()
                .ForMember(desc => desc.AddressFirstLine, opts => opts.MapFrom(src => src.Address.FirstLine))
                .ForMember(desc => desc.Postcode, opts => opts.MapFrom(src => src.Address.Postcode))
                .ForMember(dest => dest.Town, opts => opts.MapFrom(src => src.Address.Town.Name))
                .ForMember(dest => dest.Country, opts => opts.MapFrom(src => src.Address.Town.Country.Name));

                expression.CreateMap<ApplicationUser, EditUserProfiveVm>()
                .ForMember(dest => dest.Town, opts => opts.MapFrom(src => src.Town.Name))
                .ForMember(dest => dest.Country, opts => opts.MapFrom(src => src.Town.Country.Name));

                expression.CreateMap<Attraction, EditAttractionVm>()
               .ForMember(desc => desc.AddressFirstLine, opts => opts.MapFrom(src => src.Address.FirstLine))
               .ForMember(desc => desc.Postcode, opts => opts.MapFrom(src => src.Address.Postcode))
               .ForMember(dest => dest.Town, opts => opts.MapFrom(src => src.Address.Town.Name))
               .ForMember(dest => dest.Country, opts => opts.MapFrom(src => src.Address.Town.Country.Name));

                expression.CreateMap<Review, ReviewVm>()
                    .ForMember(desc => desc.Author, opts => opts.MapFrom(src => src.Author.UserName))
                    .ForMember(desc => desc.Attraction, opts => opts.MapFrom(src => src.Attraction.Id));

                expression.CreateMap<ApplicationUser, AdminPageUserVm>();
            });
        }
    }
}
