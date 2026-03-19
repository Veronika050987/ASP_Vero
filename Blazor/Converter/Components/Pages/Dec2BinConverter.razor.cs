namespace Converter.Components.Pages
{
	public partial class Decimal2BinaryConverter
	{
		public string ToBinary(int decimalNumber)
		{
			return Convert.ToString(decimalNumber, 2);
		}
	}
}
