using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _appDbContext.Categories.AsNoTracking().ToListAsync();
        }
        public Category GetById(int categoryId)
        {
            var category = _appDbContext.Categories.FirstOrDefault(c => c.Id == categoryId);
            try
            { 
                    return category;
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

        public int Update(Category category)
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
                    return _appDbContext.SaveChanges();
                }
                else //category is null ==>>> no category found
                {
                return 0; 
                }
        }

        public int Delete(int id)
        {
            var category = _appDbContext.Categories.FirstOrDefault(c => c.Id == id);
            
                if (category != null)
                {
                    _appDbContext.Categories.Remove(category);
                    var oldImage = Path.Combine($"{_webHostEnvironment.WebRootPath}{Settings.imagesPath}", category.Image);
                    File.Delete(oldImage);
                    return _appDbContext.SaveChanges();
                }
                else
                {
                return 0;
                }
        }

        public IEnumerable<SelectListItem> GetCategoriesList()
        {
          return  _appDbContext.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text= c.Name})
                .OrderBy(c=>c.Text).AsNoTracking();
        }

        public string SaveImgInServer(IFormFile file)
        {
            var fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";
            var path = Path.Combine($"{_webHostEnvironment.WebRootPath}{Settings.imagesPath}", fileName);
            using var stream = File.Create(path);
            file.CopyToAsync(stream);
            return fileName;
        }
    }
}
