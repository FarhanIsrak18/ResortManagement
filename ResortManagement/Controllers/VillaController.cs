using Microsoft.AspNetCore.Mvc;
using ResortManagement.Domain.Entities;
using ResortManagement.Infrastructure.Data;

namespace ResortManagement.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public VillaController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var villas = _db.Villas.ToList();
            return View(villas);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Villa obj) 
        {
            if (obj.Name == obj.Description) 
            {
                ModelState.AddModelError("name","Description and Name can not be same");
            }
            if (ModelState.IsValid)
            {
                _db.Villas.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index", "Villa");
            }
            return View();
        }

        public IActionResult Update(int villaId) 
        {
            Villa? obj = _db.Villas.FirstOrDefault(x => x.Id == villaId);
            //Villa? obj = _db.Villas.Find(villaId);
            //var VillaList = _db.Villas.Where(x => x.Price < 50 && x.Occupancy > 0);
            if (obj == null) 
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(Villa obj)
        {
            if (ModelState.IsValid && obj.Id > 0)
            {
                _db.Villas.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index", "Villa");
            }
            return View();
        }

    }
}
