using URLShorter.Backend.Data;

namespace URLShorter.Backend.Repositories.Repositories;

public class UnitOfWork(UrlShorterDbContext dbContext) : IUnitOfWork
{
    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}