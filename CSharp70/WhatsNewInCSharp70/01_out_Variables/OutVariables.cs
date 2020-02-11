// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7#out-variables
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Console;

//  variables
// The existing syntax that supports out parameters has been improved 
// in this version.

class OutVariables
{

	// Previously, you would need to separate the declaration of the out 
	// variable and its initialization into two different statements:
	void ParseNumberStringOldWay(string input)
	{
		int numericResult;
		if (int.TryParse(input, out numericResult))
			WriteLine(numericResult);
		else
			WriteLine("Could not parse input");
	}

	// You can now declare out variables in the argument list of a method 
	// call, rather than writing a separate declaration statement:

	void ParseNumberStringNewWay(string input)
	{
		if (int.TryParse(input, out int result))
			WriteLine(result);
		else
			WriteLine("Could not parse input");
	}

	// You may want to specify the type of the out variable for clarity, 
	// as shown above. However, the language does support using an 
	// implicitly typed local variable:
	void ParseNumberStringNewWay2(string input)
	{
		if (int.TryParse(input, out var answer))
			WriteLine(answer);
		else
			WriteLine("Could not parse input");
	}

	// The code is easier to read.

	// You declare the out variable where you use it, not on another line above.

	// No need to assign an initial value.

	// By declaring the out variable where it is used in a method call, you can't 
	// accidentally use it before it is assigned.

	// The most common use for this feature will be the Try pattern. In this 
	// pattern, a method returns a bool indicating success or failure and an 
	// out variable that provides the result if the method succeeds.

	// When using the out variable declaration, the declared variable "leaks" 
	// into the outer scope of the if statement.This allows you to use the 
	// variable afterwards:
	int? ParseNumberStringNewWay3(string input)
	{
		if (!int.TryParse(input, out int result))
		{
			return null;
		}

		return result;
	}
}
