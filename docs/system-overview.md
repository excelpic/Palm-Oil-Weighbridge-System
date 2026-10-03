# System Overview

## Domain Context

In a **PKS (Pabrik Kelapa Sawit)**, the weighbridge records the gross/tare-related measurements associated with vehicles moving palm-oil products and related commodities. The application centralizes these transactions in a desktop workflow.

## Main Modules

### Authentication

Users sign in through `FormLogin`. The application establishes `UserSession` state and applies role and transaction-type access rules.

### Weighbridge Transactions

`FormInputTimbangan`, `FormEditTimbangan`, and `FormDaftarTimbangan` manage the weighing transaction lifecycle.

### Master Data

`FormMasterData` supports application master data used by weighing transactions, including products, customers, and transporters.

### Reporting

`FormLaporan` and the RDLC report definition provide transaction reporting and export workflows.

### Printing

`PrintHelper` centralizes ticket/receipt printing logic, including thermal-print workflows used by the weighbridge operation.

### Serial Communication

`SerialPortHelper` encapsulates serial-port communication with configurable weighbridge indicators and protocols.

### Audit Logging

`AuditLog` and the database audit table provide a history of important user and transaction activities.

### Settings

`FormPengaturan` and `SettingsHelper` manage application settings such as serial configuration, printer configuration, and related operational preferences.
