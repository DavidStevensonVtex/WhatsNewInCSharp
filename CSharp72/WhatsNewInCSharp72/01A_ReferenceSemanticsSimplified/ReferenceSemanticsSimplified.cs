using static System.Console;

public struct Point3D
{
	public int X, Y, Z;
	private static Point3D origin = new Point3D();
	public static ref readonly Point3D Origin => ref origin;
}
class ReferenceSemanticsSimplified
{
	static void TestIn(in Point3D p)
	{
		WriteLine($"TestIn: p.X: {p.X} p.Y: {p.Y} p.Z: {p.Z}");
		// CS8332  Cannot assign to a member of variable 'in Point3D' 
		// because it is a readonly variable (in Point3D p)
		//p.X = 123;
	}
	static void Main(string[] args)
	{
		Point3D p1 = new Point3D() { X = 1, Y = 2, Z = 3 };
		TestIn(p1);
		p1.X = 123;
		WriteLine($"p1: X: {p1.X} Y: {p1.Y} Z: {p1.Z}");

		// CS8332  Cannot assign to a member of property 'Point3D.Origin' 
		// because it is a readonly variable
		//Point3D.Origin.X = 456;
	}
}

// create an immutable struct
readonly public struct ReadonlyPoint3D
{
	public ReadonlyPoint3D(double x, double y, double z)
	{
		this.X = x;
		this.Y = y;
		this.Z = z;
	}

	public double X { get; }
	public double Y { get; }
	public double Z { get; }

	private static readonly ReadonlyPoint3D origin = new ReadonlyPoint3D();
	public static ref readonly ReadonlyPoint3D Origin => ref origin;
}


//	►	The ref struct declaration, to indicate that a struct type 
//		accesses managed memory directly and must always be stack 
//		allocated.
