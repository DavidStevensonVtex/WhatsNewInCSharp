// C# Tuple types
// https://docs.microsoft.com/en-us/dotnet/csharp/tuples

// C# Tuples are types that you define using a lightweight syntax. The advantages include 
// a simpler syntax, rules for conversions based on number (referred to as cardinality) 
// and types of elements, and consistent rules for copies and assignments. As a tradeoff, 
// Tuples do not support some of the object oriented idioms associated with inheritance. 
// You can get an overview in the section on Tuples in the What's new in C# 7 topic.

// In this topic, you'll learn the language rules governing Tuples in C# 7, different 
// ways to use them, and initial guidance on working with Tuples.

// The new tuples features require the ValueTuple types.You must add the NuGet package 
// System.ValueTuple in order to use it on platforms that do not include the types.

// This is similar to other language features that rely on types delivered in the 
// framework. Examples include async and await relying on the INotifyCompletion interface, 
// and LINQ relying on IEnumerable<T>.However, the delivery mechanism is changing as .NET 
// is becoming more platform independent.The.NET Framework may not always ship on the 
// same cadence as the language compiler.When new language features rely on new types, 
// those types will be available as NuGet packages when the language features ship. As 
// these new types get added to the.NET Standard API and delivered as part of the 
// framework, the NuGet package requirement will be removed.

// Let's start with the reasons for adding new Tuple support. Methods return a single 
// object. Tuples enable you to package multiple values in that single object more easily.

// The.NET Framework already has generic Tuple classes.These classes, however, had two 
// major limitations.For one, the Tuple classes named their properties Item1, Item2, and 
// so on.Those names carry no semantic information.Using these Tuple types does not enable 
// communicating the meaning of each of the properties. The new language features enable 
// you to declare and use semantically meaningful names for the elements in a tuple.

// Another concern is that the Tuple classes are reference types.Using one of the Tuple types 
// means allocating objects.On hot paths, this can have a measurable impact on your 
// application's performance. Therefore, the language support for tuples leverages the 
// new ValueTuple structs.


// To avoid those deficiencies, you could create a class or a struct to carry multiple elements.Unfortunately, that's more work for you, and it obscures your design intent. Making a struct or class implies that you are defining a type with both data and behavior. Many times, you simply want to store multiple values in a single object.

// The language features and the ValueTuple generic structs enforce the rule that you cannot add any behavior(methods) to these tuple types.All the ValueTuple types are mutable structs. Each member field is a public field.That makes them very lightweight.However, that means tuples should not be used where immutability is important.

// Tuples are both simpler and more flexible data containers than class and struct types. 
// Let's explore those differences.

using System;
using static System.Console;

public class Tuples
{
    // Named and unnamed tuples
    // https://docs.microsoft.com/en-us/dotnet/csharp/tuples#named-and-unnamed-tuples

    // The ValueTuple struct has fields named Item1, Item2, Item3 and so on, similar to 
    // the properties defined in the existing Tuple types.These names are the only names 
    // you can use for unnamed tuples. When you do not provide any alternative field names 
    // to a tuple, you've created an unnamed tuple:

    public static void Test1()
    {
        var unnamed = ("one", "two");
        WriteLine($"unnamed.Item1: {unnamed.Item1} unnamed.Item2: {unnamed.Item2}");
    }

    // The tuple in the previous example was initialized using literal constants and 
    // won't have element names created using Tuple field name projections in C# 7.1.

    // However, when you initialize a tuple, you can use new language features that give 
    // better names to each field.Doing so creates a named tuple.Named tuples still have 
    // elements named Item1, Item2, Item3 and so on. But they also have synonyms for any 
    // of those elements that you have named. You create a named tuple by specifying the 
    // names for each element. One way is to specify the names as part of the tuple 
    // initialization:

    public static void Test2()
    {
        var named = (first: "one", second: "two");
        WriteLine($"named.first: {named.first} named.second: {named.second}");
    }

    // These synonyms are handled by the compiler and the language so that you can 
    // use named tuples effectively. IDEs and editors can read these semantic names 
    // using the Roslyn APIs. This enables you to reference the elements of a named 
    // tuple by those semantic names anywhere in the same assembly. The compiler 
    // replaces the names you've defined with Item* equivalents when generating 
    // the compiled output. The compiled Microsoft Intermediate Language (MSIL) 
    // does not include the names you've given these elements.

    // Beginning with C# 7.1, the field names for a tuple may be provided from the 
    // variables used to initialize the tuple. This is referred to as tuple projection 
    // initializers. The following code creates a tuple named accumulation with 
    // elements count (an integer), and sum (a double).

    public static void Test3()
    {
        var sum = 12.5;
        var count = 5;
        var accumulation = (count, sum);
        // CS8306  Tuple element name 'count' is inferred. Please use language version 7.1 or greater to access an element by its inferred name.
        // CS8306  Tuple element name 'sum' is inferred. Please use language version 7.1 or greater to access an element by its inferred name.
        WriteLine($"accumulation.count: {accumulation.count} accumulation.sum: {accumulation.sum}");
    }

    public static void Main()
    {
        Test1();
        Test2();
        Test3();
    }
}

// The compiler must communicate those names you created for tuples that are 
// returned from public methods or properties. In those cases, the compiler 
// adds a TupleElementNamesAttribute attribute on the method. This attribute 
// contains a TransformNames list property that contains the names given to 
// each of the elements in the Tuple.

// Note

// Development Tools, such as Visual Studio, also read that metadata, and 
// provide IntelliSense and other features using the metadata field names.

// It is important to understand these underlying fundamentals of the new 
// tuples and the ValueTuple type in order to understand the rules for 
// assigning named tuples to each other.
