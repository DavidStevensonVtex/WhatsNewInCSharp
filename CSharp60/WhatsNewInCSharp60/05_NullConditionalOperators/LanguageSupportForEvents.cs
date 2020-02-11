// https://docs.microsoft.com/en-us/dotnet/csharp/events-overview#language-support-for-events


using System;
using System.IO;

public class FileListArgs :EventArgs
{
	public FileInfo FoundFile { get; }
	public FileListArgs ( FileInfo fi )
	{
		this.FoundFile = fi;
	}
}

// Language Support for Events

// The syntax for defining events, and subscribing or unsubscribing from 
// events is an extension of the syntax for delegates.

// To define an event you use the event keyword:
public class LanguageSupportForEvents
{
	public event EventHandler<FileListArgs> Progress;

	// The type of the event (EventHandler<FileListArgs> in this example) 
	// must be a delegate type. There are a number of conventions that you 
	// should follow when declaring an event. Typically, the event delegate 
	// type has a void return. Event declarations should be a verb, or a 
	// verb phrase. Use past tense (as in this example) when the event 
	// reports something that has happened. Use a present tense verb 
	// (for example, Closing) to report something that is about to happen. 
	// Often, using present tense indicates that your class supports some 
	// kind of customization behavior. One of the most common scenarios is 
	// to support cancellation. For example, a Closing event may include 
	// an argument that would indicate if the close operation should continue, 
	// or not. Other scenarios may enable callers to modify behavior by updating 
	// properties of the event arguments. You may raise an event to indicate 
	// a proposed next action an algorithm will take. The event handler may 
	// mandate a different action by modifying properties of the event argument.

	// When you want to raise the event, you call the event handlers using 
	// the delegate invocation syntax:

	public void InvokeEvent(FileInfo file)
	{
		Progress?.Invoke(this, new FileListArgs(file));
	}

	public static void Main()
	{
		LanguageSupportForEvents lister = new LanguageSupportForEvents();

		// As discussed in the section on delegates, the ?. operator makes it easy 
		// to ensure that you do not attempt to raise the event when there are no 
		// subscribers to that event.

		// You subscribe to an event by using the += operator:
		EventHandler<FileListArgs> onProgress = (sender, eventArgs) => 
		Console.WriteLine(eventArgs.FoundFile);
		lister.Progress += onProgress;

		// The handler method typically is the prefix 'On' followed by the event name, 
		// as shown above.

		// You unsubscribe using the -= operator:

		lister.Progress -= onProgress;
	}
}

