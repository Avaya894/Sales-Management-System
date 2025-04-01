using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SalesApp.Data;
using SalesApp.Models;

namespace SalesApp.Controllers;

public class SalesController : Controller
{
    private readonly SalesContext _context;

    public SalesController(SalesContext context)
    {
        _context = context;
    }

    // Index (Displays the list of sales transactions, customers, products, and invoices)
    public async Task<IActionResult> Index()
    { 
        // Fetch sales transactions with related Product, Customer, and Invoice details.
        var salesTransactions = await _context.SalesTransactions
                .Include(s => s.Product)
                .Include(c=>c.Customer)
                .Include(i => i.Invoice)
                .OrderByDescending(s => s.CreatedDate)
                .ToListAsync();
        
        // Fetch all customers
        var customers = await _context.Customers.ToListAsync();
        
        // Fetch all products
        var products = await _context.Products.ToListAsync();
        
        // Fetch invoices, including related customer details, sorted by invoice date (descending)
        var invoices = await _context.Invoices
            .Include(s=>s.Customer)
            .OrderByDescending(s => s.InvoiceDate)
            .ToListAsync();
        
        // Get today's date (UTC)
        var today = DateTime.UtcNow.Date;
        
        // Fetch customers who have transactions today but haven't been invoiced yet
        var generateInvoices = await _context.Customers
            .Where(c => c.SalesTransactions != null && 
                        c.SalesTransactions!.Any(st => 
                            st.CreatedDate.Date == today && 
                            st.InvoiceId == null))
            .Include(c => c.SalesTransactions
                .Where(st => st.CreatedDate.Date == today && st.InvoiceId == null))
            .ToListAsync();
        
        // ViewModel to pass data to the view
        var viewModel = new SalesViewModel
        {
            SalesTransactions = salesTransactions,
            Customers = customers,
            Products = products,
            Invoices = invoices,
            GenerateInvoices = generateInvoices
        };
        
        return View(viewModel); 
    }
    
    
    // Handles the creation of a new sales transaction.
    [HttpPost]
    public async Task<IActionResult> Create([Bind("ProductId, CustomerId, Quantity, Rate, Total")]SalesTransaction salesTransaction)
    {
        if (ModelState.IsValid)
        {
            // Add the new sales transaction to the database
            _context.SalesTransactions.Add(salesTransaction);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Sales created successfully!";
        }
        return RedirectToAction("Index", "Sales");
    }
    
    
    // Handles editing an existing sales transaction
    [HttpPost]
    public async Task<IActionResult> Edit([Bind("SalesTransactionId, ProductId, CustomerId, Quantity, Rate, Total")]SalesTransaction salesTransaction)
    {
        if (ModelState.IsValid)
        {
            // Retrieve the existing sales transaction from the database
            var existingTransaction = await _context.SalesTransactions.FindAsync(salesTransaction.SalesTransactionId);
            
            
            if (existingTransaction == null)
            {
                TempData["Error"] = "Sales transaction not found!";
                return RedirectToAction("Index", "Sales");
            }
            
            // Update the existing transaction's details
            existingTransaction.ProductId = salesTransaction.ProductId;
            existingTransaction.CustomerId = salesTransaction.CustomerId;
            existingTransaction.Quantity = salesTransaction.Quantity;
            existingTransaction.Rate = salesTransaction.Rate;
            existingTransaction.Total = salesTransaction.Total;
            existingTransaction.EditedDate = DateTime.UtcNow;
            
            
            // Save changes to the database
            await _context.SaveChangesAsync();
            TempData["Message"] = "Sales Updated successfully!";
        }
        return RedirectToAction("Index", "Sales");
    }
} 