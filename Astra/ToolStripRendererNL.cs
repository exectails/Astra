﻿using System.Windows.Forms;

namespace Astra
{
	/// <summary>
	/// ToolStrip renderer without border.
	/// </summary>
	public class ToolStripRendererNL : ToolStripSystemRenderer
	{
		public ToolStripRendererNL()
		{
		}

		protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
			//base.OnRenderToolStripBorder(e);
		}
	}
}
