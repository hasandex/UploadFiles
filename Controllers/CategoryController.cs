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

        [HttpGet]
        public IActionResult Update(int categoryId)
        {
            var category = _categoryRepo.GetById(categoryId);
            return View(category);
        }
        [HttpPost]
        public IActionResult Update(Category category)
        {
            var cate = _categoryRepo.GetById(category.Id);
            if(category.ImageFile == null)
            {
                ModelState.Remove("ImageFile");
            }
            
            if (!ModelState.IsValid)
            {
                return View(cate);
            }
            if (category.ImageFile != null)
            {
                _categoryRepo.Update(category);
            }
            else
            {
                _categoryRepo.Update(category);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            _categoryRepo.Delete(id);
            return RedirectToAction("Index");
        }
        

    }
}
