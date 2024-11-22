using URLShorter.Backend.Models.DTOs.UrlDto;
using URLShorter.Backend.Models.Entities;

namespace URLShorter.Backend.Common.Interfaces;

public interface IUrlService
{
    Task<UrlResponseDto> GetUrlById(Guid id);
    Task<IEnumerable<UrlResponseDto>> GetAllUrls();
    Task<UrlResponseDto> CreateUrl(CreateUrlDto createUrlDto, Guid userId);
    Task<UrlResponseDto> GetUrlByOriginalUrl(string shortUrl);
    Task DeleteUrl(Guid id);
}