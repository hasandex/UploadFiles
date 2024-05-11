using Microsoft.AspNetCore.Mvc;
using UploadFiles.Repo.Base;

namespace UploadFiles.Controllers
{
    public class CategoryController : Controller
    {
        public CategoryController(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        private readonly ICategoryRepo _categoryRepo;
        public IActionResult Index()
        {
            return View(_categoryRepo.GetAll());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _categoryRepo.Create(category);
            return RedirectToAction("Index");
        }
        
    }
}
