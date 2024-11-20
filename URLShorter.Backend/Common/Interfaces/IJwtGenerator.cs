using URLShorter.Backend.Data.Entities;

namespace URLShorter.Backend.Common.Interfaces;

public interface IJwtGenerator
{
    public string GenerateJwtToken(User user);
}