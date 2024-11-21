using System.Text;
using URLShorter.Backend.Common.Interfaces;
using URLShorter.Backend.Models.DTOs.UrlDto;
using URLShorter.Backend.Models.Entities;
using URLShorter.Backend.Repositories.Interfaces;

namespace URLShorter.Backend.Common.Services;

public class UrlService(IUrlRepository urlRepository, IUnitOfWork unitOfWork) : IUrlService
{
    public async Task<Url> GetUrlById(Guid id)
    {
        var url = await urlRepository.GetUrlById(id);
        return url;
    }

    public async Task<Url> GetUrlByOriginalUrl(string shortUrl)
    {
        var url = await urlRepository.GetUrlByShortUrl(shortUrl);
        return url;
    }
    public async Task<string> CreateUrl(CreateUrlDto createUrlDto, Guid userId)
    {
        var existedUrl = await urlRepository.GetUrlByOriginalUrl(createUrlDto.OriginalUrl);
        if (existedUrl is not null)
        {
            return existedUrl.ShortenedURL;
        }
        var shortUrl = GenerateShortUrl(createUrlDto.OriginalUrl);
        var url = new Url
        {
            OriginalURL = createUrlDto.OriginalUrl,
            ShortenedURL = shortUrl,
            CreatedAt = DateTime.UtcNow,
            UserId = userId
        };
        urlRepository.Insert(url);
        await unitOfWork.SaveChangesAsync();
        return shortUrl;
    }

    public async Task DeleteUrl(Guid id)
    {
        var url = await urlRepository.GetUrlById(id);
        if (url is null)
        {
            throw new Exception("Url not found");
        }
        urlRepository.Remove(url);
        await unitOfWork.SaveChangesAsync();
    }

    private static string GenerateShortUrl(string longUrl)
    {
        const string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        const int shortUrlLength = 7;
        
        while(true)
        {
            var shortUrl = new char[shortUrlLength];
            var random = new Random();

            for (var i = 0; i < shortUrlLength; i++)
            {
                var randomIndex = random.Next(0, characters.Length - 1);
                shortUrl[i] = characters[randomIndex];
            }

            var code = new string(shortUrl);

            if (shortUrl.ToString() != longUrl)
            {
                return code;
            }
        }
    }

}   