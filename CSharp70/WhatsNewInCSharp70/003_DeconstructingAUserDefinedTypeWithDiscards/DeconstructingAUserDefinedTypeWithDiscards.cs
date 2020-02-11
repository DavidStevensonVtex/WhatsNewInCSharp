// Deconstructing a user-defined type with discards
// https://docs.microsoft.com/en-us/dotnet/csharp/deconstruct#deconstructing-a-user-defined-type-with-discards
using System;
class DeconstructingAUserDefinedTypeWithDiscards
{
	static void Main(string[] args)
	{
		// Deconstruct the person object.
		Person p = new Person("John", "Quincy", "Adams", "Boston", "MA");
		var (fName, _, city, _) = p;
		Console.WriteLine($"Hello {fName} of {city}!");
		// The example displays the following output:
		//      Hello John of Boston!	}
	}
}

