namespace UploadFiles.Repo.Base
{
    public interface ICategoryRepo
    {
        IEnumerable<Category> GetAll();
        int Create(Category category);
    }
}
