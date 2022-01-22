using CoreIdentity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace CoreIdentity.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }   
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM, string? returnUrl)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, loginVM.Name),
                new Claim(ClaimTypes.Email,"Kurdi@gmail.com"),
                new Claim("Db","IT"),
                new Claim("Engineer","true"),
                new Claim("EmploymentDate","2021-05-01")
            };

            IIdentity identity = new ClaimsIdentity(claims,"MyCookieAuth");

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("MyCookieAuth", principal);

            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Home");
        }
    }
}
