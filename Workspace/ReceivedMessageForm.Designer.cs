namespace Workspace
{
	partial class ReceivedMessageForm
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
			this.TopPanel = new System.Windows.Forms.Panel();
			this.MessegeInfoGroupBox = new System.Windows.Forms.GroupBox();
			this.MessegeInfoLabel = new System.Windows.Forms.Label();
			this.MessagePanel = new System.Windows.Forms.Panel();
			this.MessegeTextBox = new System.Windows.Forms.RichTextBox();
			this.BottomPanel = new System.Windows.Forms.Panel();
			this.ActionPanel = new System.Windows.Forms.Panel();
			this.QuoteCheckBox = new System.Windows.Forms.CheckBox();
			this.ReplyButton = new System.Windows.Forms.Button();
			this.CloseButton = new System.Windows.Forms.Button();
			this.TopPanel.SuspendLayout();
			this.MessegeInfoGroupBox.SuspendLayout();
			this.MessagePanel.SuspendLayout();
			this.BottomPanel.SuspendLayout();
			this.ActionPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// TopPanel
			// 
			this.TopPanel.Controls.Add(this.MessegeInfoGroupBox);
			this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.TopPanel.Location = new System.Drawing.Point(0, 0);
			this.TopPanel.Name = "TopPanel";
			this.TopPanel.Size = new System.Drawing.Size(284, 77);
			this.TopPanel.TabIndex = 0;
			// 
			// MessegeInfoGroupBox
			// 
			this.MessegeInfoGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.MessegeInfoGroupBox.Controls.Add(this.MessegeInfoLabel);
			this.MessegeInfoGroupBox.Location = new System.Drawing.Point(12, 12);
			this.MessegeInfoGroupBox.Name = "MessegeInfoGroupBox";
			this.MessegeInfoGroupBox.Size = new System.Drawing.Size(260, 59);
			this.MessegeInfoGroupBox.TabIndex = 1;
			this.MessegeInfoGroupBox.TabStop = false;
			this.MessegeInfoGroupBox.Text = "Messege Info";
			// 
			// MessegeInfoLabel
			// 
			this.MessegeInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MessegeInfoLabel.Location = new System.Drawing.Point(3, 16);
			this.MessegeInfoLabel.Name = "MessegeInfoLabel";
			this.MessegeInfoLabel.Size = new System.Drawing.Size(254, 40);
			this.MessegeInfoLabel.TabIndex = 0;
			this.MessegeInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// MessagePanel
			// 
			this.MessagePanel.Controls.Add(this.MessegeTextBox);
			this.MessagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MessagePanel.Location = new System.Drawing.Point(0, 77);
			this.MessagePanel.Name = "MessagePanel";
			this.MessagePanel.Size = new System.Drawing.Size(284, 185);
			this.MessagePanel.TabIndex = 1;
			// 
			// MessegeTextBox
			// 
			this.MessegeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.MessegeTextBox.Location = new System.Drawing.Point(12, 6);
			this.MessegeTextBox.Name = "MessegeTextBox";
			this.MessegeTextBox.ReadOnly = true;
			this.MessegeTextBox.Size = new System.Drawing.Size(260, 133);
			this.MessegeTextBox.TabIndex = 0;
			this.MessegeTextBox.Text = "";
			// 
			// BottomPanel
			// 
			this.BottomPanel.Controls.Add(this.ActionPanel);
			this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.BottomPanel.Location = new System.Drawing.Point(0, 222);
			this.BottomPanel.Name = "BottomPanel";
			this.BottomPanel.Size = new System.Drawing.Size(284, 40);
			this.BottomPanel.TabIndex = 2;
			this.BottomPanel.Resize += new System.EventHandler(this.BottomPanel_Resize);
			// 
			// ActionPanel
			// 
			this.ActionPanel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.ActionPanel.Controls.Add(this.QuoteCheckBox);
			this.ActionPanel.Controls.Add(this.ReplyButton);
			this.ActionPanel.Controls.Add(this.CloseButton);
			this.ActionPanel.Location = new System.Drawing.Point(42, 6);
			this.ActionPanel.Name = "ActionPanel";
			this.ActionPanel.Size = new System.Drawing.Size(203, 31);
			this.ActionPanel.TabIndex = 4;
			// 
			// QuoteCheckBox
			// 
			this.QuoteCheckBox.AutoSize = true;
			this.QuoteCheckBox.Location = new System.Drawing.Point(145, 8);
			this.QuoteCheckBox.Name = "QuoteCheckBox";
			this.QuoteCheckBox.Size = new System.Drawing.Size(55, 17);
			this.QuoteCheckBox.TabIndex = 2;
			this.QuoteCheckBox.Text = "Quote";
			this.QuoteCheckBox.UseVisualStyleBackColor = true;
			// 
			// ReplyButton
			// 
			this.ReplyButton.Location = new System.Drawing.Point(74, 3);
			this.ReplyButton.Name = "ReplyButton";
			this.ReplyButton.Size = new System.Drawing.Size(65, 25);
			this.ReplyButton.TabIndex = 1;
			this.ReplyButton.Text = "Reply";
			this.ReplyButton.UseVisualStyleBackColor = true;
			this.ReplyButton.Click += new System.EventHandler(this.ReplyButton_Click);
			// 
			// CloseButton
			// 
			this.CloseButton.Location = new System.Drawing.Point(3, 3);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new System.Drawing.Size(65, 25);
			this.CloseButton.TabIndex = 0;
			this.CloseButton.Text = "Close";
			this.CloseButton.UseVisualStyleBackColor = true;
			this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
			// 
			// ReceivedMessageForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.BottomPanel);
			this.Controls.Add(this.MessagePanel);
			this.Controls.Add(this.TopPanel);
			this.Name = "ReceivedMessageForm";
			this.Text = "Received Message";
			this.TopPanel.ResumeLayout(false);
			this.MessegeInfoGroupBox.ResumeLayout(false);
			this.MessagePanel.ResumeLayout(false);
			this.BottomPanel.ResumeLayout(false);
			this.ActionPanel.ResumeLayout(false);
			this.ActionPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel TopPanel;
		private System.Windows.Forms.Panel MessagePanel;
		private System.Windows.Forms.Panel BottomPanel;
		private System.Windows.Forms.Label MessegeInfoLabel;
		private System.Windows.Forms.Panel ActionPanel;
		private System.Windows.Forms.Button CloseButton;
		private System.Windows.Forms.RichTextBox MessegeTextBox;
		private System.Windows.Forms.GroupBox MessegeInfoGroupBox;
		private System.Windows.Forms.Button ReplyButton;
		private System.Windows.Forms.CheckBox QuoteCheckBox;
	}
}