# MS Identity WebApp and WebAPI

This repository contains a solution that demonstrates the integration of Microsoft Identity with a **Web Application** and a **Web API**. The solution is built using ASP.NET Core and showcases how to use the end user identity end-to-end using Entra.

## Disclaimer

This project is provided as-is without any warranty or support. It is intended to be used as a sample implementation and may not be suitable for production use. Please review the code and make any necessary changes to meet your requirements.

## Features

- **Web Application**: An ASP.NET Core MVC web application that uses Microsoft Identity for authentication.
- **Web API**: An ASP.NET Core Web API that is secured using Microsoft Identity.
- **Authentication and Authorization**: Demonstrates how to authenticate users and authorize access to the web API using Entra tokens. The sample showcase how to use the `Microsoft.Identity` library to authenticate users and pass its identity using the `On-Behalf Off` flow to the Web API and Microsoft Graph.

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Getting Started

### 1. Clone the repository

```powershell
git clone https://github.com/your-repo/ms-identity-webapp-webapi.git
cd ms-identity-webapp-webapi
```

### 2. Configure Entra

Register two applications in the Entra portal - one for the web app and one for the web api.
* Make sure you add the redirect URI for both. In this sample, we used `https://localhost:7197/signin-oidc` for the web app and `https://localhost:7284/signin-oidc` for the web api.
* For the web api, make sure you add the scope `api://<web-api-client-id>/school.read` to the app registration in the "Expose an API" section.
* Once the scope has been added in your web api app registration, add a permission for your custom API scope `school.read` to the web app app registration in the "API permissions" section.
* The web app should also have "User.Read" delegated permission.
* The web api should have "User.Read" and "Group.Read.All" delegated permissions.
* Make sure you grant admin consent for the permissions.
    
### 3. Configure the Solution
1. Configure the appsettings.json files in both the web app and web api projects with the Entra configuration values.

```json
{
  "AzureAd": {
    "Instance": "https://login.microsoftonline.com/",
    "TenantId": "your-tenant-id",
    "ClientId": "your-client-id",
    "ClientSecret": "your-client-secret",
    "CallbackPath": "/signin-oidc",
  }
}
```
3. Configure the appsettings.json file in the web app project with the reference to the web api.

```json
{
  "SchoolApi": {
    "BaseUrl": "https://localhost:7260/School",
    "Scopes": ["api://<web-api-client-id>/.default"]
  },
}
```

### 4. Run the Solution

Navigate to the project directory and run the following commands:

```powershell
# Run the Web API
cd WebAPI
dotnet debug --launch-profile https

# Run the Web App
cd ../WebApp
dotnet run --launch-profile https
```
Alternatively, you can run the solution using Visual Studio Code.  Go to the Solution Explorer, right-click on each project (starting with the WebApi), and under Debug, start a new instance.

## Project Structure

- **WebApp**: Contains the ASP.NET Core MVC web application.
- **WebAPI**: Contains the ASP.NET Core Web API.

## Contributing

Contributions are welcome!

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more information.

## Resources

- [Microsoft Identity Platform Documentation](https://docs.microsoft.com/en-us/azure/active-directory/develop/)
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
