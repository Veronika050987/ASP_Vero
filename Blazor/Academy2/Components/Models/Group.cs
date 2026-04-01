using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy2.Components.Models
{
	public class Group
	{
		[Key]
		public int group_id { get; set; }
		[Required]
		public string group_name { get; set; }
		[Required]
		[Column(TypeName = "tinyint")]
		//[MinLength(1)]
		[ForeignKey(nameof(Direction))]
		public int direction { get; set; }
		
		[Column(TypeName = "tinyint")]
		public int learning_days { get; set; }
		public TimeOnly start_time { get; set; }
		
		//Navigation properties
		//[Required]
		public Direction? Direction { get; set; }

	}
}
