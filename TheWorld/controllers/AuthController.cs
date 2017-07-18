using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.models;
using TheWorld.viewModels;

namespace TheWorld.controllers
{
    public class AuthController:Controller
    {
        private SignInManager<WorldUser> _signInManager;
        public AuthController(SignInManager<WorldUser> _signInManager)
        {
            this._signInManager = _signInManager;
        }
        public IActionResult login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("trips", "app");
            }
            return View();
        }
        [HttpPost]
        public ActionResult login(LoginViewModel vm,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = _signInManager.PasswordSignInAsync(vm.username, vm.password, true, false);
                if (signInResult.Result.Succeeded)
                {
                    ViewBag.message = null;
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("trips", "app");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username or password inorrect");
                    ViewBag.message = "Username or password inorrect";
                }
            }
            return View();
        }

        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "app");
        }
    }
}
