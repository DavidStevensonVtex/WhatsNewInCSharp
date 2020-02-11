using System.Collections.Generic;

public enum Standing
{
	Freshman,
	Sophomore,
	Junior,
	Senior
}
public class Student
{
	public string FirstName { get; }
	public string LastName { get; }

	// Auto-Property Initializers

	// Auto-Property Initializers let you declare the initial value for an 
	// auto-property as part of the property declaration.
	// In earlier versions, these properties would need to have setters and 
	// you would need to use that setter to initialize 
	// the data storage used by the backing field.Consider this class for a 
	// student that contains the name and a list of the 
	// student's grades:

	public Student(string firstName, string lastName)
	{
		FirstName = firstName;
		LastName = lastName;
	}

	// As this class grows, you may include other constructors. 
	// Each constructor needs to initialize this field, or you'll introduce errors.

	// C# 6 enables you to assign an initial value for the storage used 
	// by an auto-property in the auto-property declaration:

	public ICollection<double> Grades { get; } = new List<double>();

	// The Grades member is initialized where it is declared. That makes it 
	// easier to perform the initialization exactly once.The initialization 
	// is part of the property declaration, making it easier to equate the 
	// storage allocation with public interface for Student objects.+

	// Property Initializers can be used with read/write properties as well 
	// as read-only properties, as shown here.

	public Standing YearInSchool { get; set; } = Standing.Freshman;
}