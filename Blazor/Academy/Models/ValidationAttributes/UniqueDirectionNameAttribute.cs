using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Academy.Data;

namespace Academy.Models.ValidationAttributes
{
	public class UniqueDirectionNameAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value == null || string.IsNullOrWhiteSpace(value.ToString())) return ValidationResult.Success;
			string directionName = value.ToString();
			IDbContextFactory<AcademyContext> dbContextFactory = validationContext.GetService<IDbContextFactory<AcademyContext>>();
			if (dbContextFactory == null) return new ValidationResult("No data....");
			using (var context = dbContextFactory.CreateDbContext())
			{
				bool exists = context.Directions.Any(d => d.direction_name.ToLower() == directionName.ToLower());
				if (exists) return new ValidationResult(ErrorMessage ?? $"Direction '{directionName}' already exists");
			}
			return ValidationResult.Success;
		}
	}
}
