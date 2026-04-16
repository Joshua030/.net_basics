# DatingApp

ASP.NET Core API with Angular client. Uses SQLite for development and Azure SQL Server for production.

## Database Setup

### Environment Overview

| Environment | Provider   | Connection String Location      | Schema Management       |
|-------------|------------|---------------------------------|-------------------------|
| Development | SQLite     | `appsettings.Development.json`  | `EnsureCreatedAsync()`  |
| Production  | SQL Server | `appsettings.json` / Azure      | EF Migrations via CI/CD |

### How to Update the Database (Step by Step)

Follow these steps every time you make changes to your entities or DbContext.

#### Step 1: Make your model changes

Edit your entity classes in `API/Entities/` or update `AppDbContext.cs`.

```csharp
// Example: adding a new property to Photo.cs
public bool IsApproved { get; set; } = false;
```

#### Step 2: Test locally with SQLite

Delete the local database so `EnsureCreated` rebuilds it from scratch with your new model:

```bash
rm API/datingapp.db
dotnet run --project API
```

`EnsureCreated` will generate the full schema from your current model and `SeedUsers` will populate the database with fresh seed data from `Data/UserSeedData.json` (it only seeds when the database is empty). Test your app at `http://localhost:5000` and verify everything works.

> **Note:** `EnsureCreated` recreates the entire database from the model. You will lose any local data. This is fine for development.
>
> **To refresh seed data:** If you updated `Data/UserSeedData.json` or the `Seed.cs` logic, just delete `API/datingapp.db` and restart — the seed only runs when no users exist.

#### Step 3: Generate a SQL Server migration

Once your changes are tested locally, generate a migration targeting SQL Server (production):

```bash
ASPNETCORE_ENVIRONMENT=Production dotnet ef migrations add DescriptiveMigrationName -p API -o Data/Migrations
```

Use a descriptive name like `AddIsApprovedToPhoto`, `AddMessageEntity`, `RemoveAgeColumn`, etc.

#### Step 4: Review the generated migration

Check the generated file in `API/Data/Migrations/` to make sure the SQL Server types and operations are correct:

```bash
# Look at the latest migration file
ls -t API/Data/Migrations/*.cs | head -2
```

Verify it uses SQL Server types (`nvarchar`, `int`, `bit`, etc.) and not SQLite types (`TEXT`, `INTEGER`).

#### Step 5: Push to GitHub

```bash
git add API/Entities/ API/Data/ API/API.csproj
git commit -m "Add migration: DescriptiveMigrationName"
git push
```

#### Step 6: CI/CD handles production automatically

When you push to `main`, the GitHub Actions workflow will:

1. Build the Angular client and .NET API
2. Generate an **idempotent SQL script** from your migrations (`dotnet ef migrations script --idempotent`)
3. Apply the SQL script to your Azure SQL database
4. Deploy the app to Azure App Service

The `--idempotent` flag means the script checks what has already been applied and only runs what's new. It's safe to run multiple times.

### Common Scenarios

#### Adding a new entity

1. Create the class in `API/Entities/`
2. Add a `DbSet<YourEntity>` in `AppDbContext.cs`
3. Add any relationships in `OnModelCreating` if needed
4. Follow Steps 2-6 above

#### Modifying an existing entity

1. Change the property in the entity class
2. Follow Steps 2-6 above

#### Renaming or removing a column

1. Make the change in your entity
2. Follow Steps 2-6 above
3. **Check the migration carefully** (Step 4) — EF might generate a drop + create instead of a rename. If so, edit the migration manually to use `RenameColumn`

#### Starting fresh (resetting all migrations)

If your production database doesn't exist yet or you want to reset:

```bash
# Delete all migration files
rm API/Data/Migrations/*.cs

# Regenerate from scratch
ASPNETCORE_ENVIRONMENT=Production dotnet ef migrations add InitialCreate -p API -o Data/Migrations
```

### Required GitHub Secrets

These must be configured in your repo under Settings > Secrets > Actions:

| Secret | Description |
|--------|-------------|
| `AZURE_SQL_CONNECTION_STRING` | Full Azure SQL connection string for running migrations |
| `AZUREAPPSERVICE_CLIENTID_*` | Azure service principal client ID |
| `AZUREAPPSERVICE_TENANTID_*` | Azure AD tenant ID |
| `AZUREAPPSERVICE_SUBSCRIPTIONID_*` | Azure subscription ID |

### Quick Reference

```bash
# Run locally (SQLite, auto-creates DB)
dotnet run --project API

# Reset local database
rm API/datingapp.db

# Generate a new production migration
ASPNETCORE_ENVIRONMENT=Production dotnet ef migrations add MigrationName -p API -o Data/Migrations

# Preview the SQL that would run in production
ASPNETCORE_ENVIRONMENT=Production dotnet ef migrations script --idempotent -p API

# Remove the last migration (if not yet applied)
ASPNETCORE_ENVIRONMENT=Production dotnet ef migrations remove -p API
```
