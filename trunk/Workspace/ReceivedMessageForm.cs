using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SocketCommunication.Classes;

namespace Workspace
{
	public partial class ReceivedMessageForm : Form
	{
		#region Data Members

		private UserInfo ReceivedUserInfo = null;
		private TextMessageData ReceivedTextMessageData = null;

		#endregion

		#region Constructor
		public ReceivedMessageForm(TextMessageData textMessageData)
		{
			if (textMessageData == null)
			{
				throw new ArgumentNullException("textMessageData", "Something is wrong. TextMessageData should not be null.");
			}
			ReceivedTextMessageData = textMessageData;

			ReceivedUserInfo = SocketCommService.GetUserInfo(textMessageData.IPAddress);
			if (ReceivedUserInfo == null)
			{
				throw new Exception("Text message received from unknown user");
			}

			InitializeComponent();

			StringBuilder stringBuilder = new StringBuilder("Received message from ");
			stringBuilder.Append(ReceivedUserInfo.UserName);
			if (ReceivedUserInfo.LogOnName != ReceivedUserInfo.UserName)
			{
				stringBuilder.AppendFormat(" [{0}]", ReceivedUserInfo.LogOnName);
			}
			stringBuilder.AppendFormat(" ({0})", ReceivedUserInfo.MachineName);
			stringBuilder.AppendFormat("{0}at {1}", Environment.NewLine, ReceivedTextMessageData.ReceiptTime);

			MessegeInfoLabel.Text = stringBuilder.ToString();
			MessegeTextBox.Text = ReceivedTextMessageData.Text;
		}
		#endregion

		#region public ReceivedMessageForm(TextMessageData textMessageData, UserInfo userInfo)
		public ReceivedMessageForm(TextMessageData textMessageData, UserInfo userInfo)
		{
			if (textMessageData == null)
			{
				throw new ArgumentNullException("textMessageData", "Something is wrong. TextMessageData should not be null.");
			}
			ReceivedTextMessageData = textMessageData;
			if (userInfo == null)
			{
				throw new Exception("Text message received from unknown user");
			}
			ReceivedUserInfo = userInfo;

			InitializeComponent();

			StringBuilder stringBuilder = new StringBuilder("Received message from ");
			stringBuilder.Append(ReceivedUserInfo.UserName);
			if (userInfo.LogOnName != ReceivedUserInfo.UserName)
			{
				stringBuilder.AppendFormat(" [{0}]", ReceivedUserInfo.LogOnName);
			}
			stringBuilder.AppendFormat(" ({0})", ReceivedUserInfo.MachineName);
			stringBuilder.AppendFormat("{0}at {1}", Environment.NewLine, ReceivedTextMessageData.ReceiptTime);

			MessegeInfoLabel.Text = stringBuilder.ToString();
			MessegeTextBox.Text = ReceivedTextMessageData.Text;
		}
		#endregion

		#region Event Handlers

		#region private void BottomPanel_Resize(object sender, EventArgs e)
		private void BottomPanel_Resize(object sender, EventArgs e)
		{
			ActionPanel.Location = new Point((BottomPanel.Size.Width / 2) - (ActionPanel.Width / 2), ActionPanel.Location.Y);
		}
		#endregion

		#region private void CloseButton_Click(object sender, EventArgs e)
		private void CloseButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		#endregion

		#region private void ReplyButton_Click(object sender, EventArgs e)
		private void ReplyButton_Click(object sender, EventArgs e)
		{
			SendMenuForm sendMenuForm = new SendMenuForm();
			sendMenuForm.SelectMenuEntry(ReceivedUserInfo.IPAddress);
			if (QuoteCheckBox.Checked)
			{
				sendMenuForm.PopulateText(ReceivedTextMessageData.Text);
			}
			this.Close();
			sendMenuForm.Show();
		}
		#endregion

		#endregion
	}
}
