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

	public Student(string firstName, string lastName)
	{
		FirstName = firstName;
		LastName = lastName;
	}

	public ICollection<double> Grades { get; } = new List<double>();

	public Standing YearInSchool { get; set; } = Standing.Freshman;
}