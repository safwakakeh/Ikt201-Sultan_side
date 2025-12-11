# Sultan Side Restaurant Management System

This is an ASP.NET Core MVC application designed for managing a restaurant called "Sultan Side". It includes features for menu management, table reservations, customer reviews, and an AI-powered chatbot.

## Features

*   **Public Interface:**
    *   View Menu (Dishes, Categories, Dietary Properties).
    *   Table Booking/Reservations.
    *   Customer Reviews.
    *   AI Chatbot (powered by Google Gemini) for customer assistance.
    *   Stripe Payment integration.
*   **Admin Area:**
    *   Dashboard for overview.
    *   Manage Menu (Add/Edit/Delete dishes and categories).
    *   Manage Reservations.
    *   Manage Reviews.
*   **Authentication:**
    *   User registration and login using ASP.NET Core Identity.
    *   Role-based access control (Admin/User).
*   **API:**
    *   RESTful API endpoints for various resources.
    *   Swagger UI documentation.

## Tech Stack

*   **Framework:** .NET 9.0 (ASP.NET Core MVC)
*   **Database:** SQLite (Entity Framework Core)
*   **Frontend:** Razor Views, HTML, CSS, JavaScript
*   **Authentication:** ASP.NET Core Identity
*   **External Services:**
    *   **Stripe:** Payment processing.
    *   **Google Gemini:** AI Chatbot integration.
    *   **SendGrid:** Email services (referenced in dependencies).

## Prerequisites

*   [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
*   A code editor like Visual Studio Code or Visual Studio 2022.

## Getting Started

1.  **Clone the repository:**
    ```bash
    git clone <repository-url>
    cd Ikt201-Sultan_side
    ```

2.  **Configuration:**
    Update `appsettings.json` or use [User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) to configure the following settings:

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "DataSource=app.db;Cache=Shared"
      },
      "Stripe": {
        "SecretKey": "your_stripe_secret_key",
        "PublishableKey": "your_stripe_publishable_key"
      },
      "Gemini": {
        "ApiKey": "your_google_gemini_api_key"
      },
      "AdminUser": {
        "Email": "admin@example.com",
        "Password": "SecurePassword123!"
      }
    }
    ```

3.  **Database Setup:**
    Apply the Entity Framework migrations to create the database.
    ```bash
    dotnet ef database update
    ```

4.  **Run the Application:**
    ```bash
    dotnet run
    ```

5.  **Access the Application:**
    *   Web App: `http://localhost:5080` (or the port indicated in the terminal).
    *   Swagger UI: `http://localhost:5080/swagger`.

## Project Structure

*   `Controllers/`: MVC Controllers for handling web requests.
*   `ApiControllers/`: API Controllers for REST endpoints.
*   `Models/`: Entity Framework models (Booking, Dish, Person, etc.).
*   `Views/`: Razor views for the UI.
*   `Areas/`: Contains the Admin area and Identity pages.
*   `Services/`: Business logic and external service integrations (Email, Gemini, Menu, Reservation).
*   `Data/`: Database context and migrations.
*   `wwwroot/`: Static assets (CSS, JS, Images).

## Admin User Seeding

The application is configured to seed an admin user on startup if the `AdminUser` configuration is present in `appsettings.json`.


