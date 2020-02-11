﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


// Exception Filters

// Another new feature in C# 6 is exception filters. Exception Filters are 
// clauses that determine when a given catch clause should be applied. If 
// the expression used for an exception filter evaluates to true, the catch 
// clause performs its normal processing on an exception. If the expression 
// evaluates to false, then the catch clause is skipped.

// One use is to examine information about an exception to determine if a 
// catch clause can process the exception:

public class ExceptionFilterExamples
{
	public static async Task<string> MakeRequest()
	{
		var client = new System.Net.Http.HttpClient();
		var streamTask = client.GetStringAsync("https://localHost:10000");
		try
		{
			var responseText = await streamTask;
			return responseText;
		}
		catch (System.Net.Http.HttpRequestException e) when (e.Message.Contains("301"))
		{
			return "Site Moved";
		}
	}

	// The code generated by exception filters provides better information about 
	// an exception that is thrown and not processed. Before exception filters 
	// were added to the language, you would need to create code like the following:

	public static async Task<string> MakeRequestOldWay()
	{
		var client = new System.Net.Http.HttpClient();
		var streamTask = client.GetStringAsync("https://localHost:10000");
		try
		{
			var responseText = await streamTask;
			return responseText;
		}
		catch (System.Net.Http.HttpRequestException e)
		{
			if (e.Message.Contains("301"))
				return "Site Moved";
			else
				throw;
		}
	}

	// The point where the exception is thrown changes between these two examples. 
	// In the previous code, where a throw clause is used, any stack trace analysis 
	// or examination of crash dumps will show that the exception was thrown from 
	// the throw statement in your catch clause. The actual exception object will 
	// contain the original call stack, but all other information about any variables 
	// in the call stack between this throw point and the location of the original 
	// throw point has been lost.

	// Contrast that with how the code using an exception filter is processed: 
	// the exception filter expression evaluates to false. Therefore, execution 
	// never enters the catch clause.Because the catch clause does not execute, 
	// no stack unwinding takes place.That means the original throw location is 
	// preserved for any debugging activities that would take place later.

	// Whenever you need to evaluate fields or properties of an exception, instead 
	// of relying solely on the exception type, use an exception filter to preserve 
	// more debugging information.

	// Another recommended pattern with exception filters is to use them for logging 
	// routines. This usage also leverages the manner in which the exception throw 
	// point is preserved when an exception filter evaluates to false.

	// A logging method would be a method whose argument is the exception that 
	// unconditionally returns false:
	private void PerformFailingOperation()
	{
		if (DateTime.Now.Millisecond == 500)
			throw new Exception("At the half way mark");
	}
	public void MethodThatFailsSometimes()
	{
		try
		{
			PerformFailingOperation();
		}
		catch (Exception e) when (e.LogException())
		{
			// This is never reached!
		}
	}

	// The exceptions are never caught, because the LogException method 
	// always returns false. That always false exception filter means that 
	// you can place this logging handler before any other exception handlers:
	public void MethodThatFailsButHasRecoveryPath()
	{
		try
		{
			PerformFailingOperation();
		}
		catch (Exception e) when (e.LogException())
		{
			// This is never reached!
		}
		catch (RecoverableException ex)
		{
			Console.WriteLine(ex.ToString());
			// This can still catch the more specific
			// exception because the exception filter
			// above always returns false.
			// Perform recovery here 
		}
	}

	// The preceding example highlights a very important facet of exception 
	// filters. The exception filters enable scenarios where a more general 
	// exception catch clause may appear before a more specific one. It's 
	// also possible to have the same exception type appear in multiple 
	// catch clauses:

	public static async Task<string> MakeRequestWithNotModifiedSupport()
	{
		var client = new System.Net.Http.HttpClient();
		var streamTask = client.GetStringAsync("https://localHost:10000");
		try
		{
			var responseText = await streamTask;
			return responseText;
		}
		catch (System.Net.Http.HttpRequestException e) when (e.Message.Contains("301"))
		{
			return "Site Moved";
		}
		catch (System.Net.Http.HttpRequestException e) when (e.Message.Contains("304"))
		{
			return "Use the Cache";
		}
	}

	// Another recommended pattern helps prevent catch clauses from processing exceptions 
	// when a debugger is attached. This technique enables you to run an application with 
	// the debugger, and stop execution when an exception is thrown.

	// In your code, add an exception filter so that any recovery code executes only when 
	// a debugger is not attached:
	public void MethodThatFailsWhenDebuggerIsNotAttached()
	{
		try
		{
			PerformFailingOperation();
		}
		catch (Exception e) when (e.LogException())
		{
			// This is never reached!
		}
		catch (RecoverableException ex) when (!System.Diagnostics.Debugger.IsAttached)
		{
			Console.WriteLine(ex.ToString());
			// Only catch exceptions when a debugger is not attached.
			// Otherwise, this should stop in the debugger. 
		}
	}

	// After adding this in code, you set your debugger to break on all unhandled 
	// exceptions. Run the program under the debugger, and the debugger breaks 
	// whenever PerformFailingOperation() throws a RecoverableException. The debugger 
	// breaks your program, because the catch clause won't be executed due to the 
	// false-returning exception filter.

}

public static class ExceptionExtensions
{

	// Whenever you want to log an exception, you can add a catch clause, 
	// and use this method as the exception filter:
	public static bool LogException(this Exception e)
	{
		Console.Error.WriteLine($"Exceptions happen: {e}");
		return false;
	}
}

public class RecoverableException : Exception { }