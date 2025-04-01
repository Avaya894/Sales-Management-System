using SalesApp.Models;

public class SalesViewModel
{
    public required List<SalesTransaction> SalesTransactions { get; set; }
    public required List<Customer> Customers { get; set; }
    public required List<Product> Products { get; set; }
    public required List<Invoice> Invoices { get; set; }
    
    public required List<Customer> GenerateInvoices { get; set; }

}