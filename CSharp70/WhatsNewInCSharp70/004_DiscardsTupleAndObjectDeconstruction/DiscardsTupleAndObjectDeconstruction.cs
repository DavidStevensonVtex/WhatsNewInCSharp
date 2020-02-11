// Discards - C# Guide
// https://docs.microsoft.com/en-us/dotnet/csharp/discards

// Starting with C# 7, C# supports discards, which are temporary, dummy variables 
// that are intentionally unused in application code. Discards are equivalent to 
// unassigned variables; they do not have a value. Because there is only a single 
// discard variable, and that variable may not even be allocated storage, discards 
// can reduce memory allocations. Because they make the intent of your code clear, 
// they enhance its readability and maintainability.

// You indicate that a variable is a discard by assigning it the underscore(_) 
// as its name.For example, the following method call returns a 3-tuple in which 
// the first and second values are discards and area is a previously declared 
// variable to be set to the corresponding third component returned by 
// GetCityInformation:

// (_, _, area) = city.GetCityInformation(cityName);

// In C# 7, discards are supported in assignments in the following contexts:

// Tuple and object deconstruction.
// Pattern matching with is and switch.
// Calls to methods with out parameters.
// A standalone _ when no _ is in scope.


// The Deconstruct method of a class, structure, or interface also allows 
// you to retrieve and deconstruct a specific set of data from an object. 
// You can use discards when you are interested in working with only a 
// subset of the deconstructed values. Ihe following example deconstructs 
// a Person object into four strings (the first and last names, the city, 
// and the state), but discards the last name and the state.

// Tuple and object deconstruction
// https://docs.microsoft.com/en-us/dotnet/csharp/discards#tuple-and-object-deconstruction

// Discards are particularly useful in working with tuples when your application 
// code uses some tuple elements but ignores others.For example, the following 
// QueryCityDataForYears method returns a 6-tuple with the name of a city, 
// its area, a year, the city's population for that year, a second year, and the 
// city's population for that second year.The example shows the change in 
// population between those two years.Of the data available from the tuple, 
// we're unconcerned with the city area, and we know the city name and the 
// two dates at design-time. As a result, we're only interested in the two 
// population values stored in the tuple, and can handle its remaining values 
// as discards.

using System;
using System.Collections.Generic;

public class Example
{
	public static void Main()
	{
		var (_, _, _, pop1, _, pop2) = QueryCityDataForYears("New York City", 1960, 2010);

		Console.WriteLine($"Population change, 1960 to 2010: {pop2 - pop1:N0}");
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