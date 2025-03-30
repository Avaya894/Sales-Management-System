using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesApp.Models;

public class SalesTransaction
{
    [Key]
    public int SalesTransactionId { get; set; }

    [Required]
    [ForeignKey("Product")]
    public int ProductId { get; set; }

    [Required]
    [ForeignKey("Customer")]
    public int CustomerId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal Rate { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal Total { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime? EditedDate { get; set; }
    
    // Foreign key to Invoice (nullable)
    [ForeignKey("Invoice")]
    public int? InvoiceId { get; set; }

    // Navigation properties
    public Product? Product { get; set; }
    public Customer? Customer { get; set; }
    public Invoice? Invoice { get; set; }
}
