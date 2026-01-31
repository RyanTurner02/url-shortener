# Backend

This is the backend API for the URL Shortener web app, built with **.NET 10**.

## Prerequisites

* .NET SDK 10.0+
* Visual Studio 2026
* MySQL 8.0+
* Docker

## Database Setup

1. Install and run MySQL
2. Create a new database for the URL shortener
3. Open the terminal
4. Navigate to the `url-shortener/Backend/Url.Shortener` project directory
5. Apply the database migrations:

```bash
dotnet ef database update
```

6. Add the database connection string secret:

```bash
dotnet user-secrets set "Database:ConnectionString" "server=<SERVER>; database=<DATABASE>; user=<USER>; password=<PASSWORD>;"
```

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

**Note:** For integration tests, Docker will need to be running.
