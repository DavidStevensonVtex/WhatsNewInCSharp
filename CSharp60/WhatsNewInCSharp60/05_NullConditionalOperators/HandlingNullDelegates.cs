// https://docs.microsoft.com/en-us/dotnet/csharp/delegates-patterns#handling-null-delegates

using System;

// Handling Null Delegates

// Finally, let's update the LogMessage method so that it is robust for those 
// cases when no output mechanism is selected. The current implementation 
// will throw a NullReferenceException when the WriteMessage delegate does 
// not have an invocation list attached. You may prefer a design that 
// silently continues when no methods have been attached. This is easy using 
// the null conditional operator, combined with the Delegate.Invoke() method:

public class HandlingNullDelegates
{
	public static Action<string> WriteMessage = null;
	public static void LogMessage(string msg)
	{
		WriteMessage?.Invoke(msg);
	}
}

// The null conditional operator (?.) short-circuits when the left operand 
// (WriteMessage in this case) is null, which means no attempt is made to 
// log a message.

// You won't find the Invoke() method listed in the documentation for 
// System.Delegate or System.MulticastDelegate. The compiler generates a 
// type safe Invoke method for any delegate type declared. In this example, 
// that means Invoke takes a single string argument, and has a void return type.
