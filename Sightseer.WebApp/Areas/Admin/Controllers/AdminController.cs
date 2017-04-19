namespace Sightseer.WebApp.Areas.Admin.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using Models.ViewModels.Admin;
    using Services;

    [Authorize(Roles = "Admin")]
    [RouteArea("admin")]
    public class AdminController : Controller
    {
        private AdminService service;

        public AdminController()
        {
            this.service = new AdminService();
        }

        [HttpGet]
        [Route]
        public ActionResult Index()
        {
            AdminPageVm vm = this.service.GetAdminPageVm();
            return this.View(vm);
        }

        // GET: Attractions/Delete/5
        [HttpGet]
        [Route("attraction/delete/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vm = this.service.GetAttractionDetailsVm((int)id);

            if (vm == null)
            {
                return this.HttpNotFound();
            }

            return this.View(vm);
        }

        // POST: Attractions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("attraction/delete/{id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            this.service.DeleteAttraction(id);
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        [Route("user/delete")]
        public ActionResult DeleteUser(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vm = this.service.GetUserDetailsVm(username);

            if (vm == null)
            {
                return this.HttpNotFound();
            }

            return this.View(vm);
        }

        [HttpPost, ActionName("DeleteUser")]
        [Route("user/delete")]
        public ActionResult DeleteUserConfirmed(string username)
        {
            this.service.DeleteUser(username);
            return this.RedirectToAction("Index");
        }
    }
}