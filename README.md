# TeamWork-Server API Readme

.NET core RESTful API that serves the [TeamWork-Client](https://github.com/basemelyyan/TeamWork-Client).

## Production URL

ToDo

## Staging URL

ToDo

___

## Project Requirements on Mac

- [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2017+/Visual Studio Code](https://visualstudio.microsoft.com/downloads/)
- [Docker](https://www.docker.com/products/docker-desktop)
- Database Manager ([dBeaver](https://dbeaver.io/download/) is a good choice) OR ([Azure Data Studio - Insiders](https://docs.microsoft.com/en-us/sql/azure-data-studio/download-azure-data-studio?view=sql-server-ver15) are a good free cross-platform choice that supports MSSQL)
- Starting with .NET Core 3.0, the EF tools are no longer installed with the .NET SDK. Install them specifically with `dotnet tool install --global dotnet-ef`.

## Project Setup

The project uses multiple `appsettings.json` for each environment. They are not included in version control. The included `appsettings.json` has empty values. Contact the maintainers to populate `appsettings.Development.json`, `appsettings.Staging.json`, and `appsettings.Production.json`.
Run `dotnet run --environment "Development"  -p TeamWork` to switch to dev settings, or setup that in `launch.json`

### Local Environment - for debugging

Running the project where the database is containerized and the .NET project runs locally requires more startup steps, but allows for fine-grained debugging (this is going to be further streamlined).

  1. Clone the repo.
  2. Install .NET Core on the local machine, at least version 3.1. The install time should be nominal regardless of OS.
  3. Install Docker on the local machine. The install time should be nominal regardless of OS.
  4. Acquire the .NET project environment variables for the project (currently available from the project owners) and add them to `/appsettings.json`.
  5. Run `docker-compose up`. `PASSWORD` and `PORT` within .env file should match the password in `appsettings.json`. This sets up the dockerized MSSQL container.
  6. Run `dotnet restore` then `dotnet list package` from the root directory - this ensures that the NuGet packages are installed in the project.
  7. Run `dotnet ef migrations add initial-database -s teamwork -p teamwork.repository --verbose` each time you updated database structure to generate/update migrations files.
  8. Run `dotnet ef database update -p teamwork.repository -s teamwork` from the root directory - this seeds the database with the initialized records in the app (the project is code-first).
  9. Run the .NET Core project in VS/VS Code `dotnet run --environment "Development" -p TeamWork` .

### Full Docker Container

Running this project through a full docker container (where both the database and the .NET project are containerized) is the quickest way to start the project, but it comes at the cost of not having access to local debugging.

  1. Clone the repo.
  2. Install Docker on the local machine. The install time should be nominal regardless of OS.
  3. Acquire the .NET project environment variables for the project (currently available from the project owners) and add them to `appsettings.json`.
  4. Acquire the docker environment variables for the project (currently available from the project owners) and add them to `/.env`.
  5. Run `docker-compose down && docker-compose up -d` from the root directory. The initial install time will be somewhat substantial - expect at least 5 minutes. This will start up the project, run the database migrations and serve the project.
  6. Run `docker logs teamwork_server_web_1 -f` from the repo directory. This will show server logs from the command line where it was run.

## Running the Project

- Click Run Project button or hit F5. (This works locally, not with the fully dockerized container).
- The project serves on <http://127.0.0.1:5003> locally.

## Accessing the Database Locally

  1. Run a database manager ([dBeaver](https://dbeaver.io/download/) is a good choice).
  2. If using dBeaver, set up an instance for MSSSQL. There will be a one-time download.
  3. Connect with the following credentials:
      - Username: SA
      - Password: The same password as `appsettings.json`/`.env`
      - Port: 5434 (In Docker .env file) - this is purely to avoid collisions with the default. It can be changed.

## Swagger - API Documentation

TO Do

## Postman

To Do

## Development Process

Any feature code should get acceptance testing in the staging environment in addition to any code review.

This project should follows SemVer. The tag version and release should only be incremented after following the review process listed above.

This version is distinct from the app versions.

## Deployment Environment

There are two deployment environments: staging and production. Each environment has its own database. Staging should be used as a sandbox to test changes in a server environment before updating production. However, the app is only connected to the production database, so it cannot test data changes directly in-app from staging.

## Deployment Process

Since the server and client are deployed to servers running IIS, the deployments do not use the built-in docker container.

To Do

### Visual Studio (Windows-only)

- Right click on the project in the solution explorer and choose publish.
- Choose the appropriate publish profile.
- Click on edit, and choose the `Connection` tab.
  - Enter the account credentials.
  - Put in db password when deploying (The code maintainers have the credentials).
- Click on the `Settings` tab.
  - Confirm the publish profile is set to `Self-Contained` under `Deployment Mode` - this prevents runtime errors.
  - Confirm that the Target Runtime is `win-x64`.
  - Open the Databases accordion, check the use box and confirm the connection string matches what is in `appsettings.json` for the desired environment.
  - Open the Entity Framework Migrations accordion, check the apply box, and provide the connection string (should be same as above).
- Click Save.
- Click Publish.
- Confirm success in logs and through testing.

### .NET CLI (Windows, OS X, Linux)

To Do
