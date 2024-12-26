using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApiNetCore.Api.Data;

public partial class ApiLog
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(200)]
    public string MethodName { get; set; } = null!;

    public string? MethodParams { get; set; }

    [StringLength(100)]
    public string? RequestIp { get; set; }

    [StringLength(100)]
    public string ResponseCode { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime ExecDate { get; set; }
}