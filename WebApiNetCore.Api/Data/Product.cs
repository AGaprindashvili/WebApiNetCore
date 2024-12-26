using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApiNetCore.Api.Data;

public partial class Product
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(100)]
    public string ProductName { get; set; } = null!;

    [StringLength(1000)]
    public string? ProductDescrip { get; set; }

    public string? Specifications { get; set; }

    public Guid CategoryId { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    [Column(TypeName = "money")]
    public decimal? Discount { get; set; }

    public int Quantity { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category Category { get; set; } = null!;
}