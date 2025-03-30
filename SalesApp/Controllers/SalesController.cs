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

    public async Task<IActionResult> Index()
    { 
        var salesTransactions = await _context.SalesTransactions
                .Include(s => s.Product)
                .Include(c=>c.Customer)
                .ToListAsync();
        
        var customers = await _context.Customers.ToListAsync();
        var products = await _context.Products.ToListAsync();
        var invoices = await _context.Invoices.ToListAsync();

        var viewModel = new SalesViewModel
        {
            SalesTransactions = salesTransactions,
            Customers = customers,
            Products = products,
            Invoices = invoices
        };
        
        return View(viewModel); 
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([Bind("ProductId, CustomerId, Quantity, Rate, Total")]SalesTransaction salesTransaction)
    {
        if (ModelState.IsValid)
        {
            Console.WriteLine($"ProductId: {salesTransaction.ProductId}, CustomerId: {salesTransaction.CustomerId}, Quantity: {salesTransaction.Quantity}, Rate: {salesTransaction.Rate}, Total: {salesTransaction.Total}");
            Console.Write("Create saleTransaction pressed");
            _context.SalesTransactions.Add(salesTransaction);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Sales created successfully!";
        }
        return RedirectToAction("Index", "Sales");
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit([Bind("SalesTransactionId, ProductId, CustomerId, Quantity, Rate, Total")]SalesTransaction salesTransaction)
    {
        if (ModelState.IsValid)
        {
            Console.WriteLine($"ProductId: {salesTransaction.ProductId}, CustomerId: {salesTransaction.CustomerId}, Quantity: {salesTransaction.Quantity}, Rate: {salesTransaction.Rate}, Total: {salesTransaction.Total}");
            Console.Write("update saleTransaction pressed");
            
            var existingTransaction = await _context.SalesTransactions.FindAsync(salesTransaction.SalesTransactionId);
            
            if (existingTransaction == null)
            {
                TempData["Error"] = "Sales transaction not found!";
                return RedirectToAction("Index", "Sales");
            }
            
            Console.WriteLine($"Existing salesTransaction: {existingTransaction.ProductId}");
            
            existingTransaction.ProductId = salesTransaction.ProductId;
            existingTransaction.CustomerId = salesTransaction.CustomerId;
            existingTransaction.Quantity = salesTransaction.Quantity;
            existingTransaction.Rate = salesTransaction.Rate;
            existingTransaction.Total = salesTransaction.Total;

            
            // _context.SalesTransactions.Update(salesTransaction);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Sales Updated successfully!";
        }
        return RedirectToAction("Index", "Sales");
    }
    
    
} 