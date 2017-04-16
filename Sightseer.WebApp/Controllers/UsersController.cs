namespace Sightseer.WebApp.Controllers
{
    using System.Web.Mvc;
    using Models.BindingModels;
    using Models.ViewModels.Users;
    using Services;

    public class UsersController : Controller
    {
        private UserService service;

        public UsersController()
        {
            this.service = new UserService();
        }

        [Route("profile")]
        public ActionResult UserProfile()
        {
            string userName = this.User.Identity.Name;
            UserProfileVm vm = this.service.GetUserProfile(userName);

            return this.View(vm);
        }

        [HttpGet]
        [Route("editprofile")]
        public ActionResult EditProfile()
        {
            string username = this.User.Identity.Name;
            EditUserProfiveVm vm = this.service.GetEditProfileVm(username);

            return this.View(vm);
        }

        [HttpPost]
        [Route("editprofile")]
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