using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy2.Components.Models
{
	public class Teacher : Human
	{
		[Key]
		[Column(TypeName = "smallint")]
		public int teacher_id { get; set; }
		public DateOnly work_since { get; set; } = default!;
		public decimal rate { get; set; }
		public byte[] PhotoData { get; set; }
	}
}
