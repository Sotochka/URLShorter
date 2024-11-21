using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using URLShorter.Backend.Common.Interfaces;
using URLShorter.Backend.Models.DTOs.UrlDto;
using URLShorter.Backend.Models.Entities;
using URLShorter.Backend.Repositories.Interfaces;

namespace URLShorter.Backend.Controllers;

[ApiController]
[Route("url")]
public class UrlController(IUrlRepository urlRepository, IUnitOfWork unitOfWork, IUrlService urlService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUrlById(Guid id)
    {
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpPost("{create-url}")]
    public async Task<IActionResult> CreateUrl([FromBody] CreateUrlDto createUrlDto)
    {
        var userId = Validate(User);
        var url = await urlService.GetUrlByOriginalUrl(createUrlDto.OriginalUrl);
        if (url is not null) return Ok(url);
        var shortUrl = await urlService.CreateUrl(createUrlDto, userId);
        var newUrl = new Url
        {
            OriginalURL = createUrlDto.OriginalUrl,
            ShortenedURL = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/url/api/{shortUrl}",
            CreatedAt = DateTime.UtcNow,
            UserId = userId
        };
        return Ok(newUrl);
    }

    [HttpGet("api/{shortUrl}")]
    public async Task<IActionResult> RedirectUrl(string shortUrl)
    {
        var url = await urlRepository.GetUrlByShortUrl(shortUrl);

        return Redirect(url.OriginalURL);
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUrl(Guid id)
    {
        var userId = Validate(User);
        var url = await urlRepository.GetUrlById(id);
        if(url.UserId != userId)
        {
            return Unauthorized();
        }

        urlRepository.Remove(url);
        await unitOfWork.SaveChangesAsync();

        return NoContent();
    }
    
    private static Guid Validate(ClaimsPrincipal user)
    {
        var claim = user.FindFirst(ClaimTypes.NameIdentifier);

        return Guid.TryParse(claim?.Value, out var userId) ? userId : Guid.Empty;
    }
}