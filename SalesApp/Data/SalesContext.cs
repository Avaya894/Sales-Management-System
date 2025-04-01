using Microsoft.EntityFrameworkCore;
using SalesApp.Models;

namespace SalesApp.Data;

public class SalesContext : DbContext
{
    public SalesContext(DbContextOptions<SalesContext> options) : base(options) { }
    
    // Represents the Products table in the database
    public DbSet<Product> Products { get; set; }
    
    // Represents the Customers table in the database
    public DbSet<Customer> Customers { get; set; }
    
    // Represents the Invoices table in the database
    public DbSet<Invoice> Invoices { get; set; }
    
    // Represents the SalesTransactions table in the database
    public DbSet<SalesTransaction> SalesTransactions { get; set; }
}