namespace Sightseer.WebApp.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using Models.BindingModels;
    using Models.ViewModels.Users;
    using Services.Interfaces;

    public class UsersController : Controller
    {
        private IUserService service;

        public UsersController(IUserService service)
        {
            this.service = service;
        }

        [Route("profile")]
        public ActionResult UserProfile(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfileVm vm = this.service.GetUserProfile(username);

            if (vm == null)
            {
                return this.HttpNotFound();
            }

            return this.View(vm);
        }

        [HttpGet]
        [Route("profile/edit")]
        public ActionResult EditProfile(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EditUserProfiveVm vm = this.service.GetEditProfileVm(username);

            if (vm == null)
            {
                return this.HttpNotFound();
            }

            return this.View(vm);
        }

        [HttpPost]
        [Route("profile/edit")]
        public ActionResult EditProfile(EditUserBm bind, string username)
        {
            if (this.ModelState.IsValid && this.service.IsEmailUnique(bind.Email, username))
            {
                this.service.EditUser(bind, username);
                return this.RedirectToAction("UserProfile", new {username = username});
            }
            
            EditUserProfiveVm vm = this.service.GetEditProfileVm(username);
            return this.View(vm);
        }
    }
}