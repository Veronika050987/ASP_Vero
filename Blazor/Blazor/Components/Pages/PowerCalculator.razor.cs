using Microsoft.AspNetCore.Components;

namespace Blazor.Components.Pages
{
	public partial class PowerCalculator
	{
		private double baseNumber;
		private double exponent;
		private double result;
		private bool isCalculated = false;

		private void Calculate()
		{
			result = Math.Pow(baseNumber, exponent);
			isCalculated = true;
		}
	}
}
