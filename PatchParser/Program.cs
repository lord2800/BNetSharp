using System;
using System.Windows.Forms;

namespace MPQParser
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			if(args.Length == 2 && args[0] == "--update")
				MainForm.ParsePatch(args[1], delegate { }, delegate { });
			else
				Application.Run(new MainForm());
		}
	}
}
