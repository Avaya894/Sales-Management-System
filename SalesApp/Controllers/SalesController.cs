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
        return View(salesTransactions); 
    } 
} 