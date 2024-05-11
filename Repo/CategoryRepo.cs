using Microsoft.DotNet.Scaffolding.Shared.Messaging;
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
        public Category GetById(int categoryId)
        {
            var category = _appDbContext.Categories.FirstOrDefault(c => c.Id == categoryId);
            try
            {
                if (category != null)
                {
                    return category;
                }
                else
                {
                    throw new Exception("Category not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }
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

        public void Update(Category category)
        {
            try
            {
                var old_category = _appDbContext.Categories.FirstOrDefault(c => c.Id == category.Id);

                if (old_category != null)
                {
                    if(category.ImageFile != null)
                    {
                        var oldImage = Path.Combine($"{_webHostEnvironment.WebRootPath}{Settings.imagesPath}", old_category.Image);
                        File.Delete(oldImage);
                        category.Image = SaveImgInServer(category.ImageFile);
                    }
                    else
                    {
                        category.Image = old_category.Image;
                    }
                    _appDbContext.Entry(old_category).State = EntityState.Detached;
                    _appDbContext.Categories.Update(category);
                    _appDbContext.SaveChanges();
                }
                else
                {
                    throw new Exception("Category not found."); 
                }
            }
            catch (Exception ex)
            {
       
                Console.WriteLine("An error occurred: " + ex.Message);

                throw;
            }

        }

        public int Delete(int categoryId)
        {
            var category = _appDbContext.Categories.FirstOrDefault(c => c.Id == categoryId);
            try
            {
                if (category != null)
                {
                    _appDbContext.Categories.Remove(category);
                    var oldImage = Path.Combine($"{_webHostEnvironment.WebRootPath}{Settings.imagesPath}", category.Image);
                    File.Delete(oldImage);
                    return _appDbContext.SaveChanges();
                }
                else
                {
                    throw new Exception("Category not found.");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }
            
        }

      
    }
}
