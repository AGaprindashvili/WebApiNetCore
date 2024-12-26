using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApiNetCore.Api.Models
{
    [SwaggerSchema("Model for product change")]
    public class ChangeProductModel
    {
        [SwaggerSchema("Product Id")]
        public Guid ProductId { get; set; }

        [SwaggerSchema("Product info")]
        public NewProductModel ProductInfo { get; set; }
    }
}