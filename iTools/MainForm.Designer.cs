namespace iTools
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.groupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.utilitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.utilitiesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.pnlMain = new System.Windows.Forms.Panel();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem,
            this.utilitiesToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(504, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.groupsToolStripMenuItem,
            this.editItemsToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
			this.toolsToolStripMenuItem.Text = "Tools";
			// 
			// groupsToolStripMenuItem
			// 
			this.groupsToolStripMenuItem.Name = "groupsToolStripMenuItem";
			this.groupsToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
			this.groupsToolStripMenuItem.Text = "Groups";
			this.groupsToolStripMenuItem.Click += new System.EventHandler(this.GroupsToolStripMenuItemClick);
			// 
			// editItemsToolStripMenuItem
			// 
			this.editItemsToolStripMenuItem.Name = "editItemsToolStripMenuItem";
			this.editItemsToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
			this.editItemsToolStripMenuItem.Text = "Edit Items";
			this.editItemsToolStripMenuItem.Click += new System.EventHandler(this.EditItemsToolStripMenuItemClick);
			// 
			// utilitiesToolStripMenuItem
			// 
			this.utilitiesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.utilitiesToolStripMenuItem1});
			this.utilitiesToolStripMenuItem.Name = "utilitiesToolStripMenuItem";
			this.utilitiesToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
			this.utilitiesToolStripMenuItem.Text = "Utilities";
			// 
			// utilitiesToolStripMenuItem1
			// 
			this.utilitiesToolStripMenuItem1.Name = "utilitiesToolStripMenuItem1";
			this.utilitiesToolStripMenuItem1.Size = new System.Drawing.Size(113, 22);
			this.utilitiesToolStripMenuItem1.Text = "Utilities";
			this.utilitiesToolStripMenuItem1.Click += new System.EventHandler(this.utilitiesToolStripMenuItem1_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 437);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(504, 22);
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// pnlMain
			// 
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMain.Location = new System.Drawing.Point(0, 24);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(504, 413);
			this.pnlMain.TabIndex = 2;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(504, 459);
			this.Controls.Add(this.pnlMain);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(16, 38);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "iTools";
			this.Resize += new System.EventHandler(this.MainFormResize);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem groupsToolStripMenuItem;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.Panel pnlMain;
		private System.Windows.Forms.ToolStripMenuItem editItemsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem utilitiesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem utilitiesToolStripMenuItem1;
	}
}

