using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Academy2.Components.Models
{
	public class Human
	{
		
		[Required]
		[Display(Name = "Фамилия")]
		public string last_name { get; set; }
		[Required]
		[Display(Name = "Имя")]
		public string first_name { get; set; }
		[Display(Name = "Отчество")]
		public string? middle_name { get; set; }
		[Required]
		[DataType(DataType.Date)]
		[Display(Name = "Дата рождения")]
		public DateOnly birth_date { get; set; }
		public string? email { get; set; }
		public string? phone { get; set; }
		public byte[]? photo { get; set; }

	}
}
