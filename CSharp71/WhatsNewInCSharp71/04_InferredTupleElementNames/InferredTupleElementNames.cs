// Inferred tuple element names
// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7-1#inferred-tuple-element-names

using static System.Console;

// Inferred tuple element names

// This feature is a small enhancement to the tuples feature introduced 
// in C# 7.0. Many times when you initialize a tuple, the variables 
// used for the right side of the assignment are the same as the names 
// you'd like for the tuple elements:
class InferredTupleElementNames
{
	static void Main(string[] args)
	{
		int count = 5;
		string label = "Colors used in the map";
		var pair = (count: count, label: label);
		WriteLine($"pair:  count: {pair.count} label: {pair.label}");

		// The names of tuple elements can be inferred from the 
		// variables used to initialize the tuple in C# 7.1:
		var pair2 = (count, label); // element names are "count" and "label"
		WriteLine($"pair2: count: {pair2.count} label: {pair2.label}");
	}
}
