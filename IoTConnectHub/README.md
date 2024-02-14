# IoTConnectHub

## Overview
Created and presented in 01.2024.

Developed with a focus on .NET technologies, IoTConnectHub is a comprehensive solution for IoT device management and simulation, showcasing scalability, efficiency, and security. It includes two main components: MyIoTService for backend management and DeviceEmulator for device behavior simulation.

## Features
- **Device Management**: Enables registration and management of IoT devices.
- **User Interaction**: Handles user accounts and provides a seamless interaction experience.
- **API Integration**: Offers secure RESTful APIs for efficient device communication.
- **Simulation**: DeviceEmulator simulates real-world IoT device behavior, crucial for testing MyIoTService functionalities.
- **Scalable Architecture**: Designed to efficiently process and manage data from a vast number of devices.

## Installation

### Prerequisites
- .NET 8 or later
- Visual Studio or similar IDE
- SQL Server Express LocalDB

### Getting Started
1. Clone the repository:
```
git clone https://github.com/eiramada/CodeShowcase.git
``` 
2. Navigate to the IoTConnectHub project directory.
3. Restore NuGet packages and build the project:
```
dotnet restore
dotnet build
``` 


### Setting Up
1. Update `appsettings.json` with your database connection string.
2. Apply the database schema using migrations:
```
dotnet ef database update
``` 

### Running the Application
- Launch MyIoTService and DeviceEmulator from your IDE.
- Access MyIoTService's Swagger UI for API documentation and testing.

## DeviceEmulator
Simulates real IoT device behaviors, supporting device registration, data generation, and user-device interactions.

### Usage
- Run alongside MyIoTService for full functionality testing.

## Notes and Limitations
- **User Experience**: Prioritizes backend functionality; front-end design is minimal.
- **Error Handling**: Basic; restart recommended on encountering errors.
- **Future Enhancements**: Include UX improvements, advanced error handling, and comprehensive unit testing.

