// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-6

using System;

// using static
// The using static enhancement enables you to import the static methods of 
// a single class. Previously, the using statement imported all types in a 
// namespace.

// Often we use a class' static methods throughout our code. Repeatedly 
// typing the class name can obscure the meaning of your code. A common 
// example is when you write classes that perform many numeric calculations. 
// Your code will be littered with Sin, Sqrt and other calls to different 
// methods in the Math class. The new using static syntax can make these 
// classes much cleaner to read. You specify the class you're using:

using static System.Math;

// And now, you can use any static method in the Math class without qualifying 
// the Math class. The Math class is a great use case for this feature because 
// it does not contain any instance methods. You can also use using static to 
// import a class' static methods for a class that has both static and instance 
// methods. One of the most useful examples is String:

using static System.String;



public class UsingStaticExample
{
	public void ExampleCalls(string lastName)
	{
		// You can now call static methods defined in the String class without 
		// qualifying those methods as members of that class:

		if (IsNullOrWhiteSpace(lastName))
			throw new ArgumentException(message: "Cannot be blank", 
				paramName: nameof(lastName));
	}
}