// private protected access modifier
// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7-2#private-protected-access-modifier

// Finally, a new compound access modifier: private protected indicates 
// that a member may be accessed by containing class or derived classes 
// that are declared in the same assembly. While protected internal allows 
// access by derived classes or classes that are in the same assembly, 
// private protected limits access to derived types declared in the same assembly.

// For more information see access modifiers in the language reference.

// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/access-modifiers

// private protected (C# Reference)

// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/private-protected




// Assembly1.cs  
// Compile with: /target:library  
public class BaseClass
{
	private protected int myValue = 0;

	private void TestAccessingValue()
	{
		myValue = 123;
	}
}

public class DerivedClass1 : BaseClass
{
	void Access()
	{
		BaseClass baseObject = new BaseClass();

		// Error CS1540, because myValue can only be accessed by
		// classes derived from BaseClass.
		//baseObject.myValue = 5;

		this.myValue = 123;

		// OK, accessed through the current derived class instance
		myValue = 5;
	}
}

class DerivedClass3 : BaseClass
{
	void Access()
	{
		// can be accessed by derived types in this assembly
		myValue = 10;
	}
}