using Microsoft.AspNetCore.Mvc;
using WheelDeal.AppDBContext;
using WheelDeal.Models;

namespace WheelDeal.Controllers
{
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
    }
}
