using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApiNetCore.Api.Models
{
    [SwaggerSchema("Category model")]
    public class CategoriesModel
    {
        [SwaggerSchema("Category GuId")]
        public Guid Id { get; set; }

        [SwaggerSchema("Category name")]
        public string CategoryName { get; set; }

        [SwaggerSchema("Category description")]
        public string? CategoryDescrip { get; set; }

        [SwaggerSchema("Category status (active, inactive)")]
        public bool IsActive { get; set; }

        [SwaggerSchema("Category add date")]
        public DateTime CreateDate { get; set; }
    }
}