using Microsoft.EntityFrameworkCore;
using UploadFiles.Data;
using UploadFiles.Repo.Base;

namespace UploadFiles.Repo
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CategoryRepo(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IEnumerable<Category> GetAll()
        {
            return _appDbContext.Categories.AsNoTracking().ToList();
        }
        public int Create(Category category)
        {
            category.Image = SaveImgInServer(category.ImageFile);
            _appDbContext.Categories.Add(category);
            return _appDbContext.SaveChanges();
        }

        public  string SaveImgInServer(IFormFile file)
        {
            var fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";
            var path = Path.Combine($"{_webHostEnvironment.WebRootPath}{Settings.imagesPath}", fileName);
            using var stream = File.Create(path);
            file.CopyToAsync(stream);
            return fileName ;
        }
        //"C:\\Users\\alika\\source\\repos\\GameZone\\wwwroot/assets/images\\8905b78b-01b7-4c8f-90e5-335d81783d02.jpg"
        //"C:\\Users\\alika\\source\\repos\\UploadFiles\\wwwroot/assets/images\\b3fd6689-dcf3-43f8-abd2-df170e417c1b.png"
    }
}
