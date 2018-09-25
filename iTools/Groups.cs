using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using iTools.LaunchTool;

namespace iTools
{
	public partial class Groups : UserControl
	{
		private List<TabPage> tabPages;
		private readonly GroupsController _myController;
		public Groups()
		{
			InitializeComponent();
			_myController = new GroupsController();
			_myController.Load();
			GenerateControls();
		}

		public void RefreshLayout()
		{
			foreach (var page in tabPages)
			{
				LayoutHelper.CalcSpace(page, 100, 100);
			}
		}

		private void GenerateControls()
		{
			var tc = new TabControl();
			tc.Dock = DockStyle.Fill;
			tc.SelectedIndexChanged += new EventHandler(TcSelectedIndexChanged);
			Controls.Add(tc);
			tabPages = new List<TabPage>();
			foreach (var group in _myController.LaunchConfigurationGroups.Groups)
			{
				var tp = new TabPage();
				tp.Width = tc.Width -10;
				tp.Name = group.Name;
				tp.Text = group.Name;
				tp.TabIndex = Convert.ToInt32(group.Index);

				foreach (var item in group.Items)
				{
					tp.Controls.Add(CreateButton(item));
				}
				LayoutHelper.CalcSpace(tp, 100, 100);
				tc.TabPages.Add(tp);
				tabPages.Add(tp);
			}
		}

		void TcSelectedIndexChanged(object sender, EventArgs e)
		{
			RefreshLayout();
		}

		private ToolButton CreateButton(LaunchItem item)
		{
			var button = new ToolButton
							 {
								 Size = new System.Drawing.Size(100, 100),
								 TabIndex = 0,
								 Text = item.Name,
								 ButtonValue = item.Value,
								 UseVisualStyleBackColor = true,
								 BackColor = LayoutHelper.GetRamdomColor
							 };
			button.Click += ButtonClick;

			return button;
		}

		private void ButtonClick(object sender, EventArgs e)
		{
			SendCommandWithCli(((ToolButton)sender).ButtonValue);
		}

		/// <summary>
		/// send command with command line
		/// </summary>
		/// <param name="command"></param>
		protected void SendCommandWithCli(string command)
		{
			var procStartInfo = new ProcessStartInfo("cmd.exe", @"/c " + command.Replace(Environment.NewLine, "&"))
			{
				UseShellExecute = false,
				RedirectStandardInput = true,
				RedirectStandardOutput = true,
				CreateNoWindow = true
			};

			var process = Process.Start(procStartInfo);
            
			process.WaitForExit(1000);
            
			process.Close();
		}
	}
}
