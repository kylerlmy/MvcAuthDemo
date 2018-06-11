using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;     //授权
using Microsoft.AspNetCore.Authentication;    //证明
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using MvcAuth.Models;
using MvcAuth.ViewModels;

namespace MvcAuth.Controllers
{

    public class AccountController : Controller
    {

        private UserManager<ApplicationUser> _userManager;
        private UserManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, UserManager<ApplicationUser> signInManage)
        {
            _userManager = userManager;
            _signInManager = signInManage;
        }

        /// <summary>
        /// 添加注册功能时（添加包好ViewModel的方法）时，也要保留这个方法，否则，无法请求注册页面（返回状态码404）
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            var identityUser = new ApplicationUser
            {
                Email = viewModel.Email,
                UserName = viewModel.Email,
                NormalizedUserName = viewModel.Email
            };
            var identityResult = await _userManager.CreateAsync(identityUser, viewModel.Password);

            if (identityResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }


            return View();
        }



        public IActionResult Login()
        {
            return View();
        }


        //[Authorize]  如果启用，将会一直循环跳转
        public IActionResult MakeLogin()
        {

            //模拟用户认证登录
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,"Kyle"),
                new Claim(ClaimTypes.Role,"Admin"),
            };
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);


            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimIdentity));
            return Ok();       //返回一个OK(),它会变成一个Api
        }


        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();
            return Ok();
        }
    }



}