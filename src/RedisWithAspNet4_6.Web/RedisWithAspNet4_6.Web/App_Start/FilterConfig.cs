using System.Web;
using System.Web.Mvc;

namespace RedisWithAspNet4_6.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
