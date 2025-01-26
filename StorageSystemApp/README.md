# Storage System App

## Introduction

Created and presented in 04.2024 as a part of a coding challenge.

This application was developed to address the issue of efficient inventory management. It provides users with a comprehensive system to manage their storage environments effectively. The app allows users to catalog, track, and manage items while providing tools for administrators to oversee system-wide usage and analytics.

The project primarily focuses on backend development using C# .NET 8 and MS SQL, with a React-based frontend for an interactive user interface. It also integrates features like user authentication, role-based access, and API documentation for seamless navigation and extensibility.

## Problem Addressed

Managing inventory across multiple storage systems can be challenging, especially for users needing clear organization, quick access, and robust oversight. This app simplifies these complexities by:

- Enabling users to set up multi-level storage systems.
- Providing tailored tools for admins to monitor and manage user activities.
- Incorporating robust authentication mechanisms to ensure data security.
- Allowing detailed tracking and management of stored items.


## Technology Stack

- **Frontend:** React
- **Backend:** C# .NET 8
- **Database:** MS SQL
- **API Documentation:** Swagger

## Features

- **User Authentication:** Robust registration and login functionality.
- **User Roles:** Distinct roles (basic users and admins) with tailored capabilities.
- **Inventory Management:** Capability for users to create and manage multiple storage levels and items.
- **Admin Dashboard:** Specialized analytics and management tools for system-wide user statistics.


## Getting Started

### Prerequisites

- .NET 8 SDK or later.
- Visual Studio or Visual Studio Code.
- Node.js v20 or later.
- SQL Server Express LocalDB. [Download here](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16).

### Installation and Setup

1. **Download the project:**
   - Clone the repository or download the ZIP file and extract it to your preferred location.

#### Backend Setup

2. **Open the solution:**
   - Open `Inventory.API.sln` in Visual Studio.

3. **Restore dependencies:**
   - Right-click on the solution and select "Restore NuGet Packages."

*Optional: 
4. **Configure CORS:**
   - Navigate to `appsettings.json` and set the `Cors:AllowSpecificOrigin:Origins` key to your frontend URL. Default is `https://localhost:3000`

#### Frontend Setup

5. **Prepare the frontend environment:**
   - Open the `inventory-ui` folder in Visual Studio Code.
   - Run `npm install` in the terminal to install dependencies.
   - Start the application with `npm start`.

#### Database Setup

6. **Configure the database connection:**
   - Modify the `DefaultConnection` string in `appsettings.json` with your database credentials.

7. **Apply database migrations:**
   - Use the Package Manager Console: `Update-Database`
   - Or via .NET Core CLI: `dotnet ef database update`
   
   *Note: The initial setup includes seeding the database with an admin user:*
   - **Username:** admin
   - **Password:** securePassword

### Running the Application

- **Start the backend and frontend:**
  - Run the backend through Visual Studio by pressing F5.
  - Navigate to `https://localhost:3000` or your configured CORS URL.
 
 

### API Documentation

- Access detailed API documentation through the integrated Swagger UI by navigating to `/swagger` on your backend server's URL.

## Future Enhancements

- **User Experience:** Improve visual designs and enhance usability across various components.
- **Admin Experienxe:** Expand functionalities to include more comprehensive data views and management tools.
- **Error Handling:** Implement advanced error handling and recovery mechanisms to improve system robustness.
- **Scalability:** Optimize backend services to handle a larger number of concurrent users effectively.
- **Testing:** Develop comprehensive test suites for both the backend and frontend.

## Notes and Limitations

### Common Issues & Troubleshooting

- **Build Errors:** Ensure all dependencies are correctly restored and that your SDK versions match the project requirements.
- **Database Connections:** Verify that the SQL Server is operational and the connection string in `appsettings.json` is accurate.
- **API Accessibility:** Confirm that the backend server is correctly configured and reachable.

### Error Handling

Given the scope of this challenge, the application may not include advanced error recovery mechanisms. It's advisable to restart the application in case of critical errors to restore normal operations.

## Conclusion

This README provides a starting point for setting up and using the Storage System App. For more details or assistance, consult the API documentation or reach out to the project contributors.
