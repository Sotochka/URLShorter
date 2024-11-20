using Microsoft.EntityFrameworkCore;
using URLShorter.Backend.Data;
using URLShorter.Backend.Data.Entities;
using URLShorter.Backend.Repositories.Interfaces;

namespace URLShorter.Backend.Repositories.Repositories;

public class UrlRepository(UrlShorterDbContext dbContext) : IUrlRepository
{
    public void Insert(Url url)
    {
        dbContext.Urls.Add(url);
    }

    public void Remove(Url url)
    {
        dbContext.Urls.Remove(url);
    }
}