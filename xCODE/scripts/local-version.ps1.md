# The local-version.ps1 PowerShell-Script

* **[Deployment Guide](../../../../DEV_MAN/07%20DEPLOY/_FlClicker_Deployment%20Guide.md)**: gives you the full context why and for what this script is used

* **[public github version.ps1](public-github-version.ps1.md)**: creates GitHub Releases from this output

* **[launcher.json](../.vscode/launcher.json.md)**: VSC configuration file that links PF5-Run-options to this script. 

## What it does
This PowerShell-script compiles the current sourcecode into a  "FlClicker-v0.x.y.zip" named distribution package to be found in the /artifacts/release subfolder, and which will serve as the input for the subsequent "publish-github-verions.ps1" script, that will upload it to the
GitHub Repo and publish is as a new "v0.x.y"-named GitHub Release. 

Additionally, to support local testing, this script copies all required binaries and related resource files (from teh Languages and Images folders) into the locally installed FlowLauncher app folder, after which the FlowLauncher.exe is stopped and restarted, so that the new version can be readily tested from the FlowLauncher's command-line: such as "click version" to get the current, hardcoded version from the Main.cs GitHub Repo and marks it with a "v0.x.y" named Release-Tag. 

To build the final GitHub release run the [public github version.ps1](public-github-version.ps1.md) hereafter. 

## How to use it
Before running the script, make sure the correct semVer version number in the v0.x.y format is set in the code. 

For a list of already used version numbers check the FlClicker_Release_Notes.md Doc in the /docs folder. 

## How to launch this script
This script can be launched with PF5 or from within the Terminal

### PF5 launch
1. Open the Run-View and select the "FC: Local Version"-option
![PF5_local version](./zPICs/PF5_local-version.ps1). 

This "FC: Local Version is associated with the following VSC configuration entry in the **.vscode/launch.json** file: 

```json
{
    "version": "0.2.0",
    "configurations": [
        {
            "name": "FC: Local Version",
            "type": "PowerShell",
            "request": "launch",
            "script": "C:/me/REPO/PRJ/FLClicker/40 DEV/VSC/Flow.Launcher.Plugin.FlClicker/scripts/local-version.ps1"
        }
    ]
}
```

Which finally makes the link between the PF5 entry and the PowerShell-Script. 


### Terminal launch
1. Open the VSC terminal and run the script as follows:

> Powershell.exe -executionpolicy remotesigned -File "C:\me\REPO\PRJ\FLClicker\40 DEV\VSC\scripts\local-version.ps1"

Mind the enclosing " " for the file-path, so that the "40 DEV" Path-Partial is properly recognized!

## The Process in Detail
1. builds/compiles new version *.dlls and related resource files from the Languages and Icons folders into various /artifact folders

2. It Stops an eventually running FlowLauncher instance

3. copies the such created new plugin files from the developer path to the actual FlowLauncher's plugin folder

4. Restarts the FlowLauncher-Window again after a few seconds so that you can test the new plugin life from 
   its commandline

## Suggested Modifications
### Insert the Version Number from the CommandLine
in the origianl version, the now hardcoded Version Number was interactively inserted from the command-line when the script was launched. However, during early development, we have considered this as error-prown and prefer a static, hartcoded version number so that resulting output just replaces the old one or comes back with an error, rather than creating unwanted output folders and files with "strange" version numbers.

This might ev. change back when the code becomes more stable and relaeses are less. 

## The Code
last version copied in April 15th, 2026

param(
    [string]$Version = "v0.0.6",

    [string]$RelSourcePath = "src/Flow.Launcher.Plugin.FlClicker",
    [string]$PluginId = "badecafe-8037-1965-2026-a04b12c09d10",
    [string]$PluginName = "FlClicker",
    [string]$Repo = "realB12/Flow.Launcher.Plugin.FlClicker",
    [string]$Framework = "net8.0-windows",
    [string]$Runtime = "win-x64"
)

$ErrorActionPreference = "Stop"


$Root = Resolve-Path "."   # C:\me\REPO\PRJ\FLClicker\40 DEV\VSC\Flow.Launcher.Plugin.FlClicker

$Artifacts = Join-Path $Root "artifacts"         # Root/artifacts
$PublishRoot = Join-Path $Artifacts "publish"    # Root/artifacts/publish
$PackageRoot = Join-Path $Artifacts "package"    # Root/artifacts/package
$ReleaseRoot = Join-Path $Artifacts "release"    # Root/artifacts/release
$ZipName = "$PluginName-$Version.zip"            # FlClicker-0.0.5.zip
$ZipPath = Join-Path $ReleaseRoot $ZipName       # Root/artifacts/release/FlClicker-0.1.5.zip

Write-Host "Cleaning old artifacts..."
Remove-Item $Artifacts -Recurse -Force -ErrorAction SilentlyContinue
New-Item -ItemType Directory -Force -Path $PublishRoot, $PackageRoot, $ReleaseRoot | Out-Null

Write-Host "Restoring..."
dotnet restore $RelSourcePath

Write-Host "Publishing..."
dotnet publish $RelSourcePath `
    -c Release `
    -r $Runtime `
    --no-self-contained `
    -o $PublishRoot

Write-Host "Copying runtime files..."
Copy-Item "$PublishRoot\*" $PackageRoot -Recurse -Force

# Optional cleanup
Get-ChildItem $PackageRoot -Recurse -Include "*.pdb","*.xml" | Remove-Item -Force -ErrorAction SilentlyContinue

Write-Host "Creating zip..."
if (Test-Path $ZipPath) { Remove-Item $ZipPath -Force }
Compress-Archive -Path "$PackageRoot\*" -DestinationPath $ZipPath -Force

Write-Host "Deleting the /bin folder..."
# Remove-Item -Path $BinFolder -Recurse -Force

Write-Host "Done:"
Write-Host "Package folder: $PackageRoot"
Write-Host "Zip: $ZipPath"

## Troubleshooting a failed installation 
### New Version with identical ID in plugin.json!
<span style="color:red; font-weight:bold">Attention</span>: You might want to **change the ID in the plugin.json** file as the Plugin Manger and "pm install" sometimes for some not yet understood reasons, will refuse to install different package-versions with both having the same ID!!

So in case the FlowLauncher.exe "suddenly" refuses to load your Plugin without any error message or indications, chances are high, that you are loading an updated version with a former/unchanged ID from a previous version!
