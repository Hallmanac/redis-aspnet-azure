using RedisWithAspNet4_6.Web.Models;

using System;
using System.Web.Mvc;

using RedisWithAspNet4_6.Web.App_Core.Commands;
using RedisWithAspNet4_6.Web.App_Core.ReadServices;


namespace RedisWithAspNet4_6.Web.Controllers
{
    public class MinionsController : Controller
    {
        private readonly IMinionsReadService _minionsReadSvc;
        private readonly IMinionCommands _minionCommands;

        public MinionsController()
        {
            _minionsReadSvc = new MinionsReadService();
            _minionCommands = new MinionCommands(_minionsReadSvc);
        }

        // GET: Minions
        public ActionResult Index()
        {
            var allMinions = _minionsReadSvc.GetAllMinions();
            return View("MinionsList", allMinions);
        }

        // GET: Minions/Details/5
        public ActionResult Details(Guid id)
        {
            var minion = _minionsReadSvc.GetMinion(id);
            return View("MinionDetails", minion);
        }

        // GET: Minions/Create
        public ActionResult Create()
        {
            return View("CreateMinion");
        }

        // POST: Minions/Create
        [HttpPost]
        public ActionResult Create(Minion minion)
        {
            try
            {
                var result = _minionCommands.CreateMinion(minion);
                if (result.IsFailure)
                {
                    //TODO: Add error messages
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
        public ActionResult Edit(Guid id)
        {
            var minion = _minionsReadSvc.GetMinion(id);
            return View("EditMinion", minion);
        }

        // POST: Minions/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Minion minion)
        {
            try
            {
                // TODO: Add update logic here
                var result = _minionCommands.UpdateMinion(minion);
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
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
