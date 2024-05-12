using Microsoft.AspNetCore.Mvc.Rendering;

namespace UploadFiles.Repo.Base
{
    public interface ICategoryRepo
    {
        IEnumerable<Category> GetAll();
        Category GetById(int categoryId);
        int Create(Category category);
        int Update(Category category);
        int Delete(int categoryId);
        IEnumerable<SelectListItem> GetCategoriesList();


    }
}
