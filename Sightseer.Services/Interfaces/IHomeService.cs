namespace Sightseer.Services.Interfaces
{
    using System.Collections.Generic;
    using Models.ViewModels.Attractions;

    public interface IHomeService
    {
        IEnumerable<AttractionVm> GetTopAttractions();
    }
}
