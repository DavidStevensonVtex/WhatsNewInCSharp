using System;
using System.Collections;
using System.Collections.Generic;
using static System.Console;

class PatternMatchingSimplified
{
	static void Main(string[] args)
	{
		ArrayList al = new ArrayList()
		{
			123,
			DateTime.Now,
			new string [] { "abc", "def" },
			0,
			null,
			new object [] { new { Cost = 123 }, new { Street = "123 Main Street" } },
			new List<object> () { new List<string>() { "xyz", "stu" }, new int[] { 123, 456 } }
		};
		foreach (object obj in al)
		{
			// As part of pattern matching, we can create a variable to capture the value.
			if (obj == null)
				WriteLine("null");
			else if (obj is int zero && zero == 0)
				WriteLine("zero");
			else if (obj is int val)
				WriteLine($"int: {val}");
			else if (obj is DateTime date)
				WriteLine($"date: {date}");
			else if (obj is IEnumerable<string> stringlist)
			{
				foreach (string s in stringlist)
					WriteLine($"stringlist: {s}");
			}
			else if (obj is IEnumerable<object> list)
			{
				foreach (object subobj in list)
				{
					if (subobj is List<string> stringlist2)
						stringlist2.ForEach(s => WriteLine($"stringlist2: value: {s}"));
					else if (subobj is IEnumerable<int> intlist)
					{
						foreach (int i in intlist)
							WriteLine($"intlist: value: {i}");
					}
					else
						WriteLine("\nsubobj: " + subobj.GetType().ToString());
				}
			}
			else
				WriteLine("\nobj: " + obj.GetType().ToString());

		}
		WriteLine("\n\nSwitch Statement:\n");
		foreach (object obj in al)
		{
			switch (obj)
			{
				case 0:
					WriteLine("zero");
					break;
				case null:
					WriteLine("null");
					break;
				case int val:
					WriteLine($"int: {val}");
					break;
				case DateTime date:
					WriteLine($"date: {date}");
					break;
				case IEnumerable<string> stringlist:
					foreach (string s in stringlist)
						WriteLine($"stringlist: {s}");
					break;
				case IEnumerable<object> list:
					foreach (object subobj in list)
					{
						if (subobj is List<string> stringlist2)
							stringlist2.ForEach(s => WriteLine($"stringlist2: value: {s}"));
						else if (subobj is IEnumerable<int> intlist)
						{
							foreach (int i in intlist)
								WriteLine($"intlist: value: {i}");
						}
						else
							WriteLine("\nsubobj: " + subobj.GetType().ToString());
					}

					break;
				default:
					WriteLine("\nobj: " + obj.GetType().ToString());
					break;
			}
		}
	}
}