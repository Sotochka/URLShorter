namespace URLShorter.Backend.Repositories.Interfaces;

public interface IUnitOfWork
{
    public Task SaveChangesAsync();
}