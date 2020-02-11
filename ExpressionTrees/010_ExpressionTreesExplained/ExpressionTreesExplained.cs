// Expression Trees
// https://docs.microsoft.com/en-us/dotnet/csharp/expression-trees

// If you have used LINQ, you have experience with a rich library where the Func 
// types are part of the API set. (If you are not familiar with LINQ, you probably 
// want to read the LINQ tutorial and the tutorial on lambda expressions before this 
// one.) Expression Trees provide richer interaction with the arguments that are 
// functions.

// You write function arguments, typically using Lambda Expressions, when you create 
// LINQ queries.In a typical LINQ query, those function arguments are transformed 
// into a delegate the compiler creates.

// When you want to have a richer interaction, you need to use Expression Trees. 
// Expression Trees represent code as a structure that you can examine, modify, or 
// execute. These tools give you the power to manipulate code during run time. You 
// can write code that examines running algorithms, or injects new capabilities.
// In more advanced scenarios, you can modify running algorithms, and even translate 
// C# expressions into another form for execution in another environment.

// You've likely already written code that uses Expression Trees. Entity Framework's 
// LINQ APIs accept Expression Trees as the arguments for the LINQ Query Expression 
// Pattern. That enables Entity Framework to translate the query you wrote in C# 
// into SQL that executes in the database engine. Another example is Moq, which 
// is a popular mocking framework for .NET.

// The remaining sections of this tutorial will explore what Expression Trees are, 
// examine the framework classes that support expression trees, and show you how to 
// work with expression trees. You'll learn how to read expression trees, how to 
// create expression trees, how to create modified expression trees, and how to 
// execute the code represented by expression trees. After reading, you will be 
// ready to use these structures to create rich adaptive algorithms.

//  1.  Expression Trees Explained
//      Understand the structure and concepts behind Expression Trees.

//  2.  Framework Types Supporting Expression Trees
//      Learn about the structures and classes that define and manipulate expression trees.

//  3.  Executing Expressions
//      Learn how to convert an expression tree represented as a Lambda Expression 
//      into a delegate and execute the resulting delegate.

//  4.  Interpreting Expressions
//      Learn how to traverse and examine expression trees to understand what code 
//      the expression tree represents.

//  5.  Building Expressions
//      Learn how to construct the nodes for an expression tree and build expression 
//      trees.

//  6.  Translating Expressions
//      Learn how to build a modified copy of an expression tree, or translate an 
//      expression tree into a different format.

//  7.  Summing up
//      Review the information on expression trees.


// Expression Trees Explained
// https://docs.microsoft.com/en-us/dotnet/csharp/expression-trees-explained

// An Expression Tree is a data structure that defines code. They are based on the 
// same structures that a compiler uses to analyze code and generate the compiled 
// output. As you read through this tutorial, you will notice quite a bit of similarity 
// between Expression Trees and the types used in the Roslyn APIs to build Analyzers 
// and CodeFixes. (Analyzers and CodeFixes are NuGet packages that perform static 
// analysis on code and can suggest potential fixes for a developer.) The concepts 
// are similar, and the end result is a data structure that allows examination of 
// the source code in a meaningful way. However, Expression Trees are based on a 
// totally different set of classes and APIs than the Roslyn APIs.

// Let's look at a simple example. Here's a line of code:

//      var sum = 1 + 2;

// If you were to analyze this as an expression tree, the tree contains several 
// nodes.The outermost node is a variable declaration statement with assignment 
// (var sum = 1 + 2;) That outermost node contains several child nodes: a variable 
// declaration, an assignment operator, and an expression representing the right 
// hand side of the equals sign. That expression is further subdivided into 
// expressions that represent the addition operation, and left and right operands 
// of the addition.

// Let's drill down a bit more into the expressions that make up the right side 
// of the equals sign. The expression is 1 + 2. That's a binary expression. More 
// specifically, it's a binary addition expression. A binary addition expression 
// has two children, representing the left and right nodes of the addition 
// expression. Here, both nodes are constant expressions: The left operand is 
// the value 1, and the right operand is the value 2.

// Visually, the entire statement is a tree: You could start at the root node, 
// and travel to each node in the tree to see the code that makes up the statement:

//  Variable declaration statement with assignment (var sum = 1 + 2;)
//      Implicit variable type declaration (var sum)
//          Implicit var keyword (var)
//          Variable name declaration (sum)
//      Assignment operator (=)
//      Binary addition expression (1 + 2)
//          Left operand (1)
//          Addition operator (+)
//          Right operand (2)

// This may look complicated, but it is very powerful. Following the same process, 
// you can decompose much more complicated expressions. Consider this expression:

// var finalAnswer = this.SecretSauceFunction(
//    currentState.createInterimResult(), currentState.createSecondValue(1, 2),
//    decisionServer.considerFinalOptions("hello")) +
//    MoreSecretSauce('A', DateTime.Now, true);

// The expression above is also a variable declaration with an assignment. In this 
// instance, the right hand side of the assignment is a much more complicated tree.
// I'm not going to decompose this expression, but consider what the different nodes 
// might be. There are method calls using the current object as a receiver, one that 
// has an explicit this receiver, one that does not. There are method calls using 
// other receiver objects, there are constant arguments of different types. And finally, 
// there is a binary addition operator. Depending on the return type of 
// SecretSauceFunction() or MoreSecretSauce(), that binary addition operator may be 
// a method call to an overridden addition operator, resolving to a static method 
// call to the binary addition operator defined for a class.

// Despite this perceived complexity, the expression above creates a tree structure 
// that can be navigated as easily as the first sample.You can keep traversing child 
// nodes to find leaf nodes in the expression. Parent nodes will have references to 
// their children, and each node has a property that describes what kind of node it is.

// The structure of an expression tree is very consistent. Once you've learned the 
// basics, you can understand even the most complex code when it is represented as 
// an expression tree. The elegance in the data structure explains how the C# compiler 
// can analyze the most complex C# programs and create proper output from that 
// complicated source code.

// Once you become familiar with the structure of expression trees, you will find 
// that knowledge you've gained quickly enables you to work with many more and more 
// advanced scenarios. There is incredible power to expression trees.

// In addition to translating algorithms to execute in other environments, expression 
// trees can be used to make it easier to write algorithms that inspect code before 
// executing it. You can write a method whose arguments are expressions and then 
// examine those expressions before executing the code. The Expression Tree is a 
// full representation of the code: you can see values of any sub-expression. You 
// can see method and property names.You can see the value of any constant expressions.
// You can also convert an expression tree into an executable delegate, and execute 
// the code.

// The APIs for Expression Trees enable you to create trees that represent almost 
// any valid code construct. However, to keep things as simple as possible, some 
// C# idioms cannot be created in an expression tree. One example is asynchronous 
// expressions (using the async and await keywords). If your needs require 
// asynchronous algorithms, you would need to manipulate the Task objects directly, 
// rather than rely on the compiler support. Another is in creating loops. Typically, 
// you create these by using for, foreach, while or do loops. As you'll see later in 
// this series, the APIs for expression trees support a single loop expression, with 
// break and continue expressions that control repeating the loop.

// The one thing you can't do is modify an expression tree. Expression Trees are 
// immutable data structures. If you want to mutate (change) an expression tree, 
// you must create a new tree that is a copy of the original, but with your 
// desired changes.