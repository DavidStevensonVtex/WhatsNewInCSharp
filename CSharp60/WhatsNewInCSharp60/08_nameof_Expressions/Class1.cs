using System;
using static System.String;
using System.Collections.Generic;
using System.Linq;

// nameof Expressions
// The nameof expression evaluates to the name of a symbol. It's a great way to get 
// tools working whenever you need the name of a variable, a property, or a member field.

// One of the most common uses for nameof is to provide the name of a symbol that caused 
// an exception:
public class NameofExpressionExamples
{
	private void TestLastName()
	{
		if (IsNullOrWhiteSpace(lastName))
			throw new ArgumentException(message: "Cannot be blank", paramName: nameof(lastName));
	}

	// Another use is with XAML based applications that implement the 
	// INotifyPropertyChanged interface:

	public string LastName
	{
		get { return lastName; }
		set
		{
			if (value != lastName)
			{
				lastName = value;
				PropertyChanged?.Invoke(this,
					new PropertyChangedEventArgs(nameof(LastName)));
			}
		}
	}

	// The advantage of using the nameof operator over a constant string is that 
	// tools can understand the symbol. If you use refactoring tools to rename 
	// the symbol, it will rename it in the nameof expression. Constant strings 
	// don't have that advantage. Try it yourself in your favorite editor: 
	// rename a variable, and any nameof expressions will update as well.

	// The nameof expression produces the unqualified name of its argument 
	// (LastName in the previous examples) even if you use the fully qualified 
	// name for the argument:

	public string FirstName
	{
		get { return firstName; }
		set
		{
			if (value != firstName)
			{
				firstName = value;
				PropertyChanged?.Invoke(this,
					new PropertyChangedEventArgs(nameof(UXComponents.ViewModel.FirstName)));
			}
		}
	}
	// This nameof expression produces FirstName, not UXComponents.ViewModel.FirstName.

	private string firstName;
	private string lastName;
	public event EventHandler<PropertyChangedEventArgs> PropertyChanged;
}


public class PropertyChangedEventArgs : EventArgs
{
	public string PropertyName { get; set; }
	public PropertyChangedEventArgs(string propertyName)
	{
		this.PropertyName = propertyName;
	}
}

namespace UXComponents
{
	public class ViewModel
	{
		public string FirstName { get; set; }
	}
}