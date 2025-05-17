using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LanchesMac.Controllers
{
    public class ContaController : Controller
    {
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string returnUrl)
        {
            // Exemplo básico de login fixo (substitua pela validação real de usuário)
            if (username == "admin" && password == "123")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "Cookies");

                await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity));

                return Redirect(returnUrl ?? "/");
            }

            ViewBag.Error = "Usuário ou senha inválidos.";
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AcessoNegado()
        {
            return View();
        }
    }
}
