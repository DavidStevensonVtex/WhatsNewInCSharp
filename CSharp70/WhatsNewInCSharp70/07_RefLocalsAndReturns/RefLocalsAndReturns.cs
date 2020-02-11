// Ref locals and returns
// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7#ref-locals-and-returns

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class MatrixSearch
{
	// This feature enables algorithms that use and return references to variables 
	// defined elsewhere. One example is working with large matrices, and finding 
	// a single location with certain characteristics. One method would return 
	// the two indices for a single location in the matrix:
	public static (int i, int j) Find(int[,] matrix, Func<int, bool> predicate)
	{
		for (int i = 0; i < matrix.GetLength(0); i++)
			for (int j = 0; j < matrix.GetLength(1); j++)
				if (predicate(matrix[i, j]))
					return (i, j);
		return (-1, -1); // Not found
	}

	// There are many issues with this code. First of all, it's a public 
	// method that's returning a tuple. The language supports this, but 
	// user defined types (either classes or structs) are preferred for 
	// public APIs.

	// Second, this method is returning the indices to the item in the 
	// matrix.That leads callers to write code that uses those indices 
	// to dereference the matrix and modify a single element:

	static void Main(string[] args)
	{
		int[,] matrix = new int[,] { { 0, 1, 2 }, { 3, 42, 5 } };
		var indices = MatrixSearch.Find(matrix, (val) => val == 42);
		Console.WriteLine(indices);
		matrix[indices.i, indices.j] = 24;
	}

	// You'd rather write a method that returns a reference to the 
	// element of the matrix that you want to change. You could only 
	// accomplish this by using unsafe code and returning a pointer 
	// to an int in previous versions.

	// Let's walk through a series of changes to demonstrate the ref 
	// local feature and show how to create a method that returns a 
	// reference to internal storage. Along the way, you'll learn 
	// the rules of the ref return and ref local feature that protects 
	// you from accidentally misusing it.

	// Start by modifying the Find method declaration so that it returns 
	// a ref int instead of a tuple.Then, modify the return statement so 
	// it returns the value stored in the matrix instead of the two indices:


	// Note that this won't compile. 
	// Method declaration indicates ref return,
	// but return statement specifies a value return.
	public static ref int Find2(int[,] matrix, Func<int, bool> predicate)
	{
		for (int i = 0; i < matrix.GetLength(0); i++)
			for (int j = 0; j < matrix.GetLength(1); j++)
				if (predicate(matrix[i, j]))
					return ref matrix[i, j];
		//return matrix[i, j];    // ref keyword is required.
		throw new InvalidOperationException("Not found");
	}

	// When you declare that a method returns a ref variable, you must 
	// also add the ref keyword to each return statement. That indicates 
	// return by reference, and helps developers reading the code later 
	// remember that the method returns by reference:

	public static ref int Find3(int[,] matrix, Func<int, bool> predicate)
	{
		for (int i = 0; i < matrix.GetLength(0); i++)
			for (int j = 0; j < matrix.GetLength(1); j++)
				if (predicate(matrix[i, j]))
					return ref matrix[i, j];
		throw new InvalidOperationException("Not found");
	}

	// Now that the method returns a reference to the integer value in 
	// the matrix, you need to modify where it's called. The var declaration 
	// means that valItem is now an int rather than a tuple:
	static void Main2(string[] args)
	{
		int[,] matrix = new int[,] { { 0, 1, 2 }, { 3, 42, 5 } };
		ref var item = ref MatrixSearch.Find3(matrix, (val) => val == 42);
		Console.WriteLine(item);
		item = 24;
		Console.WriteLine(matrix[4, 2]);
	}

}

// Now, the second WriteLine statement in the example above will print out 
// the value 24, indicating that the storage in the matrix has been modified. 
// The local variable has been declared with the ref modifier, and it will 
// take a ref return. You must initialize a ref variable when it is declared, 
// you cannot split the declaration and the initialization.

// The C# language has three other rules that protect you from misusing the 
// ref locals and returns:

// You cannot assign a standard method return value to a ref local variable.

// That disallows statements like ref int i = sequence.Count();

// You cannot return a ref to a variable whose lifetime does not extend beyond 
// the execution of the method.

// That means you cannot return a reference to a local variable or a variable 
// with a similar scope.

// ref locals and returns can't be used with async methods.

// The compiler can't know if the referenced variable has been set to its final 
// value when the async method returns.

// The addition of ref locals and ref returns enable algorithms that are more 
// efficient by avoiding copying values, or performing dereferencing operations 
// multiple times.