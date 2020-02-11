// Deconstructing a user-defined type with discards
// https://docs.microsoft.com/en-us/dotnet/csharp/deconstruct#deconstructing-a-user-defined-type-with-discards
using System;
using static System.Console;
class DeconstructingAUserDefinedTypeWithDiscards
{
	static void Main(string[] args)
	{
		// Deconstruct the person object.
		Person p = new Person("John", "Quincy", "Adams", "Boston", "MA");
		(string firstName, string lastName) = p;
		WriteLine($"First Name: {firstName} Last Name: {lastName}\n");

		string first, middle, last;
		(first, middle, last) = p;
		WriteLine($"First: {first} Middle: {middle} Last: {last}\n");

		var (fName, _, city, _) = p;
		WriteLine($"Hello {fName} of {city}!\n");
		// The example displays the following output:
		//      Hello John of Boston!	}

		var (First, Last, City, State) = p;
		WriteLine($"{First} {Last}\n{City}, {State}");
	}
}