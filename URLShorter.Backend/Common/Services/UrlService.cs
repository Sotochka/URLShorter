using URLShorter.Backend.Common.Interfaces;
using URLShorter.Backend.Models.DTOs.UrlDto;
using URLShorter.Backend.Models.Entities;
using URLShorter.Backend.Repositories.Interfaces;

namespace URLShorter.Backend.Common.Services;

public class UrlService(IUrlRepository urlRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : IUrlService
{
    public async Task<UrlResponseDto> GetUrlById(Guid id)
    {
        var url = await urlRepository.GetUrlById(id);

        var urlDto = new UrlResponseDto(
            url.Id,
            url.OriginalURL,
            $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}/url/api/{url.ShortenedURL}",
            url.CreatedAt,
            url.UserId);
        
        return urlDto;
    }

    public async Task<IEnumerable<UrlResponseDto>> GetAllUrls()
    {
        var urls = await urlRepository.GetAllUrls();

        return urls.Select(url => new UrlResponseDto(
            url.Id,
            url.OriginalURL,
            $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}/url/api/{url.ShortenedURL}",
            url.CreatedAt, url.UserId)).ToList();
    }

    public async Task<UrlResponseDto> GetUrlByOriginalUrl(string shortUrl)
    {
        var url = await urlRepository.GetUrlByShortUrl(shortUrl);
        
        var urlDto = new UrlResponseDto(
            url.Id,
            url.OriginalURL,
            $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}/url/api/{url.ShortenedURL}",
            DateTime.UtcNow,
            url.UserId);
        
        return urlDto;
    }
    public async Task<UrlResponseDto> CreateUrl(CreateUrlDto createUrlDto, Guid userId)
    {
        var existedUrl = await urlRepository.GetUrlByOriginalUrl(createUrlDto.OriginalUrl);
        
        if (existedUrl != null)
        {
            throw new Exception("Url already exists");
        }
        
        var shortUrl = GenerateShortUrl(createUrlDto.OriginalUrl);
        
        var url = new Url
        {
            OriginalURL = createUrlDto.OriginalUrl,
            ShortenedURL = shortUrl,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };
        
        var urlDto = new UrlResponseDto(
            url.Id,
            url.OriginalURL,
            $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}/url/api/{url.ShortenedURL}",
            DateTime.UtcNow,
            url.UserId);
        
        urlRepository.Insert(url);
        
        await unitOfWork.SaveChangesAsync();
        
        return urlDto;
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