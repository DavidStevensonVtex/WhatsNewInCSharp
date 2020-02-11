// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-6#await-in-catch-and-finally-blocks

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Await in Catch and Finally blocks
// C# 5 had several limitations around where you could place await expressions. 
// One of those has been removed in C# 6. You can now use await in catch or 
// finally expressions.

// The addition of await expressions in catch and finally blocks may appear to 
// complicate how those are processed.Let's add an example to discuss how this appears. 
// In any async method, you can use an await expression in a finally clause.

// With C# 6, you can also await in catch expressions. This is most often used 
// with logging scenarios:

public class AwaitInCatchAndFinallyBlocksExample
{ 
	public static async Task<string> MakeRequestAndLogFailures()
	{
		await logMethodEntrance();
		var client = new System.Net.Http.HttpClient();
		var streamTask = client.GetStringAsync("https://localHost:10000");
		try
		{
			var responseText = await streamTask;
			return responseText;
		}
		catch (System.Net.Http.HttpRequestException e) when (e.Message.Contains("301"))
		{
			await logError("Recovered from redirect", e);
			return "Site Moved";
		}
		finally
		{
			await logMethodExit();
			client.Dispose();
		}
	}

	// The implementation details for adding await support inside catch and 
	// finally clauses ensures that the behavior is consistent with the behavior 
	// for synchronous code. When code executed in a catch or finally clause 
	// throws, execution looks for a suitable catch clause in the next 
	// surrounding block. If there was a current exception, that exception is 
	// lost. The same happens with awaited expressions in catch and finally 
	// clauses: a suitable catch is searched for, and the current exception, 
	// if any, is lost.

	public static async Task logMethodEntrance()
	{
		await Task.Delay(1000);
	}
	public static async Task logError(string s, Exception e)
	{
		await Task.Delay(1000);
	}
	public static async Task logMethodExit()
	{
		await Task.Delay(1000);
	}
}

