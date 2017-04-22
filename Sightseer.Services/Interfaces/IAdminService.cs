namespace Sightseer.Services.Interfaces
{
    using Models.ViewModels.Admin;
    using Models.ViewModels.Attractions;

    public interface IAdminService
    {
        AdminPageVm GetAdminPageVm();
        AttractionDetailsVm GetAttractionDetailsVm(int id);
        void DeleteAttraction(int id);
        void DeleteUser(string username);
        AdminPageUserVm GetUserDetailsVm(string username);
    }
}
