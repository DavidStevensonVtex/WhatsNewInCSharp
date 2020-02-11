using static System.Console;

class Tuples_Contrast_And_Compare
{
	static void Main(string[] args)
	{
		// A variable with "one" value.
		var var1 = "abc";
		System.Console.WriteLine("var1: {0}", var1);
		WriteLine($"var1: {var1}");

		// A variable (tuple) with multiple values. 
		// (An ordered list/sequence of elements, mathematically speaking)
		// Works like an anonymous type with properties.
		// Property names generated automatically by compiler.
		var t1 = (123, "abc", 'z', 456.78m);
		WriteLine($"t1: Item1: {t1.Item1} Item2: {t1.Item2} " +
			$"Item3: {t1.Item3} Item4: {t1.Item4}\n");

		// A tuple with named properties.
		var t2 = (Street: "123 Main Street", City: "Rochester", State: "NY");
		WriteLine($"t2:    Street: {t2.Street} City: {t2.City} State: {t2.State}");

		// An anonymous type to contrast tuple syntax
		var anon1 = new { Street = "123 Main Street", City = "Rochester", State = "NY" };
		WriteLine($"anon1: Street: {anon1.Street} City: {anon1.City} State: {anon1.State}\n");

		// A method that returns no values.
		void ReturnNoValues() { return; }
		ReturnNoValues();

		// A method that returns one value.
		string ReturnOneValue() { return "abc"; }
		string abc = ReturnOneValue();

		// A method that returns multiple values.
		(int StreetNumber, string StreetName, decimal Cost) ReturnMultipleValues()
		{
			return (123, "Main Street", 456.78m);
		};

		int streetNum;
		string street;
		decimal cost;
		(streetNum, street, cost) = ReturnMultipleValues();
		WriteLine($"street number: {streetNum} street: {street} cost: {cost:C2}");

		var (sn, sname, c) = ReturnMultipleValues();
		WriteLine($"street number: {sn} street: {sname} cost: {c:C2}");

		dynamic foo = ReturnMultipleValues();
		WriteLine(foo.Item1);

		(int streetNumber, string streetName, decimal costOfItem) ReturnMultipleValuesNamed()
		{
			return (123, "Main Street", 456.78m);
		};
		(streetNum, street, cost) = ReturnMultipleValuesNamed();
		WriteLine($"street number: {streetNum} street: {street} cost: {cost:C2}\n");
	}
}