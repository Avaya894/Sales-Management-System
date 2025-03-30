namespace SalesApp.Models;

public class InvoiceDetailsViewModel
{
    public required int InvoiceId { get; set; }
    public required string CustomerName { get; set; }
    public required DateTime InvoiceDate { get; set; }
    public required string InvoiceNumber { get; set; }
    public required decimal InvoiceTotal { get; set; }
    public required List<SalesTransaction> SalesTransactions { get; set; }
}