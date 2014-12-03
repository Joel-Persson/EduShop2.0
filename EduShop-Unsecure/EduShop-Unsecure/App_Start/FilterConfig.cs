using System.Web.Mvc;
using EduShop_Unsecure.Models;

namespace EduShop_Unsecure
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            if (Settings.IsSecured)
            {
                filters.Add(new ValidateAntiForgeryTokenAttribute());
            }
        }
    }
}
