using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace Astra
{
	static class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			CultureInfo.DefaultThreadCurrentCulture =
			CultureInfo.DefaultThreadCurrentUICulture =
			Thread.CurrentThread.CurrentCulture =
			Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FrmMain(args));
		}
	}
}
