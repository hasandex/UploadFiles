using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
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
            FormProductViewModel model = new FormProductViewModel()
            {
                SelectCategoriesList = _categoryRepo.GetCategoriesList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(FormProductViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                viewModel.SelectCategoriesList = _categoryRepo.GetCategoriesList();
                return View(viewModel);
            }
            _productrepo.Create(viewModel);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int productId)
        {
            var product = _productrepo.GetById(productId);
            if(product != null)
            {
                var productViewModel = new FormProductViewModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    CategoryId = product.CategoryId,
                    Description = product.Description,
                    Images = product.ProductImages.Select(image => image.Path).ToList(),
                    SelectCategoriesList = _categoryRepo.GetCategoriesList(),
            };
                return View(productViewModel);
            }
            return NotFound();
        }

        public IActionResult Delete(int id)
        {
            var result = _productrepo.Delete(id);
            if(result == 0)
            {
                return NotFound();
            }
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
        public IActionResult DeleteImage(int productId)
        {
            var product = _productrepo.GetById(productId);
            if (product != null)
            {
                var result = _productrepo.DeleteImage(productId);
                if (result > 0)
                    return RedirectToAction("Update", new { productId });
            }
            return NotFound();
        }
    }
}
