using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Boutique.Data;
using Boutique.Models;
using Microsoft.AspNetCore.Identity;
using Boutique.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Boutique.Controllers
{
    public class EmployesController : Controller
    {
        private readonly BoutiqueContext _context;

        private readonly UserManager<Personne> _userManager;
        private readonly SignInManager<Personne> _signInManager;
        public EmployesController(UserManager<Personne> userManager,
                              SignInManager<Personne> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Employes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employe.ToListAsync());
        }
        public IActionResult Register()
        {
            return View();
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Personne
                {
                    
                    Email = model.Email,
                    UserName = model.Email,
                    Employe = new Employe()
                    {
                        Nom = model.Nom,
                        Prenom = model.Prenom,
                        Addresse = model.Addresse,
                        Telephone = model.Telephone,
                        Age = model.Age,
                    }
                    

                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Employe");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(user);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}
