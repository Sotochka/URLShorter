using URLShorter.Backend.Models.Entities;

namespace URLShorter.Backend.Common.Interfaces;

public interface IJwtGenerator
{
    public string GenerateJwtToken(User user);
}