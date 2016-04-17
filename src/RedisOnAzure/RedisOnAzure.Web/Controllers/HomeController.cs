using System.Threading.Tasks;
using System.Web.Mvc;

using RedisOnAzure.Web.Models;

using RedisRepo.Src;


namespace RedisOnAzure.Web.Controllers
{
    public class HomeController : Controller
    {
        // This is an abstraction for interacting with a cache for a particular instance
        private readonly ICacheRepo<ApplicationUser> _appUserCache;


        public HomeController()
        {
            // The CacheRepo needs an instance of IAppCache (from the RedisRepo library) in order to instantiate
            IAppCache redisCache = new RedisCache(RedisConfiguration.GetRedisConfig());
            _appUserCache = new CacheRepo<ApplicationUser>(redisCache);
        }


        public async Task<ActionResult> Index()
        {
            // Example of having the redis cache get accessed on the first request. If the redis cache server isn't running an exception will get
            // thrown here.
            var user = await _appUserCache.FindAsync("UsernameIndex", User.Identity.Name);
            return View();
        }   


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}