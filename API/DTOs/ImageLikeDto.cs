using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
	public class ImageLikeDto
	{
		[Required]
		public int ImageId { get; set; }
		[Required]
		public int Value { get; set; }
	}
}

