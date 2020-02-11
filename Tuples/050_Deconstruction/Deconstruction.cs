// Deconstruction
// https://docs.microsoft.com/en-us/dotnet/csharp/tuples#deconstruction

// You can unpackage all the items in a tuple by deconstructing the tuple returned by 
// a method.There are three different approaches to deconstructing tuples.First, you 
// can explicitly declare the type of each field inside parentheses to create discrete 
// variables for each of the elements in the tuple:


using System;
using System.Collections.Generic;
using System.Linq;



class Deconstruction
{
    private static (int Count, double Sum, double SumOfSquares) ComputeSumAndSumOfSquares(IEnumerable<double> sequence)
    {
        double sum = 0;
        double sumOfSquares = 0;
        int count = 0;

        foreach (var item in sequence)
        {
            count++;
            sum += item;
            sumOfSquares += item * item;
        }

        return (count, sum, sumOfSquares);
    }

    public static double StandardDeviation(IEnumerable<double> sequence)
    {
        (int count, double sum, double sumOfSquares) = ComputeSumAndSumOfSquares(sequence);

        var variance = sumOfSquares - sum * sum / count;
        return Math.Sqrt(variance / count);
    }

    // You can also declare implicitly typed variables for each field in a tuple by using 
    // the var keyword outside the parentheses:

    public static double StandardDeviation2(IEnumerable<double> sequence)
    {
        //var (sum, sumOfSquares, count) = ComputeSumAndSumOfSquares(sequence);
        // It is also legal to use the var keyword with any, or all of the variable declarations inside the parentheses.
        (double sum, var sumOfSquares, var count) = ComputeSumAndSumOfSquares(sequence);

        var variance = sumOfSquares - sum * sum / count;
        return Math.Sqrt(variance / count);
    }

    // Note that you cannot use a specific type outside the parentheses, even if every field 
    // in the tuple has the same type.


    static void Main(string[] args)
    {
    }
}

// You can deconstruct tuples with existing declarations as well:
public class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y) => (X, Y) = (x, y);
}

// Warning

// You cannot mix existing declarations with declarations inside the parentheses. For instance, 
// the following is not allowed: (var x, y) = MyMethod();. This produces error CS8184 because 
// x is declared inside the parentheses and y is previously declared elsewhere.
