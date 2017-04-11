namespace Sightseer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Models.EntityModels;
    using Models.ViewModels.Attractions;

    public class HomeService : Service
    {
        public IEnumerable<AttractionVM> GetTopAttractions()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Attraction, AttractionVM>()
                        .ForMember(dest => dest.Town, opts => opts.MapFrom(src => src.Address.Town.Name))
                        .ForMember(dest => dest.Country, opts => opts.MapFrom(src => src.Address.Town.Country.Name));
            });

            IMapper mapper = config.CreateMapper();
            IEnumerable<Attraction> attractions = this.Context.Attractions.Take(3);
            IEnumerable<AttractionVM> avms = mapper.Map<IEnumerable<Attraction>, IEnumerable<AttractionVM>>(attractions);

            return avms;
        }
    }
}
