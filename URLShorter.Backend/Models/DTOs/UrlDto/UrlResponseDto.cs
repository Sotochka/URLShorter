namespace URLShorter.Backend.Models.DTOs.UrlDto;

public record UrlResponseDto(string OriginalURL, string ShortenedURL, DateTime CreatedAt);