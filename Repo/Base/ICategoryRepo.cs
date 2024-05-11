namespace UploadFiles.Repo.Base
{
    public interface ICategoryRepo
    {
        IEnumerable<Category> GetAll();
        Category GetById(int categoryId);
        int Create(Category category);
        void Update(Category category);
        int Delete(int categoryId);
       
    }
}
