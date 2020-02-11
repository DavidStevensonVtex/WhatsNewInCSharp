// Throw Expressions
// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7#throw-expressions

using System;

// In C#, throw has always been a statement. Because throw is a statement, 
// not an expression, there were C# constructs where you could not use it. 
// These included conditional expressions, null coalescing expressions, 
// and some lambda expressions. The addition of expression-bodied members 
// adds more locations where throw expressions would be useful. So that 
// you can write any of these constructs, C# 7 introduces throw expressions.

// The syntax is the same as you've always used for throw statements. The 
// only difference is that now you can place them in new locations, such 
// as in a conditional expression:

class ConfigResource
{ }
class ApplicationOptions
{
	private string name;
	public string Name
	{
		get => name;
		set => name = value ??
			throw new ArgumentNullException(
				paramName: nameof(value), 
				message: "New name must not be null");
	}
	private static ConfigResource LoadConfigResourceOrDefault() => null ;

	// This features enables using throw expressions in initialization expressions:
	private ConfigResource loadedConfig = LoadConfigResourceOrDefault() ??
		throw new InvalidOperationException("Could not load config");


	// Previously, those initializations would need to be in a constructor, 
	// with the throw statements in the body of the constructor:
	public ApplicationOptions()
	{
		loadedConfig = LoadConfigResourceOrDefault();
		if (loadedConfig == null)
			throw new InvalidOperationException("Could not load config");

	}
}

// Note

// Both of the preceding constructs will cause exceptions to be thrown 
// during the construction of an object. Those are often difficult to 
// recover from. For that reason, designs that throw exceptions during 
// construction are discouraged.

