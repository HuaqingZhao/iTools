using System;
using System.Linq;
using System.Windows.Forms;
using iTools.LaunchTool;

namespace iTools
{
	public partial class ItemsEditUserControl : UserControl
	{
        private string currentGroup;
		public ItemsEditUserControl()
		{
			InitializeComponent();
			GenerateData();

			cbxGroups.SelectedIndex = 0;
            currentGroup = cbxGroups.SelectedItem.ToString();
		}

		private void GenerateData()
		{
			cbxGroups.Items.Clear();
			cbxGroups.Items.AddRange(LaunchConfiguration.LaunchGroups.Groups.Select<LaunchGroup, object>(p => p.Name).ToArray<object>());

			txtItem.Text = string.Empty;
			txtItemValue.Text = string.Empty;
		}

		private void LtbItemsSelectedIndexChanged(object sender, EventArgs e)
		{
            if (ltbItems.SelectedIndex > -1)
			{
				txtItem.Text = ltbItems.SelectedItem.ToString();
				txtItemValue.Text = LaunchConfiguration.GetItemValue(ltbItems.SelectedItem.ToString(), currentGroup);
			}
		}

		private void BtnSaveClick(object sender, EventArgs e)
		{
			var item = new LaunchItem {Name = txtItem.Text, Value = txtItemValue.Text};
			LaunchConfiguration.AddNewItem(item, cbxGroups.SelectedItem.ToString());
			RefreshItem();
		}

		private void BtnDeleteClick(object sender, EventArgs e)
		{
			LaunchConfiguration.DeleteItem(new LaunchItem { Name = txtItem.Text, Value = txtItemValue.Text }, cbxGroups.SelectedItem.ToString());
			RefreshItem();
		}

		private void BtnUpClick(object sender, EventArgs e)
		{
			if (ltbItems.SelectedIndex > 0)
			{
				var selectedIndex = ltbItems.SelectedIndex;
				LaunchConfiguration.MoveUpItem(new LaunchItem { Name = txtItem.Text, Value = txtItemValue.Text },currentGroup);
				GenerateData();
                cbxGroups.SelectedIndex = cbxGroups.Items.IndexOf(currentGroup);
				ltbItems.SelectedIndex = selectedIndex - 1;
			}
		}

		private void BtnDownClick(object sender, EventArgs e)
		{
			if (ltbItems.SelectedIndex > -1 && ltbItems.SelectedIndex < ltbItems.Items.Count - 1)
			{
				var selectedIndex = ltbItems.SelectedIndex;
				LaunchConfiguration.MoveDownItem(new LaunchItem { Name = txtItem.Text, Value = txtItemValue.Text }, currentGroup);
				GenerateData();
                cbxGroups.SelectedIndex = cbxGroups.Items.IndexOf(currentGroup);
				ltbItems.SelectedIndex = selectedIndex + 1;
			}
		}

		private void CbxGroupsSelectedIndexChanged(object sender, EventArgs e)
		{
			ltbItems.Items.Clear();
			ltbItems.Items.AddRange(LaunchConfiguration.LaunchGroups.Groups.Single(p => p.Name.Equals(cbxGroups.SelectedItem)).Items.Select<LaunchItem, object>(p => p.Name).ToArray<object>());
			txtGroupName.Text = LaunchConfiguration.LaunchGroups.Groups.Single(p => p.Name.Equals(cbxGroups.SelectedItem)).Name;
			txtGroupTitle.Text = LaunchConfiguration.LaunchGroups.Groups.Single(p => p.Name.Equals(cbxGroups.SelectedItem)).Title;
			txtGroupIndex.Text = LaunchConfiguration.LaunchGroups.Groups.Single(p => p.Name.Equals(cbxGroups.SelectedItem)).Index;
            currentGroup = cbxGroups.SelectedItem.ToString();
		}

		private void BtnDeleteGroupClick(object sender, EventArgs e)
		{
			LaunchConfiguration.RemoveGroup(txtGroupName.Text);
			RefreshGroup();
			cbxGroups.SelectedIndex = 0;
            currentGroup = cbxGroups.SelectedItem.ToString();
		}

		private void BtnAddGroupClick(object sender, EventArgs e)
		{
			LaunchConfiguration.AddNewGroup(new LaunchGroup()
				                               {
					                               Name = txtGroupName.Text,
					                               Title = txtGroupTitle.Text,
					                               Index = txtGroupIndex.Text
				                               });
			RefreshGroup();

			cbxGroups.SelectedIndex = Convert.ToInt32(txtGroupIndex.Text);
            currentGroup = cbxGroups.SelectedItem.ToString();
		}

		private void RefreshGroup()
		{
			GenerateData();
			RefreshItem();
		}

		private void RefreshItem()
		{
			ReloadItems();
			ClearItemNameAndValue();
		}

		private void ReloadItems()
		{
			try
			{
				ltbItems.Items.Clear();
				ltbItems.Items.AddRange(
					LaunchConfiguration.LaunchGroups.Groups.Single(p => p.Name.Equals(cbxGroups.SelectedItem)).Items.Select
						<LaunchItem, object>(p => p.Name).ToArray<object>());
			}
			catch(Exception ex)
			{
				
			}
		}

		private void ClearItemNameAndValue()
		{
			txtItem.Text = string.Empty;
			txtItemValue.Text = string.Empty;
		}

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            LaunchConfiguration.UpdateItem(new LaunchItem() { Name = txtItem.Text, Value = txtItemValue.Text }, currentGroup);
            RefreshItem();
        }
	}
}
