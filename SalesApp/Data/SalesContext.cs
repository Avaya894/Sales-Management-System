using Microsoft.EntityFrameworkCore;
using SalesApp.Models;

namespace SalesApp.Data;

public class SalesContext : DbContext
{
    public SalesContext(DbContextOptions<SalesContext> options) : base(options) { }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<SalesTransaction> SalesTransactions { get; set; }
}