namespace URLShorter.Backend.Models.DTOs.UrlDto;

public record UrlResponseDto(Guid id, string OriginalURL, string ShortenedURL, DateTime CreatedAt, Guid UserId);