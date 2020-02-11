// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-6
// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-6#expression-bodied-function-members

public class Student
{
	public string FirstName { get; }
	public string LastName { get; }

	public Student(string firstName, string lastName)
	{
		FirstName = firstName;
		LastName = lastName;
	}

	// Expression-bodied function members

	// The body of a lot of members that we write consist of only one 
	// statement that can be represented as an expression. You can reduce 
	// that syntax by writing an expression-bodied member instead. It works 
	// for methods and read-only properties. For example, an override of 
	// ToString() is often a great candidate:

	public override string ToString() => $"{LastName}, {FirstName}";

	// You can also use expression-bodied members in read-only properties as well:

	public string FullName => $"{FirstName} {LastName}";

	public string FullName2
	{
		get {  return $"{FirstName} {LastName}"; }
	}
}