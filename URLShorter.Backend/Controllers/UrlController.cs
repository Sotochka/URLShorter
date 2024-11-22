using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using URLShorter.Backend.Common.Interfaces;
using URLShorter.Backend.Models.DTOs.UrlDto;
using URLShorter.Backend.Repositories.Interfaces;

namespace URLShorter.Backend.Controllers;

[ApiController]
[Route("url")]
public class UrlController(IUrlRepository urlRepository, IUnitOfWork unitOfWork, IUrlService urlService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUrlById(Guid id)
    {
        var url = await urlService.GetUrlById(id);
        return Ok(url);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllUrls()
    {
        var urls = await urlService.GetAllUrls();
        return Ok(urls);
    }

    [Authorize]
    [HttpPost("create-url")]
    public async Task<IActionResult> CreateUrl([FromBody] CreateUrlDto createUrlDto)
    {
        var userId = Validate(User);
        
        var shortUrl = await urlService.CreateUrl(createUrlDto, userId);
        
        return Ok(shortUrl);
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
        
        if (url.UserId != userId)
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