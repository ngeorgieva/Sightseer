namespace Sightseer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Models.EntityModels;
    using Models.ViewModels.Attractions;

    public class AttractionsService : Service
    {
        public AttractionDetailsVm GetDetails(int id)
        {
            Attraction attraction = this.Context.Attractions.Find(id);

            if (attraction == null)
            {
                return null;
            }

            AttractionDetailsVm avm = Mapper.Map<Attraction, AttractionDetailsVm>(attraction);
            return avm;
        }

        public Attraction GetAttractionImage(int id)
        {
            var attr = this.Context.Attractions.Find(id);

            return attr;
        }

        public IEnumerable<AttractionVm> GetAllAttractions()
        {
            IEnumerable<Attraction> attractions = this.Context.Attractions.OrderByDescending(a => a.Rating);
            IEnumerable<AttractionVm> avms = Mapper.Map<IEnumerable<Attraction>, IEnumerable<AttractionVm>>(attractions);

            return avms;
        }
    }
}
