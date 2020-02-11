// Ref locals and returns
// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7#ref-locals-and-returns

using System;
using static System.Console;


class MatrixSearch
{
	// ref readonly is available in C# 7.2
	public static ref int Find(int[,] matrix, Func<int, bool> predicate)
	{
		for (int i = 0; i < matrix.GetLength(0); i++)
			for (int j = 0; j < matrix.GetLength(1); j++)
				if (predicate(matrix[i, j]))
					return ref matrix[i, j];
		throw new InvalidOperationException("Not found");
	}

	static void Main(string[] args)
	{
		int[,] matrix = new int[,] { { 0, 1, 2 }, { 3, 42, 5 } };
		ref var item = ref MatrixSearch.Find(matrix, (val) => val == 42);
		WriteLine($"Matrix item: {item}");
		item = 24;
		WriteLine($"Matrix item: {item} (after update to value 24).");
	}
}