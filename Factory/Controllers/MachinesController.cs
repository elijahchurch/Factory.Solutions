using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.Controllers
{
    public class MachinesController: Controller
    {
        private readonly FactoryContext _db;
        public MachinesController(FactoryContext db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            List<Machine> model = _db.Machines.ToList();
            ViewBag.Title = "Manage Machines";
            return View(model);
        }
        public ActionResult Create()
        {
            ViewBag.Title = "Add Machine";
            return View();
        }

        [HttpPost]
        public ActionResult Create(Machine machine)
        {
            if (!ModelState.IsValid)
            {
                return View(machine);
            }
            else
            {
                _db.Machines.Add(machine);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Details(int id)
        {
            Machine model = _db.Machines
                        .Include(machine => machine.JoinEntities)
                        .ThenInclude(entry => entry.Engineer)
                        .FirstOrDefault(entry => entry.MachineId == id);
                        ViewBag.Title = "Machine Details";
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            Machine model = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
            ViewBag.Title = "Edit Machine Information";
            return View (model);
        }

        [HttpPost]
        public ActionResult Edit(Machine machine)
        {
            if (!ModelState.IsValid)
            {
                return View(machine);
            }
            else
            {
            _db.Machines.Update(machine);
            _db.SaveChanges();
            return RedirectToAction("Details", new {id = machine.MachineId});
            }
        }

        public ActionResult Delete(int id)
        {
            Machine model = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
            ViewBag.Title = "Machine Delete Confirmation";
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
        Machine deleteTarget = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
        _db.Machines.Remove(deleteTarget);
        _db.SaveChanges();
        return RedirectToAction("Index");
        }

        public ActionResult AddEngineer(int id)
        {
            Machine model = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
            ViewBag.Engineers = _db.Engineers.ToList();
            ViewBag.Title = "Add Engineer Authorization";
            ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
            return View(model);
        }

        [HttpPost]
        public ActionResult AddEngineer(Machine machine, int EngineerId)
        {
            #nullable enable
            EngineerMachine? joinEntity = _db.EngineerMachines.FirstOrDefault(join => (join.MachineId == machine.MachineId && join.EngineerId == EngineerId));
            #nullable disable
            if (joinEntity == null && EngineerId != 0)
            {
                _db.EngineerMachines.Add(new EngineerMachine() { EngineerId = EngineerId, MachineId = machine.MachineId });
                _db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = machine.MachineId });
        }

    }
}