# Backend

This is the backend API for the URL Shortener web app, built with **.NET 9**.

## Prerequisites

* MySQL 8.0+
* .NET SDK 9.0+
* Visual Studio 2026

## Database Setup

### 1. Setting Up the Database

1. Install and run MySQL
2. Create a new database for the URL shortener

### 2. Adding the Database Connection String Secret

1. Open the terminal
2. Navigate to the `url-shortener/Backend/Url.Shortener` project directory
3. Add the database connection string secret:

```bash
dotnet user-secrets set "Database:ConnectionString" "server=<SERVER>; database=<DATABASE>; user=<USER>; password=<PASSWORD>"
```

### 3. Running the Database

1. Open MySQL
2. Connect to the database

## Debugging

1. Open **Visual Studio**
2. Click on **Open a project or solution**
3. Open `url-shortener/Backend/Backend.sln`
4. Build the solution (**Build** > **Build Solution**)
5. Run the **Backend** debugger profile

## Testing

1. In **Visual Studio**, open the **Test Explorer** (**View** > **Test Explorer**)
2. Select tests
3. Click **Run** or **Debug**
