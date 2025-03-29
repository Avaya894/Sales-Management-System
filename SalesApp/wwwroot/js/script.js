
let transactions = [];
let invoices = [];

document.getElementById('salesLink').addEventListener('click', showSalesPage);
document.getElementById('productLink').addEventListener('click', showProductPage);
document.getElementById('customerLink').addEventListener('click', showCustomerPage);
document.getElementById('invoiceLink').addEventListener('click', showInvoicePage);
document.getElementById('saveProduct').addEventListener('click', saveProduct);
document.getElementById('saveCustomer').addEventListener('click', saveCustomer);
document.getElementById('saveSalesTransaction').addEventListener('click', saveSalesTransaction);
document.getElementById('transactionProductName').addEventListener('change', setProductRate);


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

function saveProduct() {
  console.log("product save clicked");
  document.getElementById('productCreateForm').submit();
  bootstrap.Modal.getInstance(document.getElementById('productModal')).hide();
  document.getElementById('transactionForm').reset();
}


function saveCustomer() {
  console.log("customer save clicked");
  try{
    document.getElementById('customerCreateForm').submit();
    bootstrap.Modal.getInstance(document.getElementById('customerModal')).hide();
    document.getElementById('customerCreateForm').reset();
  } catch(err) {
    console.log(err);
  }
}

function saveSalesTransaction() {
  console.log("salesTransaction save clicked");
  try{
    document.getElementById('transactionForm').submit();
    bootstrap.Modal.getInstance(document.getElementById('transactionModal')).hide();
    document.getElementById('transactionForm').reset();
  } catch(err) {
    console.log(err);
  }
}

function setProductRate() {
  var selectedOption = this.options[this.selectedIndex]; // Get the selected option
  var rate = selectedOption.getAttribute('data-rate'); // Get the rate from the selected option
  var rateField = document.getElementById('transactionProductRate'); // Get the rate input field

  // If a product is selected, set the rate in the field, but allow it to be editable
  if (rate) {
    rateField.value = rate; // Set the rate
  } else {
    rateField.value = ''; // Clear the rate if no product is selected
  }
}
