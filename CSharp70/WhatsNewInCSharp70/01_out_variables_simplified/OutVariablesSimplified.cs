using static System.Console;

class OutVariablesSimplified
{
	static void Main(string[] args)
	{
		// Inline out variables
		// The most common use for this feature will be the Try pattern.

		// Old way
		int value;
		if (int.TryParse("123", out value))
			WriteLine($"{nameof(value)}:  {value}");

		// Inline out variables
		if (int.TryParse("456", out int value2))
			WriteLine($"{nameof(value2)}: {value2}");

		// Inline implicitly typed out variable.
		if ( int.TryParse("789", out var value3 ))
			WriteLine($"{nameof(value3)}: {value3}");
	}
}