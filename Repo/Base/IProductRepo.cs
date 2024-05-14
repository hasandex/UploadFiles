namespace UploadFiles.Repo.Base
{
    public interface IProductRepo
    {
        IEnumerable<Product> GetAll();
        Product GetById(int productId);
        int Create(FormProductViewModel CreateProductViewModel);
        void Update(Product product);
        int Delete(int productId);
        int DeleteImage(int productId);
    }
}
