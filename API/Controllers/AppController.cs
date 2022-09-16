using System;
using System.Linq;
using System.Security.Claims;
using API.DTOs;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
	[ApiController]
	[Route("api/app")]
	public class AppController : ControllerBase
	{
		const int ImageCount = 10;

		IImageService _imageService;
		DataContext _dataContext;

		public AppController(IImageService imageService, DataContext dataContext)
		{
			_imageService = imageService;
			_dataContext = dataContext;
		}

		[HttpGet("image")]
		[Authorize]
		public async Task<IActionResult> NewImage(int id)
		{
			if (id < 0 || id > ImageCount)
			{
				return NotFound();
			}

			var user = await _dataContext.Users.SingleOrDefaultAsync(u => u.Username == User.FindFirstValue(ClaimTypes.Name));
			var userId = user!.Id;

			if (id == 0)
			{
				var r = new Random();
				id = r.Next() % ImageCount + 1;
			}

			// request image by id
			return Ok(new ImageDto
			{
				ImageBase64 = "data:image/jpg;base64," + Convert.ToBase64String(await _imageService.LoadImage(id)),
				ImageId = id
			});
        }

		[HttpPost("image")]
		[Authorize]
		public async Task<IActionResult> LikeImage([FromBody] ImageLikeDto like)
		{
			if (like.ImageId < 0 || like.ImageId > ImageCount)
			{
				return NotFound();
			}

            var user = await _dataContext.Users.SingleOrDefaultAsync(u => u.Username == User.FindFirstValue(ClaimTypes.Name));
            var dbLike = await _dataContext.Likes.FirstOrDefaultAsync(l => l.ImageId == like.ImageId && l.User == user!);

			// if image hasn't been interacted with previously
			if (dbLike == null)
			{
				await _dataContext.Likes.AddAsync(new Entities.LikeEntity
				{
					ImageId = like.ImageId,
					User = user!,
					Value = like.Value > 0 ? 1 : -1
				});
			}
			else
			{
                // if image like value is same as incoming like value, 'unlike'
                if (dbLike.Value == like.Value)
                {
                    _dataContext.Remove(dbLike);
                }
				else
				{
					dbLike.Value = like.Value;
				}
            }

            await _dataContext.SaveChangesAsync();
            return Ok();
        }

		[HttpGet("liked")]
		[Authorize]
		public async Task<IActionResult> GetLiked(int imageId)
		{
            if (imageId < 0 || imageId > ImageCount)
            {
                return NotFound();
            }

            var user = await _dataContext.Users.SingleOrDefaultAsync(u => u.Username == User.FindFirstValue(ClaimTypes.Name));
            var dbLike = await _dataContext.Likes.FirstOrDefaultAsync(l => l.ImageId == imageId && l.User == user!);

			if (dbLike == null)
			{
				return Ok(0);
			}

			return Ok(dbLike.Value);
        }
	}
}

