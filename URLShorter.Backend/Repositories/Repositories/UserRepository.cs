using Microsoft.EntityFrameworkCore;
using URLShorter.Backend.Data;
using URLShorter.Backend.Data.Entities;
using URLShorter.Backend.Repositories.Interfaces;

namespace URLShorter.Backend.Repositories.Repositories;

public class UserRepository(UrlShorterDbContext dbContext) : IUserRepository
{
    public async Task<User> GetUserByEmail(string userEmail)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(user => user.Email == userEmail);
        if(user is null) throw new Exception("User not found");
        return user;
    }
    public void Insert(User user)
    {
        dbContext.Users.Add(user);
    }

    public void Remove(User user)
    {
        dbContext.Users.Remove(user);
    }
}