using System.ComponentModel.DataAnnotations;

namespace WebApiNetCore.Web.Models
{
    public class NewCategoryModel
    {
        [Required(ErrorMessage = "CategoryName is required.")]
        public string CategoryName { get; set; }

        public string? CategoryDescrip { get; set; }
    }
}