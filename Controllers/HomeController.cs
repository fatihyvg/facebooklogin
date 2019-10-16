using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using facebook.Models;
namespace facebook.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
             var fbtype = HttpContext.User as ClaimsPrincipal;

            if(fbtype == null) { throw null; };
            
            List<FacebookLogin>  facebookLogin = new List<FacebookLogin>();
            FacebookLogin log = new FacebookLogin();
            log.UserFbId = fbtype.FindFirstValue(ClaimTypes.NameIdentifier);
            log.UserName = fbtype.FindFirstValue(ClaimTypes.Name);
            log.UserEmail = fbtype.FindFirstValue(ClaimTypes.Email);
            facebookLogin.Add(log);
            return View(facebookLogin);
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl,string provider)
        {
            AuthenticationProperties authprop = new AuthenticationProperties();
            authprop.RedirectUri = Url.Action("Index","Home");
            return Challenge(authprop,provider);
        }

    }
}