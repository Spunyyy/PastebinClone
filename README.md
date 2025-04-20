# Pastebin Clone

A simple web application built with ASP.NET Core.

User authentication and authorization are handled using ASP.NET Identity.

Creating pastes is only available to authenticated users.

## Installation

Before running the application, you need to configure the database connection in `appsettings.json`
```json
  "DefaultConnection": "Host=ipAdress;Database=dbName;Username=user;Password=password"
```
