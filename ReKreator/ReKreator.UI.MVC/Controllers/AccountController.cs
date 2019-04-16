using System;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReKreator.Domain;
using System.Threading.Tasks;
using ReKreator.UI.MVC.Models.Account;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ReKreator.BL.Interfaces;
using ReKreator.Emailing;

namespace ReKreator.UI.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ISender _sender;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IUserService userService, IMapper mapper, ISender sender)
        {

            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
        }

        [HttpGet]
        [Authorize]
        public async Task<ViewResult> Profile()
        {
            var user = await _userService.GetAsync(u => u.UserName == User.Identity.Name, u => u.UserMailing);
            var model = _mapper.Map<AccountProfileViewModel>(user);
            model.Roles = await _userManager.GetRolesAsync(user);
            return View("Profile", model);
        }

        [HttpGet]
        [Authorize]
        public async Task<ViewResult> Edit()
        {
            var user = await _userService.GetAsync(u => u.UserName == User.Identity.Name, u => u.UserMailing);
            return View("Edit", _mapper.Map<AccountEditViewModel>(user));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(AccountEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && user.UserName != User.Identity.Name)
            {
                ModelState.AddModelError("Email", $"User with email '{model.Email}' already exist");
                return View("Edit", model);
            }
            user = await _userService.GetAsync(u => u.UserName == User.Identity.Name, u => u.UserMailing);
            if (user != null)
            {
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.UserMailing.MailingPeriod = model.NoveltyMailingPeriod;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Profile");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View("Edit", model);
        }

        [HttpGet]
        [Authorize]
        public ViewResult EditPassword()
        {
            return View("EditPassword");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditPassword(AccountPasswordEditVIewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("EditPassword", model);
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    return RedirectToAction("Profile");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View("EditPassword", model);
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult Register()
        {
            return PartialView("Register");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] AccountRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Register", model);
            }

            if (await _userManager.FindByNameAsync(model.UserName) != null)
            {
                ModelState.AddModelError("UserName", $"User with username '{model.UserName}' already exists");
            }
            else if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                ModelState.AddModelError("Email", $"User with email '{model.Email}' already exists");
            }
            else
            {
                var user = _mapper.Map<User>(model);
                user.UserMailing = new UserMailing();
                user.RegistrationDate = DateTime.Now;
                user.UserMailing = new UserMailing();
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new {Username = user.UserName, Token = token},
                        protocol: HttpContext.Request.Scheme);
                    await _sender.MessageToUserAsync(user, "Account confirmation",
                        $"<span>Please, confirm your account. Follow this link: </span><a href='{callbackUrl}'>link</a>");

                    return RedirectToAction("LogIn",
                        new
                        {
                            message =
                                "To complete the registration, check the email and click on the link indicated in the letter."
                        });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return PartialView("Register", model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string username, string token)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                ViewData["EmailConfirmationResult"] = "Email was not confirmed :(";
                return View("EmailConfirmationResult");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
                ViewData["EmailConfirmationResult"] = "Email was not confirmed :(";
            else
                ViewData["EmailConfirmationResult"] = "Email confirmed!";

            return View("EmailConfirmationResult");
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult LogIn(string message)
        {
            if (!string.IsNullOrEmpty(message))
                ModelState.AddModelError(string.Empty, message);
            return PartialView("Login");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn([FromBody]AccountLogInViewModel model)
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            if (!ModelState.IsValid)
            {
                return PartialView("Login");
            }
            var user = await _userManager.FindByNameAsync(model.Login);
            if (user != null && !await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError(string.Empty, "Email not confirmed. Please, check your email");
                return PartialView("Login");
            }
            var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Incorrect login or password");
                return PartialView("Login");
            }

            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<RedirectToActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public PartialViewResult GetUserDDLProfile()
        {
            return PartialView("_UserProfileDDLPartial");
        }
    }
}