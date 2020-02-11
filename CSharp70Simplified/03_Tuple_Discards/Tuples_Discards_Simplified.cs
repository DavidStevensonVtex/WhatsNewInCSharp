using static System.Console;

class Tuples_Discards_Simplified
{
	static void Main(string[] args)
	{
		// A discard is a write-only variable whose name is _ (the underscore character)

		// Using a discard in a tuple assignment.
		var t1 = (Street: "123 Main Street", City: "Rochester", State: "NY");
		string state2;
		(_, _, state2) = t1;
		WriteLine($"{nameof(state2)}: {state2}\n");
		if (state2 == null)
			throw new System.ArgumentNullException(nameof(state2));

		(int streetNumber, string streetName, decimal costOfItem) ReturnMultipleValuesNamed()
		{
			return (123, "Main Street", 456.78m);
		};
		// Discard street number and name. Only interested in cost.
		decimal cost;
		(_, _, cost) = ReturnMultipleValuesNamed();
		WriteLine($"cost: {cost:C2}\n");

		// You can use discards when calling methods with out parameters.
		if (int.TryParse("123", out _))
			WriteLine("123 is an integer\n");
	}
}