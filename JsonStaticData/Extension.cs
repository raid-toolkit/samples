using Client.Model;
using Raid.Toolkit.Extensibility;

using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RaidToolkitExtension1
{
	public class Extension : ExtensionPackage
	{
		private readonly IGameInstanceManager GameInstanceManager;
		private MenuEntry? MenuEntry;
		public Extension(IGameInstanceManager gameInstanceManager)
		{
			GameInstanceManager = gameInstanceManager;
		}

		public override void OnActivate(IExtensionHost host)
		{
			MenuEntry = new(){ DisplayName = "Extract static game data", IsEnabled = GameInstanceManager.Instances.Count > 0, IsVisible = true };
			MenuEntry.Activate += MenuEntry_Activate;
			Disposables.Add(host.RegisterMenuEntry(MenuEntry));
			GameInstanceManager.OnRemoved += GameInstanceManager_OnUpdate;
			GameInstanceManager.OnAdded += GameInstanceManager_OnUpdate;
		}

		public override void OnDeactivate(IExtensionHost host)
		{
			GameInstanceManager.OnRemoved -= GameInstanceManager_OnUpdate;
			GameInstanceManager.OnAdded -= GameInstanceManager_OnUpdate;
			base.OnDeactivate(host);
		}

		private void GameInstanceManager_OnUpdate(object? sender, IGameInstanceManager.GameInstancesUpdatedEventArgs e)
		{
			if (MenuEntry == null)
				return;

			MenuEntry.IsEnabled = GameInstanceManager.Instances.Count > 0;
		}

		private void MenuEntry_Activate(object? sender, EventArgs e)
		{
			IGameInstance? gameInstance = GameInstanceManager.Instances.FirstOrDefault();
			if (gameInstance == null)
				return;

			AppModel appModel = AppModel._instance.GetValue(gameInstance.Runtime);

			string staticData = Plarium.Common.Serialization.JsonMain.ToJsonStr(
				appModel.Source, 
				appModel.StaticDataManager.StaticData, 
				ignoreNames: true);

			using SaveFileDialog sfd = new()
			{
				DefaultExt = "json",
			};

			DialogResult result = sfd.ShowDialog();
			if (result != DialogResult.OK)
				return;

			File.WriteAllText(sfd.FileName, staticData);
		}
	}
}
