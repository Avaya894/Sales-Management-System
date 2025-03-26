using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesApp.Models;

public class Invoice
{
    [Key]
    public int InvoiceId { get; set; }

    [Required]
    [ForeignKey("Customer")]
    public int CustomerId { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime InvoiceDate { get; set; }

    [Required]
    [StringLength(50)]
    public required string InvoiceNumber { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal InvoiceTotal { get; set; }

    // Navigation properties
    public Customer? Customer { get; set; }
    public ICollection<SalesTransaction>? SalesTransactions { get; set; }
}
