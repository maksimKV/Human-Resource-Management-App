# Human Resource Management App (HRM)

## Overview of Stack
- Server
  - ASP.NET Core
  - PostgreSQL
  - Entity Framework Core w/ EF Migrations
- Client
  - React
  - Webpack for asset bundling and HMR (Hot Module Replacement)
  - CSS Modules
  - Fetch API for REST requests

## Setup

1. Add your database settings in the appsettings.json as a connectionString.
2. Migrate database using Update-Database command.
3. For the React client run `npm install && npm start`

## Scripts

### `npm install`

When first cloning the repo or adding new dependencies, run this command.  This will:

- Install Node dependencies from package.json
- Install .NET Core dependencies from api/api.csproj(using dotnet restore)

### `npm start`

To start the app for development, run this command.
