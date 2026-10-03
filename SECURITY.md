# Security Policy

## Scope

This repository is a public portfolio snapshot of the Palm Oil Weighbridge Management Software (Sistem Timbangan PKS).

## Sensitive Information

Do not commit any of the following:

- SQL usernames or passwords
- API keys, access tokens, or private keys
- Production database backups (`.bak`, `.mdf`, `.ldf`)
- Real customer, supplier, transporter, driver, or transaction records when they are confidential
- Local `db_config.txt` files
- Internal infrastructure secrets or credentials

The repository `.gitignore` excludes common local database/configuration files, but contributors must still review changes before committing.

## Local Database Credentials

When SQL Authentication is used, the application stores the credential payload using Windows DPAPI with `DataProtectionScope.CurrentUser`. The protected local configuration file is not part of the repository.

## Password Storage

New passwords are stored using PBKDF2-HMAC-SHA256 with per-password random salts. Legacy unsalted SHA-256 hashes are accepted only for migration and are upgraded after a successful login.

## Reporting a Vulnerability

For this portfolio repository, please do not publish sensitive exploit details together with credentials or private data. Contact the project author through the GitHub profile associated with this repository before disclosure where practical.
