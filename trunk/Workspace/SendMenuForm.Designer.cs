namespace Workspace
{
	partial class SendMenuForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendMenuForm));
			this.TopPanel = new System.Windows.Forms.Panel();
			this.MenuGridView = new System.Windows.Forms.DataGridView();
			this.RefreshButton = new System.Windows.Forms.Button();
			this.MembersLabel = new System.Windows.Forms.Label();
			this.TopRightPanelSplitter = new System.Windows.Forms.Splitter();
			this.TopSplitter = new System.Windows.Forms.Splitter();
			this.BottomPanel = new System.Windows.Forms.Panel();
			this.ActionPanel = new System.Windows.Forms.Panel();
			this.SendButton = new System.Windows.Forms.Button();
			this.MessageTextBox = new System.Windows.Forms.RichTextBox();
			this.TopPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MenuGridView)).BeginInit();
			this.BottomPanel.SuspendLayout();
			this.ActionPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// TopPanel
			// 
			this.TopPanel.Controls.Add(this.MenuGridView);
			this.TopPanel.Controls.Add(this.RefreshButton);
			this.TopPanel.Controls.Add(this.MembersLabel);
			this.TopPanel.Controls.Add(this.TopRightPanelSplitter);
			this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.TopPanel.Location = new System.Drawing.Point(0, 0);
			this.TopPanel.Name = "TopPanel";
			this.TopPanel.Size = new System.Drawing.Size(484, 150);
			this.TopPanel.TabIndex = 0;
			// 
			// MenuGridView
			// 
			this.MenuGridView.AllowUserToAddRows = false;
			this.MenuGridView.AllowUserToDeleteRows = false;
			this.MenuGridView.AllowUserToOrderColumns = true;
			this.MenuGridView.AllowUserToResizeRows = false;
			this.MenuGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.MenuGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.MenuGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.MenuGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.MenuGridView.Location = new System.Drawing.Point(3, 3);
			this.MenuGridView.Name = "MenuGridView";
			this.MenuGridView.ReadOnly = true;
			this.MenuGridView.RowHeadersVisible = false;
			this.MenuGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.MenuGridView.Size = new System.Drawing.Size(400, 140);
			this.MenuGridView.TabIndex = 0;
			// 
			// RefreshButton
			// 
			this.RefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.RefreshButton.Location = new System.Drawing.Point(411, 47);
			this.RefreshButton.Name = "RefreshButton";
			this.RefreshButton.Size = new System.Drawing.Size(65, 25);
			this.RefreshButton.TabIndex = 2;
			this.RefreshButton.Text = "Refresh";
			this.RefreshButton.UseVisualStyleBackColor = true;
			this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
			// 
			// MembersLabel
			// 
			this.MembersLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.MembersLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.MembersLabel.Location = new System.Drawing.Point(411, 12);
			this.MembersLabel.Margin = new System.Windows.Forms.Padding(3);
			this.MembersLabel.Name = "MembersLabel";
			this.MembersLabel.Size = new System.Drawing.Size(65, 30);
			this.MembersLabel.TabIndex = 1;
			this.MembersLabel.Text = "Members\r\n0";
			this.MembersLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// TopRightPanelSplitter
			// 
			this.TopRightPanelSplitter.Dock = System.Windows.Forms.DockStyle.Right;
			this.TopRightPanelSplitter.Enabled = false;
			this.TopRightPanelSplitter.Location = new System.Drawing.Point(481, 0);
			this.TopRightPanelSplitter.Name = "TopRightPanelSplitter";
			this.TopRightPanelSplitter.Size = new System.Drawing.Size(3, 150);
			this.TopRightPanelSplitter.TabIndex = 4;
			this.TopRightPanelSplitter.TabStop = false;
			// 
			// TopSplitter
			// 
			this.TopSplitter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TopSplitter.Dock = System.Windows.Forms.DockStyle.Top;
			this.TopSplitter.Location = new System.Drawing.Point(0, 150);
			this.TopSplitter.Name = "TopSplitter";
			this.TopSplitter.Size = new System.Drawing.Size(484, 3);
			this.TopSplitter.TabIndex = 1;
			this.TopSplitter.TabStop = false;
			// 
			// BottomPanel
			// 
			this.BottomPanel.Controls.Add(this.ActionPanel);
			this.BottomPanel.Controls.Add(this.MessageTextBox);
			this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BottomPanel.Location = new System.Drawing.Point(0, 153);
			this.BottomPanel.Name = "BottomPanel";
			this.BottomPanel.Padding = new System.Windows.Forms.Padding(3);
			this.BottomPanel.Size = new System.Drawing.Size(484, 159);
			this.BottomPanel.TabIndex = 2;
			this.BottomPanel.Resize += new System.EventHandler(this.BottomPanel_Resize);
			// 
			// ActionPanel
			// 
			this.ActionPanel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.ActionPanel.Controls.Add(this.SendButton);
			this.ActionPanel.Location = new System.Drawing.Point(206, 122);
			this.ActionPanel.Name = "ActionPanel";
			this.ActionPanel.Size = new System.Drawing.Size(71, 31);
			this.ActionPanel.TabIndex = 3;
			// 
			// SendButton
			// 
			this.SendButton.Location = new System.Drawing.Point(3, 3);
			this.SendButton.Name = "SendButton";
			this.SendButton.Size = new System.Drawing.Size(65, 25);
			this.SendButton.TabIndex = 2;
			this.SendButton.Text = "Send";
			this.SendButton.UseVisualStyleBackColor = true;
			this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
			// 
			// MessageTextBox
			// 
			this.MessageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.MessageTextBox.Location = new System.Drawing.Point(6, 6);
			this.MessageTextBox.Name = "MessageTextBox";
			this.MessageTextBox.Size = new System.Drawing.Size(472, 116);
			this.MessageTextBox.TabIndex = 0;
			this.MessageTextBox.Text = "";
			// 
			// SendMenuForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 312);
			this.Controls.Add(this.BottomPanel);
			this.Controls.Add(this.TopSplitter);
			this.Controls.Add(this.TopPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SendMenuForm";
			this.Text = "Send Menu";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SendMenuForm_FormClosing);
			this.TopPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.MenuGridView)).EndInit();
			this.BottomPanel.ResumeLayout(false);
			this.ActionPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel TopPanel;
		private System.Windows.Forms.Label MembersLabel;
		private System.Windows.Forms.Splitter TopRightPanelSplitter;
		private System.Windows.Forms.Button RefreshButton;
		private System.Windows.Forms.Splitter TopSplitter;
		private System.Windows.Forms.Panel BottomPanel;
		private System.Windows.Forms.DataGridView MenuGridView;
		private System.Windows.Forms.RichTextBox MessageTextBox;
		private System.Windows.Forms.Button SendButton;
		private System.Windows.Forms.Panel ActionPanel;
	}
}

