using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Academy2.Data;

namespace Academy2.Models.ValidationAttributes
{
	public class UniqueDirectionNameAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value == null || string.IsNullOrWhiteSpace(value.ToString())) return ValidationResult.Success;
			string directionName = value.ToString();
			IDbContextFactory<Academy2Context> dbContextFactory = validationContext.GetService<IDbContextFactory<Academy2Context>>();
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
