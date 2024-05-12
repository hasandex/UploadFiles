using System.ComponentModel.DataAnnotations.Schema;
using UploadFiles.Attributes;

namespace UploadFiles.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [ForeignKey("Category")]
        public int CategoryId {  get; set; }
        public Category Category { get; set; }
        public ICollection<ProductImages> ProductImages { get; set; }
    }
}
