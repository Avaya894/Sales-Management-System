using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        // var products = await  _context.Products.ToListAsync(); 
        // var salesTransactions = await  _context.SalesTransactions.ToListAsync(); 
        // return View(products); 
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

    public IActionResult CreateProduct()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([Bind ("productName", "productRate")] Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
} 