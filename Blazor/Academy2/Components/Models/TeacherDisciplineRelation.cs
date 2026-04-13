using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy2.Components.Models
{
	[PrimaryKey(nameof(teacher), nameof(discipline))]
	public class TeacherDisciplineRelation
	{
		[Column(TypeName = "SMALLINT")]
		[ForeignKey(nameof(Teacher))]
		public int teacher { get; set; }

		[Column(TypeName = "SMALLINT")]
		[ForeignKey(nameof(Discipline))]
		public int discipline { get; set; }

		//Navigation properties:
		public Teacher Teacher { get; set; }
		public Discipline Discipline { get; set; }
		//public ICollection<Teacher> Teachers { get; set; } = default!;
		//public ICollection<Discipline> Disciplines { get; set; } = default!;
	}
}
