# Plugins Folder

* [**FlClicker_Deployment Guide**](../../../../DEV_MAN/07%20DEPLOY/_FlClicker_Deployment%20Guide.md): find here more context about the why and the maintenance of this file. 

* [**plugin.json**](../src/Flow.Launcher.Plugin.FlClicker/plugin.json.md): make sure the values of this file match the values of your local plugin.json file you will need for local adhoc testing. 

--- 

This (optional) folder was created to draft the mandatory "Manifestation" file(s), that the FlowLauncherPlugin Site requires to announce and expose your Plugin to the Public and to provide all required information the FlowLauncher Plugin Manager needs to load and configure your newest Version of your FlClicker-Plugin. 

This Manifestation, to get "life" needs to be added to  the FlowLaunchers collection of PluginsManifest on  https://github.com/Flow-Launcher/Flow.Launcher.PluginsManifest/tree/main/plugins. 

For this you have to 
1. fork the latest version of this https://github.com/Flow-Launcher/Flow.Launcher.PluginsManifest/ Repo, 
2. add YOUR PluginsManifest-file into its /plugins folder
3. Submit a pull request that targets the default branch.

The plugin will be available in Flow after the pull request is approved by the Flow Launcher Team after a few days. 

The current Manifest's name is: 

[**FlClicker-badecafe-8037-1965-2026-a04b12c09d10.json**](FlClicker-badecafe-8037-1965-2026-a04b12c09d10.json.md)

and its contents (on April 15th, 2026) lookes like this: 

```json
{
  "ID": "FlClicker-badecafe-8037-1965-2026-a04b12c09d10",
  "Name": "FlClicker",
  "Description": "provides efficient TaskList Management for ClickUp.com",
  "Author": "realB12",
  "Version": "0.0.6",
  "Language": "csharp",
  "Website": "https://github.com/realB12/Flow.Launcher.Plugin.FlClicker",
  "UrlDownload": "",
  "UrlSourceCode": "https://github.com/realB12/Flow.Launcher.Plugin.FlClicker/tree/main/src/Flow.Launcher.Plugin.FlClicker", 
  "Tested": false,
  "IcoPath": "https://cdn.jsdelivr.net/gh/realB12/Flow.Launcher.Plugin.FlClicker@main/src/Flow.Launcher.Plugin.FlClicker/Images/ClickUpIcon_256x256.png",
  "LatestReleaseDate": "2026-04-15T11:56:32Z",
  "DateAdded": "2026-05-01T11:00:00Z"
}
```

