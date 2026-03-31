using System.Security.Cryptography;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(UserManager<AppUser> userManager, ITokenService tokenService) : BaseApiController
{

  [HttpPost("register")]
  // public async Task<ActionResult<AppUser>> Register(string email, string displayName, string password)// usin query params
  public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
  {

    // if (await UserExists(registerDto.Email)) return BadRequest("Email is already taken");

    // using var hmac = new HMACSHA512();

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
      UserName = registerDto.Email,
      Member = new Member
      {
        DisplayName = registerDto.DisplayName,
        Gender = registerDto.Gender,
        City = registerDto.City,
        Country = registerDto.Country,
        DateOfBirth = registerDto.DateOfBirth
      }
    };

    /*   context.Users.Add(user);
      await context.SaveChangesAsync(); */

    var result = await userManager.CreateAsync(user, registerDto.Password);

    if (!result.Succeeded)
    {
      foreach (var error in result.Errors)
      {
        ModelState.AddModelError("identity", error.Description);
      }

      return ValidationProblem();
    }

    return await user.ToDto(tokenService);
  }

  [HttpPost("login")]

  public async Task<ActionResult<UserDto>> Login(LoginDto loginDto) // we can use [FROMBODY] before LoginDto OR [FROMQUERY] to specify where to get the data
  {
    // var user = await context.Users.SingleOrDefaultAsync(u => u.Email.ToLower() == loginDto.Email.ToLower());
    var user = await userManager.FindByEmailAsync(loginDto.Email);

    if (user == null) return Unauthorized("Invalid email");

    //! Doing with own athentication system
    /*  using var hmac = new HMACSHA512(user.PasswordSalt);

     var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(loginDto.Password));

     for (int i = 0; i < computedHash.Length; i++)
     {
       if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
     }
  */

    var result = await userManager.CheckPasswordAsync(user, loginDto.Password);

    if (!result) return Unauthorized("Invalid password");

    return await user.ToDto(tokenService);

  }

  /*  private async Task<bool> UserExists(string email)
   {
     return await context.Users.AnyAsync(u => u.Email!.ToLower() == email.ToLower());
   } */
}

