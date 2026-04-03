namespace Academy2.Components.Pages.GroupPages
{
	public class Week
	{
		readonly string[] DAYNAMES = { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс" };
		int days;
		public Week(int days)
		{
			this.days = days;
		}
		public override string ToString()
		{
			List<string> result = new List<string>();
			for (int i = 0; i < 7; i++)
			{
				if (((days >> i) & 1) != 0) result.Add(DAYNAMES[i]);
			}
			return string.Join(", ", result);
		}
	}
}
