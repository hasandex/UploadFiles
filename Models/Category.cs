using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UploadFiles.Attributes;

namespace UploadFiles.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string? Image {  get; set; }
        [NotMapped]
        [AllowedExtensions(Settings.allowedExtensions)]
        [MaxFileSize(Settings.maxFileSizeImg)]
        public IFormFile ImageFile { get; set; }
    }
}
