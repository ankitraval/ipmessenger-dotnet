using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using SocketCommunication.Interfaces;
using SocketCommunication.Classes;
using System.Threading;

namespace Workspace
{
	class CMain : IObserver
	{
		private delegate void ProcessTextMessageDelegate(TextMessageData textMessageData);

		#region Data Members

		private static CMain SingletonInstance = null;
		private NotifyIcon IPMessengerNotifyIcon;
		private ProcessTextMessageDelegate ProcessTextMessageDelegateInstance;

		#endregion

		#region private CMain()
		private CMain()
		{

		}
		#endregion

		#region public static void Intialize()
		public static void Intialize()
		{
			if (SingletonInstance == null)
			{
				SingletonInstance = new CMain();
			}
			SingletonInstance.InitializeNotifyIcon();
			SocketCommService.SubscribeForTextMessages(SingletonInstance);
		}
		#endregion

		#region private void InitializeNotifyIcon()
		private void InitializeNotifyIcon()
		{
			if (File.Exists("IPMessengerIcon.ico") == false)
			{
				MessageBox.Show("IPMessengerIcon.ico not found");
				Application.Exit();
			}

			ContextMenuStrip contextMenu = new ContextMenuStrip();
			contextMenu.ShowImageMargin = false;
			contextMenu.ShowCheckMargin = true;

			ToolStripItem item = contextMenu.Items.Add("Exit");
			item.Name = "Exit";
			item.Click += new EventHandler(Exit_Click);

			IPMessengerNotifyIcon = new NotifyIcon();
			IPMessengerNotifyIcon.Icon = new System.Drawing.Icon("IPMessengerIcon.ico");
			IPMessengerNotifyIcon.Text = "IPMessenger";
			IPMessengerNotifyIcon.Visible = true;
			IPMessengerNotifyIcon.ContextMenuStrip = contextMenu;
			IPMessengerNotifyIcon.DoubleClick += new EventHandler(IPMessengerNotifyIcon_DoubleClick);
			IPMessengerNotifyIcon.BalloonTipClicked += new EventHandler(IPMessengerNotifyIcon_BalloonTipClicked);
		}
		#endregion

		#region private void Exit_Click(object sender, EventArgs e)
		private void Exit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		#endregion

		#region private void IPMessengerNotifyIcon_DoubleClick(object sender, EventArgs e)
		private void IPMessengerNotifyIcon_DoubleClick(object sender, EventArgs e)
		{
			SendMenuForm sendMenuForm = new SendMenuForm();
			sendMenuForm.Show();
		}
		#endregion

		#region void IPMessengerNotifyIcon_BalloonTipClicked(object sender, EventArgs e)
		void IPMessengerNotifyIcon_BalloonTipClicked(object sender, EventArgs e)
		{
			if (IPMessengerNotifyIcon.Tag == null || (IPMessengerNotifyIcon.Tag is TextMessageData) == false)
			{
				return;
			}
			TextMessageData textMessageData = IPMessengerNotifyIcon.Tag as TextMessageData;
			if (textMessageData == null)
			{
				return;
			}
			UserInfo userInfo = SocketCommService.GetUserInfo(textMessageData.IPAddress);
			if (userInfo == null)
			{
				return;
			}
			ContextMenuStrip contextMenuStrip = IPMessengerNotifyIcon.ContextMenuStrip;
			if (contextMenuStrip != null && contextMenuStrip.Items.ContainsKey("Received Messages"))
			{
				ToolStripMenuItem receivedMessagesToolStripMenuItem = contextMenuStrip.Items["Received Messages"] as ToolStripMenuItem;
				if (receivedMessagesToolStripMenuItem != null)
				{
					string key = string.Format("{0} at {1}", userInfo.UserName, textMessageData.ReceiptTime);
					int index = receivedMessagesToolStripMenuItem.DropDownItems.IndexOfKey(key);
					if (index >= 0)
					{
						receivedMessagesToolStripMenuItem.DropDownItems.RemoveAt(index);
						if (receivedMessagesToolStripMenuItem.DropDownItems.Count == 0)
						{
							contextMenuStrip.Items.Remove(receivedMessagesToolStripMenuItem);
						}
					}
				}
			}
			MessageBox.Show(textMessageData.Text, string.Format("Received From {0} at {1}", userInfo.UserName, textMessageData.ReceiptTime));
		}
		#endregion

		#region private void ProcessTextMessage(TextMessageData textMessageData)
		private void ProcessTextMessage(TextMessageData textMessageData)
		{
			if (textMessageData == null)
			{
				return;
			}
			UserInfo userInfo = SocketCommService.GetUserInfo(textMessageData.IPAddress);
			if (userInfo == null)
			{
				return;
			}
			ContextMenuStrip contextMenuStrip = IPMessengerNotifyIcon.ContextMenuStrip;
			if (contextMenuStrip == null)
			{
				MessageBox.Show(textMessageData.Text, string.Format("Received From {0} at {1}", userInfo.UserName, textMessageData.ReceiptTime));
				return;
			}
			ToolStripMenuItem receivedMessagesToolStripMenuItem;
			if (contextMenuStrip.Items.ContainsKey("Received Messages") == false)
			{
				receivedMessagesToolStripMenuItem = new ToolStripMenuItem("Received Messages");
				receivedMessagesToolStripMenuItem.Name = "Received Messages";
				int exitIndex = contextMenuStrip.Items.IndexOfKey("Exit");
				contextMenuStrip.Items.Insert(exitIndex, receivedMessagesToolStripMenuItem);
			}
			else
			{
				receivedMessagesToolStripMenuItem = contextMenuStrip.Items["Received Messages"] as ToolStripMenuItem;
			}
			string key = string.Format("{0} at {1}", userInfo.UserName, textMessageData.ReceiptTime);
			ToolStripItem toolStripItem = receivedMessagesToolStripMenuItem.DropDownItems.Add(key);
			toolStripItem.Name = key;
			toolStripItem.Tag = textMessageData;
			toolStripItem.Click += new EventHandler(ToolStripItem_Click);

			IPMessengerNotifyIcon.Tag = textMessageData;
			string balloonToolTipText = string.Format("Received message from {0}", userInfo.UserName);
			IPMessengerNotifyIcon.ShowBalloonTip(1000, "Received Message", balloonToolTipText, ToolTipIcon.Info);
		}
		#endregion

		#region void ToolStripItem_Click(object sender, EventArgs e)
		void ToolStripItem_Click(object sender, EventArgs e)
		{
			if (sender == null || !(sender is ToolStripItem))
			{
				return;
			}
			ToolStripItem item = sender as ToolStripItem;
			if (item == null)
			{
				return;
			}
			if (item.Tag == null || !(item.Tag is TextMessageData))
			{
				if (item.OwnerItem != null && item.OwnerItem is ToolStripMenuItem)
				{
					ToolStripMenuItem owner = item.OwnerItem as ToolStripMenuItem;
					owner.DropDownItems.Remove(item);
					if (owner.DropDownItems.Count == 0)
					{
						if (owner.Owner != null)
						{
							ToolStrip toolStrip = owner.Owner;
							toolStrip.Items.Remove(owner);
						}
					}
				}
				return;
			}

			TextMessageData textMessageData = item.Tag as TextMessageData;
			if (textMessageData == null)
			{
				return;
			}
			UserInfo userInfo = SocketCommService.GetUserInfo(textMessageData.IPAddress);
			if (userInfo == null)
			{
				return;
			}
			MessageBox.Show(textMessageData.Text, string.Format("Received From {0} at {1}", userInfo.UserName, textMessageData.ReceiptTime));
			if (item.OwnerItem != null && item.OwnerItem is ToolStripMenuItem)
			{
				ToolStripMenuItem owner = item.OwnerItem as ToolStripMenuItem;
				owner.DropDownItems.Remove(item);
				if (owner.DropDownItems.Count == 0)
				{
					if (owner.Owner != null)
					{
						ToolStrip toolStrip = owner.Owner;
						toolStrip.Items.Remove(owner);
					}
				}
			}
		}
		#endregion

		#region IObserver Members

		#region public void Notify(object data)
		public void Notify(object data)
		{
			if (data == null || !(data is TextMessageData))
			{
				return;
			}
			TextMessageData textMessageData = data as TextMessageData;
			if (textMessageData == null)
			{
				return;
			}
			if (IPMessengerNotifyIcon.ContextMenuStrip == null)
			{
				ProcessTextMessage(textMessageData);
				return;
			}
			else if (IPMessengerNotifyIcon.ContextMenuStrip.InvokeRequired)
			{
				if (ProcessTextMessageDelegateInstance == null)
				{
					ProcessTextMessageDelegateInstance = new ProcessTextMessageDelegate(ProcessTextMessage);
				}
				IPMessengerNotifyIcon.ContextMenuStrip.BeginInvoke(ProcessTextMessageDelegateInstance, textMessageData);
			}
			else
			{
				ProcessTextMessage(textMessageData);
			}
		}
		#endregion

		#endregion
	}
}
