using EntityLayer.Concretes;
using EntityLayer.ViewModels;
using KuaforSite.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KuaforSite.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthenticationController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginVM _loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var hasUser = await _userManager.FindByEmailAsync(_loginVM.Email);
            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Email ya da şifre yanlış!");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(hasUser.UserName!, _loginVM.Password, isPersistent: true, true);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, $"Kalan deneme hakkınız: {await _userManager.GetAccessFailedCountAsync(hasUser)}/4");
            }

            if (result.IsLockedOut)
            {
                var lockoutEndDate = await _userManager.GetLockoutEndDateAsync(hasUser);
                var remainingTime = lockoutEndDate.Value - DateTimeOffset.Now;
                var minutes = (int)remainingTime.TotalMinutes;
                var seconds = remainingTime.Seconds;
                ModelState.AddModelErrorList(new List<string>() { $"Hesabınız kilitlendi, {minutes} dakika {seconds} saniye sonra tekrar deneyiniz!" });
                return View();
            }
            ModelState.AddModelErrorList(new List<string>() { "Email ya da şifre yanlış!" });
            return View();
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterVM _registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var identityResult = await _userManager.CreateAsync(new()
            {
                UserName = _registerVM.Email,
                Email = _registerVM.Email,
                NameSurname = _registerVM.NameSurname,
                PhoneNumber = _registerVM.PhoneNumber
            }, _registerVM.ConfirmPassword);

            if (identityResult.Succeeded)
            {
                TempData["SuccessRegister"] = "Kayıt işleminiz başarılı, giriş yapınız.";
                return RedirectToAction(nameof(SignIn), "Authentication");
            }

            ModelState.AddModelErrorList(identityResult.Errors.Select(x => x.Description).ToList());
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(AuthenticationController.SignIn), "Authentication");
        }
    }
}
