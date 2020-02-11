// Deconstructing a tuple
// https://docs.microsoft.com/en-us/dotnet/csharp/deconstruct#deconstructing-a-tuple


using System;
using static System.Console;

public class Example
{
	public static void Main()
	{
		var result = QueryCityData("New York City");

		var city = result.Item1;
		var pop = result.Item2;
		var size = result.Item3;
		WriteLine($"City (Item1): {result.Item1} Pop (Item2): {result.Item2} Size (Item3): {result.Item3}");

		Test2();
		Test3();
		Test4();
		Test5();
	}

	private static (string Name, int Population, double SizeSquareMiles) 
		QueryCityData(string name)
	{
		if (name == "New York City")
			return (name, 8175133, 468.48);

		return ("", 0, 0);
	}

	public static void Test2()
	{
		// There are three ways to deconstruct a tuple:

		// 1.	You can explicitly declare the type of each field inside parentheses. 
		//		The following example uses this approach to deconstruct the 3-tuple 
		//		returned by the QueryCityData method.

		(string city, int population, double area) = QueryCityData("New York City");
		WriteLine($"City: {city} Population: {population} Area: {area}");
	}
	public static void Test3()
	{
		// 2.	You can use the var keyword so that C# infers the type of each 
		//		variable. You place the var keyword outside of the parentheses. 
		//		The following example uses type inference when deconstructing 
		//		the 3-tuple returned by the QueryCityData method.
		var (city, population, area) = QueryCityData("New York City");
		WriteLine($"City: {city} Population: {population} Area: {area}");
	}
	public static void Test4()
	{
		// 3.	You can also use the var keyword individually with any or all 
		//		of the variable declarations inside the parentheses.

		(var city, int population, double area) = QueryCityData("New York City");
		WriteLine($"City: {city} Population: {population} Area: {area}");
	}

	// This is cumbersome and is not recommended.

	// Lastly, you may deconstruct the tuple into variables that have already 
	// been declared.
	public static void Test5()
	{
		string city = "Raleigh";
		int population = 458880;
		double area = 144.8;

		(city, population, area) = QueryCityData("New York City");
		WriteLine($"City: {city} Population: {population} Area: {area}");
	}
}