namespace Sightseer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Interfaces;
    using Models.EntityModels;
    using Models.ViewModels.Attractions;

    public class HomeService : Service, IHomeService
    {
        public IEnumerable<AttractionVm> GetTopAttractions()
        {
            IEnumerable<Attraction> attractions = this.Context.Attractions.OrderByDescending(a => a.Rating).Take(3);
            IEnumerable<AttractionVm> avms = Mapper.Map<IEnumerable<Attraction>, IEnumerable<AttractionVm>>(attractions);

            return avms;
        }
    }
}
