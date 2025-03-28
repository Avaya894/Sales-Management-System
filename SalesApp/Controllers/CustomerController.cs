using Microsoft.AspNetCore.Mvc;
using SalesApp.Data;
using SalesApp.Models;

namespace SalesApp.Controllers;

public class ProductController : Controller
{
    // GET
    private readonly SalesContext _context;
    
    public ProductController(SalesContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([Bind("ProductName, Rate")]Product product)
    {
        if (ModelState.IsValid)
        {
            Console.Write("Create product pressed");
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Product created successfully!";
        }
        return RedirectToAction("Index", "Sales");
    }
    
}