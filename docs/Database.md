1. Write operations → EF Core
2. Read operations → Dapper
3. Transactions / commits → delegated to UnitOfWork
4. Domain invariants → enforced in factory methods and value objects


Best practices:
1. Never call SaveChangesAsync in repositories. Always use UnitOfWork.
2. 
