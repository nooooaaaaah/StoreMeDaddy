* * *

StoreMeDaddy: A File Server Application
=======================================

Welcome to StoreMeDaddy, a lightweight file server application built on top of the .NET Core platform. This application serves as a secure file storage system with JWT-based authentication.

Features
--------

*   User authentication and authorization using JWT tokens.
*   Secure file storage with access restrictions.
*   Configurable token settings.
*   Intuitive and modularized codebase for easy extension.

Getting Started
---------------

### Prerequisites

*   .NET 7 SDK 
*   A modern IDE, preferably Visual Studio or Visual Studio Code.

### Installation

1.  Clone the repository:

bashCopy code

`git clone https://github.com/yourusername/StoreMeDaddy.git`

2.  Navigate to the project directory:

bashCopy code

`cd StoreMeDaddy`

3.  Restore the NuGet packages:

bashCopy code

`dotnet restore`

### Configuration

Before running the application, ensure that the `appsettings.json` file is properly configured.

jsonCopy code

`{   "TokenSettings": {       "SecretKey": "YOUR_SECRET_KEY",       "Issuer": "YOUR_ISSUER",       "ExpiryMinutes": "TOKEN_EXPIRY_DURATION",       "Roles": "ALLOWED_ROLES"   } }`

Replace placeholders (`YOUR_SECRET_KEY`, `YOUR_ISSUER`, etc.) with appropriate values.

### Running the application

In the project directory:

bashCopy code

`dotnet run`

Your server will start, typically at `http://localhost:5000` or `https://localhost:5001`.

Endpoints
---------

*   **/api/auth**: Authenticate a user and obtain a JWT token.
*   **/api/files**: Store and retrieve files.

Contributing
------------

We welcome contributions! Please read our [CONTRIBUTING.md](./CONTRIBUTING.md) for details on how to submit pull requests.

License
-------

This project is licensed under the MIT License. See the [LICENSE.md](./LICENSE.md) file for details.
