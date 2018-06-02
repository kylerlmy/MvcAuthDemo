using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;     //授权
using Microsoft.AspNetCore.Authentication;    //证明
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace MvcAuth.Controllers
{

    public class AccountController : Controller
    {
        //[Authorize]  如果启用，将会一直循环跳转
        public IActionResult MakeLogin()
        {

            //模拟用户认证登录
            var claims=new List<Claim>
            {
                new Claim(ClaimTypes.Name,"Kyle"),
                new Claim(ClaimTypes.Role,"Admin"),
            };
                var claimIdentity=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);


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