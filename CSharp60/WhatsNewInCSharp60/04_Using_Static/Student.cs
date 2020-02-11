// The static using feature and extension methods interact in interesting ways, 
// and the language design included some rules that specifically address those 
// interactions. The goal is to minimize any chances of breaking changes in 
// existing codebases, including yours.

// Extension methods are only in scope when called using the extension method 
// invocation syntax, not when called as a static method. You'll often see this 
// in LINQ queries. You can import the LINQ pattern by importing Enumerable.

using System.Collections.Generic;
using static System.Linq.Enumerable;

// This imports all the methods in the Enumerable class. However, the extension 
// methods are only in scope when called as extension methods. They are not in 
// scope if they are called using the static method syntax:

public class Student
{
	public string FirstName { get; }
	public string LastName { get; }
	public ICollection<double> Grades { get; } = new List<double>();

	public Student(string firstName, string lastName)
	{
		FirstName = firstName;
		LastName = lastName;
	}

	// using static System.Linq.Enumerable;
	// This imports all the methods in the Enumerable class. However, 
	// the extension methods are only in scope when called as extension methods. 
	// They are not in scope if they are called using the static method syntax:
	public bool MakesDeansList()
	{
		return Grades.All(g => g > 3.5) && Grades.Any();
		// Code below generates CS0103: 
		// The name 'All' does not exist in the current context.
		//return All(Grades, g => g > 3.5) && Grades.Any();
	}

	// This decision is because extension methods are typically called using 
	// extension method invocation expressions. In the rare case where they 
	// are called using the static method call syntax it is to resolve ambiguity. 
	// Requiring the class name as part of the invocation seems wise.

	// There's one last feature of static using. The static using directive also 
	// imports any nested types. That enables you to reference any nested types 
	// without qualification.
}