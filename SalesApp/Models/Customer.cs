using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesApp.Models;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [Required]
    [StringLength(100)]
    public required string CustomerName { get; set; }

    [StringLength(200)]
    public string? ContactInfo { get; set; }

    // Navigation properties
    public ICollection<SalesTransaction>? SalesTransactions { get; set; }
    public ICollection<Invoice>? Invoices { get; set; }
}
