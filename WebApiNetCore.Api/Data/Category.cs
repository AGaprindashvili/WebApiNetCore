using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApiNetCore.Api.Data;

public partial class Category
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(100)]
    public string CategoryName { get; set; } = null!;

    [StringLength(1000)]
    public string? CategoryDescrip { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}