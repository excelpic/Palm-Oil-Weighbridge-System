# System Architecture

Palm Oil Weighbridge Management Software is a Windows Forms desktop application built with VB.NET and .NET Framework 4.8.

## Main Components
- Forms: Windows Forms UI, validation, and workflow orchestration.
- Classes: database access, authentication, printing, serial communication, settings, audit logging, and weighing logic.
- database: SQL Server reference scripts.
- Reports: RDLC report definitions.
- SqlServerTypes: SQL Server spatial runtime support.

## Core Workflow
1. Identify the vehicle and transaction context.
2. Capture the first weighing (Berat Masuk).
3. Complete the loading or unloading process.
4. Capture the second weighing (Berat Keluar).
5. Calculate net weight and applicable deductions.
6. Validate and save the transaction.
7. Print the ticket or receipt where required.
8. Record audit information and expose the transaction through reporting.

## Integration
- SQL Server via ADO.NET and System.Data.SqlClient.
- Weighbridge indicator via serial/COM-port communication.
- RDLC reporting via Microsoft ReportViewer.
- Windows and thermal printing workflows.

## Architecture Boundary
This repository documents the practical architecture of the existing desktop system. It avoids a high-risk rewrite solely for portfolio presentation and keeps the original solution structure recognizable.