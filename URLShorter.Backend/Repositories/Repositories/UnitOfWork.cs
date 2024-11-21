using URLShorter.Backend.Data;
using URLShorter.Backend.Repositories.Interfaces;

namespace URLShorter.Backend.Repositories.Repositories;

public class UnitOfWork(UrlShorterDbContext dbContext) : IUnitOfWork
{
    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}