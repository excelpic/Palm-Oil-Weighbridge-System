# Database Reference

The application uses Microsoft SQL Server. These scripts provide a human-readable reference for the database objects created and maintained by the application.

- [`schema.sql`](schema.sql) contains the core tables, defaults, demo product master data, and the filtered unique index for `NoDO`.
- [`stored-procedures.sql`](stored-procedures.sql) contains the ticket-number and NoDO validation procedures.

The application also includes migration logic in `Classes/DatabaseMigration.vb` so that an existing installation can be brought forward without replacing the database manually.

No production database backup or production transaction data is included in this repository.
