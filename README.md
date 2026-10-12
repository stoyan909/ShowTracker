# ShowTracker

ShowTracker is a web application built with ASP.NET Core that allows users to explore TV shows and keep track of their watching progress. The project uses a layered architecture to separate data access, business logic, models, and presentation.

## Features

- **User authentication** — User registration, login, and account management using ASP.NET Core Identity.
- **TV show management** — Organize shows and their related seasons and episodes.
- **Watch tracking** — Associate shows and episodes with individual users to track their progress.
- **Season and episode management** — Maintain the structure of TV shows through seasons and episodes.
- **Role-based access** — Support for user roles and administrative functionality.
- **Database integration** — Persist application data using Entity Framework Core.
- **Separation of concerns** — Keep models, services, mapping, and infrastructure in separate projects.
- **Automated tests** — A dedicated test project for service-layer testing.

## Technologies Used

- **C#**
- **.NET 8**
- **ASP.NET Core MVC**
- **Razor Pages**
- **ASP.NET Core Identity**
- **Entity Framework Core**
- **Microsoft SQL Server**
- **SQLite** (available as a project dependency)
- **AutoMapper**
- **Dependency Injection**
- **Unit Testing**

## Project Structure

The solution is organized into multiple projects, each responsible for a specific part of the application.

| Project | Responsibility |
|---|---|
| `ShowTracker` | Main ASP.NET Core web application, routing, and presentation |
| `ShowTracker.Common` | Shared constants and validation rules |
| `ShowTracker.Data.Models` | Database entities and domain models |
| `ShowTracker.Data` | Entity Framework Core database context and data configuration |
| `ShowTracker.Infrastructures` | Infrastructure components and application initialization |
| `ShowTracker.Mapping` | AutoMapper profiles and object mapping |
| `ShowTracker.Services.Core` | Application services and business logic |
| `ShowTracker.ViewModel` | View models used by the presentation layer |
| `ShowTracker.Services.Tests` | Automated tests for application services |

## Getting Started

### Prerequisites

Before running the application, make sure you have installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Microsoft SQL Server](https://www.microsoft.com/sql-server) or a compatible SQL Server instance
- [Visual Studio](https://visualstudio.microsoft.com/) or another IDE that supports .NET development
- [Git](https://git-scm.com/)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/stoyan909/ShowTracker.git
   ```

2. Navigate to the repository directory:

   ```bash
   cd ShowTracker
   ```

3. Open the solution in Visual Studio, or navigate to the directory containing the main web project:

   ```bash
   cd ShowTracker
   ```

4. Restore the NuGet dependencies:

   ```bash
   dotnet restore
   ```

### Database Configuration

The application uses Entity Framework Core with SQL Server. Its database connection is configured through the `DevConnection` connection string.

Open `appsettings.json` and configure the connection string for your SQL Server instance. For example:

```json
{
  "ConnectionStrings": {
    "DevConnection": "Server=YOUR_SERVER;Database=ShowTrackerDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

Replace `YOUR_SERVER` with your actual SQL Server instance name and adjust the authentication settings to match your environment.

**Important:** Preserve any other existing settings in `appsettings.json`, especially the Identity configuration. Do not commit passwords, API keys, or other secrets to source control.

Ensure that the database schema is initialized using the migrations included in the repository or the database setup instructions provided by the project.

### Running the Application

From the repository root, run:

```bash
dotnet run --project ShowTracker/ShowTracker.csproj
```

Alternatively, open the solution in Visual Studio, select `ShowTracker` as the startup project, and run it.

Once the application starts, open the local URL displayed in the terminal or IDE.

## Running the Tests

To execute the automated tests, run:

```bash
dotnet test
```

This command builds the solution and runs the available test projects.

## Architecture

ShowTracker uses a layered architecture to keep responsibilities separated and make the codebase easier to maintain.

- **Presentation layer:** Handles web requests, controllers, Razor views, and user interaction.
- **Service layer:** Contains application operations and business logic.
- **Data layer:** Defines database entities, relationships, and Entity Framework Core configuration.
- **Mapping layer:** Handles transformations between models and view models.
- **Infrastructure layer:** Provides supporting application components and initialization.
- **Testing layer:** Contains automated tests for service functionality.

ASP.NET Core's built-in dependency injection is used to register and provide the application's services.

## Data Model

The database context includes entities for:

- **Shows** — TV show information.
- **Seasons** — Seasons belonging to shows.
- **Episodes** — Episodes belonging to seasons.
- **UsersShows** — Relationships between users and shows.
- **UserEpisodes** — Relationships between users and episodes.

ASP.NET Core Identity is used to manage user accounts and roles.

## Contributing

Contributions and suggestions are welcome.

1. Fork the repository.
2. Create a feature branch:

   ```bash
   git checkout -b feature/your-feature
   ```

3. Commit your changes:

   ```bash
   git commit -m "Add your feature"
   ```

4. Push your branch:

   ```bash
   git push origin feature/your-feature
   ```

5. Open a pull request.

## License

No license has been specified in this README. Check the repository for a license file before redistributing or reusing the project.

## Author

**Stoyan**

GitHub: [@stoyan909](https://github.com/stoyan909)

## Repository

[https://github.com/stoyan909/ShowTracker](https://github.com/stoyan909/ShowTracker)
