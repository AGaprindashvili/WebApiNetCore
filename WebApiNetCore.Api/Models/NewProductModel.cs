using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApiNetCore.Api.Models
{
    [SwaggerSchema("Model for new product")]
    public class NewProductModel
    {
        [SwaggerSchema("Product name")]
        [Required(ErrorMessage = "ProductName is required.")]
        public string ProductName { get; set; }

        [SwaggerSchema("Product description")]
        public string? ProductDescrip { get; set; }

        [SwaggerSchema("Product specifications")]
        public string? Specifications { get; set; }

        [SwaggerSchema("Product category GuId")]
        [Required(ErrorMessage = "CategoryId is required.")]
        public Guid CategoryId { get; set; }

        [SwaggerSchema("Product price")]
        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [SwaggerSchema("Discounted amount")]
        public decimal? Discount { get; set; }

        [SwaggerSchema("Product quantity")]
        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }
    }
}