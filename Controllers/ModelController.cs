using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WheelDeal.AppDBContext;
using WheelDeal.Models;
using WheelDeal.Models.ViewModels;

namespace WheelDeal.Controllers
{
    [Authorize(Roles = "Admin,Executive")]
    public class ModelController : Controller
    {
        private readonly WheelDealDbContext _dbContext;

        [BindProperty]
        public ModelViewModel ModelVM { get; set; }
        public ModelController(WheelDealDbContext dbContext)
        {
            _dbContext = dbContext;
            ModelVM = new ModelViewModel()
            {
                Makes = _dbContext.Makes.ToList(),
                Model = new Models.Model()
            };
        }
        public IActionResult Index()
        {
            var model = _dbContext.Models.Include(m => m.Make);
            return View(model);
        }
        
        public IActionResult Create()
        {
            return View(ModelVM);
        }

        [HttpPost, ActionName("Create")]
        public IActionResult CreatePost()
        {
            if (ModelState.IsValid)
            {
                return View(ModelVM);
            }
            _dbContext.Models.Add(ModelVM.Model);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ModelVM.Model = _dbContext.Models.Include(m => m.Make).SingleOrDefault(m => m.Id == id);
            if(ModelVM.Model == null)
            {
                return NotFound();
            }
            // Populate the Makes list

            ModelVM.Makes = _dbContext.Makes.ToList();
            return View(ModelVM);
        }

        [HttpPost, ActionName("Edit")]
        public IActionResult EditPost()
        {
            if (ModelState.IsValid)
            {
                // Re-populate the Makes list for the dropdown
                ModelVM.Makes = _dbContext.Makes.ToList();
                return View(ModelVM);
            }

            // Update the model
            _dbContext.Update(ModelVM.Model);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Model model = _dbContext.Models.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            _dbContext.Models.Remove(model);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
