namespace URLShorter.Backend.Data.Entities;

public class Url
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string OriginalURL { get; set; }
    public string ShortenedURL { get; set; }
}