# Known Limitations

This portfolio repository is a cleaned and security-hardened snapshot, not a full rewrite of the application architecture.

## Runtime Validation

A Windows/.NET Framework 4.8 environment is required to verify the final build and runtime behavior. Live serial weighing, printer output, and SQL Server behavior cannot be reproduced by static inspection alone.

## UI / Service Boundaries

Some business workflow orchestration remains inside Windows Forms classes. A future architectural iteration could extract additional application services and repositories to improve unit-testability.

## Ticket Number Concurrency

Ticket numbering is generated from existing transaction values. The database enforces uniqueness, but high-concurrency deployments could still require a stronger sequence/transaction strategy.

## Local Credential Scope

DPAPI protection uses the current Windows user scope. This improves confidentiality but means protected SQL credentials are not portable between different Windows user profiles.

## Third-Party Components

Some dependencies are retained because the existing application relies on them for reporting, PDF/spreadsheet export, or SQL Server native type support. Their licensing and redistribution terms remain independent from this repository's no-license status.
