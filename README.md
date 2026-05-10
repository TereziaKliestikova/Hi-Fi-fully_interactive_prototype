# High-Fidelity Fully Interactive Prototype

This folder contains the source code of the fully interactive high-fidelity prototype used in the final phase of usability testing.

The prototype consists of one backend application and three frontend variants. During testing, the backend remained the same, while the frontend was switched according to the tested navigation structure.

## Folder Structure

| Folder         | Description                                                  |
| -------------- | ------------------------------------------------------------ |
| `BE/`          | Backend implementation of the application.                   |
| `navigation1/` | Frontend implementation for the first navigation structure.  |
| `navigation2/` | Frontend implementation for the second navigation structure. |
| `navigation3/` | Frontend implementation for the third navigation structure.  |

---

## Prerequisites

To run the prototype locally, the following tools are required:

| Tool        | Required version / configuration                        |
| ----------- | ------------------------------------------------------- |
| PostgreSQL  | Local database running on port `5432`                   |
| .NET SDK    | Version `8.0`                                           |
| Node.js     | Version `20.18.0` recommended                           |
| npm         | Included with Node.js                                   |
| Angular CLI | Installed globally                                      |
| dotnet-ef   | Required only if migrations need to be applied manually |

The expected local PostgreSQL configuration is:

```text
Host: localhost
Port: 5432
Database: postgres
Username: postgres
Password: postgres
```

---

## 1. Cloning the Repository

First, clone this repository to your local computer:

```bash
git clone https://github.com/TereziaKliestikova/Hi-Fi-fully_interactive_prototype.git
```

Then navigate into the folder:

```bash
cd Hi-Fi-fully_interactive_prototype
```

---

## 2. Running the Backend

Before running any frontend version, start the backend.

Navigate to the backend folder:

```bash
cd BE
```

Run the backend:

```bash
dotnet run
```

The backend should run locally on port `5255`.

Swagger should be available at:

```text
http://localhost:5255/swagger/index.html
```

---

## 3. Applying Database Migrations

In most cases, the backend can be started directly using:

```bash
dotnet run
```

If there are pending database migrations, or if the application does not start correctly because of the database state, run the following commands from the `BE/` folder:

```bash
dotnet ef database update -c Duende.IdentityServer.EntityFramework.DbContexts.PersistedGrantDbContext --connection "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres;Include Error Detail=true"
```

```bash
dotnet ef database update -c Duende.IdentityServer.EntityFramework.DbContexts.ConfigurationDbContext --connection "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres;Include Error Detail=true"
```

```bash
dotnet ef database update -c BE.Data.AppDbContext
```

After applying the migrations, run the backend again:

```bash
dotnet run
```

---

## 4. Running a Frontend Version

Each navigation structure has its own frontend folder. Only one frontend version should be running at a time.

Open a new terminal window and navigate to one of the frontend folders.

For example, for the first navigation structure:

```bash
cd navigation1
```

Install dependencies:

```bash
npm ci
```

Run the frontend:

```bash
ng serve --configuration development --open
```

The frontend should open automatically in the browser. If it does not, open:

```text
http://localhost:4200/
```

---

## 5. Switching Between Navigation Structures

During usability testing, the backend stays running and only the frontend is switched.

### Navigation structure 1

Terminal 1:

```bash
cd BE
dotnet run
```

Terminal 2:

```bash
cd navigation1
npm ci
ng serve --configuration development --open
```

After finishing this variant (after task n. 3), stop only the frontend using `Ctrl + C`.

### Navigation structure 2

Keep the backend running.

In the frontend terminal, run:

```bash
cd ../navigation2
npm ci
ng serve --configuration development --open
```

After finishing this variant, stop only the frontend using `Ctrl + C`.

### Navigation structure 3

Keep the backend running.

In the frontend terminal, run:

```bash
cd ../navigation3
npm ci
ng serve --configuration development --open
```

---

## 6. Login Credentials

Use the following testing account to access the application:

```text
Username: johndoe@example.com
Password: TestPassword896
```

The backend must be running for login and application content to work correctly.

---

## 7. Troubleshooting

If the frontend opens but the content does not load, check the following:

1. PostgreSQL is running locally.
2. The database uses the expected credentials:
   - username: `postgres`
   - password: `postgres`
   - port: `5432`
   - database: `postgres`
3. The backend is running.
4. The correct frontend version is running.
5. Only one frontend version is running at a time.
6. The user is logged in with the testing account.
7. If the backend fails because of the database state, apply the migration commands listed above.
