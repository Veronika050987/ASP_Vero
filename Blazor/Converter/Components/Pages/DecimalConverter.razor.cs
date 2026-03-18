namespace Converter.Components.Pages
{
	public partial class DecimalConverter
	{
		public string ToBinary(int decimalNumber)
		{
			return Convert.ToString(decimalNumber, 2);
		}
	}
}
