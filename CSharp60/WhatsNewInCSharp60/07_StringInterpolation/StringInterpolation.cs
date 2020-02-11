using System;
using System.Collections.Generic;
using System.Linq;

// String Interpolation
// C# 6 contains new syntax for composing strings from a format string and 
// expressions that can be evaluated to produce other string values.

// Traditionally, you needed to use positional parameters in a method like string.Format:
public class Student
{
	public string FirstName { get; }
	public string LastName { get; }
	public ICollection<double> Grades { get; } = new List<double>();

	// With C# 6, the new string interpolation feature enables you to embed 
	// the expressions in the format string. Simple preface the string with $:

	public string FullName => $"{FirstName} {LastName}";

	// This initial example used variable expressions for the substituted expressions. 
	// You can expand on this syntax to use any expression. For example, you could 
	// compute a student's grade point average as part of the interpolation:

	public string GetFormattedGradePoint() =>
		$"Name: {LastName}, {FirstName}. G.P.A: {Grades.Average()}";

	// Running the preceding example, you would find that the output for 
	// Grades.Average() might have more decimal places than you would like. 
	// The string interpolation syntax supports all the format strings available 
	// using earlier formatting methods. You add the format strings inside the 
	// braces. Add a : following the expression to format:

	public string GetGradePointPercentage() =>
		$"Name: {LastName}, {FirstName}. G.P.A: {Grades.Average():F2}";

	// The preceding line of code will format the value for Grades.Average() as a floating-point number with two decimal places.

	// The : is always interpreted as the separator between the expression 
	// being formatted and the format string. This can introduce problems 
	// when your expression uses a : in another way, such as a 
	// conditional operator:

	// Error CS1003  Syntax error, ':' 

	//public string GetGradePointPercentages() =>
	//	$"Name: {LastName}, {FirstName}. G.P.A: {Grades.Any() ? Grades.Average() : double.NaN:F2}";

	// In the preceding example, the : is parsed as the beginning of the 
	// format string, not part of the conditional operator. In all cases 
	// where this happens, you can surround the expression with parentheses 
	// to force the compiler to interpret the expression as you intend:

	public string GetGradePointPercentages() =>
		$"Name: {LastName}, {FirstName}. G.P.A: {(Grades.Any() ? Grades.Average() : double.NaN):F2}";

	// There aren't any limitations on the expressions you can place between 
	// the braces. You can execute a complex LINQ query inside an interpolated 
	// string to perform computations and display the result:

	public string GetAllGrades() =>
		$@"All Grades: {Grades.OrderByDescending(g => g)
		.Select(s => s.ToString("F2")).Aggregate((partial, element) => $"{partial}, {element}")}";

	// You can see from this sample that you can even nest a string interpolation expression 
	// inside another string interpolation expression. This example is very likely more 
	// complex than you would want in production code. Rather, it is illustrative of the 
	// breadth of the feature. Any C# expression can be placed between the curly braces 
	// of an interpolated string.'

	// String interpolation and specific cultures

	// All the examples shown in the preceding section will format the strings using the 
	// current culture and language on the machine where the code executes. Often you may 
	// need to format the string produced using a specific culture. The object produced 
	// from a string interpolation is a type that has an implicit conversion to either 
	// String or FormattableString.

	// The FormattableString type contains the format string, and the results of evaluating 
	// the arguments before converting them to strings. You can use public methods of 
	// FormattableString to specify the culture when formatting a string. For example, 
	// the following will produce a string using German as the language and culture. 
	// (It will use the ',' character for the decimal separator, and the '.' character 
	// as the thousands separator.)

	public void StringInterpolationAndSpecificCultures()
	{
		Student s = new Student() { Grades = { 3.8, 4.0, 3.9 } };
		FormattableString str = $"Average grade is {s.Grades.Average()}";
		//var gradeStr = string.Format(null,
		//	System.Globalization.CultureInfo.CreateSpecificCulture("de-de"),
		//	str.GetFormat(), str.GetArguments());
	}
}

// In general, string interpolation expressions produce strings as their 
// output. However, when you want greater control over the culture used 
// to format the string, you can specify a specific output. If this is a 
// capability you often need, you can create convenience methods, as 
// extension methods, to enable easy formatting with specific cultures.
