# Nike
Backend for Nike clone

Verify that the **DefaultConnection** connection string within **appsettings.json** points to a valid SQL Server instance. 

When you run the application the database will be automatically created (if necessary) and the latest migrations will be applied.

### Database Migrations

To use `dotnet-ef` for your migrations please add the following flags to your command (values assume you are executing from repository root)

* `--project src/Common/CleanArchitecture.Infrastructure` (optional if in this folder)
* `--startup-project src/Apps/CleanArchitecture.Api`
* `--output-dir Persistence/Migrations`

For example, to add a new migration from the root folder:

 `dotnet ef migrations add "CreateDb" --project src\Common\Nike.Infrastructure --startup-project src\Apps\Nike.Api --output-dir Persistence\Migrations`

 `dotnet ef database update --project src\Common\Nike.Infrastructure --startup-project src\Apps\Nike.Api`
