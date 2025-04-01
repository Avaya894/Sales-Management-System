using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using SalesApp.Data;

namespace SalesApp.Controllers;

public class InvoiceController : Controller
{
    private readonly SalesContext _context;

    public InvoiceController(SalesContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return Content("Invoice Page");
    }
    
    // Display invoice details  
    public async Task<IActionResult> InvoiceDetails(int id)
    {
        // Fetch the invoice along with customer details
        var invoice = await _context.Invoices
            .Include(i => i.Customer)
            .Where(i => i.InvoiceId == id)
            .FirstOrDefaultAsync();

        if (invoice == null)
        {
            // Return a not found result if the invoice does not exist
            return NotFound();
        }

        // Fetch sales transactions for this invoice and the customer on the same date
        var invoiceDate = invoice.InvoiceDate;
        var salesTransactions = await _context.SalesTransactions
            .Include(st => st.Product)
            .Where(st => st.InvoiceId == id)
            .ToListAsync();

        // Prepare the data for the view
        var model = new InvoiceDetailsViewModel
        {
            InvoiceId = invoice.InvoiceId,
            CustomerName = invoice.Customer?.CustomerName ?? "Null",
            InvoiceDate = invoice.InvoiceDate,
            InvoiceNumber = invoice.InvoiceNumber,
            InvoiceTotal = invoice.InvoiceTotal,
            SalesTransactions = salesTransactions
        };

        return View(model);
    }
    
    public async Task<bool> CreateInvoiceForCustomer(int id)
    {
        var today = DateTime.UtcNow.Date;

        // Calculate the total sales 
        var totalSales = await _context.SalesTransactions
            .Where(st => st.CustomerId == id && st.CreatedDate.Date == today && st.Invoice == null)
            .SumAsync(st => st.Total);

        if (totalSales == 0)
        {
            // No sales found for this customer today, so no invoice needed
            return false; 
        }

        // Generate a unique invoice number
        var invoiceNumber = $"INV-{id}-{DateTime.UtcNow:yyyyMMddHHmm}";

        // Create Invoice
        var invoice = new Invoice
        {
            CustomerId = id,
            InvoiceDate = today,
            InvoiceNumber = invoiceNumber,
            InvoiceTotal = totalSales
        };

        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync(); 

        // Invoice successfully created
        return true; 
    }

    
    // Create invoices for all customers 
    public async Task<bool> CreateInvoiceForAllCustomer()
    {
        var today = DateTime.UtcNow.Date;

        // Get distinct CustomerIds who made sales today
        var customerIds = await _context.SalesTransactions
            .Where(st => st.CreatedDate.Date == today && st.Invoice == null)
            .Select(st => st.CustomerId)
            .Distinct()
            .ToListAsync();
            
            
        foreach (var customerId in customerIds)
        {
            // Generate a unique invoice number (Customize logic as needed)
            var invoiceNumber = $"INV-{customerId}-{DateTime.UtcNow:yyyyMMddHHmm}";

            var invoice = new Invoice
            {
                CustomerId = customerId,
                InvoiceDate = today,
                InvoiceNumber = invoiceNumber,
                InvoiceTotal = await _context.SalesTransactions
                    .Where(st => st.CustomerId == customerId && st.CreatedDate.Date == today)
                    .SumAsync(st => st.Total)
            };

            _context.Invoices.Add(invoice);
        }

        await _context.SaveChangesAsync(); // Save all invoices
        // return RedirectToAction("Index", "Sales");
        return true;
    }
    

    // Tag invoices to all the sales transactions 
    public async Task<bool> UpdateInvoiceForAllCustomer()
    {
        var today = DateTime.UtcNow.Date;

        // Fetch all invoices created today
        var invoices = await _context.Invoices
            .Where(i => i.InvoiceDate == today)
            .ToListAsync();

        foreach (var invoice in invoices)
        {
            // Get all sales transactions for this customer that don't have an invoiceId
            var salesTransactions = await _context.SalesTransactions
                .Where(st => st.CustomerId == invoice.CustomerId && st.CreatedDate.Date == today && st.InvoiceId == null)
                .ToListAsync();

            foreach (var transaction in salesTransactions)
            {
                transaction.InvoiceId = invoice.InvoiceId;
            }
        }

        await _context.SaveChangesAsync(); // Save updated sales transactions
        // return Ok("Sales transactions updated with invoices successfully.");
        return true;
    }
    
    
    // Create Invoice and Tag invoice to SalesTransaction
    public async Task<IActionResult> GenerateAndTagEachInvoices(int id)
    {
        // Create invoices
        var invoiceCreationSuccess = await CreateInvoiceForCustomer(id);
        if (!invoiceCreationSuccess)
        {
            return RedirectToAction("Error", "Home"); 
        }

        // Update Sales Transactions
        var invoiceUpdateSuccess = await UpdateInvoiceForAllCustomer();
        if (!invoiceUpdateSuccess)
        {
            // Handle failure if needed
            return RedirectToAction("Error", "Home"); 
        }
        
        // Redirect to Sale/Index
        return RedirectToAction("Index", "Sales");
    }
    
    public async Task<IActionResult> GenerateAndTagAllInvoices()
    {
        // Create invoices
        var invoiceCreationSuccess = await CreateInvoiceForAllCustomer();
        if (!invoiceCreationSuccess)
        {
            // Handle failure if needed 
            return RedirectToAction("Error", "Home"); 
        }

        // Update Sales Transactions
        var invoiceUpdateSuccess = await UpdateInvoiceForAllCustomer();
        if (!invoiceUpdateSuccess)
        {
            // Handle failure if needed
            return RedirectToAction("Error", "Home"); 
        }

        // Redirect to Sale/Index
        return RedirectToAction("Index", "Sales");
    }

}
