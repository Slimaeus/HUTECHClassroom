# HUTECH Classroom

This project is a <u>web API</u> for managing classroom data. It is built using .NET Framework, C#, and follows _Clean Architecture_, the Mediator pattern, the _Repository pattern_ with a _Unit of Work_ implementation for data access. **JWT authentication and authorization** have also been implemented to ensure secure access to the API.

## Getting Started

To get started with this project, you will need to have Visual Studio installed on your computer. Once you have cloned the repository, you can open the solution file in Visual Studio and run the project.

## Prerequisites

Visual Studio/ Visual Studio code
.NET Framework

## Installing

Clone the repository to your local machine.

Open the solution file in Visual Studio.

Update connection settings in `appsettings.json`.

Build and run the project or `dotnet restore`.

## Usage

This API allows you to manage student data. You can create, read, update, and delete student records using the endpoints provided.

## Endpoints

#### Mission

- 🔎GET api/v1/Missions - Returns a list of all missions.
- 🔍GET api/v1/Missions/{id} - Returns a single mission record by ID.
- ➕POST api/v1/Missions - Creates a new mission record.
- ✏️PUT api/v1/Missions/{id} - Updates an existing mission record by ID.
- 🗑️DELETE api/v1/Missions/{id} - Deletes a mission record by ID.

#### Account

- ➕POST api/v1/Account/Register - Registers a new user.
- 👤POST api/v1/Account/Login - Logs in a user and returns a JWT token.
- 🔍GET api/v1/User - Returns information about the currently logged in user.

## Authentication

This API uses JWT authentication to secure access to the endpoints. You will need to provide a valid token in the Authorization header to access the endpoints.

## Authorization

Authorization has also been implemented for certain endpoints. Only users with the appropriate role can access these endpoints. Please refer to the documentation for more information on the required roles.

## Contributing

If you find this project useful, please give it a stars⭐⭐⭐! Contributions are also welcome. Please fork the repository and submit a pull request with your changes.

## Authors

Nguyễn Hồng Thái

## License

This project is licensed under the [MIT License](https://opensource.org/licenses/MIT). See the LICENSE.md file for details.
