using System.Threading.Tasks;
using System.Web.Mvc;

using RedisOnAzure.Web.App_Cache;

using RedisRepo.Src;


namespace RedisOnAzure.Web.Controllers
{
    public class HomeController : Controller
    {
        // The UserCache is a local abstraction for interacting with the redis cache for an ApplicationUser type
        private readonly UserCache _appUserCache;


        public HomeController()
        {
            // The CacheRepo needs an instance of IAppCache (from the RedisRepo library) in order to instantiate
            IAppCache redisCache = new RedisCache(RedisConfiguration.GetRedisConfig());
            _appUserCache = new UserCache(redisCache);
        }


        public async Task<ActionResult> Index()
        {
            // Example of having the redis cache get accessed on the first request. If the redis cache server isn't running an exception will get
            // thrown here.
            var user = await _appUserCache.FindAsync(_appUserCache.UsernameIndex, User.Identity.Name);
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