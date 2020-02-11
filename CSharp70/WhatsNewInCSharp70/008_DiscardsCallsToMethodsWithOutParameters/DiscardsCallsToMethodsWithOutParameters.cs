// Calls to methods with out parameters
// https://docs.microsoft.com/en-us/dotnet/csharp/discards#calls-to-methods-with-out-parameters

// When calling the Deconstruct method to deconstruct a user-defined type (an 
// instance of a class, structure, or interface), you can discard the values 
// of individual out arguments. But you can also discard the value of out 
// arguments when calling any method with an out parameter.

// The following example calls the DateTime. TryParse(String, out DateTime) 
// method to determine whether the string representation of a date is valid 
// in the current culture.Because the example is concerned only with validating 
// the date string and not with parsing it to extract the date, the out argument 
// to the method is a discard.

using System;

public class Example
{
	public static void Main()
	{
		string[] dateStrings = {"05/01/2018 14:57:32.8", "2018-05-01 14:57:32.8",
							  "2018-05-01T14:57:32.8375298-04:00", "5/01/2018",
							  "5/01/2018 14:57:32.80 -07:00",
							  "1 May 2018 2:57:32.8 PM", "16-05-2018 1:00:32 PM",
							  "Fri, 15 May 2018 20:10:57 GMT" };
		foreach (string dateString in dateStrings)
		{
			if (DateTime.TryParse(dateString, out _))
				Console.WriteLine($"'{dateString}': valid");
			else
				Console.WriteLine($"'{dateString}': invalid");
		}
	}
}
// The example displays output like the following:
//       '05/01/2018 14:57:32.8': valid
//       '2018-05-01 14:57:32.8': valid
//       '2018-05-01T14:57:32.8375298-04:00': valid
//       '5/01/2018': valid
//       '5/01/2018 14:57:32.80 -07:00': valid
//       '1 May 2018 2:57:32.8 PM': valid
//       '16-05-2018 1:00:32 PM': invalid
//       'Fri, 15 May 2018 20:10:57 GMT': invalid