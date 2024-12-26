using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApiNetCore.Api.Models
{
    [SwaggerSchema("Model for new category")]
    public class NewCategoryModel
    {
        [SwaggerSchema("Category name")]
        [Required(ErrorMessage = "CategoryName is required.")]
        public string CategoryName { get; set; }

        [SwaggerSchema("Category description")]
        public string? CategoryDescrip { get; set; }
    }
}