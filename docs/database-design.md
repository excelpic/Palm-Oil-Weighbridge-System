# Database Design

## Core Tables

### `Users`
Stores application accounts, password hashes, roles, transaction-type access scope, and audit timestamps.

### `Customers`
Master data for the customer/owner associated with a weighing transaction.

### `Transporters`
Master data for transport companies or transport parties associated with vehicle movements.

### `Products`
Master data for commodities handled by the weighbridge workflow, including CPO and other PKS-related products.

### `Timbangan`
The primary transaction table. It stores vehicle information, weigh-in/weigh-out values, net/deduction calculations, transaction type, delivery/order references, quality fields, responsible operators, timestamps, and print count.

### `AuditLog`
Stores important application activity with the acting user, action, target table/record, descriptive values, IP address, computer name, and timestamp.

### `Settings`
Stores application preferences that are configurable at runtime, such as printer, serial communication, company profile, and transaction-related settings.

## Relationship Model

The application keeps some denormalized display fields inside `Timbangan` (for example `CustomerNama`, `TransporterNama`, and `ProductNama`) so historical transactions retain the displayed values even when master data later changes.

The current database design is therefore intentionally hybrid: identifier columns provide relationships to master records while snapshot/display fields preserve transaction context.

## Integrity Controls

- Unique username constraint on `Users.Username`
- Unique ticket number on `Timbangan.NoTiket`
- Filtered unique index on `Timbangan.NoDO` when `NoDO` is populated
- `IsActive` flags on master and user records
- Default values for transaction status and numeric calculation fields
- Runtime compatibility migrations for older database versions

## Reference Scripts

See [`../database/schema.sql`](../database/schema.sql) and [`../database/stored-procedures.sql`](../database/stored-procedures.sql).
