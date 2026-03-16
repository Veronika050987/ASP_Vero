using System.Numerics;

namespace Blazor.Components.Pages
{
	public partial class Factorial
	{
		int number;
		BigInteger factorial = 1;
		List<BigInteger> results = new List<BigInteger>();
		void setNumber(int number)
		{
			this.number = number;
		}
		void Calculate()
		{
			factorial = 1;
			results = new List<BigInteger>();
			for (int i = 1; i <= number; i++)
			{
				factorial *= i;
				results.Add(factorial);
			}
		}
	}
}
