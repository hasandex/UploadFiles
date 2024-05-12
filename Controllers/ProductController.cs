using Microsoft.AspNetCore.Mvc;
using UploadFiles.Models;

namespace UploadFiles.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepo _productrepo;
        private readonly ICategoryRepo _categoryRepo;

        public ProductController(IProductRepo productrepo, ICategoryRepo categoryRepo)
        {
            _productrepo = productrepo;
            _categoryRepo = categoryRepo;
        }

        public IActionResult Index()
        {
            return View(_productrepo.GetAll());
        }
        [HttpGet]
        public IActionResult Create()
        {
            CreateProductViewModel model = new CreateProductViewModel()
            {
                SelectCategoriesList = _categoryRepo.GetCategoriesList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(CreateProductViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                viewModel.SelectCategoriesList = _categoryRepo.GetCategoriesList();
                return View(viewModel);
            }
            _productrepo.Create(viewModel);
            return RedirectToAction("Index");
        }
        public IActionResult ProductImages(int id)
        {
            var product = _productrepo.GetById(id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
