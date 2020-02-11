// Assembly2.cs  
// Compile with: /reference:Assembly1.dll  
class DerivedClass2 : BaseClass
{
	void Access()
	{
		// Error CS0122, because myValue can only be
		// accessed by types in Assembly1
		//myValue = 10;
	}
}