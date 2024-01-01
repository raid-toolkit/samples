using Raid.Toolkit.Extensibility;

using SharedModel.Battle.Commands;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_BackgroundService;
public partial class ExtensionForm : Form
{
	private readonly IExtensionHost m_host;
	public ExtensionForm(IExtensionHost host)
	{
		InitializeComponent();
		m_host = host;
	}

	protected override void OnClosed(EventArgs e)
	{
		// don't tear down so we can be re-shown
		Hide();
	}

	internal void ShowError(Exception ex)
	{
		errorText.Text = ex.ToString();
	}

	internal void UpdateLastBattle(FinishBattleResponseDto finishBattleResponse)
	{
		errorText.Text = string.Empty;
		lastBattlePropGrid.SelectedObject = finishBattleResponse;
	}

	private void backgroundWorkerButton_Click(object sender, EventArgs e)
	{
		var backgroundWorker = RTKBackgroundWorker.Create(m_host);
		backgroundWorker.WorkerSupportsCancellation = true;
		backgroundWorker.DoWork += (sender, e) => Thread.Sleep(2000);
		backgroundWorker.RunWorkerCompleted += (sender, e) => MessageBox.Show("Hello there!");
		backgroundWorker.RunWorkerAsync();
	}
}
