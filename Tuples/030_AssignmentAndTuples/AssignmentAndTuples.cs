// Assignment and tuples
// https://docs.microsoft.com/en-us/dotnet/csharp/tuples#assignment-and-tuples

// The language supports assignment between tuple types that have the same number 
// of elements and implicit conversions for the types for each of those elements. 
// Other conversions are not considered for assignments.Let's look at the kinds 
// of assignments that are allowed between tuple types.

// Consider these variables used in the following examples:

using System;
using static System.Console;

class AssignmentAndTuples
{
    public static void Test1()
    {
        // The 'arity' and 'shape' of all these tuples are compatible. 
        // The only difference is the field names being used.
        var unnamed = (42, "The meaning of life");
        WriteLine($"unnamed.Item1: {unnamed.Item1} unnamed.Item2: {unnamed.Item2}");

        var anonymous = (16, "a perfect square");
        WriteLine($"anonymous.Item1: {anonymous.Item1} anonymous.Item2: {anonymous.Item2}");
        var named = (Answer: 42, Message: "The meaning of life");
        WriteLine($"named.Answer: {named.Answer} named.Message: {named.Message}");

        var differentNamed = (SecretConstant: 42, Label: "The meaning of life");
        WriteLine($"differentNamed.SecretConstant: {differentNamed.SecretConstant}" +
            $"differentNamed.Label: {differentNamed.Label}");

        // Notice that the names of the tuples are not assigned. The values of the 
        // elements are assigned following the order of the elements in the tuple.

        // Tuples of different types or numbers of elements are not assignable:

        // Does not compile.
        // CS0029: Cannot assign Tuple(int,int,int) to Tuple(int, string)
        var differentShape = (1, 2, 3);
        //named = differentShape;
    }



    static void Main(string[] args)
    {
        Test1();
    }
}
