using System.ComponentModel.DataAnnotations;

namespace WebApiNetCore.Web.Models
{
    public class NewProductModel
    {
        [Required(ErrorMessage = "ProductName is required.")]
        public string ProductName { get; set; }

        public string? ProductDescrip { get; set; }

        public string? Specifications { get; set; }

        [Required(ErrorMessage = "CategoryId is required.")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        public decimal? Discount { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }
    }
}