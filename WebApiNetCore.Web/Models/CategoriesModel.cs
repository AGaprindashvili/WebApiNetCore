using System.ComponentModel.DataAnnotations;

namespace WebApiNetCore.Web.Models
{
    public class CategoriesModel
    {
        public Guid Id { get; set; }

        public string CategoryName { get; set; }

        public string? CategoryDescrip { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }
    }
}