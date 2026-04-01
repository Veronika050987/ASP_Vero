using Academy2.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy2.Components.Models
{
	public class Direction
	{
		[Key]
		[Column(TypeName = "tinyint")]
		public int direction_id { get; set; }
		[Required]
		[UniqueDirectionName(ErrorMessage = "Error: такое направление уже существует")]
		public string direction_name { get; set; }
	}
}
