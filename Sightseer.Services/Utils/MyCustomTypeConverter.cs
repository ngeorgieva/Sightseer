namespace Sightseer.Services.Utils
{
    using System.Collections.Generic;
    using AutoMapper;
    using Models.EntityModels;
    using Models.ViewModels.Attractions;
    using PagedList;

    public static class MyCustomTypeConverter
    {
        public static IPagedList<AttractionVm> ToMappedPagedList<TSource, TDestination>(this IPagedList<Attraction> list)
        {
            IEnumerable<AttractionVm> sourceList = Mapper.Map<IEnumerable<Attraction>, IEnumerable<AttractionVm>>(list);
            IPagedList<AttractionVm> pagedResult = new StaticPagedList<AttractionVm>(sourceList, list.GetMetaData());
            return pagedResult;
        }
    }
}