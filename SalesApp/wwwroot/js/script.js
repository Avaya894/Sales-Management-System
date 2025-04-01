
let transactions = [];
let invoices = [];

document.getElementById('salesLink').addEventListener('click', showSalesPage);
document.getElementById('productLink').addEventListener('click', showProductPage);
document.getElementById('customerLink').addEventListener('click', showCustomerPage);
document.getElementById('generateInvoiceLink').addEventListener('click', showGenerateInvoicePage);
document.getElementById('invoiceLink').addEventListener('click', showInvoicePage);
document.getElementById('saveProduct').addEventListener('click', saveProduct);
document.getElementById('saveCustomer').addEventListener('click', saveCustomer);
document.getElementById('saveSalesTransaction').addEventListener('click', saveSalesTransaction);
document.getElementById('editSalesTransaction').addEventListener('click', editSalesTransaction);
document.getElementById('transactionProductName').addEventListener('change', setProductRate);
document.getElementById('editTransactionProductName').addEventListener('change', setEditProductRate);

// Calculate the change in total 
document.getElementById('transactionProductQuantity').addEventListener('input', calculateSalesTotal);
document.getElementById('transactionProductRate').addEventListener('input', calculateSalesTotal);
// Calculate the change in total 
document.getElementById('editTransactionQuantity').addEventListener('input', calculateEditSalesTotal);
document.getElementById('editTransactionProductRate').addEventListener('input', calculateEditSalesTotal);


function showSalesPage() {
  document.getElementById('salesPage').style.display = 'block';
  document.getElementById('productPage').style.display = 'none';
  document.getElementById('customerPage').style.display = 'none';
  document.getElementById('generateInvoicePage').style.display = 'none';
  document.getElementById('invoicePage').style.display = 'none';
  document.getElementById('salesLink').classList.add('active');
  document.getElementById('productLink').classList.remove('active');
  document.getElementById('customerLink').classList.remove('active');
  document.getElementById('generateInvoiceLink').classList.remove('active');
  document.getElementById('invoiceLink').classList.remove('active');
  
}

function showProductPage() {
  document.getElementById('salesPage').style.display = 'none';
  document.getElementById('productPage').style.display = 'block';
  document.getElementById('customerPage').style.display = 'none';
  document.getElementById('invoicePage').style.display = 'none';
  document.getElementById('generateInvoicePage').style.display = 'none';
  document.getElementById('salesLink').classList.remove('active');
  document.getElementById('productLink').classList.add('active');
  document.getElementById('customerLink').classList.remove('active');
  document.getElementById('generateInvoiceLink').classList.remove('active');
  document.getElementById('invoiceLink').classList.remove('active');

}

function showCustomerPage() {
  document.getElementById('salesPage').style.display = 'none';
  document.getElementById('productPage').style.display = 'none';
  document.getElementById('customerPage').style.display = 'block';
  document.getElementById('generateInvoicePage').style.display = 'none';
  document.getElementById('invoicePage').style.display = 'none';
  document.getElementById('salesLink').classList.remove('active');
  document.getElementById('productLink').classList.remove('active');
  document.getElementById('customerLink').classList.add('active');
  document.getElementById('generateInvoiceLink').classList.remove('active');
  document.getElementById('invoiceLink').classList.remove('active');

}

function showGenerateInvoicePage() {
  console.log("Invoice Page clicked");
  document.getElementById('salesPage').style.display = 'none';
  document.getElementById('productPage').style.display = 'none';
  document.getElementById('customerPage').style.display = 'none';
  document.getElementById('generateInvoicePage').style.display = 'block';
  document.getElementById('invoicePage').style.display = 'none';
  document.getElementById('salesLink').classList.remove('active');
  document.getElementById('productLink').classList.remove('active');
  document.getElementById('customerLink').classList.remove('active');
  document.getElementById('generateInvoiceLink').classList.add('active');
  document.getElementById('invoiceLink').classList.remove('active');
}

function showInvoicePage() {
  console.log("Invoice Page clicked");
  document.getElementById('salesPage').style.display = 'none';
  document.getElementById('productPage').style.display = 'none';
  document.getElementById('customerPage').style.display = 'none';
  document.getElementById('generateInvoicePage').style.display = 'none';
  document.getElementById('invoicePage').style.display = 'block';
  document.getElementById('salesLink').classList.remove('active');
  document.getElementById('productLink').classList.remove('active');
  document.getElementById('customerLink').classList.remove('active');
  document.getElementById('generateInvoiceLink').classList.remove('active');
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

function editSalesTransaction() {
  console.log("editSalesTransaction save clicked");
  try{
    document.getElementById('editTransactionForm').submit();
    bootstrap.Modal.getInstance(document.getElementById('editTransactionModal')).hide();
    document.getElementById('editTransactionForm').reset();
  } catch(err) {
    console.log(err);
  }
}

function setProductRate(id) {
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

function setEditProductRate(id) {
  var selectedOption = this.options[this.selectedIndex]; // Get the selected option
  var rate = selectedOption.getAttribute('data-rate'); // Get the rate from the selected option
  var rateField = document.getElementById('editTransactionProductRate'); // Get the rate input field

  // If a product is selected, set the rate in the field, but allow it to be editable
  if (rate) {
    rateField.value = rate; // Set the rate
  } else {
    rateField.value = ''; // Clear the rate if no product is selected
  }
}

function calculateSalesTotal() {
  var quantity = parseFloat(document.getElementById('transactionProductQuantity').value) || 0;
  var rate = parseFloat(document.getElementById('transactionProductRate').value) || 0;
  document.getElementById('transactionTotal').value = quantity * rate;
}

function calculateEditSalesTotal() {
  var quantity = parseFloat(document.getElementById('editTransactionQuantity').value) || 0;
  var rate = parseFloat(document.getElementById('editTransactionProductRate').value) || 0;
  document.getElementById('editTransactionTotal').value = quantity * rate;
}



document.querySelectorAll("#edit-transaction").forEach(button => {
  button.addEventListener("click", function () {
    let salesTransactionId = this.getAttribute("data-id");
    let productId = this.getAttribute("data-product-id");
    let customerId = this.getAttribute("data-customer-id");
    let quantity = this.getAttribute("data-quantity");
    let rate = this.getAttribute("data-rate");

    // Set values in the edit form
    document.getElementById("editTransactionId").value = salesTransactionId;
    document.getElementById("editTransactionProductName").value = productId;
    document.getElementById("customerName").value = customerId;
    document.getElementById("editTransactionQuantity").value = quantity;
    document.getElementById("editTransactionProductRate").value = rate;
    document.getElementById("editTransactionTotal").value = quantity * rate; // Auto-calculate total

    // Trigger change event in case any dependent fields need to be updated
    document.getElementById("editTransactionProductName").dispatchEvent(new Event("change"));
  });
});
