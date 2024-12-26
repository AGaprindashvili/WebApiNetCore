using System.ComponentModel.DataAnnotations;

namespace WebApiNetCore.Web.Models
{
    public class ProductsModel
    {
        public Guid Id { get; set; }

        public string ProductName { get; set; }

        public string? ProductDescrip { get; set; }

        public string? Specifications { get; set; }

        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string? CategoryDescrip { get; set; }

        public decimal Price { get; set; }

        public decimal? Discount { get; set; }

        public int Quantity { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }
    }
}