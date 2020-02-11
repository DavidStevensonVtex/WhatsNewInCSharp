// Numeric literal syntax improvements
// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7#numeric-literal-syntax-improvements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Misreading numeric constants can make it harder to understand 
// code when reading it for the first time. This often occurs 
// when those numbers are used as bit masks or other symbolic 
// rather than numeric values. C# 7 includes two new features 
// to make it easier to write numbers in the most readable fashion 
// for the intended use: binary literals, and digit separators.

// For those times when you are creating bit masks, or whenever 
// a binary representation of a number makes the most readable 
// code, write that number in binary:


class NumericLiteralSyntaxImprovements
{
	public const int One = 0b0001;
	public const int Two = 0b0010;
	public const int Four = 0b0100;
	public const int Eight = 0b1000;

	// The 0b at the beginning of the constant indicates that 
	// the number is written as a binary number.

	// Binary numbers can get very long, so it's often easier 
	// to see the bit patterns by introducing the _ as a digit 
	// separator:

	public const int Sixteen = 0b0001_0000;
	public const int ThirtyTwo = 0b0010_0000;
	public const int SixtyFour = 0b0100_0000;
	public const int OneHundredTwentyEight = 0b1000_0000;

	// The digit separator can appear anywhere in the constant. 
	// For base 10 numbers, it would be common to use it as a 
	// thousands separator:
	public const long BillionsAndBillions = 100_000_000_000;

	// The digit separator can be used with decimal, float 
	// and double types as well:

	public const double AvogadroConstant = 6.022_140_857_747_474e23;
	public const decimal GoldenRatio = 1.618_033_988_749_894_848_204_586_834_365_638_117_720_309_179M;

	// Taken together, you can declare numeric constants with much more readability.

	static void Main(string[] args)
	{
	}
}
