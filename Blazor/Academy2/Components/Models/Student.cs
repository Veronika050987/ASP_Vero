using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Academy2.Components.Models
{
	public class Student
	{
		[Key]
		public int stud_id { get; set; }
		[Required]
		public string last_name { get; set; }
		[Required]
		public string first_name { get; set; }
		public string? middle_name { get; set; }
		[Required]
		[DataType(DataType.Date)]
		public DateOnly birth_date { get; set; }
		public string? email { get; set; }
		public string? phone { get; set; }
		public byte[]? photo { get; set; }
		[Required]
		[ForeignKey(nameof(Group))]
		public int group { get; set; }

		//navigation properties
		public Group? Group { get; set; }
	}
}
