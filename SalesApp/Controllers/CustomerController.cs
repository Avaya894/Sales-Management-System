using Microsoft.AspNetCore.Mvc;
using SalesApp.Data;
using SalesApp.Models;

namespace SalesApp.Controllers;

public class CustomerController : Controller
{
    // GET
    private readonly SalesContext _context;
    
    public CustomerController(SalesContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return Content("Index");
    }
    
    // Creates new customer (handles post request)
    [HttpPost]
    public async Task<IActionResult> Create([Bind("CustomerName, ContactInfo")] Customer customer)
    {
        if (ModelState.IsValid)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index", "Sales");
    }
    
}