// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-6#null-conditional-operators

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class NullConditionalOperators
{
	public event EventHandler SomethingHappened;
	public void ExampleCode()
	{
		Person person = new Person("David", "Stevenson");


		// Null-conditional operators
		// Null values complicate code. You need to check every access of variables 
		// to ensure you are not dereferencing null.The null conditional operator 
		// makes those checks much easier and fluid.

		// Simply replace the member access.with?.:

		var first = person?.FirstName;

		// In the preceding example, the variable first is assigned null if the 
		// person object is null. Otherwise, it gets assigned the value of the 
		// FirstName property. Most importantly, the ?. means that this line of 
		// code does not generate a NullReferenceException when the person 
		// variable is null. Instead, it short-circuits and produces null.

		// Also, note that this expression returns a string, regardless of the 
		// value of person.In the case of short circuiting, the null value 
		// returned is typed to match the full expression.

		// You can often use this construct with the null coalescing operator 
		// to assign default values when one of the properties are null:

		first = person?.FirstName ?? "Unspecified";

		// The right hand side operand of the ?. operator is not limited to 
		// properties or fields. You can also use it to conditionally invoke 
		// methods. The most common use of member functions with the null 
		// conditional operator is to safely invoke delegates (or event handlers) 
		// that may be null. You'll do this by calling the delegate's Invoke 
		// method using the ?. operator to access the member. You can see an 
		// example in the delegate patterns topic.

		// https://docs.microsoft.com/en-us/dotnet/csharp/delegates-patterns#handling-null-delegates

		// The rules of the ?. operator ensure that the left-hand side of the 
		// operator is evaluated only once. This is important and enables many 
		// idioms, including the example using event handlers.Let's start with 
		// the event handler usage. In previous versions of C#, you were 
		// encouraged to write code like this:

		EventArgs eventArgs = new EventArgs();
		var handler = this.SomethingHappened;
		if (handler != null)
			handler(this, eventArgs);

		// This was preferred over a simpler syntax:

		// // Not recommended
		if (this.SomethingHappened != null)
			this.SomethingHappened(this, eventArgs);

		// Important

		// The preceding example introduces a race condition. The 
		// SomethingHappened event may have subscribers when checked against 
		// null, and those subscribers may have been removed before the event 
		// is raised. That would cause a NullReferenceException to be thrown.

		// In this second version, the SomethingHappened event handler might 
		// be non-null when tested, but if other code removes a handler, 
		// it could still be null when the event handler was called.

		// The compiler generates code for the?. operator that ensures the 
		// left side(this.SomethingHappened) of the ?. expression is evaluated 
		// once, and the result is cached:

		// preferred in C# 6:
		this.SomethingHappened?.Invoke(this, eventArgs);

		// Ensuring that the left side is evaluated only once also enables 
		// you to use any expression, including method calls, on the left 
		// side of the?.Even if these have side-effects, they are evaluated 
		// once, so the side effects occur only once. You can see an example 
		// in our content on events.

	}
}

