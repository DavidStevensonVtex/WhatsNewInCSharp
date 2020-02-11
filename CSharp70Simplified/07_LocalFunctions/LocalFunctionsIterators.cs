// Local Functions
// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7#local-functions

// Local functions

// There are two very common use cases for local functions: public iterator 
// methods and public async methods. Both types of methods generate code that 
// reports errors later than programmers might expect. 

using System;
using System.Collections.Generic;
using static System.Console;
class LocalFunctionsIterator
{
	public static IEnumerable<char> AlphabetOne(char start, char end)
	{
		if ( ! (char.IsLower(start) && char.IsLower(end)))
		{
			throw new ArgumentOutOfRangeException(
				paramName: "start or end",
				message: "start and end must be a lower case letter");
		}
		for (char c = start; c < end; c++)
			yield return c;
	}
	public static IEnumerable<char> AlphabetTwo(char start, char end)
	{
		if (!(char.IsLower(start) && char.IsLower(end)))
		{
			throw new ArgumentOutOfRangeException(
				paramName: "start or end",
				message: "start and end must be a lower case letter");
		}

		IEnumerable<char> ReturnSequence ()
		{
			for (char c = start; c < end; c++)
				yield return c;
		}
		return ReturnSequence();
	}

	public static void Main()
	{
		// The desire is to catch the error when defining the IEnumerable
		// rather than finding the error when iterating it.
		try
		{
			WriteLine("Defining AlphaOne");
			IEnumerable<char> AlphaOne = AlphabetOne('A', 'z');
			WriteLine("Iterating AlphaOne");
			foreach (char c in AlphaOne)
				Write($"{c} ");
		}
		catch ( Exception ex )
		{
			WriteLine($"Exception: AlphaOne: {ex.Message}");
		}

		try
		{
			WriteLine("\n\nDefining AlphaTwo");
			IEnumerable<char> AlphaTwo = AlphabetTwo('A', 'z');
			WriteLine("Iterating AlphaTwo");
			foreach (char c in AlphaTwo)
				Write($"{c} ");
		}
		catch (Exception ex)
		{
			WriteLine($"Exception: AlphaTwo: {ex.Message}");
		}
	}
}