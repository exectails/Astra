using MabiWorld;
using MabiWorld.Data;
using OpenPainter.ColorPicker;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Astra
{
	/// <summary>
	/// Main form.
	/// </summary>
	public partial class FrmMain : Form
	{
		public static string Title;

		private string _openFilePath;
		private Prop _workProp;
		//private Prop _selectedPropTemp;
		private Shape _selectedShape;

		/// <summary>
		/// Initializes form.
		/// </summary>
		/// <param name="args"></param>
		public FrmMain(string[] args)
		{
			this.InitializeComponent();

			this.ToolStrip.Renderer = new ToolStripRendererNL();
			this.LstProps.DoubleBuffered(true);

			this.BtnPropColorOverride.BackColor =
			this.BtnPropColor1.BackColor =
			this.BtnPropColor2.BackColor =
			this.BtnPropColor3.BackColor =
			this.BtnPropColor4.BackColor =
			this.BtnPropColor5.BackColor =
			this.BtnPropColor6.BackColor =
			this.BtnPropColor7.BackColor =
			this.BtnPropColor8.BackColor =
			this.BtnPropColor9.BackColor = Color.FromArgb(0);

			Title = this.Text;

			this.SetChanges(false);
			this.UpdatePropButtons(null);
		}

		/// <summary>
		/// Loads form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrmMain_Load(object sender, EventArgs e)
		{
		}

		/// <summary>
		/// Opens prop palette from given file.
		/// </summary>
		/// <param name="filePath"></param>
		private void Open(string filePath)
		{
			var ext = Path.GetExtension(filePath);
			if (ext != ".plt")
			{
				MessageBox.Show($"Unknown file extension '{ext.TrimStart('.')}'", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			try
			{
				PropPalette.Clear();
				PropPalette.Load(filePath);

				this.RefreshPropList();

				_openFilePath = filePath;

				this.SetChanges(false);
				this.SelectProp(null);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Opening file failed. Error: '{ex.Message}'", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Saves open prop palette to given file.
		/// </summary>
		/// <param name="filePath"></param>
		private void Save(string filePath)
		{
			try
			{
				PropPalette.Save(filePath);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Saving file failed. Error: '{ex.Message}'", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
		}

		/// <summary>
		/// Reloads prop list.
		/// </summary>
		private void RefreshPropList()
		{
			var props = PropPalette.GetEntries().OrderBy(a => a.Id);

			this.LstProps.BeginUpdate();

			this.LstProps.Items.Clear();
			foreach (var prop in props)
			{
				var lvi = new ListViewItem();
				lvi.Text = string.Format("{0}", prop.Id);
				lvi.SubItems.Add(prop.State);
				lvi.Tag = prop;

				this.LstProps.Items.Add(lvi);
			}

			this.LstProps.EndUpdate();
		}

		/// <summary>
		/// Changes title and enables buttons based on whether the file was
		/// modified or not.
		/// </summary>
		/// <param name="anyChanges"></param>
		private void SetChanges(bool anyChanges)
		{
			if (_openFilePath == null)
			{
				this.Text = Title;
				this.BtnSave.Enabled = false;
				return;
			}

			var newTitle = string.Format("{0} - {1}", Title, _openFilePath);
			if (anyChanges)
				newTitle = '*' + newTitle;

			this.Text = newTitle;
			this.BtnSave.Enabled = anyChanges;
		}

		/// <summary>
		/// Enables buttons based on whether a prop is selected.
		/// Disables them if prop is null.
		/// </summary>
		/// <param name="prop"></param>
		private void UpdatePropButtons(Prop prop)
		{
			//var enabled = (prop != null);

			//this.BtnSave.Enabled = enabled;
			//this.BtnNewProp.Enabled = enabled;
			//this.BtnCopyProp.Enabled = enabled;
			//this.BtnRemoveProp.Enabled = enabled;
		}

		/// <summary>
		/// Opens the given prop for editing.
		/// </summary>
		/// <param name="prop"></param>
		private void SelectProp(Prop prop)
		{
			if (prop == null)
			{
				this.TxtPropId.Text = "";
				this.TxtPropName.Text = "";
				this.TxtPropTitle.Text = "";
				this.TxtPropState.Text = "";
				this.TxtPropScale.Text = "";
				this.TxtPropRotation.Text = "";
				this.TxtPropBottomLeft.Text = "";
				this.TxtPropTopRight.Text = "";
				this.TxtPropColorOverride.Text = "";
				this.TxtPropColor1.Text = "";
				this.TxtPropColor2.Text = "";
				this.TxtPropColor3.Text = "";
				this.TxtPropColor4.Text = "";
				this.TxtPropColor5.Text = "";
				this.TxtPropColor6.Text = "";
				this.TxtPropColor7.Text = "";
				this.TxtPropColor8.Text = "";
				this.TxtPropColor9.Text = "";
				this.BtnPropColorOverride.BackColor =
				this.BtnPropColor1.BackColor =
				this.BtnPropColor2.BackColor =
				this.BtnPropColor3.BackColor =
				this.BtnPropColor4.BackColor =
				this.BtnPropColor5.BackColor =
				this.BtnPropColor6.BackColor =
				this.BtnPropColor7.BackColor =
				this.BtnPropColor8.BackColor =
				this.BtnPropColor9.BackColor = Color.FromArgb(0);
				this.ChkPropCollision.Checked = false;
				this.ChkPropFixedAltitude.Checked = false;

				this.LstShapes.Items.Clear();
				this.TxtShapeDirX1.Text = "";

				_workProp = null;
			}
			else
			{
				this.TxtPropId.Text = string.Format("{0}", prop.Id);
				this.TxtPropName.Text = string.Format("{0}", prop.Name);
				this.TxtPropTitle.Text = string.Format("{0}", prop.Title);
				this.TxtPropState.Text = string.Format("{0}", prop.State);
				this.TxtPropScale.Text = string.Format("{0:G9}", prop.Scale);
				this.TxtPropRotation.Text = string.Format("{0:G9}", prop.Rotation);
				this.TxtPropBottomLeft.Text = string.Format("{0:G9};{1:G9};{2:G9}", prop.BottomLeft.X, prop.BottomLeft.Y, prop.BottomLeft.Z);
				this.TxtPropTopRight.Text = string.Format("{0:G9};{1:G9};{2:G9}", prop.TopRight.X, prop.TopRight.Y, prop.TopRight.Z);
				this.TxtPropColorOverride.Text = string.Format("0x{0:X8}", prop.ColorOverride.ToArgb());
				this.TxtPropColor1.Text = string.Format("0x{0:X8}", prop.Colors[0].ToArgb());
				this.TxtPropColor2.Text = string.Format("0x{0:X8}", prop.Colors[1].ToArgb());
				this.TxtPropColor3.Text = string.Format("0x{0:X8}", prop.Colors[2].ToArgb());
				this.TxtPropColor4.Text = string.Format("0x{0:X8}", prop.Colors[3].ToArgb());
				this.TxtPropColor5.Text = string.Format("0x{0:X8}", prop.Colors[4].ToArgb());
				this.TxtPropColor6.Text = string.Format("0x{0:X8}", prop.Colors[5].ToArgb());
				this.TxtPropColor7.Text = string.Format("0x{0:X8}", prop.Colors[6].ToArgb());
				this.TxtPropColor8.Text = string.Format("0x{0:X8}", prop.Colors[7].ToArgb());
				this.TxtPropColor9.Text = string.Format("0x{0:X8}", prop.Colors[8].ToArgb());
				this.BtnPropColorOverride.BackColor = prop.ColorOverride;
				this.BtnPropColor1.BackColor = prop.Colors[0];
				this.BtnPropColor2.BackColor = prop.Colors[1];
				this.BtnPropColor3.BackColor = prop.Colors[2];
				this.BtnPropColor4.BackColor = prop.Colors[3];
				this.BtnPropColor5.BackColor = prop.Colors[4];
				this.BtnPropColor6.BackColor = prop.Colors[5];
				this.BtnPropColor7.BackColor = prop.Colors[6];
				this.BtnPropColor8.BackColor = prop.Colors[7];
				this.BtnPropColor9.BackColor = prop.Colors[8];
				this.ChkPropCollision.Checked = prop.IsCollision;
				this.ChkPropFixedAltitude.Checked = prop.FixedAltitude;

				_workProp = prop.Copy();

				this.LstShapes.BeginUpdate();

				this.LstShapes.Items.Clear();
				foreach (var shape in _workProp.Shapes)
					this.LstShapes.Items.Add(shape);

				this.LstShapes.EndUpdate();
			}

			this.SelectShape(null);
		}

		/// <summary>
		/// Opens the given shape for editing.
		/// </summary>
		/// <param name="shape"></param>
		private void SelectShape(Shape shape)
		{
			if (shape == null)
			{
				this.TxtShapeType.Text = "";
				this.TxtShapeDirX1.Text = "";
				this.TxtShapeDirX2.Text = "";
				this.TxtShapeDirY1.Text = "";
				this.TxtShapeDirY2.Text = "";
				this.TxtShapeLenX.Text = "";
				this.TxtShapeLenY.Text = "";
				this.TxtShapeX.Text = "";
				this.TxtShapeY.Text = "";
				this.TxtShapeBottomLeft.Text = "";
				this.TxtShapeTopRight.Text = "";
			}
			else
			{
				this.TxtShapeType.Text = string.Format("{0}", shape.Type);
				this.TxtShapeDirX1.Text = string.Format("{0:G9}", shape.DirX1);
				this.TxtShapeDirX2.Text = string.Format("{0:G9}", shape.DirX2);
				this.TxtShapeDirY1.Text = string.Format("{0:G9}", shape.DirY1);
				this.TxtShapeDirY2.Text = string.Format("{0:G9}", shape.DirY2);
				this.TxtShapeLenX.Text = string.Format("{0:G9}", shape.LenX);
				this.TxtShapeLenY.Text = string.Format("{0:G9}", shape.LenY);
				this.TxtShapeX.Text = string.Format("{0:G9}", shape.Position.X);
				this.TxtShapeY.Text = string.Format("{0:G9}", shape.Position.Y);
				this.TxtShapeBottomLeft.Text = string.Format("{0:G9};{1:G9}", shape.BottomLeft.X, shape.BottomLeft.Y);
				this.TxtShapeTopRight.Text = string.Format("{0:G9};{1:G9}", shape.TopRight.X, shape.TopRight.Y);
			}

			_selectedShape = shape;
		}

		/// <summary>
		/// Returns the selected prop via out, returns false if no prop
		/// was selected.
		/// </summary>
		/// <param name="prop"></param>
		/// <returns></returns>
		private bool TryGetSelectedProp(out Prop prop)
		{
			prop = null;

			if (this.LstProps.SelectedIndices.Count == 0)
				return false;

			var index = this.LstProps.SelectedIndices[0];
			prop = (Prop)this.LstProps.Items[index].Tag;

			return true;
		}

		/// <summary>
		/// Called when something is dragged onto this form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrmMain_DragEnter(object sender, DragEventArgs e)
		{
			var filePaths = e.Data.GetData(DataFormats.FileDrop) as string[];
			if (filePaths.Length == 0)
			{
				e.Effect = DragDropEffects.None;
				return;
			}

			var firstPath = filePaths[0];
			if (Path.GetExtension(firstPath) != ".plt")
			{
				e.Effect = DragDropEffects.None;
				return;
			}

			e.Effect = DragDropEffects.Copy;
		}

		/// <summary>
		/// Called when something is dropped onto this form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrmMain_DragDrop(object sender, DragEventArgs e)
		{
			var filePaths = e.Data.GetData(DataFormats.FileDrop) as string[];
			if (filePaths.Length == 0)
				return;

			var firstPath = filePaths[0];
			this.Open(firstPath);
		}

		/// <summary>
		/// Called when the selected item changes.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LstProps_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this.TryGetSelectedProp(out var prop))
			{
				this.UpdatePropButtons(null);
				return;
			}

			//if (this.LstProps.SelectedIndices.Count > 1)
			//{
			//	this.SelectProp(null);
			//	this.UpdatePropButtons(null);
			//	return;
			//}

			this.SelectProp(prop);
			this.UpdatePropButtons(prop);
		}

		/// <summary>
		/// Opens the About form if the About button is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnAbout_Click(object sender, EventArgs e)
		{
			var form = new FrmAbout();
			form.ShowDialog();
		}

		/// <summary>
		/// Called when the Open button is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnOpen_Click(object sender, EventArgs e)
		{
			var result = this.OfdPalette.ShowDialog();
			if (result != DialogResult.OK)
				return;

			var filePath = this.OfdPalette.FileName;
			this.Open(filePath);
		}

		/// <summary>
		/// Called when the Save button is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSave_Click(object sender, EventArgs e)
		{
			if (_openFilePath == null)
			{
				MessageBox.Show("No file open.", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			this.Save(_openFilePath);
			this.SetChanges(false);
		}

		/// <summary>
		/// Called when a key was pressed on the prop list.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LstProps_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Delete)
				return;

			if (!this.TryGetSelectedProp(out var prop))
			{
				MessageBox.Show("No prop selected.", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			if (!PropPalette.Remove(prop.Id))
			{
				MessageBox.Show("Failed to remove prop, not found in file.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var selectedIndex = this.LstProps.SelectedIndices[0];

			if (this.LstProps.Items.Count > 0)
			{
				this.LstProps.BeginUpdate();
				this.LstProps.Items.RemoveAt(selectedIndex);

				selectedIndex = Math.Max(0, Math.Min(this.LstProps.Items.Count - 1, selectedIndex));

				this.LstProps.SelectedIndices.Clear();
				this.LstProps.SelectedIndices.Add(selectedIndex);
				this.LstProps.Items[selectedIndex].EnsureVisible();

				this.LstProps.EndUpdate();
			}

			this.SetChanges(true);
		}

		/// <summary>
		/// Called when a shape is selected.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LstShapes_SelectedIndexChanged(object sender, EventArgs e)
		{
			var shape = this.LstShapes.SelectedItem as Shape;
			this.SelectShape(shape);
		}

		/// <summary>
		/// Called when the Save Prop button was clicked, saves the selected
		/// prop.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSaveProp_Click(object sender, EventArgs e)
		{
			if (_workProp == null)
			{
				MessageBox.Show("No prop selected.", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			this.HandlePropForm(false);
			this.SetChanges(true);
		}

		/// <summary>
		/// Called when the Add Prop button was clicked, adds a new prop
		/// based on the current information.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnAddProp_Click(object sender, EventArgs e)
		{
			if (_workProp == null)
			{
				MessageBox.Show("No prop selected.", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			this.HandlePropForm(true);
			this.SetChanges(true);
		}

		/// <summary>
		/// Saves or adds prop based on the form data.
		/// </summary>
		/// <param name="addNew"></param>
		private void HandlePropForm(bool addNew)
		{
			var propName = this.TxtPropName.Text.Trim();
			var propTitle = this.TxtPropTitle.Text.Trim();
			var propState = this.TxtPropState.Text.Trim();
			var isCollision = this.ChkPropCollision.Checked;
			var fixedAltitude = this.ChkPropFixedAltitude.Checked;

			if (!int.TryParse(this.TxtPropId.Text, out var propId))
			{
				MessageBox.Show("Invalid id.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!float.TryParse(this.TxtPropScale.Text, out var propScale))
			{
				MessageBox.Show("Invalid scale.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!float.TryParse(this.TxtPropRotation.Text, out var propRotation))
			{
				MessageBox.Show("Invalid rotation.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!uint.TryParse(this.TxtPropColorOverride.Text.Replace("0x", ""), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var propColorOverride))
			{
				MessageBox.Show("Invalid color override.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!uint.TryParse(this.TxtPropColor1.Text.Replace("0x", ""), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var propColor1))
			{
				MessageBox.Show("Invalid color 1.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!uint.TryParse(this.TxtPropColor2.Text.Replace("0x", ""), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var propColor2))
			{
				MessageBox.Show("Invalid color 2.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!uint.TryParse(this.TxtPropColor3.Text.Replace("0x", ""), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var propColor3))
			{
				MessageBox.Show("Invalid color 3.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!uint.TryParse(this.TxtPropColor4.Text.Replace("0x", ""), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var propColor4))
			{
				MessageBox.Show("Invalid color 4.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!uint.TryParse(this.TxtPropColor5.Text.Replace("0x", ""), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var propColor5))
			{
				MessageBox.Show("Invalid color 5.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!uint.TryParse(this.TxtPropColor6.Text.Replace("0x", ""), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var propColor6))
			{
				MessageBox.Show("Invalid color 6.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!uint.TryParse(this.TxtPropColor7.Text.Replace("0x", ""), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var propColor7))
			{
				MessageBox.Show("Invalid color 7.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!uint.TryParse(this.TxtPropColor8.Text.Replace("0x", ""), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var propColor8))
			{
				MessageBox.Show("Invalid color 8.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!uint.TryParse(this.TxtPropColor9.Text.Replace("0x", ""), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var propColor9))
			{
				MessageBox.Show("Invalid color 9.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var bottomLeftSplit = this.TxtPropBottomLeft.Text.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
			if (bottomLeftSplit.Length < 3)
			{
				MessageBox.Show("Invalid bottom left coordinates, expected format: X;Y;Z.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!float.TryParse(bottomLeftSplit[0], out var bottomLeftX))
			{
				MessageBox.Show("Invalid bottom left X coordinate.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!float.TryParse(bottomLeftSplit[1], out var bottomLeftY))
			{
				MessageBox.Show("Invalid bottom left Y coordinate.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!float.TryParse(bottomLeftSplit[2], out var bottomLeftZ))
			{
				MessageBox.Show("Invalid bottom left Z coordinate.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var topRightSplit = this.TxtPropTopRight.Text.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
			if (topRightSplit.Length < 3)
			{
				MessageBox.Show("Invalid top right coordinates, expected format: X;Y;Z.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!float.TryParse(topRightSplit[0], out var topRightX))
			{
				MessageBox.Show("Invalid top right X coordinate.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!float.TryParse(topRightSplit[1], out var topRightY))
			{
				MessageBox.Show("Invalid top right Y coordinate.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!float.TryParse(topRightSplit[2], out var topRightZ))
			{
				MessageBox.Show("Invalid top right Z coordinate.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var prop = _workProp;

			prop.Id = propId;
			prop.Name = propName;
			prop.Title = propTitle;
			prop.State = propState;
			prop.Scale = propScale;
			prop.Rotation = propRotation;
			prop.BottomLeft = new Vector3F(bottomLeftX, bottomLeftY, bottomLeftZ);
			prop.TopRight = new Vector3F(topRightX, topRightY, topRightZ);
			prop.ColorOverride = Color.FromArgb((int)propColorOverride);
			prop.Colors[0] = Color.FromArgb((int)propColor1);
			prop.Colors[1] = Color.FromArgb((int)propColor2);
			prop.Colors[2] = Color.FromArgb((int)propColor3);
			prop.Colors[3] = Color.FromArgb((int)propColor4);
			prop.Colors[4] = Color.FromArgb((int)propColor5);
			prop.Colors[5] = Color.FromArgb((int)propColor6);
			prop.Colors[6] = Color.FromArgb((int)propColor7);
			prop.Colors[7] = Color.FromArgb((int)propColor8);
			prop.Colors[8] = Color.FromArgb((int)propColor9);
			prop.IsCollision = this.ChkPropCollision.Checked;
			prop.FixedAltitude = this.ChkPropFixedAltitude.Checked;

			if (addNew)
			{
				var newIndex = GetNextPropIndex();
				prop.EntityId = 0x000A_0000_0400_0000 | (ulong)newIndex;

				PropPalette.Add(prop.Copy());

				this.LstProps.BeginUpdate();

				var lvi = new ListViewItem();
				lvi.Text = string.Format("{0}", prop.Id);
				lvi.SubItems.Add(prop.State);
				lvi.Tag = prop;

				this.LstProps.Items.Add(lvi);

				lvi.Selected = true;
				lvi.EnsureVisible();

				this.LstProps.EndUpdate();
			}
			else
			{
				var selectedItem = this.LstProps.SelectedItems[0];
				var listProp = selectedItem.Tag as Prop;

				listProp.Id = prop.Id;
				listProp.Name = prop.Name;
				listProp.Title = prop.Title;
				listProp.State = prop.State;
				listProp.Scale = prop.Scale;
				listProp.Rotation = prop.Rotation;
				listProp.BottomLeft = prop.BottomLeft;
				listProp.TopRight = prop.TopRight;
				listProp.ColorOverride = prop.ColorOverride;
				for (var i = 0; i < 9; ++i)
					listProp.Colors[i] = prop.Colors[i];
				listProp.IsCollision = prop.IsCollision;
				listProp.FixedAltitude = prop.FixedAltitude;
				listProp.Shapes.Clear();
				listProp.Shapes.AddRange(prop.Shapes);

				selectedItem.SubItems[0].Text = listProp.Id.ToString();
				selectedItem.SubItems[1].Text = listProp.State;
			}
		}

		/// <summary>
		/// Returns the current max index + 1.
		/// </summary>
		/// <returns></returns>
		private ushort GetNextPropIndex()
		{
			int result = -1;

			foreach (ListViewItem item in this.LstProps.Items)
			{
				var prop = (Prop)item.Tag;
				var index = (ushort)(prop.EntityId & 0xFFFF);

				if (index > result)
					result = index;
			}

			return (ushort)(result + 1);
		}

		/// <summary>
		/// Opens color selector and sets the appropriate text box.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnPropColorSelect_Click(object sender, EventArgs e)
		{
			var button = (Button)sender;
			var textBoxName = button.Name.Replace("Btn", "Txt");

			var textBoxField = this.GetType().GetField(textBoxName, BindingFlags.NonPublic | BindingFlags.Instance);
			if (textBoxField == null)
			{
				MessageBox.Show("Text box not found.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var textBox = textBoxField.GetValue(this);
			var textProperty = textBoxField.FieldType.GetProperty("Text", BindingFlags.Public | BindingFlags.Instance);
			if (textProperty == null)
			{
				MessageBox.Show("Text property not found.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var prevColorText = ((string)textProperty.GetValue(textBox)).Replace("0x", "");
			if (!int.TryParse(prevColorText, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var prevColorArgb))
				prevColorArgb = 0xFFFFFF;

			var prevColor = Color.FromArgb(prevColorArgb);

			var form = new PsColorPicker(prevColor);
			if (form.ShowDialog() != DialogResult.OK)
				return;

			var newColor = form.Color;
			var newColorText = string.Format("0x{0:X8}", newColor.ToArgb());

			button.BackColor = newColor;
			textProperty.SetValue(textBox, newColorText);
		}

		/// <summary>
		/// Called when a key is pressed on the shape list.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LstShapes_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Delete)
				return;

			var shape = this.LstShapes.SelectedItem as Shape;
			if (shape == null)
				return;

			_workProp.Shapes.Remove(shape);
			this.LstShapes.Items.Remove(shape);

			this.SelectShape(null);
		}

		/// <summary>
		/// Adds a new shape to the shape list when the Add Shape button
		/// is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnAddShape_Click(object sender, EventArgs e)
		{
			if (_workProp == null)
			{
				MessageBox.Show("No prop selected.", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			var shape = new Shape();

			shape.Type = 0;
			shape.DirX1 = -1;
			shape.DirX2 = 0;
			shape.DirY1 = 0;
			shape.DirY2 = 1;
			shape.LenX = 50;
			shape.LenY = 50;
			shape.Position = new PointF(0, 0);
			shape.BottomLeft = new PointF(-50, -50);
			shape.TopRight = new PointF(50, 50);

			_workProp.Shapes.Add(shape);
			this.LstShapes.Items.Add(shape);
		}

		/// <summary>
		/// Removes selected shape from the shape list when the Remove
		/// Shape button is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnRemoveShape_Click(object sender, EventArgs e)
		{
			var shape = this.LstShapes.SelectedItem as Shape;
			if (shape == null)
			{
				MessageBox.Show("No shape selected.", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			_workProp.Shapes.Remove(shape);
			this.LstShapes.Items.Remove(shape);

			this.SelectShape(null);
		}

		/// <summary>
		/// Called when the Save Shape button is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSaveShape_Click(object sender, EventArgs e)
		{
			var shape = this.LstShapes.SelectedItem as Shape;
			if (shape == null)
			{
				MessageBox.Show("No shape selected.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!int.TryParse(this.TxtShapeType.Text, out var shapeType))
			{
				MessageBox.Show("Invalid type.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!float.TryParse(this.TxtShapeDirX1.Text, out var shapeDirX1))
			{
				MessageBox.Show("Invalid DirX1.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!float.TryParse(this.TxtShapeDirX2.Text, out var shapeDirX2))
			{
				MessageBox.Show("Invalid DirX2.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!float.TryParse(this.TxtShapeDirY1.Text, out var shapeDirY1))
			{
				MessageBox.Show("Invalid DirY1.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!float.TryParse(this.TxtShapeDirY2.Text, out var shapeDirY2))
			{
				MessageBox.Show("Invalid DirY2.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!float.TryParse(this.TxtShapeLenX.Text, out var shapeLenX))
			{
				MessageBox.Show("Invalid LenX.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!float.TryParse(this.TxtShapeLenY.Text, out var shapeLenY))
			{
				MessageBox.Show("Invalid LenY.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!float.TryParse(this.TxtShapeX.Text, out var shapeX))
			{
				MessageBox.Show("Invalid X coordinate.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!float.TryParse(this.TxtShapeY.Text, out var shapeY))
			{
				MessageBox.Show("Invalid Y coordinate.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var bottomLeftSplit = this.TxtShapeBottomLeft.Text.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
			if (bottomLeftSplit.Length < 2)
			{
				MessageBox.Show("Invalid bottom left coordinates, expected format: X;Y.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!float.TryParse(bottomLeftSplit[0], out var bottomLeftX))
			{
				MessageBox.Show("Invalid bottom left X coordinate.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!float.TryParse(bottomLeftSplit[1], out var bottomLeftY))
			{
				MessageBox.Show("Invalid bottom left Y coordinate.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var topRightSplit = this.TxtShapeTopRight.Text.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
			if (topRightSplit.Length < 2)
			{
				MessageBox.Show("Invalid top right coordinates, expected format: X;Y.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!float.TryParse(topRightSplit[0], out var topRightX))
			{
				MessageBox.Show("Invalid top right X coordinate.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!float.TryParse(topRightSplit[1], out var topRightY))
			{
				MessageBox.Show("Invalid top right Y coordinate.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			shape.Type = shapeType;
			shape.DirX1 = shapeDirX1;
			shape.DirX2 = shapeDirX2;
			shape.DirY1 = shapeDirY1;
			shape.DirY2 = shapeDirY2;
			shape.LenX = shapeLenX;
			shape.LenY = shapeLenY;
			shape.Position = new PointF(shapeX, shapeY);
			shape.BottomLeft = new PointF(bottomLeftX, bottomLeftY);
			shape.TopRight = new PointF(topRightX, topRightY);
		}
	}
}
