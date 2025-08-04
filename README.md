# Library Management System (Console App) using EF Core & SQL

A simple yet powerful **Library Management System** developed in **C#** using **.NET Core Console Application**, **Entity Framework Core (Fluent API)**, and **SQL Server**.
This project demonstrates the principles of clean architecture, async programming, and real-world database management in a console-based app.

---

## Key Features

* Manage books (Add, Edit, Delete, View)
* Manage members (Add, Edit, Delete, View)
* Borrow and return books
* Track borrowed books with issue/return dates
* Modular design with a `Services` layer
* Relational database designed and managed via EF Core + SQL
* Console-based user interface
* Asynchronous operations using `async` / `await`

---

## Project Structure

```
LibraryManagementSystem/
├── Entities/             # Entity classes
│   ├── Author.cs
│   ├── Book.cs
│   ├── Borrower.cs
│   ├── BorrowedBook.cs
│   ├── Review.cs
│
├── Services/             # Business logic layer (one service per entity)
│   ├── AuthorService.cs
│   ├── BookService.cs
│   ├── BorrowerService.cs
│   ├── BorrowedBookService.cs
│   ├── ReviewService.cs
│
├── Data/                 # Database context
│   └── AppDbContext.cs
│
├── Program.cs            # Main entry point
├── mydatabase.sql        # Optional: SQL script for manual DB creation
├── appsettings.json             
├── ERD.png
├── Mapping.png    
├── README.md            # Project documentation  
```
---

## Built With

* **C#** (.NET Core Console App)
* **Entity Framework Core** (ORM)
* **Fluent API** (for relationship configuration)
* **SQL Server** (Database backend)
* **LINQ** (for expressive queries)
* **Asynchronous Programming** (`Task`, `async/await`)

---

## ERD 
<img width="836" height="457" alt="ERD" src="https://github.com/user-attachments/assets/f203a546-555e-4898-adbe-8a508ce5feb7" />

## Mapping
<img width="1279" height="646" alt="Mapping" src="https://github.com/user-attachments/assets/0b6d242f-c19b-4556-aab6-43594562f8fe" />

---

## Getting Started

### Prerequisites

* [.NET SDK](https://dotnet.microsoft.com/en-us/download)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or **LocalDB**
* [Visual Studio](https://visualstudio.microsoft.com/) or any C# IDE

---

### Run the App

1. Clone the repository:

   ```bash
   git clone https://github.com/saraanbih/Library-Management-System-Using-EF-Core-and-SQL.git
   cd Library-Management-System-Using-EF-Core-and-SQL
   ```

2. Open in Visual Studio or run via terminal:

   ```bash
   dotnet build
   dotnet run
   ```

---

## Database Setup

### Option 1: **Code-First (Automatic)**

When you run the app, EF Core will automatically:

* Create the database
* Apply model configurations via Fluent API

> No need to run the SQL file unless you prefer manual setup.

---

### Option 2: **Manual SQL Import**

Use the `mydatabase.sql` file to manually create the database.

#### On Windows (SQLCMD):

```bash
sqlcmd -S . -d master -i mydatabase.sql
```

#### Or via SQL Server Management Studio (SSMS):

* Open `mydatabase.sql`
* Execute the script

---

## Design Principles

* **Separation of Concerns**: Models, Services, and Data Access are separated
* **Fluent API**: More control over entity relationships than Data Annotations
* **Async First**: Uses `Task`-based asynchronous methods
* **Console UI**: Clean, readable prompts for user interaction

---

## Contributions

Contributions are welcome!
If you'd like to contribute:

1. Fork the repo
2. Create a new branch
3. Make your changes
4. Submit a pull request

---

## Star the Repo

If you found this project useful or learned something, please give it a ⭐ on GitHub to support it!

---
