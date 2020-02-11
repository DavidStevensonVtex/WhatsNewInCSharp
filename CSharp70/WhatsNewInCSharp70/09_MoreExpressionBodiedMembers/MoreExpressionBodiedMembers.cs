// More expression-bodied members
// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7#more-expression-bodied-members

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// C# 6 introduced expression-bodied members for member functions, and 
// read-only properties. C# 7 expands the allowed members that can be 
// implemented as expressions. In C# 7, you can implement constructors, 
// finalizers, and get and set accessors on properties and indexers. 

// The following code shows examples of each:

class ExpressionMembersExample
{
	// Expression-bodied constructor
	public ExpressionMembersExample(string label) => this.Label = label;

	// Expression-bodied finalizer
	~ExpressionMembersExample() => Console.Error.WriteLine("Finalized!");

	private string label;

	// Expression-bodied get / set accessors.
	public string Label
	{
		get => label;
		set => this.label = value ?? "Default label";
	}
}

// This example does not need a finalizer, but it is shown to demonstrate 
// the syntax. You should not implement a finalizer in your class unless 
// it is necessary to release unmanaged resources. You should also consider 
// using the SafeHandle class instead of managing unmanaged resources directly.

// These new locations for expression-bodied members represent an important 
// milestone for the C# language: These features were implemented by community 
// members working on the open-source Roslyn project.