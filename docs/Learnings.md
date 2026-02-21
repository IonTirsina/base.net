# Database

- Repository just makes the mutations, but does not commit the transaction to database.
- Unit of work is used for that. Make the transactions atomic.
- Commit once so you can roll them back.

# EF Core
- Don't use records since Change Tracking uses references for comparison
