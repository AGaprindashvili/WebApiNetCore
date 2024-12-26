using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApiNetCore.Api.Models
{
    [SwaggerSchema("Model for product chategory change")]
    public class ChangeProductCategoryModel
    {
        [SwaggerSchema("Product Id")]
        [Required(ErrorMessage = "ProductId is required.")]
        public Guid ProductId { get; set; }

        [SwaggerSchema("New category Id")]
        [Required(ErrorMessage = "NewCategoryId is required.")]
        public Guid NewCategoryId { get; set; }
    }
}