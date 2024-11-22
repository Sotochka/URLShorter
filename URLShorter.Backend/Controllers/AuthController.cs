using Microsoft.AspNetCore.Mvc;
using URLShorter.Backend.Models.Enums;
using URLShorter.Backend.Common.Interfaces;
using URLShorter.Backend.Models.DTOs.AuthDto;
using URLShorter.Backend.Models.Entities;
using URLShorter.Backend.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace URLShorter.Backend.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(IUnitOfWork unitOfWork, IUserRepository userRepository, IJwtGenerator jwtGenerator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthRegisterDto authRegisterDto)
    {
        var user = new User
        {
            Username = authRegisterDto.UserName,
            Email = authRegisterDto.Email,
            Password = Hash(authRegisterDto.Password),
            RoleId = (int)Roles.User
        };

        userRepository.Insert(user);
        
        await unitOfWork.SaveChangesAsync();

        return Ok(new AuthResponseDto(jwtGenerator.GenerateJwtToken(user), user.Username));
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthLoginDto authLoginDto)
    {
        var user = await userRepository.GetUserByEmail(authLoginDto.Email);

        if (!Verify(authLoginDto.Password, user.Password))
        {
            return Unauthorized();
        }

        return Ok(new AuthResponseDto(jwtGenerator.GenerateJwtToken(user), user.Username));
    }

    [Authorize]
    [HttpGet("get-role")]
    public IActionResult GetRole()
    {
        var role = User.FindFirst(ClaimTypes.Role)?.Value; // Extract role from claims
        return Ok(new { Role = role });
    }
    
    private static string Hash(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password);

    private static bool Verify(string password, string hashedPassword) =>
        BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}
