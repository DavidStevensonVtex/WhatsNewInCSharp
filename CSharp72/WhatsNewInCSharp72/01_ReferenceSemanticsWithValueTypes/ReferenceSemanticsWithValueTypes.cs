﻿// Reference semantics with value types

// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7-2#reference-semantics-with-value-types

using System;

// Reference semantics with value types

// Language features introduced in 7.2 let you work with value types 
// while using reference semantics. They are designed to increase 
// performance by minimizing copying value types without incurring 
// the memory allocations associated with using reference (value?) types.

// The features include:

//	►	The in modifier on parameters, to specify that an argument 
//		is passed by reference but not modified by the called method.

//	►	The ref readonly modifier on method returns, to indicate 
//		that a method returns its value by reference but doesn't 
//		allow writes to that object.

//	►	The readonly struct declaration, to indicate that a struct 
//		is immutable and should be passed as an in parameter to its 
//		member methods.

//	►	The ref struct declaration, to indicate that a struct type 
//		accesses managed memory directly and must always be stack 
//		allocated.

// You can read more about all these changes in Using value types 
// with reference semantics.

// Reference semantics with value types
// https://docs.microsoft.com/en-us/dotnet/csharp/reference-semantics-with-value-types

// An advantage to using value types is that they often avoid heap 
// allocations. The corresponding disadvantage is that they are 
// copied by value. This tradeoff makes it harder to optimize 
// algorithms that operate on large amounts of data. New language 
// features in C# 7.2 provide mechanisms that enable pass-by-reference 
// semantics with value types. If you use these features wisely you 
// can minimize both allocations and copy operations. This article 
// explores those new features.

// Much of the sample code in this article demonstrates features 
// added in C# 7.2. In order to use those features, you have to 
// configure your project to use C# 7.2 or later in your project. 
// You can use Visual Studio to select it. For each project, select 
// Project from the menu, then Properties. Select the Build tab and 
// click Advanced. From there, you can configure the language version. 
// Choose either "7.2", or "latest". 

// Specifying in parameters
// https://docs.microsoft.com/en-us/dotnet/csharp/reference-semantics-with-value-types#specifying-in-parameters

// C# 7.2 adds the in keyword to complement the existing ref and out 
// keywords when you write a method that passes arguments by reference. 
// The in keyword specifies that you are passing the parameter by 
// reference and the called method does not modify the value passed to it.

// This addition provides a full vocabulary to express your design intent.
// Value types are copied when passed to a called method when you do not 
// specify any of the following modifiers.Each of these modifiers specify 
// that a value type is passed by reference, avoiding the copy. Each modifier 
// expresses a different intent:

//	►	out: This method sets the value of the argument used as this parameter.
//	►	ref: This method may set the value of the argument used as this parameter.
//	►	in: This method does not modify the value of the argument used as this 
//		parameter.

// When you add the in modifier to pass an argument by reference, you declare 
// your design intent is to pass arguments by reference to avoid unnecessary 
// copying.You do not intend to modify the object used as that argument. 
// The following code shows an example of a method that calculates the 
// distance between two points in 3D space.

struct Point3D
{
	public double X;
	public double Y;
	public double Z;

	private static Point3D origin = new Point3D();
	public static ref readonly Point3D Origin => ref origin;
}
class ReferenceSemanticsWithValueTypes
{
	private static double CalculateDistance(in Point3D point1, in Point3D point2)
	{
		double xDifference = point1.X - point2.X;
		double yDifference = point1.Y - point2.Y;
		double zDifference = point1.Z - point2.Z;

		return Math.Sqrt(xDifference * xDifference + yDifference * yDifference + zDifference * zDifference);
	}

	// The arguments are two structures that each contain three doubles. 
	// A double is 8 bytes, so each argument is 24 bytes. By specifying 
	// the in modifier, you pass 4-byte or 8-byte reference to those 
	// arguments, depending on the architecture of the machine. The 
	// difference in size is small, but it can quickly add up when 
	// your application calls this method in a tight loop using many 
	// different values.

	// The in modifier complements out and ref in other ways as well.
	// You cannot create overloads of a method that differ only in 
	// the presence of in, out or ref. These new rules extend the same 
	// behavior that had always been defined for out and ref parameters.

	// The in modifier may be applied to any member that takes parameters: 
	// methods, delegates, lambdas, local functions, indexers, operators.

	// Unlike ref and out arguments, you may use literal values or constants 
	// for the argument to an in parameter. Also, unlike a ref or out parameter, 
	// you don't need to apply the in modifier at the call site. The following 
	// code shows you two examples of calling the CalculateDistance method. 
	// The first uses two local variables passed by reference. The second 
	// includes a temporary variable created as part of the method call.

	static void CalculateDistanceFromOrigin()
	{
		Point3D pt1 = new Point3D() { X = 1, Y = 2, Z = 3 };
		Point3D pt2 = new Point3D() { X = 2, Y = 4, Z = 6 };
		var distance = CalculateDistance(pt1, pt2);
		var fromOrigin = CalculateDistance(pt1, new Point3D());
	}

	// There are several ways in which the compiler ensures that 
	// the read-only nature of an in argument is enforced. First 
	// of all, the called method can't directly assign to an in 
	// parameter. It can't directly assign to any field of an in 
	// parameter. In addition, you cannot pass an in parameter to 
	// any method demanding the ref or out modifier. The compiler 
	// enforces that the in argument is a readonly variable. You 
	// can call any instance method that uses pass-by-value 
	// semantics. In those instances, a copy of the in parameter 
	// is created. Because the compiler can create a temporary 
	// variable for any in parameter, you can also specify default 
	// values for any in parameter. The follow code uses that to 
	// specify the origin (point 0,0) as the default value for 
	// the second point:

	private static double CalculateDistance2(
		in Point3D point1, 
		in Point3D point2 = default)
	{
		double xDifference = point1.X - point2.X;
		double yDifference = point1.Y - point2.Y;
		double zDifference = point1.Z - point2.Z;

		return Math.Sqrt(
			xDifference * xDifference + 
			yDifference * yDifference + 
			zDifference * zDifference);
	}

	// The in parameter designation can also be used with reference types 
	// or built in numeric values. However, the benefits in both cases 
	// are minimal, if any.

	// ref readonly returns

	// You may also want to return a value type by reference, but disallow 
	// the caller from modifying that value. Use the ref readonly modifier 
	// to express that design intent.It notifies readers that you are 
	// returning a reference to existing data, but not allowing modification.

	// The compiler enforces that the caller cannot modify the reference. 
	// Attempts to assign to the value directly generate a compile-time 
	// error. However, the compiler cannot know if any member method modifies 
	// the state of the struct. To ensure that the object is not modified, 
	// the compiler creates a copy and calls member references using that copy.
	// Any modifications are to that defensive copy.

	// It's likely that the library using Point3D would often use the origin 
	// throughout the code. Every instance creates a new object on the stack. 
	// It may be advantageous to create a constant and return it by reference. 
	// But, if you return a reference to internal storage, you may want to 
	// enforce that the caller cannot modify the referenced storage. 
	// The following code defines a read-only property that returns a 
	// readonly ref to a Point3D that specifies the origin.

	private static Point3D origin = new Point3D();
	public static ref readonly Point3D Origin => ref origin;

	// Creating a copy of a ref readonly return is easy: Just assign it 
	// to a variable not declared with the ref readonly modifier. The 
	// compiler generates code to copy the object as part of the assignment.

	// When you assign a variable to a ref readonly return, you can specify 
	// either a ref readonly variable, or a by-value copy of the readonly 
	// reference:

	// private static Point3D origin = new Point3D();
	// public static ref readonly Point3D Origin => ref origin;

	// The first assignment in the preceding code makes a copy of the Origin 
	// constant and assigns that copy. The second assigns a reference. 
	// Notice that the readonly modifier must be part of the declaration 
	// of the variable. The reference to which it refers cannot be modified. 
	// Attempts to do so result in a compile-time error.

	// readonly struct type

	// Applying ref readonly to high-traffic uses of a struct may be sufficient.
	// Other times, you may want to create an immutable struct. Then you can 
	// always pass by readonly reference. That practice removes the defensive 
	// copies that take place when you access methods of a struct used as an 
	// in parameter.

	// You can do that by creating a readonly struct type. You can add the 
	// readonly modifier to a struct declaration. The compiler enforces 
	// that all instance members of the struct are readonly; the struct 
	// must be immutable.

	// There are other optimizations for a readonly struct. You can use the 
	// in modifier at every location where a readonly struct is an argument.
	// In addition, you can return a readonly struct as a ref return when 
	// you are returning an object whose lifetime extends beyond the scope 
	// of the method returning the object.
}

// Finally, the compiler generates more efficient code when you call 
// members of a readonly struct: The this reference, instead of a copy 
// of the receiver, is always an in parameter passed by reference to 
// the member method.This optimization saves more copying when you use 
// a readonly struct. The Point3D is a great candidate for this change.
// The following code shows an updated ReadonlyPoint3D structure:

readonly public struct ReadonlyPoint3D
{
	public ReadonlyPoint3D(double x, double y, double z)
	{
		this.X = x;
		this.Y = y;
		this.Z = z;
	}

	public double X { get; }
	public double Y { get; }
	public double Z { get; }

	private static readonly ReadonlyPoint3D origin = new ReadonlyPoint3D();
	public static ref readonly ReadonlyPoint3D Origin => ref origin;
}

// ref struct type
// https://docs.microsoft.com/en-us/dotnet/csharp/reference-semantics-with-value-types#ref-struct-type

// Another related language feature is the ability to declare a value type 
// that must be stack allocated.In other words, these types can never be 
// created on the heap as a member of another class. The primary motivation 
// for this feature was Span<T> and related structures. Span<T> may contain 
// a managed pointer as one of its members, the other being the length of 
// the span. It's actually implemented a bit differently because C# doesn't 
// support pointers to managed memory outside of an unsafe context. Any write 
// that changes the pointer and the length is not atomic.That means a Span<T> 
// would be subject to out of range errors or other type safety violations 
// were it not constrained to a single stack frame.In addition, putting a 
// managed pointer on the GC heap typically crashes at JIT time.

// You may have similar requirements working with memory created using 
// stackalloc or when using memory from interop APIs. You can define your 
// own ref struct types for those needs.In this article, you see examples 
// using Span<T> for simplicity.

// The ref struct declaration declares that a struct of this type must be 
// on the stack. The language rules ensure the safe use of these types. 
// Other types declared as ref struct include ReadOnlySpan<T>.

// The goal of keeping a ref struct type as a stack-allocated variable 
// introduces several rules that the compiler enforces for all 
// ref struct types.

//	►	You can't box a ref struct. You cannot assign a ref struct type 
//		to a variable of type object, dynamic, or any interface type.
//	►	You can't declare a ref struct as a member of a class or a normal struct.
//	►	You cannot declare local variables that are ref struct types in 
//		async methods.You can declare them in synchronous methods that 
//		return Task, Task<T> or Task-like types.
//	►	You cannot declare ref struct local variables in iterators.
//	►	You cannot capture ref struct variables in lambda expressions or 
//		local functions.

// These restrictions ensure that you do not accidentally use a ref struct 
// in a manner that could promote it to the managed heap.

// Conclusions

// These enhancements to the C# language are designed for performance 
// critical algorithms where memory allocations can be critical to 
// achieving the necessary performance. You may find that you don't 
// often use these features in the code you write. However, these 
// enhancements have been adopted in many locations in the .NET 
// Framework. As more and more APIs make use of these features, 
// you'll see the performance of your own applications improve.
