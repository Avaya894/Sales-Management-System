
let transactions = [];
let invoices = [];

document.getElementById('salesLink').addEventListener('click', showSalesPage);
document.getElementById('productLink').addEventListener('click', showProductPage);
document.getElementById('customerLink').addEventListener('click', showCustomerPage);
document.getElementById('invoiceLink').addEventListener('click', showInvoicePage);
document.getElementById('saveTransaction').addEventListener('click', saveTransaction);
document.getElementById('sidebarCollapse').addEventListener('click', function() {
  document.getElementById('sidebar').classList.toggle('active');
});

function showSalesPage() {
  document.getElementById('salesPage').style.display = 'block';
  document.getElementById('productPage').style.display = 'none';
  document.getElementById('customerPage').style.display = 'none';
  document.getElementById('invoicePage').style.display = 'none';
  document.getElementById('salesLink').classList.add('active');
  document.getElementById('productLink').classList.remove('active');
  document.getElementById('customerLink').classList.remove('active');
  document.getElementById('invoiceLink').classList.remove('active');
  
}

function showProductPage() {
  document.getElementById('salesPage').style.display = 'none';
  document.getElementById('productPage').style.display = 'block';
  document.getElementById('customerPage').style.display = 'none';
  document.getElementById('invoicePage').style.display = 'none';
  document.getElementById('salesLink').classList.remove('active');
  document.getElementById('productLink').classList.add('active');
  document.getElementById('customerLink').classList.remove('active');
  document.getElementById('invoiceLink').classList.remove('active');

}

function showCustomerPage() {
  document.getElementById('salesPage').style.display = 'none';
  document.getElementById('productPage').style.display = 'none';
  document.getElementById('customerPage').style.display = 'block';
  document.getElementById('invoicePage').style.display = 'none';
  document.getElementById('salesLink').classList.remove('active');
  document.getElementById('productLink').classList.remove('active');
  document.getElementById('customerLink').classList.add('active');
  document.getElementById('invoiceLink').classList.remove('active');

}

function showInvoicePage() {
  console.log("Invoice Page clicked");
  document.getElementById('salesPage').style.display = 'none';
  document.getElementById('productPage').style.display = 'none';
  document.getElementById('customerPage').style.display = 'none';
  document.getElementById('invoicePage').style.display = 'block';
  document.getElementById('salesLink').classList.remove('active');
  document.getElementById('productLink').classList.remove('active');
  document.getElementById('customerLink').classList.remove('active');
  document.getElementById('invoiceLink').classList.add('active');
}

