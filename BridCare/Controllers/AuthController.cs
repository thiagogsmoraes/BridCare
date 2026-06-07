using BridCare.Models;
using BridCare.Models.Enums;
using BridCare.Models.ViewModels;
using BridCare.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BridCare.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserService _userService;
        private readonly SignInManager<User> _signInManager;

        public AuthController(UserService userService, SignInManager<User> signInManager)
        {
            _userService = userService;
            _signInManager = signInManager;
        }

        // LOGIN
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                await _userService.LoginAsync(model);

                // lê a claim de role pra redirecionar
                var user = await _userService.FindByEmailAsync(model.Email);

                if (user.Role == Role.Caregiver)
                {
                    return RedirectToAction("Index", "Caregiver");
                }
                else
                {
                    return RedirectToAction("Index", "Institution");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        // LOGOUT
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }

        // REGISTER
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _userService.RegisterAsync(model);
            return RedirectToAction(nameof(Login));
        }
    }
}
