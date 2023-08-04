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
            return View(model);
        }
    }
}