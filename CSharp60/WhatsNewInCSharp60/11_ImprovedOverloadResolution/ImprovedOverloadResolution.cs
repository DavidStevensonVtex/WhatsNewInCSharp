// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-6#improved-overload-resolution

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Improved overload resolution
// This last feature is one you probably won't notice. There were constructs where the 
// previous version of the C# compiler may have found some method calls involving 
// lambda expressions ambiguous. Consider this method:


public class ImprovedOverloadResolutionExample
{
	static Task DoThings()
	{
		return Task.FromResult(0);
	}

	public static void Main()
	{
		Task.Run(DoThings);
		// In earlier versions of C#, calling that method using the 
		// method group syntax would fail:

		// The earlier compiler could not distinguish correctly between 
		// Task.Run(Action) and Task.Run(Func<Task>()). In previous versions, 
		// you'd need to use a lambda expression as an argument:

		Task.Run(() => DoThings());
	}
}
