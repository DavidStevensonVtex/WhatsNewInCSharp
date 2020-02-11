// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7#pattern-matching

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Pattern matching

// Pattern matching is a feature that allows you to implement method dispatch 
// on properties other than the type of an object. You're probably already 
// familiar with method dispatch based on the type of an object. In Object 
// Oriented programming, virtual and override methods provide language syntax 
// to implement method dispatching based on an object's type. Base and Derived 
// classes provide different implementations. Pattern matching expressions 
// extend this concept so that you can easily implement similar dispatch 
// patterns for types and data elements that are not related through an 
// inheritance hierarchy.

// Pattern matching supports is expressions and switch expressions. Each enables 
// inspecting an object and its properties to determine if that object satisfies 
// the sought pattern.You use the when keyword to specify additional rules to 
// the pattern.



class PatternMatching
{
	// is expression

	// The is pattern expression extends the familiar is operator to query an object 
	// beyond its type.

	// Let's start with a simple scenario. We'll add capabilities to this scenario 
	// that demonstrate how pattern matching expressions make algorithms that work 
	// with unrelated types easy. We'll start with a method that computes the sum 
	// of a number of die rolls:
	public static int DiceSum(IEnumerable<int> values)
	{
		return values.Sum();
	}

	// You might quickly find that you need to find the sum of die rolls 
	// where some of the rolls are made with multiple dice(dice is the 
	// plural of die). Part of the input sequence may be multiple results 
	// instead of a single number:
	public static int DiceSum2(IEnumerable<object> values)
	{
		var sum = 0;
		foreach (var item in values)
		{
			if (item is int val)
				sum += val;
			else if (item is IEnumerable<object> subList)
				sum += DiceSum2(subList);
		}
		return sum;
	}

	// The is pattern expression works quite well in this scenario. 
	// As part of checking the type, you write a variable initialization. 
	// This creates a new variable of the validated runtime type.

	// As you keep extending these scenarios, you may find that you build 
	// more if and else if statements.Once that becomes unwieldy, you'll 
	// likely want to switch to switch pattern expressions.

	// switch statement updates
	// The match expression has a familiar syntax, based on the switch 
	// statement already part of the C# language. Let's translate the 
	// existing code to use a match expression before adding new cases:

	public static int DiceSum3(IEnumerable<object> values)
	{
		var sum = 0;
		foreach (var item in values)
		{
			switch (item)
			{
				case int val:
					sum += val;
					break;
				case IEnumerable<object> subList:
					sum += DiceSum3(subList);
					break;
			}
		}
		return sum;
	}

	// The match expressions have a slightly different syntax than the 
	// is expressions, where you declare the type and variable at the 
	// beginning of the case expression.

	// The match expressions also support constants.This can save time 
	// by factoring out simple cases:
	public static int DiceSum4(IEnumerable<object> values)
	{
		var sum = 0;
		foreach (var item in values)
		{
			switch (item)
			{
				case 0:
					break;
				case int val:
					sum += val;
					break;
				case IEnumerable<object> subList when subList.Any():
					sum += DiceSum4(subList);
					break;
				case IEnumerable<object> subList:
					break;
				case null:
					break;
				default:
					throw new InvalidOperationException("unknown item type");
			}
		}
		return sum;
	}

	// The code above adds cases for 0 as a special case of int, and null as 
	// a special case when there is no input. This demonstrates one important 
	// new feature in switch pattern expressions: the order of the case 
	// expressions now matters. The 0 case must appear before the general int 
	// case. Otherwise, the first pattern to match would be the int case, even 
	// when the value is 0. If you accidentally order match expressions such 
	// that a later case has already been handled, the compiler will flag that 
	// and generate an error.

	// This same behavior enables the special case for an empty input sequence.
	// You can see that the case for an IEnumerable item that has elements must 
	// appear before the general IEnumerable case.

	// This version has also added a default case. The default case is always 
	// evaluated last, regardless of the order it appears in the source. For 
	// that reason, convention is to put the default case last.

	static void Main(string[] args)
	{
	}
}
// Finally, let's add one last case for a new style of die. Some games 
// use percentile dice to represent larger ranges of numbers.

// Note

// Two 10-sided percentile dice can represent every number from 0 through 99. 
// One die has sides labelled 00, 10, 20, ... 90. The other die has sides 
// labeled 0, 1, 2, ... 9. Add the two die values together and you can get 
// every number from 0 through 99.

// To add this kind of die to your collection, first define a type to 
// represent the percentile dice.The TensDigit property stores values 
// 0, 10, 20, up to 90:

public struct PercentileDice
{
	public int OnesDigit { get; }
	public int TensDigit { get; }

	public PercentileDice(int tensDigit, int onesDigit)
	{
		this.OnesDigit = onesDigit;
		this.TensDigit = tensDigit;
	}
}

// Then, add a case match expression for the new type:
class PatternMatching2
{
	public static int DiceSum5(IEnumerable<object> values)
	{
		var sum = 0;
		foreach (var item in values)
		{
			switch (item)
			{
				case 0:
					break;
				case int val:
					sum += val;
					break;
				case PercentileDice dice:
					sum += dice.TensDigit + dice.OnesDigit;
					break;
				case IEnumerable<object> subList when subList.Any():
					sum += DiceSum5(subList);
					break;
				case IEnumerable<object> subList:
					break;
				case null:
					break;
				default:
					throw new InvalidOperationException("unknown item type");
			}
		}
		return sum;
	}
}

// The new syntax for pattern matching expressions makes it easier to 
// create dispatch algorithms based on an object's type, or other properties, 
// using a clear and concise syntax. Pattern matching expressions enable 
// these constructs on data types that are unrelated by inheritance.

// You can learn more about pattern matching in the topic dedicated to 
// pattern matching in C#.

// https://docs.microsoft.com/en-us/dotnet/csharp/pattern-matching