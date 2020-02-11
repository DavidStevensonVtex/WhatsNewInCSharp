// Deconstructing user defined types

// Any tuple type can be deconstructed as shown above.It's also easy to enable deconstruction on 
// any user defined type (classes, structs, or even interfaces).

// The type author can define one or more Deconstruct methods that assign values to any number of 
// out variables representing the data elements that make up the type. For example, the following 
// Person type defines a Deconstruct method that deconstructs a person object into the elements 
// representing the first name and last name:

using System;
using static System.Console;

public class Person
{
    public string FirstName { get; }
    public string LastName { get; }

    public Person(string first, string last)
    {
        FirstName = first;
        LastName = last;
    }

    public void Deconstruct(out string firstName, out string lastName)
    {
        firstName = FirstName;
        lastName = LastName;
    }
}

// The deconstruct method enables assignment from a Person to two strings, representing the
// FirstName and LastName properties:

public class TestPersonDeconstruction
{
    public static void Main()
    {
        var p = new Person("Althea", "Goodwin");
        var (first, last) = p;
    }
}

// You can enable deconstruction even for types you did not author. The Deconstruct method can be 
// an extension method that unpackages the accessible data members of an object. The example below 
// shows a Student type, derived from the Person type, and an extension method that deconstructs a 
// Student into three variables, representing the FirstName, the LastName and the GPA:

public class Student : Person
{
    public double GPA { get; }
    public Student(string first, string last, double gpa) :
        base(first, last)
    {
        GPA = gpa;
    }
}

public static class Extensions
{
    public static void Deconstruct(this Student s, out string first, out string last, out double gpa)
    {
        first = s.FirstName;
        last = s.LastName;
        gpa = s.GPA;
    }
}

// A Student object now has two accessible Deconstruct methods: the extension method declared 
// for Student types, and the member of the Person type. Both are in scope, and that enables 
// a Student to be deconstructed into either two variables or three. If you assign a student 
// to three variables, the first name, last name, and GPA are all returned. If you assign a 
// student to two variables, only the first name and the last name are returned.

public class TestStudentDeconstruction
{
    public static void Main()
    {
        var s1 = new Student("Cary", "Totten", 4.5);
        WriteLine($"Student: s1.FirstName: {s1.FirstName} s1.LastName: {s1.LastName} s1.GPA: {s1.GPA}");
        var (fName, lName, gpa) = s1;
        WriteLine($"fName: {fName} lName: {lName} gpa: {gpa}");
    }
}

// You should be very careful defining multiple Deconstruct methods in a class or a class 
// hierarchy. Multiple Deconstruct methods that have the same number of out parameters can 
// quickly cause ambiguities. Callers may not be able to easily call the desired Deconstruct 
// method.

// In this example, there is minimal chance for an ambiguous call because the Deconstruct 
// method for Person has two output parameters, and the Deconstruct method for Student has 
// three.

// Conclusion

// The new language and library support for named tuples makes it much easier to work with 
// designs that use data structures that store multiple elements but do not define behavior, 
// as classes and structs do. It's easy and concise to use tuples for those types. You get 
// all the benefits of static type checking, without needing to author types using the more 
// verbose class or struct syntax. Even so, they are most useful for utility methods that 
// are private, or internal. Create user defined types, either class or struct types when 
// your public methods return a value that has multiple elements.
