﻿namespace Astra
{
	partial class FrmAbout
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAbout));
			this.PnlHeader = new System.Windows.Forms.Panel();
			this.LblVersion = new System.Windows.Forms.Label();
			this.LblSubTitle = new System.Windows.Forms.Label();
			this.LblName = new System.Windows.Forms.Label();
			this.ImgIcon = new System.Windows.Forms.PictureBox();
			this.ImgPatreon = new System.Windows.Forms.PictureBox();
			this.ImgGitHub = new System.Windows.Forms.PictureBox();
			this.BtnOK = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.PnlHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImgIcon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ImgPatreon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ImgGitHub)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// PnlHeader
			// 
			this.PnlHeader.BackColor = System.Drawing.Color.White;
			this.PnlHeader.Controls.Add(this.LblVersion);
			this.PnlHeader.Controls.Add(this.LblSubTitle);
			this.PnlHeader.Controls.Add(this.LblName);
			this.PnlHeader.Controls.Add(this.ImgIcon);
			this.PnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.PnlHeader.ForeColor = System.Drawing.SystemColors.ControlText;
			this.PnlHeader.Location = new System.Drawing.Point(0, 0);
			this.PnlHeader.Name = "PnlHeader";
			this.PnlHeader.Size = new System.Drawing.Size(373, 81);
			this.PnlHeader.TabIndex = 24;
			// 
			// LblVersion
			// 
			this.LblVersion.AutoSize = true;
			this.LblVersion.ForeColor = System.Drawing.Color.Gray;
			this.LblVersion.Location = new System.Drawing.Point(110, 28);
			this.LblVersion.Name = "LblVersion";
			this.LblVersion.Size = new System.Drawing.Size(43, 13);
			this.LblVersion.TabIndex = 3;
			this.LblVersion.Text = "v1.0.1a";
			// 
			// LblSubTitle
			// 
			this.LblSubTitle.AutoSize = true;
			this.LblSubTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.LblSubTitle.Location = new System.Drawing.Point(65, 43);
			this.LblSubTitle.Name = "LblSubTitle";
			this.LblSubTitle.Size = new System.Drawing.Size(141, 13);
			this.LblSubTitle.TabIndex = 2;
			this.LblSubTitle.Text = "Mabinogi Prop Palette Editor";
			// 
			// LblName
			// 
			this.LblName.AutoSize = true;
			this.LblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LblName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.LblName.Location = new System.Drawing.Point(63, 20);
			this.LblName.Name = "LblName";
			this.LblName.Size = new System.Drawing.Size(52, 24);
			this.LblName.TabIndex = 2;
			this.LblName.Text = "Astra";
			// 
			// ImgIcon
			// 
			this.ImgIcon.Image = ((System.Drawing.Image)(resources.GetObject("ImgIcon.Image")));
			this.ImgIcon.Location = new System.Drawing.Point(25, 24);
			this.ImgIcon.Name = "ImgIcon";
			this.ImgIcon.Size = new System.Drawing.Size(32, 32);
			this.ImgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.ImgIcon.TabIndex = 2;
			this.ImgIcon.TabStop = false;
			// 
			// ImgPatreon
			// 
			this.ImgPatreon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ImgPatreon.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ImgPatreon.Image = ((System.Drawing.Image)(resources.GetObject("ImgPatreon.Image")));
			this.ImgPatreon.Location = new System.Drawing.Point(12, 227);
			this.ImgPatreon.Name = "ImgPatreon";
			this.ImgPatreon.Size = new System.Drawing.Size(189, 32);
			this.ImgPatreon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.ImgPatreon.TabIndex = 27;
			this.ImgPatreon.TabStop = false;
			this.ImgPatreon.Tag = "https://www.patreon.com/exectails";
			this.ImgPatreon.Click += new System.EventHandler(this.Link_Click);
			// 
			// ImgGitHub
			// 
			this.ImgGitHub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ImgGitHub.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ImgGitHub.Image = ((System.Drawing.Image)(resources.GetObject("ImgGitHub.Image")));
			this.ImgGitHub.Location = new System.Drawing.Point(207, 227);
			this.ImgGitHub.Name = "ImgGitHub";
			this.ImgGitHub.Size = new System.Drawing.Size(32, 32);
			this.ImgGitHub.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.ImgGitHub.TabIndex = 26;
			this.ImgGitHub.TabStop = false;
			this.ImgGitHub.Tag = "https://github.com/exectails";
			this.ImgGitHub.Click += new System.EventHandler(this.Link_Click);
			// 
			// BtnOK
			// 
			this.BtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnOK.Location = new System.Drawing.Point(286, 236);
			this.BtnOK.Name = "BtnOK";
			this.BtnOK.Size = new System.Drawing.Size(75, 23);
			this.BtnOK.TabIndex = 25;
			this.BtnOK.Text = "OK";
			this.BtnOK.UseVisualStyleBackColor = true;
			this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(12, 87);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(348, 131);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 29;
			this.pictureBox1.TabStop = false;
			// 
			// FrmAbout
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(373, 271);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.PnlHeader);
			this.Controls.Add(this.ImgPatreon);
			this.Controls.Add(this.ImgGitHub);
			this.Controls.Add(this.BtnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmAbout";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			this.PnlHeader.ResumeLayout(false);
			this.PnlHeader.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImgIcon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ImgPatreon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ImgGitHub)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Panel PnlHeader;
		private System.Windows.Forms.Label LblVersion;
		private System.Windows.Forms.Label LblSubTitle;
		private System.Windows.Forms.Label LblName;
		private System.Windows.Forms.PictureBox ImgIcon;
		private System.Windows.Forms.PictureBox ImgPatreon;
		private System.Windows.Forms.PictureBox ImgGitHub;
		private System.Windows.Forms.Button BtnOK;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}