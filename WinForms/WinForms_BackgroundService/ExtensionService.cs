using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Client.Model;

using Raid.Toolkit.Extensibility;
using Raid.Toolkit.Extensibility.Services;

using SharedModel.Battle.Commands;

namespace WinForms_BackgroundService;

internal class ExtensionService : IBackgroundService, IDisposable
{
	private static readonly TimeSpan kPollInterval = new(0, 0, 1);
	public TimeSpan PollInterval => kPollInterval;
	private readonly ExtensionForm m_extensionForm;
	private readonly IAppUI m_appUI;

	public ExtensionService(ExtensionForm form, IAppUI appUI)
	{
		m_extensionForm = form;
		m_appUI = appUI;
	}

	public Task Tick(IGameInstance instance)
	{
		try
		{
			AppModel appModel = AppModel._instance.GetValue(instance.Runtime);
			FinishBattleResponseDto finishBattleResponse = appModel._userWrapper.Battle.BattleData.LastResponse;
			m_appUI.Dispatch(() =>
			{
				m_extensionForm.UpdateLastBattle(finishBattleResponse);
			});
		}
		catch (Exception ex)
		{
			m_appUI.Dispatch(() => m_extensionForm.ShowError(ex));
		}
		return Task.CompletedTask;
	}

	public void Dispose()
	{
		// do any cleanup here
	}
}
