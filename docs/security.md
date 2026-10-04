# Security Review Notes

## Implemented Controls
- Hardcoded SQL credentials and default login credentials were removed from the public source.
- New and updated passwords use PBKDF2-HMAC-SHA256 with per-password random salts.
- Legacy unsalted SHA-256 credentials can be upgraded after successful authentication.
- SQL Authentication credential payloads are protected locally with Windows DPAPI.
- Application-supplied database values use SQL parameters where applicable.
- User-facing database errors are generalized while technical diagnostics remain in debug output.

## Never Commit
- Passwords
- API keys or access tokens
- Private keys
- Production database backups
- Confidential customer, supplier, driver, or transaction records
- Local credential configuration files

## Verification Boundary
Static review does not replace runtime security testing, access-control review, dependency review, and production operational hardening.