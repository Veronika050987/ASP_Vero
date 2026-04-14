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
		[NotMapped]
		[Required(ErrorMessage = "Please select at least one discipline.")]
		public List<int> SelectedDisciplineIds { get; set; } = new List<int>();
		[NotMapped]
		public string? SelectedDisciplineIdAsString { get; set; }
		//Navigation properties:
		public ICollection<TeacherDisciplineRelation> DisciplinesRelations { get; set; } = new List<TeacherDisciplineRelation>();
	}
}
