namespace Sightseer.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Web;
    using Models.BindingModels;
    using Models.EntityModels;
    using Models.ViewModels.Attractions;

    public interface IAttractionsService
    {
        AttractionDetailsVm GetAttractionDetailsVm(int id);
        Attraction GetAttraction(int id);
        IEnumerable<AttractionVm> GetAllAttractions(int? page, string searchString);
        void CreateAttraction(CreateAttractionBm bind, HttpPostedFileBase file);
        EditAttractionVm GetEditAttractionVm(int id);
        void EditAttraction(EditAttractionBm bind, HttpPostedFileBase file);
        Attraction GetAttractionByName(string name);
        void DeleteAttraction(Attraction attraction);
        void Dispose();
    }
}