using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SocketCommunication.Interfaces;
using SocketCommunication.Classes;

namespace Workspace
{
	public partial class SendMenuForm : Form, IObserver
	{
		private delegate void UpdateMenuDelegate(UserInfo userInfo);

		#region Constants
		public const string IPAddress = "IPAddress";
		public const string UserName = "UserName";
		public const string Group = "Group";
		public const string MachineName = "MachineName";
		public const string LogOnName = "LogOnName";
		#endregion

		#region Data Members

		private DataTable SendMenuSourceDataTable = new DataTable();
		private UpdateMenuDelegate UpdateMenuDelegateInstance;

		#endregion

		#region public SendMenuForm()
		public SendMenuForm()
		{
			InitializeComponent();
			InitializeSendMenuSourceDataTable();
			InitializeMenuListView();
			SocketCommService.SubscribeForUserInfos(this);
		}
		#endregion

		#region private void InitializeSendMenuSourceDataTable()
		private void InitializeSendMenuSourceDataTable()
		{
			if (SendMenuSourceDataTable == null)
			{
				SendMenuSourceDataTable = new DataTable();
			}
			SendMenuSourceDataTable.Columns.Add(IPAddress, typeof(string));
			SendMenuSourceDataTable.Columns.Add(UserName, typeof(string));
			SendMenuSourceDataTable.Columns.Add(Group, typeof(string));
			SendMenuSourceDataTable.Columns.Add(MachineName, typeof(string));
			SendMenuSourceDataTable.Columns.Add(LogOnName, typeof(string));
		}
		#endregion

		#region private void InitializeMenuListView()
		private void InitializeMenuListView()
		{
			if (SendMenuSourceDataTable == null)
			{
				return;
			}
			foreach (DataColumn column in SendMenuSourceDataTable.Columns)
			{
				if (column == null)
				{
					continue;
				}

			}
			MenuGridView.DataSource = SendMenuSourceDataTable;
		}
		#endregion

		#region Event Handlers

		#region private void SendButton_Click(object sender, EventArgs e)
		private void SendButton_Click(object sender, EventArgs e)
		{
			foreach (DataGridViewRow row in MenuGridView.SelectedRows)
			{
				SocketCommService.SendTextMessage(row.Cells[IPAddress].Value.ToString(), MessageTextBox.Text);
			}
			this.Close();
		}
		#endregion

		#region private void RefreshButton_Click(object sender, EventArgs e)
		private void RefreshButton_Click(object sender, EventArgs e)
		{
			SocketCommService.RefreshSignonService();
		}
		#endregion

		#region private void BottomPanel_Resize(object sender, EventArgs e)
		private void BottomPanel_Resize(object sender, EventArgs e)
		{
			ActionPanel.Location = new Point((BottomPanel.Size.Width / 2) - (ActionPanel.Width / 2), ActionPanel.Location.Y);
		}
		#endregion

		#region private void SendMenuForm_FormClosing(object sender, FormClosingEventArgs e)
		private void SendMenuForm_FormClosing(object sender, FormClosingEventArgs e)
		{

		}
		#endregion

		#endregion

		#region private void ClearMenu()
		private void ClearMenu()
		{
			SendMenuSourceDataTable.Clear();
			UpdateMemberCount();
		}
		#endregion

		#region private void UpdateMenu(UserInfo userInfo)
		private void UpdateMenu(UserInfo userInfo)
		{
			DataRow dataRow = SendMenuSourceDataTable.NewRow();

			foreach (DataColumn column in SendMenuSourceDataTable.Columns)
			{
				if (column == null)
				{
					continue;
				}
				switch (column.ColumnName)
				{
					case IPAddress:
						{
							dataRow[IPAddress] = userInfo.IPAddress;
						}
						break;
					case UserName:
						{
							dataRow[UserName] = userInfo.UserName;
						}
						break;
					case Group:
						{
							dataRow[Group] = userInfo.Group;
						}
						break;
					case MachineName:
						{
							dataRow[MachineName] = userInfo.MachineName;
						}
						break;
					case LogOnName:
						{
							dataRow[LogOnName] = userInfo.LogOnName;
						}
						break;
				}
			}

			SendMenuSourceDataTable.LoadDataRow(dataRow.ItemArray, true);

			UpdateMemberCount();
		}
		#endregion

		#region private void UpdateMemberCount()
		private void UpdateMemberCount()
		{
			MembersLabel.Text = string.Format("Members\n{0}", SendMenuSourceDataTable.Rows.Count);
		}
		#endregion

		#region IObserver Members

		#region public void Notify(object data)
		public void Notify(object data)
		{
			if (data == null)
			{
				return;
			}
			if (data.Equals(SocketCommService.ObserverClearCacheMessageData))
			{
				ClearMenu();
				return;
			}
			if (!(data is UserInfo))
			{
				return;
			}
			UserInfo userInfo = data as UserInfo;
			if (userInfo == null)
			{
				return;
			}

			if (this.InvokeRequired == false)
			{
				UpdateMenu(userInfo);
			}
			else
			{
				if (UpdateMenuDelegateInstance == null)
				{
					UpdateMenuDelegateInstance = new UpdateMenuDelegate(UpdateMenu);
				}
				BeginInvoke(UpdateMenuDelegateInstance, userInfo);
			}
		}
		#endregion

		#endregion
	}
}
