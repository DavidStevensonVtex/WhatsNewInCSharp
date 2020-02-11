// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7#discards

using System;
using System.Collections.Generic;

using static System.Console;

// Discards
// Often when deconstructing a tuple or calling a method with out 
// parameters, you're forced to define a variable whose value you 
// don't care about and don't intend to use. C# adds support for 
// discards to handle this scenario. A discard is a write-only 
// variable whose name is _ (the underscore character); you can 
// assign all of the values that you intend to discard to the 
// single variable. A discard is like an unassigned variable; 
// apart from the assignment statement, the discard can't be used 
// in code.

// Discards are supported in the following scenarios:

// When deconstructing tuples or user-defined types.

// When calling methods with out parameters.

// In a pattern matching operation with the is and switch statements.

// As a standalone identifier when you want to explicitly identify 
// the value of an assignment as a discard.

// The following example defines a QueryCityDataForYears method that 
// returns a 6-tuple that contains a data for a city for two different 
// years.The method call in the example is concerned only with the two 
// population values returned by the method and so treats the remaining 
// values in the tuple as discards when it deconstructs the tuple.



public class Example
{
	public static void Main()
	{
		var (_, _, _, pop1, _, pop2) = QueryCityDataForYears("New York City", 1960, 2010);

		WriteLine($"New York City, Population: 1960: {pop1:N0}");
		WriteLine($"New York City, Population: 2010: {pop2:N0}");
		WriteLine($"Population change, 1960 to 2010: {pop2 - pop1:N0}");
	}

	private static (string, double, int, int, int, int) QueryCityDataForYears(string name, int year1, int year2)
	{
		int population1 = 0, population2 = 0;
		double area = 0;

		if (name == "New York City")
		{
			area = 468.48;
			if (year1 == 1960)
			{
				population1 = 7781984;
			}
			if (year2 == 2010)
			{
				population2 = 8175133;
			}
			return (name, area, year1, population1, year2, population2);
		}

		return ("", 0, 0, 0, 0, 0);
	}
}
// The example displays the following output:
//      Population change, 1960 to 2010: 393,149

