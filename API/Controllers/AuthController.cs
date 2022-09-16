using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using API.Entities;

namespace API.Controllers
{
	[ApiController]
	[Route("api/auth")]
	public class AuthController : ControllerBase
	{
		readonly DataContext _dataContext;

		public AuthController(DataContext dataContext)
		{
			_dataContext = dataContext;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] UserDto dto)
		{
			// add user to DB if doesn't exist already
			var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
			if (user == null)
			{
				await _dataContext.AddAsync(new UserEntity
				{
					Username = dto.Username
				});
				await _dataContext.SaveChangesAsync();
			}

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, dto.Username),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return Ok();
		}

		[HttpPost("logout")]
		[Authorize]
		public async Task<IActionResult> Logout()
		{
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return Ok();
        }
	}
}

