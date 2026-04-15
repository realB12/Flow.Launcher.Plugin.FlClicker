# debug.ps1

* **[Deployment Guide](../../../../DEV_MAN/07%20DEPLOY/_FlClicker_Deployment%20Guide.md)**: gives you the full context why and for what this script is used

* **[public github version.ps1](public-github-version.ps1.md)**: creates GitHub Releases from this output

* **[launcher.json](../.vscode/launcher.json.md)**: VSC configuration file that links PF5-Run-options to this script. 

---

## What it does
This PowerShell (*.ps1) encoded Script **builds/compiles new version binaries** into the temporary **artifacts/bin** and /obj folders and copies the related files from the /Images and /Languages folders, that together are copied  to the locla FlowLauncher.exe location to automatically restart the Flowlauncher.exe for immediately testing the such new deployed  FlClicker-Plugin version. 

To make this happening, the scripts stops an eventually already running FlowLauncher instance, copies the such new created plugin files from the VSC developer path to the actual locyll installe FlowLauncher.exe's plugin folder and finally restarts the FlowLauncher-Windows again after a few seconds, so that you can test the new plugin version's function life from the commandline. 

## How to use it

### Actualize Main.cs Version string

Before running the script, change the version-string in the Main.cs to the current data/time such as "06.22.16.57" Version numbers in THIS format denot non official/non public debug/testing versions whereas Realease-Versions will follow the semVer format such as "v0.1.27"

You can display the such actualized version string by typing "click version" into the FlowLauncher's commandline. 

## How to launch this script
This script can be launched **with PF5 or from the Terminal**

### PF5 launch
1. Open the Run-View and select the "FC: Debug"-option
![PF5_local version](./zPICs/PF5_local-version.ps1). 

This "FC: Debug"-option is associated with the following VSC configuration entry in the **.vscode/launch.json** file: 

```json
{
    "name": "FC: Debug",
    "type": "PowerShell",
    "request": "launch",
    "script": "C:/me/REPO/PRJ/FLClicker/40 DEV/VSC/Flow.Launcher.Plugin.FlClicker/scripts/debug.ps1"
}
```

Which finally makes the link between the PF5 entry and the PowerShell-Script. 

### Terminal launch
1. Open the VSC terminal and run the script as follows:

> Powershell.exe -executionpolicy remotesigned -File "C:/me/REPO/PRJ/FLClicker/40 DEV/VSC/Flow.Launcher.Plugin.FlClicker/scripts/debug.ps1"

Mind the enclosing *" "* for the file-path, so that the "40 DEV" Path-partial is properly recognized!


## The Code
last version copied in April 15th, 2026

```powershell
# v-- creates a debuggable dll for the win-x64 platform
#     The command must refer to the *.csproj file WITHOUT! addin the *.csproj extention. 
dotnet publish src/Flow.Launcher.Plugin.FlClicker -c Debug -r win-x64 --no-self-contained

dotnet publish "src/Flow.Launcher.Plugin.FlClicker" `
-c Debug -r win-x64 --no-self-contained `

$AppDataFolder = [Environment]::GetFolderPath("ApplicationData")
$flowLauncherExe = "$env:LOCALAPPDATA\FlowLauncher\Flow.Launcher.exe"

if (Test-Path $flowLauncherExe) {
    Stop-Process -Name "Flow.Launcher" -Force -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 2

    if (Test-Path "$AppDataFolder\FlowLauncher\Plugins\FlClicker") {
        Remove-Item -Recurse -Force "$AppDataFolder\FlowLauncher\Plugins\FlClicker"
    }

    Copy-Item "artifacts\bin\Debug\win-x64" "$AppDataFolder\FlowLauncher\Plugins\publish" -Recurse -Force
    Rename-Item -Path "$AppDataFolder\FlowLauncher\Plugins\publish" -NewName "FlClicker"

    Start-Sleep -Seconds 2
    Start-Process $flowLauncherExe
} else {
    Write-Host "Flow.Launcher.exe not found. Please install Flow Launcher first"
}
```

## Troubleshooting a failed installation 
### New Version with identical ID in plugin.json!
<span style="color:red; font-weight:bold">Attention</span>: You might want to **change the ID in the plugin.json** file as the Plugin Manger and "pm install" sometimes for some not yet understood reasons, will refuse to install different package-versions with both having the same ID!!

So in case the FlowLauncher.exe "suddenly" refuses to load your Plugin without any error message or indications, chances are high, that you are loading an updated version with a former/unchanged ID from a previous version!