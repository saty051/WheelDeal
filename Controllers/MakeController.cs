using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WheelDeal.AppDBContext;
using WheelDeal.Models;
using WheelDeal.Helpers;

namespace WheelDeal.Controllers
{
    [Authorize(Roles = $"{Roles.Admin},{Roles.Executive}")]
    public class MakeController : Controller
    {
        private readonly WheelDealDbContext _dbContext;

        public MakeController(WheelDealDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View(_dbContext.Makes.ToList());
        }

        //HTTP Get Method
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Make make)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Add(make);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(make);
        }

        // Post Method
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var make = _dbContext.Makes.Find(id);
            if (make == null)
            {
                return NotFound();
            }

            _dbContext.Remove(make);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var make = _dbContext.Makes.Find(id);
            if (make == null)
            {
                return NotFound();
            }

            return View(make);
        }

        [HttpPost]
        public IActionResult Edit(Make make)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Update(make);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(make);
        }
    }
}
