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

    // Action to create invoices for customers who made sales today
    public async Task<bool> CreateInvoice()
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
            var invoiceNumber = $"INV-{customerId}-{DateTime.UtcNow.Ticks}";

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
    

    // Action to tag invoices to sales transactions
    public async Task<bool> UpdateInvoice()
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
    
    public async Task<IActionResult> GenerateAndTagInvoices()
    {
        // Step 1: Create invoices
        var invoiceCreationSuccess = await CreateInvoice();
        if (!invoiceCreationSuccess)
        {
            // Handle failure if needed (logging, error message)
            return RedirectToAction("Error", "Home"); // Or any error handling page
        }

        // Step 2: Update Sales Transactions
        var invoiceUpdateSuccess = await UpdateInvoice();
        if (!invoiceUpdateSuccess)
        {
            // Handle failure if needed
            return RedirectToAction("Error", "Home"); // Or any error handling page
        }

        // After both actions are successful, redirect to Sale/Index
        return RedirectToAction("Index", "Sales");
    }

}
