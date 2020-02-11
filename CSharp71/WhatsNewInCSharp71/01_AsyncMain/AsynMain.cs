// Async main
// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7-1#async-main

using System.Threading.Tasks;

class Program
{
	// An async main method enables you to use await in your Main method. 

	// Previously you would need to write:
	static Task<int> DoAsyncWork()
	{
		return Task.FromResult<int>(123);
	}
	static int MainOld(string[] args)
	{
		return DoAsyncWork().GetAwaiter().GetResult();
		//return DoAsyncWork().Result;
	}

	// You can now write:

	static async Task<int> Main2()
	{
		// This could also be replaced with the body
		// DoAsyncWork, including its await expressions:
		return await DoAsyncWork();
	}

	// If your program doesn't return an exit code, you can 
	// declare a Main method that returns a Task:


	static Task SomeAsyncMethod()
	{
		return Task.Delay(1000);
	}
	static async Task Main() // MainCSharp71Example2()
	{
		await SomeAsyncMethod();
	}
}

// You can read more about the details in the async main topic 
// in the programming guide.
// Main() and command-line arguments (C# Programming Guide)
// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/main-and-command-args/index