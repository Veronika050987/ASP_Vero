using Academy.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class UniqueDirectionNameAttribute : ValidationAttribute
{
	protected override ValidationResult IsValid(object value, ValidationContext validationContext)
	{
		if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
		{
			// Если поле пустое, другая валидация (например, Required) обработает это.
			// Или можно вернуть ValidationResult.Success, если пустое поле допустимо.
			return ValidationResult.Success;
		}

		var directionName = value.ToString();

		// Получаем DbContextFactory из ValidationContext.ServiceProvider
		var dbContextFactory = validationContext.GetService<IDbContextFactory<AcademyContext>>();
		if (dbContextFactory == null)
		{
			// Обработка ошибки, если DbContextFactory не доступен (что маловероятно в Blazor)
			return new ValidationResult("Database context factory not available.");
		}

		using (var context = dbContextFactory.CreateDbContext())
		{
			// Проверяем, существует ли уже направление с таким именем
			var exists = context.Directions.Any(d => d.direction_name.ToLower() == directionName.ToLower());

			if (exists)
			{
				// Если существует, возвращаем ошибку валидации
				return new ValidationResult(ErrorMessage ?? $"A direction with the name '{directionName}' already exists.");
			}
		}

		// Если дубликатов нет, валидация проходит успешно
		return ValidationResult.Success;
	}

	// Позволяет указывать сообщение об ошибке извне, если нужно
	public UniqueDirectionNameAttribute(string errorMessage = "Name must be unique.") : base(errorMessage)
	{
	}
}
