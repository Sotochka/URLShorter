using URLShorter.Backend.Data;

namespace URLShorter.Backend.Repositories;

public interface IUnitOfWork
{
    public Task SaveChangesAsync();
}