using TODOlist.Classes;

namespace TODOlist.Components.Pages
{
	public partial class TODO
	{
		HashSet<TODOitem> todos = [];
		string task;
		string errorMessage;
		void AddTask()
		{
			errorMessage = string.Empty;

			if (string.IsNullOrWhiteSpace(task)) return;

			// Нормализуем задачу для сравнения (убираем лишние пробелы и приводим к нижнему регистру)
			string normalizedTask = task.Trim();

			if (todos.Any(t => t.Title.Equals(normalizedTask, StringComparison.OrdinalIgnoreCase)))
			{
				errorMessage = $"Задача '{normalizedTask}' уже существует в списке. Заполните другую задачу";
				// Не добавляем задачу и выходим
				return;
			}

			todos.Add(new TODOitem { Title = task });
			task = "";
		}
	}
}
