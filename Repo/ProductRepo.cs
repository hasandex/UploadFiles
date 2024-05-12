
using Microsoft.EntityFrameworkCore;

namespace UploadFiles.Repo
{
    public class ProductRepo : IProductRepo
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductRepo(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IEnumerable<Product> GetAll()
        {
            return _appDbContext.Products.Include(p=>p.ProductImages).AsNoTracking().ToList();
        }

        public int Create(CreateProductViewModel viewModel)
        {

            List<ProductImages> productImages = new List<ProductImages>();
            foreach (var item in viewModel.FormFiles)
            {
               var image = new ProductImages { Path = SaveImgInServer(item) };
               productImages.Add(image);
            }

            Product product = new Product()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                CategoryId = viewModel.CategoryId,
                ProductImages = productImages
            };
            _appDbContext.Products.Add(product);
            return _appDbContext.SaveChanges();

        }

        public int Delete(int productId)
        {
            throw new NotImplementedException();
        }


        public Product GetById(int productId)
        {
            var product = _appDbContext.Products.Include(p => p.ProductImages)
                .AsNoTracking()
                .FirstOrDefault(p=>p.Id == productId);
            try
            {
                return product;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }
        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }

        public string SaveImgInServer(IFormFile file)
        {
            var fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";
            var path = Path.Combine($"{_webHostEnvironment.WebRootPath}{Settings.imagesPathProducts}", fileName);
            using var stream = File.Create(path);
            file.CopyToAsync(stream);
            return fileName;
        }

       
    }
}
