# HIPA-BE
## Running for the first time

There are two ways of running the backend, through **Docker** and **locally**.

> Currently, running the backend **locally** is required for swagger to be accessible (used for generating **api** folder for the frontend). Available at: [http://localhost:5255/swagger/index.html](http://localhost:5255/swagger/index.html)

### Shared prerequisites

- Development database
  - [PostgreSQL](https://www.postgresql.org/download/)
  - User: postgres
  - Password: postgres
  - Port: 5432
- Cloned repositories
  - [HIPA-BE](https://github.com/FIIT-HIPA/HIPA-BE)
  - [HIPA-Deploy](https://github.com/FIIT-HIPA/HIPA-Deploy)

### Running the backend



### Docker

**Prerequisites:**

- [Docker](https://www.docker.com/)

**Steps:**

- Navigate to `HIPA-Deploy` repository root folder
- Run `docker compose up -d --build --force-recreate`

> Alternatively, you can use `HIPA-BE/docker-compose.yml`/`HIPA-BE/Dockerfile` files.

### Locally

**Prerequisites:**

- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

**Steps:**

1. Navigate to `HIPA-BE` repository root folder
2. Run `dotnet run`

## Migrations
To handle migrations you'll have to first download `dotnet-ef` tool. If you don't have this tool globaly installed already please refer to the previous section.
This application uses multiple database contexts. Which context is required migration can be derived based on in which folder the migration, you want to apply, is located.
- Use `AspNetIdentityDb` if you want to create migrations which will alter ApplicationUser tables and user management
- `ConfigurationDb` and `PersistedGrantDb` are required by IdentityServer4
- Use `AppDbContext` for any other migrations

1. To create migrations it is mandatory to have EntityFramework Core CLI installed on your computer:
    ```shell
    dotnet tool install --global dotnet-ef
    ```

    If you are getting `dotnet-ef is not found in NuGet feeds C:\Program Files (x86)\Microsoft SDKs\NuGetPackages\".` error. You might need to use the following:
      1. Open Visual Studio
      2. Tools > Options
      3. NuGet Package Manager > Package Sources
      4. Click the green "add" icon, and add the following feed: https://api.nuget.org/v3/index.json

2. It is importatnt to apply migrations required by IdentityServer4 first (if they weren't already applied):

   Since we are using environment variables and EF Core tools do not yet support launch profiles, we need to export db connection parameters prior to applying migrations.\
   Linux / MacOS:
    ```shell
    export DB_HOST=localhost

    export DB_PORT=5432

    export DB=postgres

    export DB_USERNAME=postgres

    export DB_PASSWORD=postgres
    ```

    Windows:
    ```shell
    set DB_HOST=localhost

    set DB_PORT=5432

    set DB=postgres

    set DB_USERNAME=postgres

    set DB_PASSWORD=postgres
    ```
    Then apply migrations:
    ```shell
    dotnet ef database update --context PersistedGrantDbContext

    dotnet ef database update --context ConfigurationDbContext

    dotnet ef database update --context AspNetIdentityDbContext

    dotnet ef database update --context AppDbContext
    ```
4. Run the project
    ```shell
    dotnet run -lp https
    ```

### Creating migrations

1. To create migration:
    ```shell
    dotnet ef migrations add <migration name> --context <context name>
    ```

2. To update database with migration:
    ```shell
    dotnet ef database update --context <context name>
    ```
3. Revert to previous migration
    ```shell
    dotnet ef database update <previous migration name> --context <context name>
    ```
4. Remove unapplied migration from files and migration history
    ```shell
    dotnet ef migrations remove --context <context name>
    ```

## SMTP configuration
If backend is ran through Docker, you do not need to do anything. Otherwise navigate to `Dockerfile` in this repository, take note of the following environment variables:
- EMAIL_HOST
- EMAIL_PORT
- EMAIL_USERNAME
- EMAIL_PASSWORD

And set these manually or define these in `appsettings.json`/`appsettings.Development.json`.
