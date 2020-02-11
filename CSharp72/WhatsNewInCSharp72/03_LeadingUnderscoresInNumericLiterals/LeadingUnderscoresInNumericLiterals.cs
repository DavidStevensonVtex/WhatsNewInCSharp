// Leading underscores in numeric literals
// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7-2#leading-underscores-in-numeric-literals

// The implementation of support for digit separators in C# 7.0 didn't 
// allow the _ to be the first character of the literal value. Hex and 
// binary numeric literals may now begin with an _.

class LeadingUnderscoresInNumericLiterals
{
	int binaryValue = 0b_0101_0101;
}
