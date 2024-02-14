# IoTConnectHub

## Introduction
IoTConnectHub solution demonstrates an approach to IoT (Internet of Things) device management and simulation. This project, developed with a focus on .NET technologies, showcases my ability to build scalable, efficient, and secure applications. It includes two main components: MyIoTService and DeviceEmulator, each playing its role in providing a IoT platform.

## MyIoTService

### Overview
MyIoTService is a .NET-based backend service designed to handle the complexities of IoT device management. It manages device data, user interactions, and provides secure API endpoints. The use of modern development practices and a clear, modular architecture in MyIoTService demonstrates my ability to create software that is both reliable and adaptable to future enhancements.

### Features
- Device Registration and Management
- User Account Handling
- RESTful API for Device Interaction
- Scalable Data Processing

### Getting Started

#### Prerequisites
- .NET 8 or later
- An IDE like Visual Studio
- SQL Server Express LocalDB. Download and install it from [Microsoft's Official Documentation](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16)

#### Installation
1. Download the ZIP file.
2. Extract the files to your preferred location.

#### Setting Up
1. Open `IoTConnectHub.sln` in your IDE and navigate to the MyIoTService project.
2. Restore the NuGet packages by right-clicking on the solution and selecting "Restore NuGet Packages."

#### Database Setup
If you have a local database set up, obtain your database's connection string.
  - Open the `appsettings.json` file in your project.
  - Find the `DefaultConnection` key and replace its value with your connection string.
    
#### Applying Migrations
To set up the database schema, you need to apply the initial migration. 
  - This can be done via the Package Manager Console in Visual Studio using the command: 
    `Update-Database`
  - Alternatively, you can use the .NET Core CLI with the command:
    `dotnet ef database update`

#### Build
In your IDE, set MyIoTService as the startup project.
Build the solution by selecting "Build Solution" from the Build menu.
Ensure the build completes without errors.

#### Running the Application
Run the application by pressing F5 or by clicking the "Run" button in your IDE.
The application should start, and you might see a web browser window open, depending on your project setup.

#### API Documentation
Refer to the integrated Swagger UI for detailed API documentation.

## DeviceEmulator

### Overview
DeviceEmulator complements MyIoTService by simulating real-world IoT device behavior. This component is crucial for testing and validating the functionality of MyIoTService. My development of DeviceEmulator highlights my understanding of the simulation in software development, ensuring reliability and robustness in the final product.

### Features
- Simulates real IoT device behavior
  - Register an User 
  - Login
  - Create a New Device
  - Connect with Excisting Device
  - Automatically adds Data (temperature) that is linked with user and device

### Getting Started

#### Installation & Setup
(Follow similar steps as MyIoTService for installation and setup.)

#### Build
Set DeviceEmulator as the startup project in your IDE.
Build the solution.
Ensure the build completes without errors.

#### Usage 
Run DeviceEmulator along with MyIoTService

## Notes and Limitations

### Common Issues & Troubleshooting
- **Build Errors**: Check if all NuGet packages are restored and if the .NET SDK version matches the project requirements.
- **Database Connections**: Ensure the database server is running and the connection string in the configuration file is correct.
- **API Testing**: If you're unable to access the API, ensure the web server is running and listening on the correct port.

### User Experience
- The primary focus of the IoTConnectHub solution is on backend functionality and data handling. As such, **the solution does not prioritize user experience (UX) design**. The front-end aspects were not a requirement for this project, and the interface is minimalistic and functional.

### Error Handling
- **Error Recovery**: In the current implementation, if the user encounters an error, the application does not include advanced error recovery mechanisms. It is recommended to **restart the application in case of an error**. This limitation is due to the project's scope focusing more on backend processes rather than comprehensive user interaction.

### Future Enhancements
- **Improving User Experience**: Future enhancements could include developing a more user-friendly interface to enhance the overall usability of the application.
- **Advanced Error Handling**: Implementing more sophisticated error handling and recovery mechanisms could be an area of improvement, providing a smoother user experience.
- **Unit Tests**
