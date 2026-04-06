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
		public string direction_name { get; set; }

		//navigation properties

		public ICollection<Group> Groups { get; set; }
	}
}
