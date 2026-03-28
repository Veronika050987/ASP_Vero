using System.ComponentModel.DataAnnotations;

namespace Academy.Models
{
	public class Direction
	{
		[Key]
		public byte direction_id { get; set; }
		[Required]
		public string direction_name { get; set; }

	}
}
