// Local Functions
// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7#local-functions

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Local functions

// Many designs for classes include methods that are called from only one 
// location.These additional private methods keep each method small and 
// focused. However, they can make it harder to understand a class when 
// reading it the first time.These methods must be understood outside of 
// the context of the single calling location.

// For those designs, local functions enable you to declare methods inside 
// the context of another method.This makes it easier for readers of the 
// class to see that the local method is only called from the context in 
// which is it declared.

// There are two very common use cases for local functions: public iterator 
// methods and public async methods. Both types of methods generate code that 
// reports errors later than programmers might expect. In the case of iterator 
// methods, any exceptions are observed only when calling code that enumerates 
// the returned sequence.In the case of async methods, any exceptions are only 
// observed when the returned Task is awaited.

// Let's start with an iterator method:

class Iterator
{
	public static IEnumerable<char> AlphabetSubset(char start, char end)
	{
		if (start < 'a' || start > 'z')
			throw new ArgumentOutOfRangeException(
				paramName: nameof(start), 
				message: "start must be a letter");
		if (end < 'a' || end > 'z')
			throw new ArgumentOutOfRangeException(
				paramName: nameof(end), 
				message: "end must be a letter");

		if (end <= start)
			throw new ArgumentException($"{nameof(end)} must be greater than {nameof(start)}");
		for (var c = start; c < end; c++)
			yield return c;
	}
	static void Main2(string[] args)
	{
		// Examine the code below that calls the iterator method incorrectly:
		var resultSet = Iterator.AlphabetSubset('f', 'a');
		Console.WriteLine("iterator created");
		foreach (var thing in resultSet)
			Console.Write($"{thing}, ");
	}

	// The exception is thrown when resultSet is iterated, not when resultSet 
	// is created. In this contained example, most developers could quickly 
	// diagnose the problem. However, in larger codebases, the code that 
	// creates an iterator often isn't as close to the code that enumerates 
	// the result. You can refactor the code so that the public method 
	// validates all arguments, and a private method generates the enumeration:

	public static IEnumerable<char> AlphabetSubset2(char start, char end)
	{
		if (start < 'a' || start > 'z')
			throw new ArgumentOutOfRangeException(paramName: nameof(start), message: "start must be a letter");
		if (end < 'a' || end > 'z')
			throw new ArgumentOutOfRangeException(paramName: nameof(end), message: "end must be a letter");

		if (end <= start)
			throw new ArgumentException($"{nameof(end)} must be greater than {nameof(start)}");
		return alphabetSubsetImplementation(start, end);
	}

	private static IEnumerable<char> alphabetSubsetImplementation(char start, char end)
	{
		for (var c = start; c < end; c++)
			yield return c;
	}
	static void Main3(string[] args)
	{
		// Examine the code below that calls the iterator method incorrectly:
		var resultSet = Iterator.AlphabetSubset2('f', 'a');
		Console.WriteLine("iterator created");
		foreach (var thing in resultSet)
			Console.Write($"{thing}, ");
	}

	// This refactored version will throw exceptions immediately because the 
	// public method is not an iterator method; only the private method uses 
	// the yield return syntax. However, there are potential problems with 
	// this refactoring. The private method should only be called from the 
	// public interface method, because otherwise all argument validation 
	// is skipped. Readers of the class must discover this fact by reading 
	// the entire class and searching for any other references to the 
	// alphabetSubsetImplementation method.

	// You can make that design intent more clear by declaring the 
	// alphabetSubsetImplementation as a local function inside the 
	// public API method:

	public static IEnumerable<char> AlphabetSubset3(char start, char end)
	{
		if (start < 'a' || start > 'z')
			throw new ArgumentOutOfRangeException(paramName: nameof(start), message: "start must be a letter");
		if (end < 'a' || end > 'z')
			throw new ArgumentOutOfRangeException(paramName: nameof(end), message: "end must be a letter");

		if (end <= start)
			throw new ArgumentException($"{nameof(end)} must be greater than {nameof(start)}");

		return alphabetSubsetImplementation();

		IEnumerable<char> alphabetSubsetImplementation()
		{
			for (var c = start; c < end; c++)
				yield return c;
		}
	}
	static void Main4(string[] args)
	{
		// Examine the code below that calls the iterator method incorrectly:
		var resultSet = Iterator.AlphabetSubset3('f', 'a');
		Console.WriteLine("iterator created");
		foreach (var thing in resultSet)
			Console.Write($"{thing}, ");
	}

	// The version above makes it clear that the local method is referenced 
	// only in the context of the outer method. The rules for local functions 
	// also ensure that a developer can't accidentally call the local function 
	// from another location in the class and bypass the argument validation.

	// The same technique can be employed with async methods to ensure that 
	// exceptions arising from argument validation are thrown before the 
	// asynchronous work begins:

	public static Task<string> FirstWork(string address)
	{
		return Task.FromResult<string>("Address: " + address);
	}
	public static Task<string> SecondStep(int index, string name)
	{
		return Task.FromResult<string>($"Index: {index} name: {name}");
	}
	public static Task<string> PerformLongRunningWork(string address, int index, string name)
	{
		if (string.IsNullOrWhiteSpace(address))
			throw new ArgumentException(message: "An address is required", paramName: nameof(address));
		if (index < 0)
			throw new ArgumentOutOfRangeException(paramName: nameof(index), message: "The index must be non-negative");
		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException(message: "You must supply a name", paramName: nameof(name));

		return longRunningWorkImplementation();

		async Task<string> longRunningWorkImplementation()
		{
			var interimResult = await FirstWork(address);
			var secondResult = await SecondStep(index, name);
			return $"The results are {interimResult} and {secondResult}. Enjoy.";
		}
	}
	static void Main(string[] args)
	{
		Task<string> task = PerformLongRunningWork("123 Main Street", 456, "David Stevenson");
		Console.WriteLine($"Result: {task.Result}");
	}
}

