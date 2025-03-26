
let transactions = [];
let invoices = [];

document.getElementById('salesLink').addEventListener('click', showSalesPage);
document.getElementById('invoiceLink').addEventListener('click', showInvoicePage);
document.getElementById('saveTransaction').addEventListener('click', saveTransaction);
document.getElementById('sidebarCollapse').addEventListener('click', function() {
  document.getElementById('sidebar').classList.toggle('active');
});

function showSalesPage() {
  document.getElementById('salesPage').style.display = 'block';
  document.getElementById('invoicePage').style.display = 'none';
  document.getElementById('salesLink').classList.add('active');
  document.getElementById('invoiceLink').classList.remove('active');
}

function showInvoicePage() {
  document.getElementById('salesPage').style.display = 'none';
  document.getElementById('invoicePage').style.display = 'block';
  document.getElementById('salesLink').classList.remove('active');
  document.getElementById('invoiceLink').classList.add('active');
}

function saveTransaction() {
  const transaction = {
    id: Date.now(),
    product: document.getElementById('product').value,
    customer: document.getElementById('customer').value,
    quantity: parseFloat(document.getElementById('quantity').value),
    rate: parseFloat(document.getElementById('rate').value),
    total: parseFloat(document.getElementById('quantity').value) * parseFloat(document.getElementById('rate').value)
  };

  transactions.push(transaction);
  renderSalesTable();
  bootstrap.Modal.getInstance(document.getElementById('transactionModal')).hide();
  document.getElementById('transactionForm').reset();
}

function generateInvoice(transaction) {
  const invoice = {
    id: `INV-${Date.now()}`,
    customer: transaction.customer,
    date: new Date().toLocaleDateString(),
    total: transaction.total
  };
  
  invoices.push(invoice);
  renderInvoiceTable();
}

function renderSalesTable() {
  const tbody = document.getElementById('salesTable');
  tbody.innerHTML = '';
  
  transactions.forEach(transaction => {
    const row = document.createElement('tr');
    row.innerHTML = `
      <td>${transaction.product}</td>
      <td>${transaction.customer}</td>
      <td>${transaction.quantity}</td>
      <td>$${transaction.rate}</td>
      <td>$${transaction.total}</td>
      <td>
        <button class="btn btn-sm btn-primary" onclick="generateInvoice(${JSON.stringify(transaction)})">Generate Invoice</button>
      </td>
    `;
    tbody.appendChild(row);
  });
}

function renderInvoiceTable() {
  const tbody = document.getElementById('invoiceTable');
  tbody.innerHTML = '';
  
  invoices.forEach(invoice => {
    const row = document.createElement('tr');
    row.innerHTML = `
      <td>${invoice.id}</td>
      <td>${invoice.customer}</td>
      <td>${invoice.date}</td>
      <td>$${invoice.total}</td>
      <td>
        <button class="btn btn-sm btn-info">View</button>
      </td>
    `;
    tbody.appendChild(row);
  });
}

renderSalesTable();
renderInvoiceTable();
