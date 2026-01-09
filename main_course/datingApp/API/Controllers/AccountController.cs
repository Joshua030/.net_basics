using System.Security.Cryptography;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(AppDbContext context, ITokenService tokenService) : BaseApiController
{

  [HttpPost("register")]
  // public async Task<ActionResult<AppUser>> Register(string email, string displayName, string password)// usin query params
  public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
  {

    if (await UserExists(registerDto.Email)) return BadRequest("Email is already taken");

    using var hmac = new HMACSHA512();

    // variables using query params
    /*    var user = new AppUser
       {
         DisplayName = displayName,
         Email = email.ToLower(),
         PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)),
         PasswordSalt = hmac.Key
       }; */

    var user = new AppUser
    {
      DisplayName = registerDto.DisplayName,
      Email = registerDto.Email.ToLower(),
      PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registerDto.Password)),
      PasswordSalt = hmac.Key
    };

    context.Users.Add(user);
    await context.SaveChangesAsync();
    return user.ToDto(tokenService);
  }

  [HttpPost("login")]

  public async Task<ActionResult<UserDto>> Login(LoginDto loginDto) // we can use [FROMBODY] before LoginDto OR [FROMQUERY] to specify where to get the data
  {
    var user = await context.Users.SingleOrDefaultAsync(u => u.Email.ToLower() == loginDto.Email.ToLower());

    if (user == null) return Unauthorized("Invalid email");

    using var hmac = new HMACSHA512(user.PasswordSalt);

    var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(loginDto.Password));

    for (int i = 0; i < computedHash.Length; i++)
    {
      if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
    }

    return user.ToDto(tokenService);

  }

  private async Task<bool> UserExists(string email)
  {
    return await context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
  }
}

