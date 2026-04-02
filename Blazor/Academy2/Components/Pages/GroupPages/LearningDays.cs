namespace Academy2.Components.Pages.GroupPages
{
	public class LearningDays
	{
		class DayInfo
		{
			public string Name { get; set; } = string.Empty;
			public bool IsWorking { get; set; }
		}

		public static string GetDays(int Days)
		{
			string[] DAYNAMES = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };
			List<string> result = new List<string>();
			for (int i = 0; i < 7; i++)
			{
				if (Days % 2 == 1)
					result.Add(DAYNAMES[i]);
				Days = Days / 2;
			}
			return string.Join(", ", result);
		}

		public static string GetDaysColor(int learningDaysValue)
		{
			bool includesWeekend = false;
			if (((learningDaysValue >> 5) & 1) == 1 || ((learningDaysValue >> 6) & 1) == 1)
			{
				includesWeekend = true;
			}
			return includesWeekend ? "#FF6347" : "#4682B4"; // "inherit" - стандартный цвет текста
		}
	}
}
