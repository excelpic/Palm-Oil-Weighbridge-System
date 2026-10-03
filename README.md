# Palm Oil Weighbridge Management Software

A desktop-based Palm Oil Weighbridge Management Software for managing vehicle weighing, CPO transactions, ticket printing, reporting, and audit records using VB.NET and Microsoft SQL Server.

> **Sistem Timbangan PKS** is an Indonesian domain application for **Pabrik Kelapa Sawit (PKS)**, a Palm Oil Mill. The application is designed around the operational workflow of vehicle weighing, transaction recording, and ticket/report generation.

## Overview

This project is a Windows Forms application developed in **VB.NET** for weighbridge operations at a Palm Oil Mill. It supports the recording of incoming and outgoing vehicles, master data management, weighing calculations, ticket printing, reporting, user access control, and audit logging.

The repository is published as a **portfolio software project** to demonstrate practical desktop application development, database integration, hardware communication, reporting, security hardening, and maintainable source organization.

## Key Features

- Vehicle weigh-in and weigh-out transaction management
- Automatic net-weight and deduction calculations
- Product, customer, transporter, and vehicle-related master data workflows
- Serial/COM-port integration with configurable weighbridge indicators
- Ticket and receipt printing, including thermal printer workflows
- Daily transaction reporting with RDLC / ReportViewer
- Export workflows for reporting data
- User authentication and role-based transaction access
- Audit logging for important application activities
- Automatic database initialization and lightweight schema migrations
- Local configuration with Windows DPAPI protection for SQL Authentication credentials
- Parameterized SQL commands for application-supplied values
- PBKDF2-HMAC-SHA256 password hashing for newly created or upgraded credentials

## Technology Stack

| Area | Technology |
| --- | --- |
| Language | VB.NET |
| UI | Windows Forms |
| Framework | .NET Framework 4.8 |
| IDE | Microsoft Visual Studio |
| Database | Microsoft SQL Server |
| Database Access | ADO.NET / `System.Data.SqlClient` |
| Reporting | RDLC / Microsoft ReportViewer |
| Weighbridge Integration | Serial / COM Port |
| Printing | Windows printers and thermal printing workflows |
| Spreadsheet Export | EPPlus |
| PDF Export | iTextSharp |
| Cryptography | .NET cryptography APIs / PBKDF2 / DPAPI |

## System Workflow

```text
Vehicle Arrival
      |
      v
Vehicle / Transaction Data Entry
      |
      v
First Weighing (Berat Masuk)
      |
      v
Loading / Unloading Process
      |
      v
Second Weighing (Berat Keluar)
      |
      v
Net Weight Calculation
      |
      v
Transaction Validation
      |
      +------> Ticket / Receipt Printing
      |
      +------> Database Record
      |
      +------> Audit Trail
      |
      +------> Reporting / Export
```

## Project Structure

```text
palm-oil-weighbridge-system/
├── Classes/                    # Shared application services and domain logic
├── Forms/                      # Windows Forms application UI
├── Reports/                    # RDLC report definitions
├── SqlServerTypes/             # SQL Server spatial runtime support
├── My Project/                 # VB.NET project metadata and resources
├── database/                   # Human-readable database scripts/documentation
├── docs/                       # Architecture, setup, security, and development docs
│
├── App.config
├── SistemTimbanganPKS.vbproj
├── SistemTimbanganPKS.slnx
├── SetupTimbanganPKS.iss
├── packages.config
└── README.md
```

## Database

The application uses **Microsoft SQL Server**. A first-run setup can create the core database objects and apply lightweight compatibility migrations.

Core entities include:

- `Users`
- `Customers`
- `Transporters`
- `Products`
- `Timbangan`
- `AuditLog`
- `Settings`

The repository contains schema and stored-procedure documentation under [`database/`](database/).

**Data policy:** the public repository does not intentionally contain production transactions, customer records, passwords, or database backups. Master-data examples are generic/demo values.

## Configuration

The application stores its local database connection settings outside the source repository. When SQL Authentication is used, the username/password payload is protected using Windows **DPAPI** with the current Windows user scope.

The local configuration file is intentionally ignored by Git:

```text
%ProgramData%\WeighBridge\db_config.txt
```

Windows Authentication is the default mode in the portfolio version.

See [`docs/configuration.md`](docs/configuration.md) for details.

## Initial Administrator Setup

A clean database does **not** receive a hardcoded default administrator password.

During first use, the application prompts the operator to create the initial administrator account. New passwords are validated and stored using the application's PBKDF2 password-hashing implementation.

## Security Notes

The portfolio snapshot has been prepared to avoid publishing embedded credentials or production data. Security-related changes include:

- No hardcoded SQL password in source code
- No default programmer password in source code
- PBKDF2 password hashing for new credentials
- Legacy SHA-256 password migration on successful authentication
- Windows DPAPI protection for locally stored SQL Authentication credentials
- Parameterized SQL for user-controlled values
- Restricted transaction-type filtering
- Generic user-facing database/authentication errors with technical diagnostics kept in debug output

This repository is still a desktop application codebase and should undergo environment-specific security review before deployment in a production organization.

See [`SECURITY.md`](SECURITY.md) and [`docs/security.md`](docs/security.md).

## Code Quality & Security Hardening

The portfolio snapshot includes a focused cleanup pass covering parameterized database values, safe local filtering, password storage, local credential protection, exception handling, and repository hygiene. The source was reviewed without performing a high-risk architectural rewrite that could change application behavior.

See [`docs/development.md`](docs/development.md) and [`docs/final-review.md`](docs/final-review.md) for the review scope and remaining verification boundary.

## Development

Requirements for local development include:

- Windows
- Visual Studio with .NET Framework 4.8 desktop development support
- Microsoft SQL Server / SQL Server Express
- Required NuGet dependencies restored from `packages.config`
- A compatible weighbridge indicator if testing live serial input
- Printers required by the relevant ticket/receipt workflow

See [`docs/installation.md`](docs/installation.md) and [`docs/development.md`](docs/development.md).

## Runtime Verification Boundary

The source has been statically reviewed and repository hygiene has been checked, but hardware- and Windows-specific runtime validation still needs to be performed in a compatible local environment. In particular, the following require actual runtime testing:

- Visual Studio build on Windows with .NET Framework 4.8
- SQL Server connection and database initialization
- Serial-port communication with the weighbridge indicator
- RDLC report rendering
- Printer and thermal-print output
- Installer/runtime deployment behavior

## Portfolio & License Notice

This repository is **public for portfolio and evaluation purposes** and currently contains **no open-source license**. Copyright remains with the project author. No permission is granted by this repository to reuse, redistribute, or create derivative works from the project source beyond rights provided by applicable law or separately granted permission.

Third-party dependencies remain subject to their own licenses and terms. See [`docs/third-party-dependencies.md`](docs/third-party-dependencies.md).

## Project Status

**Portfolio preparation:** security-hardened source snapshot with documentation and repository hygiene improvements.

The project remains suitable for continued functional testing, maintenance, and incremental refactoring.
