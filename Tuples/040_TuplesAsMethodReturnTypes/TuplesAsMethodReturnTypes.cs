// Tuples as method return values
// https://docs.microsoft.com/en-us/dotnet/csharp/tuples#tuples-as-method-return-values



using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

public class TuplesAsMethodReturnTypes
{
    // One of the most common uses for Tuples is as a method return value. Let's walk 
    // through one example. Consider this method that computes the standard deviation 
    // for a sequence of numbers:
    public static double StandardDeviation(IEnumerable<double> sequence)
    {
        // Step 1: Compute the Mean:
        var mean = sequence.Average();

        // Step 2: Compute the square of the differences between each number 
        // and the mean:
        var squaredMeanDifferences = from n in sequence
                                     select (n - mean) * (n - mean);
        // Step 3: Find the mean of those squared differences:
        var meanOfSquaredDifferences = squaredMeanDifferences.Average();

        // Step 4: Standard Deviation is the square root of that mean:
        var standardDeviation = Math.Sqrt(meanOfSquaredDifferences);
        return standardDeviation;
    }


    // Note

    // These examples compute the uncorrected sample standard deviation. The corrected sample 
    // standard deviation formula would divide the sum of the squared differences from the 
    // mean by (N-1) instead of N, as the Average extension method does.Consult a statistics 
    // text for more details on the differences between these formulas for standard deviation.

    // This follows the textbook formula for the standard deviation.It produces the correct 
    // answer, but it's a very inefficient implementation. This method enumerates the sequence 
    // twice: Once to produce the average, and once to produce the average of the square of 
    // the difference of the average. (Remember that LINQ queries are evaluated lazily, so 
    // the computation of the differences from the mean and the average of those differences 
    // makes only one enumeration.)

    // There is an alternative formula that computes standard deviation using only one 
    // enumeration of the sequence.This computation produces two values as it enumerates 
    // the sequence: the sum of all items in the sequence, and the sum of the each value 
    // squared:

    public static double StandardDeviation2(IEnumerable<double> sequence)
    {
        double sum = 0;
        double sumOfSquares = 0;
        double count = 0;

        foreach (var item in sequence)
        {
            count++;
            sum += item;
            sumOfSquares += item * item;
        }

        var variance = sumOfSquares - sum * sum / count;
        return Math.Sqrt(variance / count);
    }

    // This version enumerates the sequence exactly once. But, it's not very reusable code. 
    // As you keep working, you'll find that many different statistical computations use 
    // the number of items in the sequence, the sum of the sequence, and the sum of the 
    // squares of the sequence. Let's refactor this method and write a utility method 
    // that produces all three of those values.

    // This is where tuples come in very useful.

    // Let's update this method so the three values computed during the enumeration are 
    // stored in a tuple. That creates this version:

    public static double StandardDeviation3(IEnumerable<double> sequence)
    {
        var computation = (Count: 0, Sum: 0.0, SumOfSquares: 0.0);

        foreach (var item in sequence)
        {
            computation.Count++;
            computation.Sum += item;
            computation.SumOfSquares += item * item;
        }

        var variance = computation.SumOfSquares - computation.Sum * computation.Sum / computation.Count;
        return Math.Sqrt(variance / computation.Count);
    }

    // Visual Studio's Refactoring support makes it easy to extract the functionality for 
    // the core statistics into a private method. That gives you a private static method 
    // that returns the tuple type with the three values of Sum, SumOfSquares, and Count:


    public static double StandardDeviation4(IEnumerable<double> sequence)
    {
        (int Count, double Sum, double SumOfSquares) computation = ComputeSumsAnSumOfSquares(sequence);

        var variance = computation.SumOfSquares - computation.Sum * computation.Sum / computation.Count;
        return Math.Sqrt(variance / computation.Count);
    }

    private static (int Count, double Sum, double SumOfSquares) 
        ComputeSumsAnSumOfSquares(IEnumerable<double> sequence)
    {
        var computation = (count: 0, sum: 0.0, sumOfSquares: 0.0);

        foreach (var item in sequence)
        {
            computation.count++;
            computation.sum += item;
            computation.sumOfSquares += item * item;
        }

        return computation;
    }

    // The language enables a couple more options that you can use, if you want to make a 
    // few quick edits by hand. First, you can use the var declaration to initialize the 
    // tuple result from the ComputeSumAndSumOfSquares method call. You can also create 
    // three discrete variables inside the ComputeSumAndSumOfSquares method. The final 
    // version is below:

    public static double StandardDeviation5(IEnumerable<double> sequence)
    {
        var computation = ComputeSumAndSumOfSquares2(sequence);

        var variance = computation.SumOfSquares - computation.Sum * computation.Sum / computation.Count;
        return Math.Sqrt(variance / computation.Count);
    }

    private static (int Count, double Sum, double SumOfSquares) ComputeSumAndSumOfSquares2(IEnumerable<double> sequence)
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

    // This final version can be used for any method that needs those three values, 
    // or any subset of them.

    // The language supports other options in managing the names of the elements in 
    // these tuple-returning methods.

    // You can remove the field names from the return value declaration and return 
    // an unnamed tuple:

    private static (double, double, int) ComputeSumAndSumOfSquares(IEnumerable<double> sequence)
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

        return (sum, sumOfSquares, count);
    }

    // You must address the fields of this tuple as Item1, Item2, and Item3. It's recommended 
    // that you provide semantic names to the elements of tuples returned from methods.

    static void Main(string[] args)
    {
    }
}


// Another idiom where tuples can be very useful is when you are authoring LINQ queries 
// where the final result is a projection that contains some, but not all, of the properties 
// of the objects being selected.

// You would traditionally project the results of the query into a sequence of objects that 
// were an anonymous type. That presented many limitations, primarily because anonymous types 
// could not conveniently be named in the return type for a method. Alternatives using object 
// or dynamic as the type of the result came with significant performance costs.

// Returning a sequence of a tuple type is easy, and the names and types of the elements are 
// available at compile time and through IDE tools.For example, consider a ToDo application. 
// You might define a class similar to the following to represent a single entry in the ToDo 
// list:

public class ToDoItem
{
    public int ID { get; set; }
    public bool IsDone { get; set; }
    public DateTime DueDate { get; set; }
    public string Title { get; set; }
    public string Notes { get; set; }
}

// Your mobile applications may support a compact form of the current ToDo items that 
// only displays the title. That LINQ query would make a projection that includes only 
// the ID and the title. A method that returns a sequence of tuples expresses that 
// design very well:

public class ManageToDoItems
{
    private List<ToDoItem> AllItems { get; set; }

    internal IEnumerable<(int ID, string Title)> GetCurrentItemsMobileList()
    {
        return from item in AllItems
               where !item.IsDone
               orderby item.DueDate
               select (item.ID, item.Title);
    }
}

// Note

// In C# 7.1, tuple projections enable you to create named tuples using elements, in a 
// manner similar to the property naming in anonymous types. In the above code, the select 
// statement in the query projection creates a tuple that has elements ID and Title.

// The named tuple can be part of the signature. It lets the compiler and IDE tools provide 
// static checking that you are using the result correctly. The named tuple also carries the 
// static type information so there is no need to use expensive run time features like reflection 
// or dynamic binding to work with the results.
