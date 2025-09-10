using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ST10028058_PROG7312_POE.Models;
using System.ComponentModel.DataAnnotations;

namespace ST10028058_PROG7312_POE.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signIn;
        private readonly UserManager<ApplicationUser> _users;
        private readonly RoleManager<IdentityRole> _roles;

        public AccountController(
            SignInManager<ApplicationUser> signIn,
            UserManager<ApplicationUser> users,
            RoleManager<IdentityRole> roles)
        {
            _signIn = signIn;
            _users = users;
            _roles = roles;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null) => View(new LoginVm { ReturnUrl = returnUrl });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var result = await _signIn.PasswordSignInAsync(vm.Email, vm.Password, vm.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
                return !string.IsNullOrWhiteSpace(vm.ReturnUrl) ? Redirect(vm.ReturnUrl) : RedirectToAction("Index", "Home");

            ModelState.AddModelError("", "Invalid login.");
            return View(vm);
        }

        [HttpGet]
        public IActionResult Register() => View(new RegisterVm());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = new ApplicationUser
            {
                UserName = vm.Email,
                Email = vm.Email,
                EmailConfirmed = true,
                FullName = vm.FullName
            };

            var result = await _users.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var e in result.Errors) ModelState.AddModelError("", e.Description);
                return View(vm);
            }

            // Auto-assign Citizen role on self-registration
            if (!await _roles.RoleExistsAsync("Citizen"))
                await _roles.CreateAsync(new IdentityRole("Citizen"));
            await _users.AddToRoleAsync(user, "Citizen");

            await _signIn.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signIn.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied() => View();
    }

    public class LoginVm
    {
        [Required, EmailAddress] public string Email { get; set; } = "";
        [Required, DataType(DataType.Password)] public string Password { get; set; } = "";
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }

    public class RegisterVm
    {
        [Required, StringLength(100)] public string FullName { get; set; } = "";
        [Required, EmailAddress] public string Email { get; set; } = "";
        [Required, DataType(DataType.Password)] public string Password { get; set; } = "";
        [Required, DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = "";
    }
}

//# Assistance provided by ChatGPT
//# Code and support generated with the help of OpenAI's ChatGPT.
// code attribution
// W3schools
//https://www.w3schools.com/cs/index.php

// code attribution
//Bootswatch
//https://bootswatch.com/

// code attribution
// https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-8.0&tabs=visual-studio

// code attribution
// https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-8.0&tabs=visual-studio
