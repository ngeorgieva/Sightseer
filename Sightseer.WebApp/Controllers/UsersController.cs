namespace Sightseer.WebApp.Controllers
{
    using System.Web.Mvc;
    using Models.BindingModels;
    using Models.ViewModels.Users;
    using Services;
    using Services.Interfaces;

    public class UsersController : Controller
    {
        private IUserService service;

        public UsersController(IUserService service)
        {
            this.service = service;
        }

        [Route("profile")]
        public ActionResult UserProfile()
        {
            string userName = this.User.Identity.Name;
            UserProfileVm vm = this.service.GetUserProfile(userName);

            return this.View(vm);
        }

        [HttpGet]
        [Route("profile/edit")]
        public ActionResult EditProfile()
        {
            string username = this.User.Identity.Name;
            EditUserProfiveVm vm = this.service.GetEditProfileVm(username);

            return this.View(vm);
        }

        [HttpPost]
        [Route("profile/edit")]
        public ActionResult EditProfile(EditUserBm bind)
        {
            string username = this.User.Identity.Name;

            if (this.ModelState.IsValid && this.service.IsEmailUnique(bind.Email, username))
            {
                this.service.EditUser(bind, username);
                return this.RedirectToAction("UserProfile");
            }
            
            EditUserProfiveVm vm = this.service.GetEditProfileVm(username);
            return this.View(vm);
        }
    }
}