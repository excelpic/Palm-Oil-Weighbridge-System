# Development Notes

## Source Organization
- Classes/ contains shared services and application logic.
- Forms/ contains Windows Forms UI.
- Reports/ contains RDLC report definitions.
- database/ contains SQL reference scripts.
- My Project/ contains VB.NET project settings and resources.
- SqlServerTypes/ contains SQL Server spatial runtime support.

## Development Practices
- Parameterize application-supplied SQL values.
- Keep password hashing centralized.
- Protect local credential storage.
- Use controlled user-facing error messages.
- Add audit logging for relevant operations.
- Prefer behavior-preserving refactoring.

## Feature Changes
Review the affected workflow and database objects, keep inputs parameterized, avoid secrets in source control, update documentation, and verify on Windows before deployment.