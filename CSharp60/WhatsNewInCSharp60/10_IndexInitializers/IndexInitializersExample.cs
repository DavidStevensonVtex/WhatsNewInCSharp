// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-6#index-initializers

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Index Initializers

// Index Initializers is one of two features that make collection initializers 
// more consistent. In earlier releases of C#, you could use collection initializers 
// only with sequence style collections:



public class IndexInitializersExample
{
	private List<string> messages = new List<string>
	{
		"Page not Found",
		"Page moved, but left a forwarding address.",
		"The web server can't come out to play today."
	};

	// Now, you can also use them with Dictionary<TKey,TValue> collections and similar types:
	private Dictionary<int, string> webErrors = new Dictionary<int, string>
	{
		[404] = "Page not Found",
		[302] = "Page moved, but left a forwarding address.",
		[500] = "The web server can't come out to play today."
	};
	// This feature means that associative containers can be initialized using syntax 
	// similar to what's been in place for sequence containers for several versions.
}

// Extension Add methods in collection initializers

// Another feature that makes collection initialization easier is the ability to use 
// an extension method for the Add method.This feature was added for parity with 
// Visual Basic.

// The feature is most useful when you have a custom collection class that has a 
// method with a different name to semantically add new items.

// For example, consider a collection of students like this:

public class Enrollment : IEnumerable<Student>
{
	private List<Student> allStudents = new List<Student>();

	public void Enroll(Student s)
	{
		allStudents.Add(s);
	}

	public IEnumerator<Student> GetEnumerator()
	{
		return ((IEnumerable<Student>)allStudents).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return ((IEnumerable<Student>)allStudents).GetEnumerator();
	}
}

public class ClassList
{
	// The Enroll method adds a student.But it doesn't follow the Add pattern. 
	// In previous versions of C#, you could not use collection initializers 
	// with an Enrollment object:

	public Enrollment CreateEnrollment()
	{

		// CS1061  'Enrollment' does not contain a definition for 'Add' and 
		// no extension method 'Add' accepting a first argument of type 'Enrollment' 
		// could be found

		// For fix, see StudentExtensions class.

		var classList = new Enrollment()
			{
				new Student("Lessie", "Crosby"),
				new Student("Vicki", "Petty"),
				new Student("Ofelia", "Hobbs"),
				new Student("Leah", "Kinney"),
				new Student("Alton", "Stoker"),
				new Student("Luella", "Ferrell"),
				new Student("Marcy", "Riggs"),
				new Student("Ida", "Bean"),
				new Student("Ollie", "Cottle"),
				new Student("Tommy", "Broadnax"),
				new Student("Jody", "Yates"),
				new Student("Marguerite", "Dawson"),
				new Student("Francisca", "Barnett"),
				new Student("Arlene", "Velasquez"),
				new Student("Jodi", "Green"),
				new Student("Fran", "Mosley"),
				new Student("Taylor", "Nesmith"),
				new Student("Ernesto", "Greathouse"),
				new Student("Margret", "Albert"),
				new Student("Pansy", "House"),
				new Student("Sharon", "Byrd"),
				new Student("Keith", "Roldan"),
				new Student("Martha", "Miranda"),
				new Student("Kari", "Campos"),
				new Student("Muriel", "Middleton"),
				new Student("Georgette", "Jarvis"),
				new Student("Pam", "Boyle"),
				new Student("Deena", "Travis"),
				new Student("Cary", "Totten"),
				new Student("Althea", "Goodwin")
			};

		return classList;
	}
}

// Now you can, but only if you create an extension method that maps Add to Enroll:
public static class StudentExtensions
{
	public static void Add(this Enrollment e, Student s) => e.Enroll(s);
}