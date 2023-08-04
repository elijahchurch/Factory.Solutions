using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.Controllers
{
    public class EngineersController: Controller
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
        public ActionResult Create(Engineer engineer )
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
    }
}