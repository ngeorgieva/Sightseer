namespace Sightseer.Test.Controllers
{
    using System.Net;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.BindingModels;
    using Models.ViewModels.Users;
    using Moq;
    using Sightseer.Services.Interfaces;
    using TestStack.FluentMVCTesting;
    using WebApp.Controllers;

    [TestClass]
    public class TestUsersController
    {
        [TestMethod]
        public void UserProfile_ShouldReturnCorrectView()
        {
            var serviceMock = new Mock<IUserService>();
            UserProfileVm vm = new UserProfileVm()
            {
                Email = "pesho@gamil.com"
            };
            serviceMock.Setup(s => s.GetUserProfile("pesho")).Returns(vm);
            var controller = new UsersController(serviceMock.Object);

            controller.WithCallTo(usersController => usersController.UserProfile("pesho"))
                .ShouldRenderView("UserProfile")
                .WithModel<UserProfileVm>();
        }

        [TestMethod]
        public void UserProfile_UsernameNull_ShouldReturnBadRequest()
        {
            var serviceMock = new Mock<IUserService>();
            var controller = new UsersController(serviceMock.Object);

            controller.WithCallTo(usersController => usersController.UserProfile(null))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void UserProfile_ViewModelNull_ShouldReturnHttpNotFound()
        {
            var serviceMock = new Mock<IUserService>();
            var controller = new UsersController(serviceMock.Object);
            UserProfileVm vm = null;
            serviceMock.Setup(s => s.GetUserProfile("pesho")).Returns(vm);

            controller.WithCallTo(usersController => usersController.UserProfile("pesho"))
                .ShouldGiveHttpStatus(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void EditProfile_ShouldReturnCorrectView()
        {
            var serviceMock = new Mock<IUserService>();
            EditUserProfiveVm vm = new EditUserProfiveVm()
            {
                Email = "pesho@gamil.com"
            };
            serviceMock.Setup(s => s.GetEditProfileVm("pesho")).Returns(vm);
            var controller = new UsersController(serviceMock.Object);

            controller.WithCallTo(usersController => usersController.EditProfile("pesho"))
                .ShouldRenderView("EditProfile")
                .WithModel<EditUserProfiveVm>();
        }

        [TestMethod]
        public void EditProfile_UsernameNull_ShouldReturnBadRequest()
        {
            var serviceMock = new Mock<IUserService>();
            var controller = new UsersController(serviceMock.Object);

            controller.WithCallTo(usersController => usersController.EditProfile(""))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void EditProfile_ViewModelNull_ShouldReturnHttpNotFound()
        {
            var serviceMock = new Mock<IUserService>();
            var controller = new UsersController(serviceMock.Object);
            EditUserProfiveVm vm = null;
            serviceMock.Setup(s => s.GetEditProfileVm("pesho")).Returns(vm);

            controller.WithCallTo(usersController => usersController.UserProfile("pesho"))
                .ShouldGiveHttpStatus(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void EditUserProfile_AllOk_ShouldRedirectToEditedProfile()
        {
            var serviceMock = new Mock<IUserService>();
            var controller = new UsersController(serviceMock.Object);
            EditUserBm bm = new EditUserBm()
            {
                LastName = "Jones",
                Email = "eva@gmail.com"
            };

            serviceMock.Setup(s => s.IsEmailUnique("eva@gmail.com", "eva@gmail.com")).Returns(true);
            serviceMock.Setup(s => s.EditUser(bm, "eva@gmail.com"));
            controller.WithCallTo(usersController => usersController.EditProfile(bm, "eva@gmail.com"))
                .ShouldRedirectToRoute("");
        }

        [TestMethod]
        public void EditUserProfile_AllNotOk_ShouldReturnEditViewModel()
        {
            var serviceMock = new Mock<IUserService>();
            var controller = new UsersController(serviceMock.Object);
            EditUserBm bm = new EditUserBm()
            {
                LastName = "Jones",
                Email = "eva@gmail.com"
            };

            serviceMock.Setup(s => s.IsEmailUnique("eva@gmail.com", "eva@gmail.com")).Returns(false);
            serviceMock.Setup(s => s.EditUser(bm, "eva@gmail.com"));
            controller.WithCallTo(usersController => usersController.EditProfile(bm, "eva@gmail.com"))
                .ShouldRenderView("EditProfile");
        }
    }
}
