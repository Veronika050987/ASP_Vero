using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Academy2.Components.Models
{
	public class Human
	{
		
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

	}
}
