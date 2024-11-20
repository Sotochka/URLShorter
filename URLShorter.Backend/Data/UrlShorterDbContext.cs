using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using URLShorter.Backend.Data.Entities;

namespace URLShorter.Backend.Data;

public class UrlShorterDbContext(DbContextOptions<UrlShorterDbContext> options) : DbContext(options)
{
    public DbSet<Url> Urls { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Role> roles =
        [
            new Role
            {
                Id = 1,
                Name = "Admin",
            },

            new Role
            {
                Id = 2,
                Name = "User",
            },
        ];
        modelBuilder.Entity<Url>().ToTable("urls");
        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<Role>().ToTable("roles").HasData(roles);
    }
}