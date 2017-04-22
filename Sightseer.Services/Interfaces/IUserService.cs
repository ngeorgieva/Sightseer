namespace Sightseer.Services.Interfaces
{
    using Models.BindingModels;
    using Models.ViewModels.Users;

    public interface IUserService
    {
        UserProfileVm GetUserProfile(string userName);
        EditUserProfiveVm GetEditProfileVm(string username);
        void EditUser(EditUserBm bind, string username);
        bool IsEmailUnique(string email, string username);
    }
}