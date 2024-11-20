namespace URLShorter.Backend.Models.DTOs.AuthDto;

public record AuthRegisterDto(string UserName, string Email, string Password);