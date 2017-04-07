using RedisWithAspNet4_6.Web.Models;

using System;
using System.Web.Mvc;

using RedisWithAspNet4_6.Web.App_Core.Commands;
using RedisWithAspNet4_6.Web.App_Core.ReadServices;
using System.Threading.Tasks;
using RedisWithAspNet4_6.Web.App_Core.RedisServices;
using RedisWithAspNet4_6.Web.App_Core;

namespace RedisWithAspNet4_6.Web.Controllers
{
    /*
     * This is a basic example class. There are obviously some missing "best-practices" when it comes to error handling and validation. Please understand
     * that this is only intended to provide a sample pattern of using Redis and not an example of AspNet MVC best practice patterns.
     */
    
    public class MinionsController : Controller
    {
        private readonly IMinionsReadService _minionsReadSvc;
        private readonly IMinionCommands _minionCommands;
        private readonly IAppCache _appCache;

        public MinionsController()
        {
            _appCache = new RedisAppCache(AppConsts.RedisConfig);
            _minionsReadSvc = new MinionsReadService(_appCache);
            _minionCommands = new MinionCommands(_minionsReadSvc, _appCache);
        }

        // GET: Minions
        public async Task<ActionResult> Index()
        {
            var allMinions = await _minionsReadSvc.GetAllMinionsAsync();
            return View("MinionsList", allMinions);
        }

        // GET: Minions/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var minion = await _minionsReadSvc.GetMinionAsync(id);
            return View("MinionDetails", minion);
        }

        // GET: Minions/Create
        public ActionResult Create()
        {
            return View("CreateMinion");
        }

        // POST: Minions/Create
        [HttpPost]
        public async Task<ActionResult> Create(Minion minion)
        {
            try
            {
                minion.Id = Guid.NewGuid();
                var result = await _minionCommands.CreateMinionAsync(minion);
                if (result.IsFailure)
                {
                    return View("CreateMinion");
                }
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateMinion");
            }
        }

        // GET: Minions/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var minion = await _minionsReadSvc.GetMinionAsync(id);
            return View("EditMinion", minion);
        }

        // POST: Minions/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, Minion minion)
        {
            try
            {
                var result = await _minionCommands.UpdateMinionAsync(minion);
                if (result.IsFailure)
                {
                    return View("EditMinion");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View("EditMinion");
            }
        }

        // GET: Minions/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Minions/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id, FormCollection collection)
        {
            try
            {
                await _minionCommands.DeleteMinionAsync(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
