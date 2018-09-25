using System;
using System.Windows.Forms;

namespace iTools
{
	public partial class MainForm : Form
	{
		private Groups _groups ;

		public MainForm()
		{
			InitializeComponent();
			InitializeLayout();
		}

		private void InitializeLayout()
		{
			LoadGroups();
		}

		private void GroupsToolStripMenuItemClick(object sender, EventArgs e)
		{
			LoadGroups();
		}

		private void LoadGroups()
		{
			_groups = new Groups();
			AddControlToMainPanel(_groups);
			_groups.RefreshLayout();
		}

		private void AddControlToMainPanel(Control control)
		{
			pnlMain.Controls.Clear();
			control.Dock = DockStyle.Fill;
			pnlMain.Controls.Add(control);
		}

		private void MainFormResize(object sender, EventArgs e)
		{
			_groups.RefreshLayout();
		}

		private void EditItemsToolStripMenuItemClick(object sender, EventArgs e)
		{
			AddControlToMainPanel(new ItemsEditUserControl());
		}

		private void utilitiesToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			AddControlToMainPanel(new UtilitiesUserControl());
		    this.Width = Convert.ToInt32(this.Width*1.5);
			this.Height = Convert.ToInt32(this.Height * 1.2);
		}
	}
}
