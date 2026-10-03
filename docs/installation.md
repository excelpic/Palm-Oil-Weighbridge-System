# Installation Guide

## Requirements
- Windows
- Visual Studio with .NET Framework 4.8 desktop development support
- Microsoft SQL Server or SQL Server Express
- NuGet package restore support
- Compatible weighbridge indicator for live serial-port testing
- Printer compatible with the required ticket workflow

## Setup
1. Clone the repository.
2. Open SistemTimbanganPKS.slnx in Visual Studio.
3. Restore NuGet packages referenced by packages.config.
4. Review the local database configuration.
5. Build the solution.
6. Run the application and complete initial administrator setup when no users exist.

## Database
Reference scripts are provided under database/. Review schema.sql and stored-procedures.sql. The application also contains database setup and lightweight migration logic.

## Runtime Validation
Hardware-dependent components require testing in a compatible Windows environment, including serial input, RDLC rendering, printer output, and installer behavior.