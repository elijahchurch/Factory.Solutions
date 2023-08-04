using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.Controllers
{
    public class EngineersController : Controller
    {
        private readonly FactoryContext _db;
        public EngineersController(FactoryContext db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            List<Engineer> model = _db.Engineers.ToList();
            ViewBag.Title = "Manage Engineers";
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Add Engineer";
            return View();
        }

        [HttpPost]
        public ActionResult Create(Engineer engineer)
        {
            if (!ModelState.IsValid)
            {
                return View(engineer);
            }
            else
            {
                _db.Engineers.Add(engineer);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Details(int id)
        {
            Engineer model = _db.Engineers
                        .Include(Engineers => Engineers.JoinEntities)
                        .ThenInclude(entry => entry.Machine)
                        .FirstOrDefault(entry => entry.EngineerId == id);
            ViewBag.Title = "Engineer Details";
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            Engineer model = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
            ViewBag.Title = "Edit Engineer Information";
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Engineer engineer)
        {
            if (!ModelState.IsValid)
            {
                return View(engineer);
            }
            else
            {
                _db.Engineers.Update(engineer);
                _db.SaveChanges();
                return RedirectToAction("Details", new { id = engineer.EngineerId });
            }
        }

        public ActionResult Delete(int id)
        {
            Engineer model = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
            ViewBag.Title = "Engineer Delete Confirmation";
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Engineer deleteTarget = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
            _db.Engineers.Remove(deleteTarget);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddMachine(int id)
        {
            Engineer model = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
            ViewBag.Machines = _db.Machines.ToList();
            ViewBag.Title = "Add Machine Authorization";
            ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
            return View(model);
        }

        [HttpPost]
        public ActionResult AddMachine(Engineer Engineer, int MachineId)
        {
            #nullable enable
            EngineerMachine? joinEntity = _db.EngineerMachines.FirstOrDefault(join => (join.EngineerId == Engineer.EngineerId && join.MachineId == MachineId));
            #nullable disable
            if (joinEntity == null && MachineId != 0)
            {
                _db.EngineerMachines.Add(new EngineerMachine() { MachineId = MachineId, EngineerId = Engineer.EngineerId });
                _db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = Engineer.EngineerId });
        }

        [HttpPost]
        public ActionResult DeleteJoin(int joinId)
        {
        EngineerMachine joinEntry = _db.EngineerMachines.FirstOrDefault(entry => entry.EngineerMachineId == joinId);
        int engineerId = joinEntry.EngineerId;
        _db.EngineerMachines.Remove(joinEntry);
        _db.SaveChanges();
        return RedirectToAction("Details", new { id = engineerId });
        }

    }
}