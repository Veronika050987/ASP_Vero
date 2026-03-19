namespace Converter.Components.Pages
{
	public partial class Decimal2HexadecimalConverter
	{
		public string ToHexadecimal(int decimalNumber)
		{
			return Convert.ToString(decimalNumber, 16);
		}
	}
}
