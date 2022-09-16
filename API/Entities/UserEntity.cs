using System;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
	public class UserEntity
	{
		public int Id { get; set; }
		[Required]
		public string Username { get; set; }
	}
}

