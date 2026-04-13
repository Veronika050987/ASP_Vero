using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Academy2.Components.Models;

namespace Academy2.Data
{
    public class Academy2Context : DbContext
    {
        public Academy2Context (DbContextOptions<Academy2Context> options)
            : base(options)
        {
        }

        public DbSet<Academy2.Components.Models.Direction> Directions { get; set; } = default!;
        public DbSet<Academy2.Components.Models.Group> Groups { get; set; } = default!;
        public DbSet<Academy2.Components.Models.Student> Students { get; set; } = default!;
        public DbSet<Academy2.Components.Models.Teacher> Teachers { get; set; } = default!;
        public DbSet<Academy2.Components.Models.Discipline> Disciplines { get; set; } = default!;
        public DbSet<Academy2.Components.Models.TeacherDisciplineRelation> TeachersDisciplinesRelation{ get; set; } = default!;


    }
}
