# Inventory Calculator

This project is built with .NET 7.0 and it's an executable application.

## Project Structure

The project is organized into several folders:

- `Data`: This folder contains all data-related classes and configurations.
  - `Configurations`: This subfolder contains configuration classes for the database context.
  - `Models`: This subfolder contains model classes which are entities in the database.

- `Services`: This folder contains service classes, including the implementation of the `IProductCalculatorService` interface. This is where the business logic of the application resides.

- `Program.cs`: This is the entry point of the application.

- `Tests`: This is a separate project that contains unit tests for different scenarios.

## Getting Started

To run this project, you will need .NET 7.0 installed on your machine. Once you have that, you can clone this repository and run the project using the `dotnet run` command in the terminal.

## Contributing

If you want to contribute to this project, please feel free to fork the repository, make your changes, and then submit a pull request.