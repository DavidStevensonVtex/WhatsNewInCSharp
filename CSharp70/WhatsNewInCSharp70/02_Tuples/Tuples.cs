// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7#tuples

// For .NET Framework 4.6.1 you may need to perform Nuget for System.ValueType
// Suggested Google search Nuget System.ValueType
// Install-Package System.ValueTuple -Version 4.4.0

// Alternatively, update Visual Studio 2017 via Visual Studio Installer to add
// components for .NET 4.6.2, 4.7.1 etc.
// Right-click project, select Properties, select Target Framework of 4.7.1 for example.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Console;

// Tuples

// The new tuples features require the ValueTuple types. You must add the 
// NuGet package System.ValueTuple in order to use it on platforms that 
// do not include the types.

// This is similar to other language features that rely on types delivered 
// in the framework.Example include async and await relying on the 
// INotifyCompletion interface, and LINQ relying on IEnumerable<T>. 
// However, the delivery mechanism is changing as .NET is becoming more 
// platform independent.The.NET Framework may not always ship on the same 
// cadence as the language compiler.When new language features rely on new 
// types, those types will be available as NuGet packages when the language 
// features ship. As these new types get added to the.NET Standard API and 
// delivered as part of the framework, the NuGet package requirement will 
// be removed.

// C# provides a rich syntax for classes and structs that is used to explain 
// your design intent. But sometimes that rich syntax requires extra work with 
// minimal benefit. You may often write methods that need a simple structure 
// containing more than one data element. To support these scenarios tuples 
// were added to C#. Tuples are lightweight data structures that contain 
// multiple fields to represent the data members. The fields are not validated, 
// and you cannot define your own methods

// Tuples were available before C# 7, but they were inefficient and had no 
// language support. This meant that tuple elements could only be referenced 
// as Item1, Item2 and so on. C# 7 introduces language support for tuples, 
// which enables semantic names for the fields of a tuple using new, more 
// efficient tuple types.

class Program
{
	static void Main(string[] args)
	{
		// You can create a tuple by assigning each member to a value:
		var letters = ("a", "b");
		WriteLine($"letters: Item1: {letters.Item1} Item2: {letters.Item2}");

		// That assignment creates a tuple whose members are Item1 and Item2, 
		// which you can use in the same way as Tuple. You can change the syntax 
		// to create a tuple that provides semantic names to each of the members 
		// of the tuple:

		(string Alpha, string Beta) namedLetters = ("a", "b");
		WriteLine($"namedLetters: Alpha: {namedLetters.Alpha} Beta: {namedLetters.Beta}");

		// The namedLetters tuple contains fields referred to as Alpha and Beta. 
		// Those names exist only at compile time and are not preserved for example 
		// when inspecting the tuple using reflection at runtime.

		// In a tuple assignment, you can also specify the names of the fields on 
		// the right - hand side of the assignment:

		var alphabetStart = (Alpha: "a", Beta: "b");
		WriteLine($"alphabetStart: Alpha: {alphabetStart.Alpha} Beta: {alphabetStart.Beta}");

		// You can specify names for the fields on both the left and right-hand side 
		// of the assignment:

		// Warning CS8123 The tuple element name 'Alpha' is ignored because a different name 
		// or no name is specified by the target type '(string First, string Second)'. 

		// Warning CS8123 The tuple element name 'Beta' is ignored because a different name 
		// or no name is specified by the target type '(string First, string Second)'.
		(string First, string Second) firstLetters = (Alpha: "a", Beta: "b");
		WriteLine($"firstLetters: First: {firstLetters.First} Second: {firstLetters.Second}");

		// The line above generates a warning, CS8123, telling you that the names on 
		// the right side of the assignment, Alpha and Beta are ignored because they 
		// conflict with the names on the left side, First and Second.

		// The examples above show the basic syntax to declare tuples. Tuples are most 
		// useful as return types for private and internal methods.Tuples provide a 
		// simple syntax for those methods to return multiple discrete values: 
		// You save the work of authoring a class or a struct that defines the type 
		// returned.There is no need for creating a new type.

		int [] numbers = { 2, 3, 1, -1, 5, 4 };
		var range = Range(numbers);
		WriteLine($"range: Min: {range.Min} Max: {range.Max}");

		// Test Deconstruct method.
		var p = new Point(3.14, 2.71);
		(double X, double Y) = p;
		WriteLine($"Point: p.X: {p.X:F2} p.Y: {p.Y:F2}");
	}
	// Creating a tuple is more efficient and more productive. It is a simpler, 
	// lightweight syntax to define a data structure that carries more than one 
	// value.The example method below returns the minimum and maximum values 
	// found in a sequence of integers:
	private static (int Max, int Min) Range(IEnumerable<int> numbers)
	{
		int min = int.MaxValue;
		int max = int.MinValue;
		foreach (var n in numbers)
		{
			min = (n < min) ? n : min;
			max = (n > max) ? n : max;
		}
		return (max, min);
	}

	// Using tuples in this way offers several advantages:

	// You save the work of authoring a class or a struct that 
	// defines the type returned.

	// You do not need to create new type.

	// The language enhancements removes the need to call the Create<T1>(T1) methods.

	// The declaration for the method provides the names for the fields 
	// of the tuple that is returned.When you call the method, the return 
	// value is a tuple whose fields are Max and Min:
	// int[] numbers = { 2, 3, 1, -1, 5, 4 };
	// var range = Range(numbers);

	// You can also provide a similar deconstruction for any type in .NET. 
	// This is done by writing a Deconstruct method as a member of the class. 
	// That Deconstruct method provides a set of out arguments for each of 
	// the properties you want to extract. Consider this Point class that 
	// provides a deconstructor method that extracts the X and Y coordinates:
	public class Point
	{
		public Point(double x, double y)
		{
			this.X = x;
			this.Y = y;
		}

		public double X { get; }
		public double Y { get; }

		public void Deconstruct(out double x, out double y)
		{
			x = this.X;
			y = this.Y;
		}
	}

	// You can extract the individual fields by assigning a tuple to a Point:
	// var p = new Point(3.14, 2.71);
	// (double X, double Y) = p;

	// You are not bound by the names defined in the Deconstruct method. 
	// You can rename the extract variables as part of the assignment:
}
