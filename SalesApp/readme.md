# Sales Transaction Management System

## Overview

This is an ASP.NET MVC web application with an SQL Server database for managing sales transactions and invoices. The application allows users to:

- View all sales transactions
- Add and edit sales transactions
- Generate invoices for customers on a daily basis
- Tag sales transactions with the correct invoice
- Add products and customers

## Tech Stack

- **Backend:** ASP.NET MVC, Entity Framework
- **Frontend:** Razor Views (HTML, CSS, JavaScript)
- **Database:** SQL Server
- **IDE:** JetBrains Rider, Visual Studio (or any compatible IDE)
- **Framework Version:** .NET 9

## Database Schema

The application includes the following tables:

- **Product** – Stores product details
- **Customer** – Stores customer information
- **SalesTransaction** – Records sales transactions with product, customer, quantity, rate, and total price
- **Invoice** – Stores invoice details generated for each customer per day

## Features Implemented

### 1. Sales Transaction Management
- View all sales transactions, including:
    - Product Name
    - Customer Name
    - Quantity
    - Rate
    - Total Price
    - Associated Invoice (if applicable)
- Add new sales transactions
- Edit existing sales transactions

### 2. Invoice Management
- Generate invoices for each customer based on daily transactions
- Invoice details include:
    - Customer
    - Invoice Date
    - Invoice Number
    - Invoice Total
- Tag sales transactions with the correct invoice once an invoice is processed

### 3. Customer Management
- Allows users to add customers

### 4. Product Management
- Allows users to add products

## Setup Instructions

### 1. Prerequisites
- .NET Framework/.NET Core (.NET 9 used for this project)
- SQL Server
- IDE: Visual Studio, JetBrains Rider, or any compatible IDE

### 2. Configuring the Connection String
- Open the `appsettings.json` file in the project.
- Locate the `ConnectionStrings` section.
- Update the `DefaultConnection` value with your SQL Server details. Example:

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=YOUR_DATABASE_NAME;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;"
}
```

- Save the file and restart the application to apply the changes.

### 3. Database Setup
#### Using Entity Framework Code First Migrations:
##### Using Package Manager Console in Visual Studio:
1. Open the Package Manager Console in Visual Studio.
2. Run the following commands to create migrations and apply them:

```powershell
Add-Migration InitialCreate
Update-Database
```

##### Alternative: Using dotnet ef CLI
1. Open a terminal or command prompt in the project root.
2. Run the following command to create a migration:

```sh
dotnet ef migrations add InitialCreate
```

3. Apply the migration to update the database:

```sh
dotnet ef database update
```

### 4. Running the Application
1. Open the project in Visual Studio or JetBrains Rider.
2. Update the connection string in `appsettings.json`.
3. Build and run the project.

## Todo
- Authentication & Authorization
- Error Handling & Logging
- Unit tests and integration tests
- Database operation testing through Entity Framework

## Screenshots

Screenshots of different application pages:

1. **Sales Transaction Listing Page** – Lists all sales transactions.
   [![list-Sales-Page.png](https://i.postimg.cc/k5JjC1n3/list-Sales-Page.png)](https://postimg.cc/w1r5cky0)
2. **Add Sales Transaction Page** – Form to create a new sales transaction.
[![add-Sales-Page.png](https://i.postimg.cc/23N1dW1r/add-Sales-Page.png)](https://postimg.cc/dZnQwLRx)
3. **Edit Sales Transaction Page** – Form to modify a sales transaction.
   [![edit-Sales-Page.png](https://i.postimg.cc/2jMXW7xh/edit-Sales-Page.png)](https://postimg.cc/vD7zF9kZ)
4. **Product Listing & Creation Page** – Displays and manages products.
   [![productpage.png](https://i.postimg.cc/T3qkZMvJ/productpage.png)](https://postimg.cc/LJ5B1WXJ)
5. **Customer Listing & Creation Page** – Lists and manages customers.
   [![customerpage.png](https://i.postimg.cc/vTV16hH0/customerpage.png)](https://postimg.cc/9wcX6G99)
6. **Invoice Generation Page** – Generates invoices for sales transactions.
   [![generate-Invoice-Page.png](https://i.postimg.cc/XNpzXz9n/generate-Invoice-Page.png)](https://postimg.cc/JtCq2KTF)
7. **Invoice Listing Page** – Displays all generated invoices.
   [![list-Invoice-Page.png](https://i.postimg.cc/4xTLL4q3/list-Invoice-Page.png)](https://postimg.cc/2V273Yzp)
8. **Invoice Detail View Page** – Shows detailed invoice information.
   [![invoice-Detail-Page.png](https://i.postimg.cc/L6PQ3gy7/invoice-Detail-Page.png)](https://postimg.cc/mtbSbkWN)

