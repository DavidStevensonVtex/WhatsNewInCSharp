// Non-trailing named arguments
// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7-2#non-trailing-named-arguments

using System;

// Method calls may now use named arguments that precede positional arguments 
// when those named arguments are in the correct positions. For more information 
// see:

// Named and optional arguments.

// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/named-and-optional-arguments


// Named arguments, when used with positional arguments, are valid as long as

//	►	they're not followed by any positional arguments, or
//	PrintOrderDetails("Gift Shop", 31, productName: "Red Mug");

//	►	starting with C# 7.2, they're used in the correct position. 
//		In the example below, the parameter orderNum is in the correct position 
//		but isn't explicitly named.
//		PrintOrderDetails(sellerName: "Gift Shop", 31, productName: "Red Mug");

// However, out-of-order named arguments are invalid if they're followed 
// by positional arguments.

// // This generates CS1738: Named argument specifications must appear after all fixed arguments have been specified.
//		PrintOrderDetails(productName: "Red Mug", 31, "Gift Shop");
class NamedExample
{
	static void Main(string[] args)
	{
		// The method can be called in the normal way, by using positional arguments.
		PrintOrderDetails("Gift Shop", 31, "Red Mug");

		// Named arguments can be supplied for the parameters in any order.
		PrintOrderDetails(orderNum: 31, productName: "Red Mug", sellerName: "Gift Shop");
		PrintOrderDetails(productName: "Red Mug", sellerName: "Gift Shop", orderNum: 31);

		// Named arguments mixed with positional arguments are valid
		// as long as they are used in their correct position.
		PrintOrderDetails("Gift Shop", 31, productName: "Red Mug");
		PrintOrderDetails(sellerName: "Gift Shop", 31, productName: "Red Mug");    // C# 7.2 onwards
		PrintOrderDetails("Gift Shop", orderNum: 31, "Red Mug");                   // C# 7.2 onwards

		// However, mixed arguments are invalid if used out-of-order.
		// The following statements will cause a compiler error.
		//PrintOrderDetails(productName: "Red Mug", 31, "Gift Shop");
		//PrintOrderDetails(31, sellerName: "Gift Shop", "Red Mug");
		//PrintOrderDetails(31, "Red Mug", sellerName: "Gift Shop");
	}

	static void PrintOrderDetails(string sellerName, int orderNum, string productName)
	{
		if (string.IsNullOrWhiteSpace(sellerName))
		{
			throw new ArgumentException(message: "Seller name cannot be null or empty.", paramName: nameof(sellerName));
		}

		Console.WriteLine($"Seller: {sellerName}, Order #: {orderNum}, Product: {productName}");
	}
}