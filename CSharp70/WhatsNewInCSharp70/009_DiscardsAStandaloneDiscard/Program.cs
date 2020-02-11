// A standalone discard
// https://docs.microsoft.com/en-us/dotnet/csharp/discards#a-standalone-discard

// You can use a standalone discard to indicate any variable that you choose 
// to ignore.The following example uses a standalone discard to ignore the 
// Task object returned by an asynchronous operation. This has the effect 
// of suppressing the exception that the operation throws as it is about 
// to complete.

using System;
using System.Threading.Tasks;

public class Example
{
	public static void Main()
	{
		ExecuteAsyncMethods().Wait();
	}

	private static async Task ExecuteAsyncMethods()
	{
		Console.WriteLine("About to launch a task...");
		// The discard is in the following line.
		_ = Task.Run(() => {
			var iterations = 0;
			for (int ctr = 0; ctr < int.MaxValue; ctr++)
				iterations++;
			Console.WriteLine("Completed looping operation...");
			throw new InvalidOperationException();
		});
		await Task.Delay(5000);
		Console.WriteLine("Exiting after 5 second delay");
	}
}
// The example displays output like the following:
//       About to launch a task...
//       Completed looping operation...
//       Exiting after 5 second delay