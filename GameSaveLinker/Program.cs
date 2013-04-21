using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace GameSaveLinker
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
#if DEBUG
			String currentPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
			Stream file = File.Create(String.Format("{0}\\{1}", currentPath, "Debug.log"));
			TextWriterTraceListener listener = new TextWriterTraceListener(file);
			Trace.Listeners.Add(listener);
#endif

			Trace.WriteLine(String.Format("{0} -- Started", DateTime.Now.ToString("MM/dd/yyyy h:mm tt")));
			Trace.WriteLine("");

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainWindow());

			Trace.WriteLine("");
			Trace.WriteLine(String.Format("{0} -- Ended", DateTime.Now.ToString("MM/dd/yyyy h:mm tt")));
			Trace.Flush();
		}
	}
}
