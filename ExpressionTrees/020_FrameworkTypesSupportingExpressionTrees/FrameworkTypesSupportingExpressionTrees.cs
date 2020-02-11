// Framework Types Supporting Expression Trees
// https://docs.microsoft.com/en-us/dotnet/csharp/expression-classes

// In this article

//  It all starts with System.Linq.Expression
//  Creating Expression Trees
//  Navigating the APIs

// There is a large list of classes in the.NET Core framework that work with Expression 
// Trees. You can see the full list here.Rather than run through the full list, let's 
// understand how the framework classes have been designed.

// In language design, an expression is a body of code that evaluates and returns a 
// value. Expressions may be very simple: the constant expression 1 returns the constant 
// value of 1. They may be more complicated: The expression 

//      (-B + Math.Sqrt(B* B + 4 * A* C)) / (2 * A) 

// returns one root for a quadratic equation (in the case where the equation has 
// a solution).

// It all starts with System.Linq.Expression

// One of the complexities of working with expression trees is that many different 
// kinds of expressions are valid in many places in programs. Consider an assignment 
// expression. The right hand side of an assignment could be a constant value, a 
// variable, a method call expression, or others. That language flexibility means 
// that you may encounter many different expression types anywhere in the nodes of 
// a tree when you traverse an expression tree. Therefore, when you can work with the 
// base expression type, that's the simplest way to work. However, sometimes you need 
// to know more. The base Expression class contains a NodeType property for this 
// purpose. It returns an ExpressionType which is an enumeration of possible expression 
// types. Once you know the type of the node, you can cast it to that type, and 
// perform specific actions knowing the type of the expression node. You can search 
// for certain node types, and then work with the specific properties of that kind 
// of expression.

using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

class FrameworkTypesSupportingExpressionTrees
{
    // For example, this code will print the name of a variable for a variable access 
    // expression. I've followed the practice of checking the node type, then casting 
    // to a variable access expression and then checking the properties of the specific 
    // expression type:

    static void Test1()
    {
        Expression<Func<int, int>> addFive = (num) => num + 5;

        if (addFive.NodeType == ExpressionType.Lambda)
        {
            var lambdaExp = (LambdaExpression)addFive;

            var parameter = lambdaExp.Parameters.First();

            WriteLine(parameter.Name);
            WriteLine(parameter.Type);
        }
    }

    // Creating Expression Trees

    // The System.Linq.Expression class also contains many static methods to create 
    // expressions.These methods create an expression node using the arguments 
    // supplied for its children.In this way, you build an expression up from its 
    // leaf nodes.For example, this code builds an Add expression:

    static void Test2()
    {
        // Addition is an add expression for "1 + 2"
        var one = Expression.Constant(1, typeof(int));  // ConstantExpression
        var two = Expression.Constant(2, typeof(int));  // ConstantExpression
        var addition = Expression.Add(one, two);        // BinaryExpression
    }

    // You can see from this simple example that many types are involved in creating 
    // and working with expression trees. That complexity is necessary to provide the 
    // capabilities of the rich vocabulary provided by the C# language.


    static void Main(string[] args)
    {
        Test1();
        Test2();
    }
}

// Navigating the APIs
// There are Expression node types that map to almost all of the syntax elements of 
// the C# language. Each type has specific methods for that type of language element. 
// It's a lot to keep in your head at one time. Rather than try to memorize everything, 
// here are the techniques I use to work with Expression trees:

//  1.  Look at the members of the ExpressionType enum to determine possible nodes you 
//      should be examining.This really helps when you want to traverse and understand 
//      an expression tree.
//  2.  Look at the static members of the Expression class to build an expression. Those 
//      methods can build any expression type from a set of its child nodes.
//  3.  Look at the ExpressionVisitor class to build a modified expression tree.

// You'll find more as you look at each of those three areas. Invariably, you will find 
// what you need when you start with one of those three steps.

// ExpressionType Enumeration
// https://msdn.microsoft.com/en-us/library/bb361179(v=vs.110).aspx

// Expression
// https://msdn.microsoft.com/en-us/library/system.linq.expressions.expression(v=vs.110).aspx

// ExpressionVisitor
// https://msdn.microsoft.com/en-us/library/system.linq.expressions.expressionvisitor(v=vs.110).aspx
