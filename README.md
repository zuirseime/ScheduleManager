# ScheduleManager

This is a web application built using ASP.NET Core for managing schedules, tasks, subjects, and settings.

## Features

- User authentication and authorization
- Schedule management (create, edit, delete schedules)
- Task management for different subjects
- Settings page to configure user preferences
- Responsive design
- Configuration via `appsettings.json`

## Getting Started

### Prerequisites

To run this project, you'll need:

- [.NET SDK](https://dotnet.microsoft.com/download/dotnet) (version 7.0 or later)
- A code editor like [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

### Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/yourusername/ScheduleManager.git
    ```

2. Navigate to the project directory:
    ```bash
    cd ScheduleManager
    ```

3. Restore the dependencies:
    ```bash
    dotnet restore
    ```

### Running the Application

1. Update `appsettings.json` with your database connection strings and other configurations as needed.

2. Run the application:
    ```bash
    dotnet run
    ```

3. Open a browser and go to `http://localhost:5000` to access the application.

### Project Structure

- **Controllers**: Contains the API endpoints, e.g., `AccountController.cs`
- **Models**: Defines the data models used in the application
- **Views**: Contains the Razor pages or views
- **wwwroot**: Static files (CSS, JavaScript, images)

### Screenshots

#### Schedule Page

![Schedule Page](https://github.com/user-attachments/assets/053c187f-ae5d-4cdf-975e-a84aea37809f)


#### Assignments Page

![Assignments Page](https://github.com/user-attachments/assets/be7da880-98db-4381-bf11-53d6fcd08804)


#### Disciplines Page

![Disciplines Page](https://github.com/user-attachments/assets/b30e7bf3-0849-4765-a998-2e774ca58704)

#### Settings Page

![Settings Page](https://github.com/user-attachments/assets/2d474d67-f630-4da2-be96-1539661bd93a)

### Configuration

- `appsettings.json`: Centralized configuration file for the application. Customize database connections, logging, and other settings here.
