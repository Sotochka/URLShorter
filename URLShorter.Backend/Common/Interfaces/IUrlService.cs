using URLShorter.Backend.Models.DTOs.UrlDto;
using URLShorter.Backend.Models.Entities;

namespace URLShorter.Backend.Common.Interfaces;

public interface IUrlService
{
    Task<Url> GetUrlById(Guid id);
    Task<string> CreateUrl(CreateUrlDto createUrlDto, Guid userId);
    Task<Url> GetUrlByOriginalUrl(string shortUrl);
    Task DeleteUrl(Guid id);
}