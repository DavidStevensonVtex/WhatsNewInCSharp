// Default literal expressions
// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7-1#default-literal-expressions

using System;

class DefaultLiteralExpressions
{
	// Default literal expressions

	// Default literal expressions are an enhancement to default 
	// value expressions.These expressions initialize a variable 
	// to the default value. 

	static void Main(string[] args)
	{
		// Where you previously would write:
		Func<string, bool> whereClause = default(Func<string, bool>);

		// You can now omit the type on the right-hand side of the initialization:
		Func<string, bool> whereClause2 = default;
	}
}

// You can learn more about this enhancement in the C# Programming Guide 
// topic on default value expressions.
// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/default-value-expressions

// This enhancement also changes some of the parsing rules for the default keyword.
// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/default