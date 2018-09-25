namespace iTools
{
	partial class ItemsEditUserControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtItemValue = new System.Windows.Forms.TextBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.ltbItems = new System.Windows.Forms.ListBox();
			this.cbxGroups = new System.Windows.Forms.ComboBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.btnAddGroup = new System.Windows.Forms.Button();
			this.txtGroupIndex = new System.Windows.Forms.TextBox();
			this.btnDeleteGroup = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtGroupName = new System.Windows.Forms.TextBox();
			this.txtGroupTitle = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.btnDown = new System.Windows.Forms.Button();
			this.btnUp = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.txtItem = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.panel2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtItemValue
			// 
			this.txtItemValue.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtItemValue.Location = new System.Drawing.Point(3, 28);
			this.txtItemValue.Name = "txtItemValue";
			this.txtItemValue.Size = new System.Drawing.Size(357, 20);
			this.txtItemValue.TabIndex = 1;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
			this.splitContainer1.Size = new System.Drawing.Size(631, 428);
			this.splitContainer1.SplitterDistance = 264;
			this.splitContainer1.TabIndex = 2;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.ltbItems, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.cbxGroups, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 3;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(264, 428);
			this.tableLayoutPanel2.TabIndex = 1;
			// 
			// ltbItems
			// 
			this.ltbItems.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ltbItems.FormattingEnabled = true;
			this.ltbItems.Location = new System.Drawing.Point(3, 147);
			this.ltbItems.Name = "ltbItems";
			this.ltbItems.ScrollAlwaysVisible = true;
			this.ltbItems.Size = new System.Drawing.Size(258, 317);
			this.ltbItems.TabIndex = 0;
			this.ltbItems.SelectedIndexChanged += new System.EventHandler(this.LtbItemsSelectedIndexChanged);
			// 
			// cbxGroups
			// 
			this.cbxGroups.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbxGroups.FormattingEnabled = true;
			this.cbxGroups.Location = new System.Drawing.Point(3, 123);
			this.cbxGroups.Name = "cbxGroups";
			this.cbxGroups.Size = new System.Drawing.Size(258, 21);
			this.cbxGroups.TabIndex = 3;
			this.cbxGroups.SelectedIndexChanged += new System.EventHandler(this.CbxGroupsSelectedIndexChanged);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.tableLayoutPanel3);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(3, 3);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(258, 114);
			this.panel2.TabIndex = 4;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.43411F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.56589F));
			this.tableLayoutPanel3.Controls.Add(this.btnAddGroup, 1, 3);
			this.tableLayoutPanel3.Controls.Add(this.txtGroupIndex, 1, 2);
			this.tableLayoutPanel3.Controls.Add(this.btnDeleteGroup, 0, 3);
			this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.label3, 0, 2);
			this.tableLayoutPanel3.Controls.Add(this.txtGroupName, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.txtGroupTitle, 1, 1);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 4;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(258, 114);
			this.tableLayoutPanel3.TabIndex = 3;
			// 
			// btnAddGroup
			// 
			this.btnAddGroup.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnAddGroup.Location = new System.Drawing.Point(97, 87);
			this.btnAddGroup.Name = "btnAddGroup";
			this.btnAddGroup.Size = new System.Drawing.Size(158, 24);
			this.btnAddGroup.TabIndex = 1;
			this.btnAddGroup.Text = "Add Group";
			this.btnAddGroup.UseVisualStyleBackColor = true;
			this.btnAddGroup.Click += new System.EventHandler(this.BtnAddGroupClick);
			// 
			// txtGroupIndex
			// 
			this.txtGroupIndex.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtGroupIndex.Location = new System.Drawing.Point(97, 59);
			this.txtGroupIndex.Name = "txtGroupIndex";
			this.txtGroupIndex.Size = new System.Drawing.Size(158, 20);
			this.txtGroupIndex.TabIndex = 0;
			// 
			// btnDeleteGroup
			// 
			this.btnDeleteGroup.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnDeleteGroup.Location = new System.Drawing.Point(3, 87);
			this.btnDeleteGroup.Name = "btnDeleteGroup";
			this.btnDeleteGroup.Size = new System.Drawing.Size(88, 24);
			this.btnDeleteGroup.TabIndex = 2;
			this.btnDeleteGroup.Text = "Delete Group";
			this.btnDeleteGroup.UseVisualStyleBackColor = true;
			this.btnDeleteGroup.Click += new System.EventHandler(this.BtnDeleteGroupClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 28);
			this.label1.TabIndex = 3;
			this.label1.Text = "Name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(3, 28);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 28);
			this.label2.TabIndex = 4;
			this.label2.Text = "Title";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(3, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 28);
			this.label3.TabIndex = 5;
			this.label3.Text = "Index";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtGroupName
			// 
			this.txtGroupName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtGroupName.Location = new System.Drawing.Point(97, 3);
			this.txtGroupName.Name = "txtGroupName";
			this.txtGroupName.Size = new System.Drawing.Size(158, 20);
			this.txtGroupName.TabIndex = 6;
			// 
			// txtGroupTitle
			// 
			this.txtGroupTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtGroupTitle.Location = new System.Drawing.Point(97, 31);
			this.txtGroupTitle.Name = "txtGroupTitle";
			this.txtGroupTitle.Size = new System.Drawing.Size(158, 20);
			this.txtGroupTitle.TabIndex = 7;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.txtItemValue, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.txtItem, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 172F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(363, 428);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnUpdate);
			this.panel1.Controls.Add(this.btnDown);
			this.panel1.Controls.Add(this.btnUp);
			this.panel1.Controls.Add(this.btnSave);
			this.panel1.Controls.Add(this.btnDelete);
			this.panel1.Location = new System.Drawing.Point(3, 53);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(185, 372);
			this.panel1.TabIndex = 0;
			// 
			// btnUpdate
			// 
			this.btnUpdate.Location = new System.Drawing.Point(21, 69);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(75, 23);
			this.btnUpdate.TabIndex = 4;
			this.btnUpdate.Text = "Update";
			this.btnUpdate.UseVisualStyleBackColor = true;
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			// 
			// btnDown
			// 
			this.btnDown.Location = new System.Drawing.Point(21, 229);
			this.btnDown.Name = "btnDown";
			this.btnDown.Size = new System.Drawing.Size(75, 23);
			this.btnDown.TabIndex = 3;
			this.btnDown.Text = "Down";
			this.btnDown.UseVisualStyleBackColor = true;
			this.btnDown.Click += new System.EventHandler(this.BtnDownClick);
			// 
			// btnUp
			// 
			this.btnUp.Location = new System.Drawing.Point(21, 180);
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(75, 23);
			this.btnUp.TabIndex = 2;
			this.btnUp.Text = "Up";
			this.btnUp.UseVisualStyleBackColor = true;
			this.btnUp.Click += new System.EventHandler(this.BtnUpClick);
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(21, 37);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 1;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.BtnSaveClick);
			// 
			// btnDelete
			// 
			this.btnDelete.Location = new System.Drawing.Point(21, 103);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(75, 23);
			this.btnDelete.TabIndex = 0;
			this.btnDelete.Text = "Delete";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.BtnDeleteClick);
			// 
			// txtItem
			// 
			this.txtItem.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtItem.Location = new System.Drawing.Point(3, 3);
			this.txtItem.Name = "txtItem";
			this.txtItem.Size = new System.Drawing.Size(357, 20);
			this.txtItem.TabIndex = 2;
			// 
			// ItemsEditUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "ItemsEditUserControl";
			this.Size = new System.Drawing.Size(631, 428);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox txtItemValue;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TextBox txtItem;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.ListBox ltbItems;
		private System.Windows.Forms.ComboBox cbxGroups;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button btnDeleteGroup;
		private System.Windows.Forms.Button btnAddGroup;
		private System.Windows.Forms.TextBox txtGroupIndex;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtGroupName;
		private System.Windows.Forms.TextBox txtGroupTitle;
        private System.Windows.Forms.Button btnUpdate;
	}
}
