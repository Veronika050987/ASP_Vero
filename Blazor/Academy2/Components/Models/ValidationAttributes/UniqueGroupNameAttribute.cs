using Academy2.Data;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Academy2.Components.Models;

namespace Academy2.Models.ValidationAttributes
{
	public class UniqueGroupNameAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value == null) return ValidationResult.Success;

			var dbContext = validationContext.GetService(typeof(Academy2Context)) as Academy2Context;
			if (dbContext == null)
			{
				return ValidationResult.Success;
			}

			Group entity = null!;

			if (validationContext.ObjectInstance is Group groupInstance)
			{
				entity = groupInstance;
			}

			else if (validationContext.ObjectInstance is EditContext editContext && editContext.Model is Group modelInstance)
			{
				entity = modelInstance;
			}

			if (entity == null)
			{
				return ValidationResult.Success;
			}

			bool exists = dbContext.Groups.Any(g =>
				g.group_name == value.ToString() &&
				g.group_id != entity.group_id);

			if (exists)
			{
				return new ValidationResult(ErrorMessage ?? "Такая группа уже существует");
			}

			return ValidationResult.Success;
		}
	}
}