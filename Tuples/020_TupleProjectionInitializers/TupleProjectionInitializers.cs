// Tuple projection initializers
// 

// In general, tuple projection initializers work by using the variable 
// or field names from the right-hand side of a tuple initialization 
// statement.If an explicit name is given, that takes precedence over 
// any projected name.For example, in the following initializer, the 
// elements are explicitFieldOne and explicitFieldTwo, not localVariableOne 
// and localVariableTwo:

using System;
using static System.Console;

public class TupleProjectionInitializers
{
    public static void Test1()
    {
        WriteLine("Test1\n");
        var localVariableOne = 5;
        var localVariableTwo = "some text";

        var tuple = (explicitFieldOne: localVariableOne, explicitFieldTwo: localVariableTwo);
        WriteLine($"explicitFieldOne: {tuple.explicitFieldOne} explicitFieldTwo: {tuple.explicitFieldTwo}");
    }

    // For any field where an explicit name is not provided, an applicable implicit name 
    // will be projected. Note that there is no requirement to provide semantic names, 
    // either explicitly or implicitly. The following initializer will have field names 
    // Item1, whose value is 42 and StringContent, whose value is "The answer to everything":

    public static void Test2()
    {
        WriteLine("\nTest2\n");
        var stringContent = "The answer to everything";
        var mixedTuple = (42, stringContent);
        // CS8306  Tuple element name 'stringContent' is inferred. Please use language version 7.1 or greater to access an element by its inferred name.

        WriteLine($"mixedTuple.Item1: {mixedTuple.Item1} mixedTuple.stringContent: {mixedTuple.stringContent}");

    }

    // There are two conditions where candidate field names are not projected onto the 
    // tuple field:

    //  1.  When the candidate name is a reserved tuple name.Examples include Item3, 
    //      ToString or Rest.
    //  2.  When the candidate name is a duplicate of another tuple field name, either 
    //      explicit or implicit.
    
    // These conditions avoid ambiguity. These names would cause an ambiguity if they 
    // were used as the field names for a field in a tuple. Neither of these conditions 
    // cause compile time errors. Instead, the elements without projected names do not 
    // have semantic names projected for them.The following examples demonstrate these 
    // conditions:

    public static void Test3()
    {
        WriteLine("\nTest3\n");
        var ToString = "This is some text";
        var one = 1;
        var Item1 = 5;
        var projections = (ToString, one, Item1);
        // Accessing the first field:
        WriteLine($"projections: ToString: {projections.Item1} one: {projections.one} Item1: {projections.Item3}");
        Console.WriteLine(projections.Item1);
        // There is no semantic name 'ToString'
        // Accessing the second field:
        Console.WriteLine(projections.one);
        Console.WriteLine(projections.Item2);
        // Accessing the third field:
        Console.WriteLine(projections.Item3);
        // There is no semantic name 'Item`.

        var pt1 = (X: 3, Y: 0);
        var pt2 = (X: 3, Y: 4);
        WriteLine($"pt1.X: {pt1.X} pt1.Y: {pt1.Y} pt2.X: {pt2.X} pt2.Y: {pt2.Y}");

        var xCoords = (pt1.X, pt2.X);
        // There are no semantic names for the fields
        // of xCoords. 
        WriteLine($"xCoords.Item1: {xCoords.Item1} xCoords.Item2: {xCoords.Item2}");

        // Accessing the first field:
        Console.WriteLine(xCoords.Item1);
        // Accessing the second field:
        Console.WriteLine(xCoords.Item2);
    }

    // These situations do not cause compiler errors because that would be a breaking 
    // change for code written with C# 7.0, when tuple field name projections were not 
    // available.

    public static void Main()
    {
        Test1();
        Test2();
        Test3();
    }
}