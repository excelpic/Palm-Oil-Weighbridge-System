# Configuration

## Database Defaults
The portfolio snapshot defaults to Windows Authentication.
- Server: .\SQLEXPRESS
- Database: sistemTimbanganPKS
- Authentication: Windows Authentication

These are example application defaults and may be changed for the target installation.

## Local Credential Storage
Local database settings are stored outside the repository at:

%ProgramData%\WeighBridge\db_config.txt

When SQL Authentication is used, the credential payload is protected with Windows DPAPI using the current Windows user scope.

## Repository Rule
Do not place real passwords, access tokens, API keys, or production connection strings in source files or documentation.