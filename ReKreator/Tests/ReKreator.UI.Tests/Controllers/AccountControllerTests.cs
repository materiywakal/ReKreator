using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using ReKreator.Domain;
using ReKreator.UI.MVC.Controllers;
using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using ReKreator.Emailing;
using ReKreator.UI.MVC.Models.Account;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using Microsoft.AspNetCore.Mvc.Routing;
using ReKreator.BL.Services;

namespace ReKreator.UI.MVC.Tests
{
    public class FakeUserManager : UserManager<User>
    {
        public FakeUserManager()
            : base(new Mock<IUserStore<User>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<User>>().Object,
                new IUserValidator<User>[0],
                new IPasswordValidator<User>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<User>>>().Object)
        { }
    }

    public class FakeSignInManager : SignInManager<User>
    {
        public FakeSignInManager()
            : base(new Mock<FakeUserManager>().Object,
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<User>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object)
        { }
    }



    [TestFixture(Category = "Unit tests")]
    public class AccountControllerTests
    {
        private Mock<FakeUserManager> _userManger;
        private Mock<FakeSignInManager> _signInManager;
        private Mock<UserService> _userService;
        private Mock<IMapper> _mapper;
        private Mock<ISender> _sender;
        private AccountController _controller;

        private readonly AccountLogInViewModel _accountLogInViewModel = new AccountLogInViewModel
        {
            Login = "Login",
            Password = "Password",
            RememberMe = true
        };

        private readonly AccountRegisterViewModel _accountRegisterViewModel = new AccountRegisterViewModel
        {
            UserName = "UserName",
            Email = "Email",
            Password = "Password",
            PasswordConfirmation = "PasswordConfirmation",
            FirstName = "FirstName",
            LastName = "LastName"
        };

        private readonly User _user = new User
        {
            Id = 1,
            UserName = "Username",
            Email = "Email"
        };

        [SetUp]
        public void Setup()
        {
            _userManger = new Mock<FakeUserManager>();
            _signInManager = new Mock<FakeSignInManager>();
            _userService = new Mock<UserService>();
            _mapper = new Mock<IMapper>();
            _sender = new Mock<ISender>();

            _controller = new AccountController(_userManger.Object, _signInManager.Object, _userService.Object, _mapper.Object, _sender.Object);
        }

        [Test]
        public void Constructor_When_ArgumentsAreNull_Should_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new AccountController(null, _signInManager.Object, _userService.Object, _mapper.Object, _sender.Object));
            Assert.Throws<ArgumentNullException>(() => new AccountController(_userManger.Object, null, _userService.Object, _mapper.Object, _sender.Object));
            Assert.Throws<ArgumentNullException>(() => new AccountController(_userManger.Object, _signInManager.Object, null, _mapper.Object, _sender.Object));
            Assert.Throws<ArgumentNullException>(() => new AccountController(_userManger.Object, _signInManager.Object, _userService.Object, null, _sender.Object));
            Assert.Throws<ArgumentNullException>(() => new AccountController(_userManger.Object, _signInManager.Object, _userService.Object, _mapper.Object, null));
        }

        [Test]
        public void HttpGet_Register_CheckReturnValue()
        {
            var result = _controller.Register();

            Assert.IsInstanceOf<PartialViewResult>(result);
            Assert.That(result.ViewName, Is.EqualTo("Register"));
        }

        [Test]
        public void HttpPost_Register_When_ModelStateIsNotValid_Should_ReturnPartialViewWithModel()
        {
            _controller.ModelState.AddModelError("error", "error");

            var result = _controller.Register(_accountRegisterViewModel).Result;

            Assert.IsInstanceOf<PartialViewResult>(result);
            Assert.That(((PartialViewResult)result).Model, Is.EqualTo(_accountRegisterViewModel));
            Assert.That(((PartialViewResult)result).ViewName.ToString(), Is.EqualTo("Register"));
        }

        [Test]
        public void HttpPost_Register_When_ModelStateIsValidButUserNameIsExists_Should_ReturnPartialViewWithModel()
        {
            _userManger.Setup(m => m.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(_user);

            var result = _controller.Register(_accountRegisterViewModel).Result;
            
            _userManger.Verify(m => m.FindByNameAsync(_accountRegisterViewModel.UserName));
            Assert.IsInstanceOf<PartialViewResult>(result);
            Assert.That(((PartialViewResult)result).Model, Is.EqualTo(_accountRegisterViewModel));
            Assert.That(((PartialViewResult)result).ViewName.ToString(), Is.EqualTo("Register"));
        }

        [Test]
        public void HttpPost_Register_When_ModelStateIsValidButMailIsExists_Should_ReturnPartialViewWithModel()
        {
            _userManger.Setup(m => m.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(() => null);
            _userManger.Setup(m => m.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(_user);

            var result = _controller.Register(_accountRegisterViewModel).Result;

            _userManger.Verify(m => m.FindByNameAsync(_accountRegisterViewModel.UserName));
            Assert.IsInstanceOf<PartialViewResult>(result);
            Assert.That(((PartialViewResult)result).Model, Is.EqualTo(_accountRegisterViewModel));
            Assert.That(((PartialViewResult)result).ViewName.ToString(), Is.EqualTo("Register"));
        }

        [Test]
        public void HttpPost_Register_When_ModelStateIsValidAndAllGood_Should_CreateAsyncAndGenerateEmailConfirmationTokenAsyncAndMapAndMessageToUserAsyncMethodsUserWithProperParameters()
        {
            //TODO
            _userManger.Setup(m => m.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(() => null);
            _userManger.Setup(m => m.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(() => null);
            _userManger.Setup(m => m.GenerateEmailConfirmationTokenAsync(It.IsAny<User>())).ReturnsAsync(It.IsAny<string>());
            _sender.Setup(s => s.MessageToUserAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();
            _mapper.Setup(m => m.Map<User>(It.IsAny<AccountRegisterViewModel>())).Returns(_user);
            _userManger.Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            var urlHelper = new Mock<UrlHelper>(new Mock<ActionContext>().Object);
            urlHelper.Setup(h => h.Action(It.IsAny<UrlActionContext>())).Returns(It.IsAny<string>());
            _controller.Url = urlHelper.Object;

            var result = _controller.Register(_accountRegisterViewModel).Result;

            _mapper.Verify(m => m.Map<User>(_accountRegisterViewModel));
            _userManger.Verify(m => m.CreateAsync(_user));
            _userManger.Verify(m => m.GenerateEmailConfirmationTokenAsync(_user));
            _sender.Verify(s => s.MessageToUserAsync(_user, It.IsAny<string>(), It.IsAny<string>()));
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            Assert.That(((RedirectToActionResult)result).ActionName, Is.EqualTo("LogIn"));
        }

        [Test]
        public void ConfirmEmail_CheckReturnValue()
        {
            _userManger.Setup(m => m.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(_user);
            _userManger.Setup(m => m.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            var result = _controller.ConfirmEmail("username", "token").Result;

            Assert.IsInstanceOf<RedirectToActionResult>(result);
            Assert.That(((RedirectToActionResult)result).ActionName, Is.EqualTo("Index"));
            Assert.That(((RedirectToActionResult)result).ControllerName, Is.EqualTo("Home"));
        }

        [Test]
        [TestCase("Username", "Token")]
        public void ConfirmEmail_AreFindByNameAsyncAndConfirmEmailAsyncMethodsUsedWithProperParameters(string username, string token)
        {
            _userManger.Setup(m => m.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(_user);
            _userManger.Setup(m => m.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            var result = _controller.ConfirmEmail(username, token).Result;

            _userManger.Verify(m => m.FindByNameAsync(username));
            _userManger.Verify(m => m.ConfirmEmailAsync(_user, token));
        }

        [Test]
        public void HttpGet_LogIn_CheckReturnValue()
        {
            var result = _controller.LogIn(string.Empty);

            Assert.IsInstanceOf<PartialViewResult>(result);
            Assert.That(result.ViewName, Is.EqualTo("Login"));
        }

        [Test]
        public void HttpPost_LogIn_When_ModelStateIsNotValid_Should_ReturnPartialViewWith401StatusCode()
        {
            //TODO
            _controller.ModelState.AddModelError("error", "error");
            
            var result = _controller.LogIn(_accountLogInViewModel).Result;

            Assert.IsInstanceOf<PartialViewResult>(result);
            Assert.AreEqual(((PartialViewResult)result).StatusCode, HttpStatusCode.Unauthorized);
            Assert.That(((PartialViewResult)result).ViewName.ToString(), Is.EqualTo("Login"));
        }


        [Test]
        public void HttpPost_LogIn_When_ModelStateIsValid_Should_PasswordSignInAsyncMethodUsedWithProperParameters()
        {
            _signInManager.Setup(m => m.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(SignInResult.Success);

            var result = _controller.LogIn(_accountLogInViewModel).Result;

            _signInManager.Verify(m => m.PasswordSignInAsync(_accountLogInViewModel.Login, _accountLogInViewModel.Password, _accountLogInViewModel.RememberMe, It.IsAny<bool>()));
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void LogOut_IsSignOutAsyncMethodUsed()
        {
            var result = _controller.LogOut().Result;

            _signInManager.Verify(s => s.SignOutAsync());
        }

        [Test]
        public void LogOut_CheckReturnValue()
        {
            var result = _controller.LogOut().Result;

            Assert.IsInstanceOf<RedirectToActionResult>(result);
            //Assert.That(result.ViewName, Is.EqualTo("_UserLogInRegisterLinksPartial"));
        }

        [Test]
        public void GetUserDDLProfile_CheckReturnValue()
        {
            var result = _controller.GetUserDDLProfile();

            Assert.IsInstanceOf<PartialViewResult>(result);
            Assert.That(result.ViewName, Is.EqualTo("_UserProfileDDLPartial"));
        }
    }
}