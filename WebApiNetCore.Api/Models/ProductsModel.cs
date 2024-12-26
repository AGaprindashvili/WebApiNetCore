using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApiNetCore.Api.Models
{
    [SwaggerSchema("Product model")]
    public class ProductsModel
    {
        [SwaggerSchema("Product GuId")]
        public Guid Id { get; set; }

        [SwaggerSchema("Product name")]
        public string ProductName { get; set; }

        [SwaggerSchema("Product description")]
        public string? ProductDescrip { get; set; }

        [SwaggerSchema("Product specifications")]
        public string? Specifications { get; set; }

        [SwaggerSchema("Product category GuId")]
        public Guid CategoryId { get; set; }

        [SwaggerSchema("Product category name")]
        public string CategoryName { get; set; }

        [SwaggerSchema("Product category description")]
        public string? CategoryDescrip { get; set; }

        [SwaggerSchema("Product price")]
        public decimal Price { get; set; }

        [SwaggerSchema("Discounted amount")]
        public decimal? Discount { get; set; }

        [SwaggerSchema("Product quantity")]
        public int Quantity { get; set; }

        [SwaggerSchema("Product status (active, inactive)")]
        public bool IsActive { get; set; }

        [SwaggerSchema("Product add date")]
        public DateTime CreateDate { get; set; }
    }
}