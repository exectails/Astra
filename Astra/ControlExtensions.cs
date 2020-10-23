using System.Reflection;
using System.Windows.Forms;

namespace Astra
{
	public static class ControlExtensions
	{
		// https://stackoverflow.com/a/15268338/1171898
		public static void DoubleBuffered(this Control control, bool enable)
		{
			var doubleBufferPropertyInfo = control.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
			doubleBufferPropertyInfo.SetValue(control, enable, null);
		}
	}
}
