using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using URLShorter.Backend.Data;
using URLShorter.Backend.Models.Entities;
using URLShorter.Backend.Repositories.Interfaces;

namespace URLShorter.Backend.Repositories.Repositories;

public class UrlRepository(UrlShorterDbContext dbContext) : IUrlRepository
{
    public async Task<Url> GetUrlByShortUrl(string shortUrl)
    {
        var url = await dbContext.Urls.FirstOrDefaultAsync(x => x.ShortenedURL == shortUrl);
        return url ?? throw new KeyNotFoundException("Url not found");
    }
    
    public async Task<Url> GetUrlByOriginalUrl(string originalUrl)
    {
        var url = await dbContext.Urls.FirstOrDefaultAsync(x => x.OriginalURL == originalUrl);
        return url ?? null;
    }

    public async Task<Url> GetUrlById(Guid id)
    {
        var url = await dbContext.Urls.FirstOrDefaultAsync(x => x.Id == id);
        return url ?? throw new KeyNotFoundException("Url not found");        
    }

    public void Insert(Url url)
    {
        dbContext.Urls.Add(url);
    }

    public void Remove(Url url)
    {
        dbContext.Urls.Remove(url);
    }

    public async Task<IEnumerable<Url>> GetAllUrls()
    {
        var urls = await dbContext.Urls.ToListAsync();
        return urls;
    }
}