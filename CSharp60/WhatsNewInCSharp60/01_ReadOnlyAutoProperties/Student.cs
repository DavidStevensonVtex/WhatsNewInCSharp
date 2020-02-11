public class Student
{
	// Read-only auto-properties
	// Read-only auto-properties provide a more concise syntax to create immutable types.
	// The closest you could get to immutable types in earlier versions of C# was to declare private setters:

	//public string FirstName { get; private set; }
	//public string LastName { get; private set; }

	// Using this syntax, the compiler doesn't ensure that the type really is immutable. It only enforces 
	// that the FirstName and LastName properties are not modified from any code outside the class.

	// Read-only auto-properties enable true read-only behavior. You declare the auto-property with only a get accessor:

	public string FirstName { get; }
	public string LastName { get; }

	public void ChangeName(string newLastName)
	{
		// Generates CS0200: Property or indexer cannot be assigned to -- it is read only
		// LastName = newLastName;
	}
}