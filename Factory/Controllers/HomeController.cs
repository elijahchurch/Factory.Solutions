using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.Controllers
{
    public class HomeController : Controller
    {
        private readonly FactoryContext _db;

        public HomeController(FactoryContext db)
        {
            _db = db;
        }

        [HttpGet("/")]
        public ActionResult Index()
        {
            List<Engineer> engineers = _db.Engineers.ToList();
            List<Machine> machines = _db.Machines.ToList();
            ViewBag.Engineers = engineers;
            ViewBag.Machines = machines;
            ViewBag.Title = "Dr. Sillystringz's Factory Database";
            return View();
        }
    }
}