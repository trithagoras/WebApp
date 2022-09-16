using System;
namespace API.Entities
{
	public class LikeEntity
	{
		public int Id { get; set; }
		public UserEntity User { get; set; }
		public int ImageId { get; set; }
		public int Value { get; set; }
	}
}

