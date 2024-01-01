using Raid.Toolkit.Extensibility;

using System;
using System.Windows.Forms;

namespace WinForms_BackgroundService;
public class Extension : ExtensionPackage
{
	ExtensionForm form;
	ExtensionService service;
	public override void OnActivate(IExtensionHost host)
	{
		form = host.CreateInstance<ExtensionForm>(host);
		service = host.CreateInstance<ExtensionService>(form);
		host.RegisterBackgroundService(service);
		MenuEntry menuEntry = new() { DisplayName = "WinForms_BackgroundService Sample" };
		menuEntry.Activate += MenuEntry_Activate;
		host.RegisterMenuEntry(menuEntry);
	}

	private void MenuEntry_Activate(object sender, EventArgs e)
	{
		form.Show();
	}
}
