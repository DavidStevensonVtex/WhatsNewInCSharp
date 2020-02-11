using System;
using System.Threading.Tasks;
using static System.Console;

class LocalFunctionsAsync
{
	public static Task<string> FirstWork(string address)
	{
		return Task.FromResult<string>("Address: " + address);
	}
	public static Task<string> SecondStep(int index, string name)
	{
		return Task.FromResult<string>($"Index: {index} name: {name}");
	}

	public static async Task<string> OldWay(string address, int index, string name)
	{
		if (string.IsNullOrWhiteSpace(address))
			throw new ArgumentException(
				message: "An address is required",
				paramName: nameof(address));
		if (index < 0)
			throw new ArgumentOutOfRangeException(
				paramName: nameof(index),
				message: "The index must be non-negative");
		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException(
				message: "You must supply a name",
				paramName: nameof(name));
		await Task.Delay(2000);
		var interimResult = await FirstWork(address);
		var secondResult = await SecondStep(index, name);
		return $"The results are {interimResult} and {secondResult}. Enjoy.";
	}
	public static Task<string> PerformLongRunningWork(string address, int index, string name)
	{
		if (string.IsNullOrWhiteSpace(address))
			throw new ArgumentException(
				message: "An address is required", 
				paramName: nameof(address));
		if (index < 0)
			throw new ArgumentOutOfRangeException(
				paramName: nameof(index), 
				message: "The index must be non-negative");
		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException(
				message: "You must supply a name", 
				paramName: nameof(name));

		return longRunningWorkImplementation();

		async Task<string> longRunningWorkImplementation()
		{
			await Task.Delay(2000);
			var interimResult = await FirstWork(address);
			var secondResult = await SecondStep(index, name);
			return $"The results are {interimResult} and {secondResult}. Enjoy.";
		}
	}
	static void Main(string[] args)
	{
		try
		{
			WriteLine(DateTime.Now);
			Task<string> task = OldWay(
				"123 Main Street", 456, null);
			WriteLine($"Result: {task.Result}");
				WriteLine(DateTime.Now);
		}
		// Asynchronous methods return AggregateException instead of other Exceptions.
		catch ( AggregateException ae )
		{
			WriteLine(ae.Message);
			foreach ( var ex in ae.InnerExceptions)
			{
				WriteLine(ex.Message);
			}
		}
		catch (Exception ex)
		{
			WriteLine(ex.Message);
		}
		finally
		{
			WriteLine(DateTime.Now);
		}
		WriteLine("\n\n");
		try
		{
			WriteLine(DateTime.Now);
			Task<string> task2 = PerformLongRunningWork(
				"123 Main Street", 456, null);
			WriteLine($"Result: {task2.Result}");
			WriteLine(DateTime.Now);
		}
		catch ( Exception ex )
		{
			WriteLine(ex.Message);
		}
		finally
		{
			WriteLine(DateTime.Now);
		}
	}
}