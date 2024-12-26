using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApiNetCore.Api.Models
{
    [SwaggerSchema("Model for category change")]
    public class ChangeCategoryModel
    {
        [SwaggerSchema("Category Id")]
        public Guid CategoryId { get; set; }

        [SwaggerSchema("Category info")]
        public NewCategoryModel CategoryInfo { get; set; }
    }
}