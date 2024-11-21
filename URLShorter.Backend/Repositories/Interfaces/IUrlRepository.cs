using URLShorter.Backend.Models.Entities;

namespace URLShorter.Backend.Repositories.Interfaces;

public interface IUrlRepository
{
    public Task<Url> GetUrlByShortUrl(string shortUrl);
    public Task<Url> GetUrlByOriginalUrl(string originalUrl);
    public Task<Url> GetUrlById(Guid id);
    public void Insert(Url url);
    public void Remove(Url url);
}