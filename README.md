# Palm Oil Weighbridge Management Software

A desktop-based **Palm Oil Weighbridge Management Software** for managing vehicle weighing, CPO and commodity transactions, ticket printing, reporting, user access, and audit records using **VB.NET, Windows Forms, Microsoft SQL Server, Visual Studio, and SSMS**.

> **Sistem Timbangan PKS** is an Indonesian domain application for **Pabrik Kelapa Sawit (PKS)**, a Palm Oil Mill. The system is designed around real-world weighbridge operations, including vehicle identification, first and second weighing, net-weight calculation, transaction recording, ticket generation, and reporting.

## Overview

This project is a Windows Forms desktop application developed in **VB.NET** for Palm Oil Mill weighbridge operations. It demonstrates practical software engineering across:

- desktop application development
- SQL Server database integration
- serial/COM-port hardware communication
- transactional business workflows
- ticket and thermal printing
- RDLC reporting and export
- user access control and audit logging
- configuration, access control, and security-focused engineering

The repository is published as a **portfolio software project**. Production deployment requires environment-specific configuration and validation.

## Key Features

- Vehicle weigh-in and weigh-out transaction management
- Automatic net-weight and deduction calculations
- Product, customer, transporter, and vehicle master-data workflows
- Serial/COM-port integration with configurable weighbridge indicators
- Ticket and receipt printing, including thermal-printer workflows
- Daily transaction reporting with RDLC / ReportViewer
- Export workflows for reporting data
- User authentication and role-based transaction access
- Audit logging for selected application and administrative activities
- Automatic database initialization and lightweight schema migrations
- Protected local configuration for database connectivity
- Parameterized SQL for application-supplied values
- PBKDF2-based password hashing for new and updated application credentials

## Technology Stack

| Area | Technology |
| --- | --- |
| Language | VB.NET |
| UI | Windows Forms |
| Framework | .NET Framework 4.8 |
| IDE | Microsoft Visual Studio |
| Database | Microsoft SQL Server / SQL Server Express |
| Database Access | ADO.NET / System.Data.SqlClient |
| Reporting | RDLC / Microsoft ReportViewer |
| Weighbridge Integration | Serial / COM Port |
| Printing | Windows printers and thermal printing workflows |
| Spreadsheet Export | EPPlus |
| PDF Export | Microsoft Excel automation |
| Security | .NET cryptography and protected local configuration |

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
Palm-Oil-Weighbridge-System/
├── Classes/                    # Shared services and application logic
├── Forms/                      # Windows Forms application UI
├── Reports/                    # RDLC report definitions
├── SqlServerTypes/             # SQL Server spatial runtime support
├── My Project/                 # VB.NET project settings and resources
├── database/                   # SQL schema and stored procedures
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

The application uses **Microsoft SQL Server**.

Core entities include:

- Users
- Customers
- Transporters
- Products
- Timbangan
- AuditLog
- Settings

Reference schema and stored procedures are available under [database/](database/).

The public repository does not intentionally contain production transactions, confidential business records, or database backups.

## Security & Reliability

The portfolio snapshot includes security-focused improvements such as:

- removal of embedded application credentials
- PBKDF2-based password hashing for application credentials
- protected local database configuration
- parameterized SQL for application values
- generalized user-facing database errors
- repository hygiene that excludes local credentials and generated artifacts

For the public repository, security details are intentionally kept at a high level. See [SECURITY.md](SECURITY.md).

## Development

Requirements for local development include:

- Windows
- Visual Studio with .NET Framework 4.8 desktop development support
- Microsoft SQL Server / SQL Server Express
- NuGet package restore support
- a compatible weighbridge indicator for live serial input testing
- printers required by the relevant ticket workflow

See [docs/installation.md](docs/installation.md), [docs/configuration.md](docs/configuration.md), and [docs/development.md](docs/development.md).

## Runtime Verification

This repository has been statically reviewed for source organization, repository hygiene, and security-related issues. The following still require validation in a compatible Windows environment:

- Visual Studio build
- SQL Server connection and database initialization
- first-run administrator setup
- serial-port communication with the weighbridge indicator
- RDLC report rendering
- printer and thermal-print output
- installer behavior

## Portfolio & License Notice

This repository is **public for portfolio and evaluation purposes** and currently contains **no open-source license**. Copyright remains with the project author. No permission is granted to reuse, redistribute, or create derivative works from the source beyond rights provided by applicable law.

Third-party dependencies remain subject to their own licenses and terms. See [docs/third-party-dependencies.md](docs/third-party-dependencies.md).

## Project Status

**Portfolio-ready source snapshot** with baseline security improvements, repository hygiene, database documentation, and development documentation.

The project remains suitable for continued functional testing, maintenance, and incremental improvement.