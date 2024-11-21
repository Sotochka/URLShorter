using URLShorter.Backend.Models.Entities;

namespace URLShorter.Backend.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<User> GetUserByEmail(string userEmail);
    public void Insert(User user);
    public void Remove(User user);
}