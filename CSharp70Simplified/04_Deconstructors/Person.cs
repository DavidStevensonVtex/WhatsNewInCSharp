// https://docs.microsoft.com/en-us/dotnet/csharp/discards#tuple-and-object-deconstruction

// The Deconstruct method of a class, structure, or interface also allows 
// you to retrieve and deconstruct a specific set of data from an object. 
// You can use discards when you are interested in working with only a 
// subset of the deconstructed values. Ihe following example deconstructs 
// a Person object into four strings (the first and last names, the city, 
// and the state), but discards the last name and the state.

using System;

public class Person
{
	public string FirstName { get; set; }
	public string MiddleName { get; set; }
	public string LastName { get; set; }
	public string City { get; set; }
	public string State { get; set; }

	public Person(string fname, string mname, string lname,
				  string cityName, string stateName)
	{
		FirstName = fname;
		MiddleName = mname;
		LastName = lname;
		City = cityName;
		State = stateName;
	}

	// Return the first and last name.
	public void Deconstruct(out string fname, out string lname)
	{
		Console.WriteLine("Deconstruct #1");
		fname = FirstName;
		lname = LastName;
	}

	public void Deconstruct(out string fname, out string mname, out string lname)
	{
		Console.WriteLine("Deconstruct #2");
		fname = FirstName;
		mname = MiddleName;
		lname = LastName;
	}

	public void Deconstruct(out string fname, out string lname,
							out string city, out string state)
	{
		Console.WriteLine("Deconstruct #3");
		fname = FirstName;
		lname = LastName;
		city = City;
		state = State;
	}
}