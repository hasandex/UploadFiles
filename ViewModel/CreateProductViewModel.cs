using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UploadFiles.ViewModel
{
    public class CreateProductViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [MaxFileSize(Settings.maxFileSizeImg)]
        [AllowedExtensions(Settings.allowedExtensions)]
        public List<IFormFile> FormFiles { get; set; }
        public int CategoryId { get; set; }
        [DisplayName("Categories")]

        public IEnumerable<SelectListItem> SelectCategoriesList { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
