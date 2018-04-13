using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Masato.Controllers
{
    // [Route("[Controller]")] Want these on the root / base
    public class AuthController : Controller
    {
        [HttpGet("login")]
        public async Task Login()
        {
            await HttpContext.ChallengeAsync("Anilist", new AuthenticationProperties {RedirectUri = "/"});
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
