# Form Enhancer

## Overview
Created and presented in 10.2022.

This project enhances an existing [web form](https://www.helmes.com/wp-content/uploads/2023/06/index.html), using C# and Blazor for a dynamic and interactive user experience. The goal is to dynamically populate select options from a database, validate user input, store form submissions, and allow users to edit their data within a session.

## Features

- **Dynamic "Sectors" Selectbox**: Utilizes Blazor and C# to dynamically populate the "Sectors" selectbox from the database, ensuring the form is always up-to-date.
- **Input Validation**: Implements both client-side and server-side validation to ensure all fields are correctly filled before submission.
- **Data Persistence**: On submission, the form data (Name, Sectors, Agree to terms) is stored in a database, allowing for data retrieval and persistence.
- **Session-Based Data Editing**: Users can edit their previously submitted data within the same session, improving the user experience by eliminating the need to resubmit the form for corrections.

## Getting Started

### Prerequisites

- .NET 5.0 SDK or later
- A SQL Server database
- Visual Studio 2019 or later (with C# and Blazor support)

### Installation

1. **Clone the Repository**:
   Open a command prompt or terminal and run:
```
git clone https://github.com/eiramada/CodeShowcase.git
```

2. **Database Setup**:
- Import the provided SQL Server database dump. This includes the structure and initial data needed for the "Sectors" selectbox.
- Use SQL Server Management Studio (SSMS) or command-line tools to import the dump.

3. **Project Configuration**:
- Open the solution in Visual Studio.
- Update the `appsettings.json` or similar configuration file with your database connection string.

4. **Run Migrations** (if applicable):
- In the Package Manager Console, run the following command to apply migrations to your database:
  ```
  Update-Database
  ```

5. **Run the Application**:
- Press `F5` or click "Run" in Visual Studio to start the application. Visual Studio will take care of building the project, restoring NuGet packages, and launching the web server.

### Usage

- The application will open in your default web browser. You can interact with the form, submit data, and see the results stored and displayed from the database.
- To edit previously submitted data within the same session, simply modify the form fields and resubmit.
