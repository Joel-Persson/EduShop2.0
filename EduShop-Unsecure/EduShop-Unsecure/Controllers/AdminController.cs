using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EduShop_Unsecure.Models;

namespace EduShop_Unsecure.Controllers
{
    public class AdminController : Controller
    {
        [Authorize]
        public ActionResult UserListing()
        {
            var ticket = GetAuthCookieValue();
            var user = UserModel.GetUser(ticket.Name);

            if (Settings.IsSecured)
            {
                if (user.IsAdmin)
                {
                    return View(UserModel.GetAllUsers());
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(UserModel.GetAllUsers());

            }

        }
        private FormsAuthenticationTicket GetAuthCookieValue()
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            return ticket;
        }
    }
}