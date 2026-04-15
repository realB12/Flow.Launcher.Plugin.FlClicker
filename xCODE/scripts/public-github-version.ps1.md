# The public-github-version.ps1 PowerShell-Script

* **[Deployment Guide](../../../../DEV_MAN/07%20DEPLOY/_FlClicker_Deployment%20Guide.md)**: gives you the full context why and for what this script is used

* **[local version.ps1](local-version.ps1.md)**: creates **FlClicker-v0.x.y.zip named input package** for this script

* **[launcher.json](../.vscode/launcher.json.md)**: VSC configuration file that links PF5-Run-options to this script. 

## What it does
This PowerShell-script publishes the latest FlClicker-vx.x.x.zip file that was created with the previous run of the [local-version.ps1 script](local-version.ps1.md) to my personal Flow.Launcher.Plugin.FlClicker named GitHub Repo and marks it with a "v0.x.y" named Release-Tag. 

## How to use it
Before running the script, make sure the correct semVer version number in the v0.x.y format is set in the code. 

For a list of already used version numbers check the FlClicker_Release_Notes.md Doc in the /docs folder. 

## How to launch this script
This script can be launched with PF5 or from within the Terminal

### PF5 launch
1. Open the Run-View and select the "FC: Public Github Version"-option
![PF5_local version](./zPICs/PF5_local-version.ps1). 

This "FC: Public Github Version" is associated with the following VSC configuration entry in the **.vscode/launch.json** file: 

```json
{
    "version": "0.2.0",
    "configurations": [
        {
            "name": "FC: Public Github Version",
            "type": "PowerShell",
            "request": "launch",
            "script": "C:/me/REPO/PRJ/FLClicker/40 DEV/VSC/Flow.Launcher.Plugin.FlClicker/scripts/public-github-version.ps1"
        }
    ]
}
```

Which finally makes the link between the selected PF5 optoin and the PowerShell-Script. 


### Terminal launch
1. Open the VSC terminal and run the script as follows:

> Powershell.exe -executionpolicy remotesigned -File "C:\me\REPO\PRJ\FLClicker\40 DEV\VSC\scripts\pulbic-github-version.ps1"

Mind the enclosing " " for the file-path, so that the "40 DEV" Path-Partial is properly recognized!

## The Process in Detail
1. Comitts the current project to GitHub and tags it with a Tag that has the Release's version number such as v0.0.6

2. By making the current tagged Commit to a publicly visible GitHub Release (by internally calling the GitHub CLI (gh) tool) the previously compiled FlClicker-v0.x-y.zip" named Package is exposed as a public GitHub Relase. 

When done so, the  final FlClicker-v0.0.6.zip file can be accessed and downloaded from everywhere, which includes the FlowLauncher-Plugin-Manager as well, off-course. 

## The GitHub CLI (gh) command-line tool
This PowerShell Script needs and calls the GitHub CLI (gh) command-line tool that lets you interact with GitHub directly from your terminal: in our case to create the public facing GitHub-Release. 

If not yet installed, you have to install the tool from the commandline with the following command 

> **winget install --id GitHub.cli** 

When used for the first time run 

> **gh auth login** 

and follow the interactive dialoge to confirm your GitHub connection interactively through two-factor-authentication (mobile phone required). 

## Suggested Modifications

### Insert the Version Number from the CommandLine
in the origianl version, the now hardcoded Version Number was interactively inserted from the command-line when the script was launched. However, during early development, we have considered this as error-prown and prefer a static, hartcoded version number so that resulting output just replaces the old one or comes back with an error, rather than creating unwanted output folders and files with "strange" version numbers.

This might ev. change back when the code becomes more stable and relaeses are less. 

## The Code

```powershell
param(
    [string]$Version = "v0.0.6",
    [string]$Repo = "realB12/Flow.Launcher.Plugin.FlClicker",
    [string]$PluginName = "FlClicker"
)

$ErrorActionPreference = "Stop"

$ZipPath = Resolve-Path "artifacts\release\$PluginName-$Version.zip"
$Tag = "$Version"

# Comitting the current state to become the comitted release version, which in 
# the following is tagged with a Tag that has the Release's version number 
# such as v0.0.6
git add .
git commit -m "Created new productive Release: $PluginName-$Version" 2>$null
git tag $Tag
git push
git push origin $Tag

# Finally the current *.zip is exposed as a public GitHub Relase. When done so, the  final
# FlClicker-v0.0.6.zip file can be accessed and downloaded from everywhere, which includes
# the FlowLauncher-Plugin-Manager as well, off-course. 
# GitHub CLI (gh) is a command-line tool that lets you interact with GitHub directly from your terminal.
# if not yet installed, install it with >winget install --id GitHub.cli 
# when used for the first time run > gh auth login and follow the interactive dialoge to confirm 
# your GitHub connection interactively through 2 factor Authentication. 
gh release create $Tag $ZipPath `
--repo $Repo `
--title "$PluginName - $Version" `
--notes "Release $Version : Find release notes in \docs\FlClicker_Release_Notes.md"
```

## Troubleshooting a failed installation 
### New Version with identical ID in plugin.json!
<span style="color:red; font-weight:bold">Attention</span>: You might want to **change the ID in the plugin.json** file as the Plugin Manger and "pm install" sometimes for some not yet understood reasons, will refuse to install different package-versions with both having the same ID!!

So in case the FlowLauncher.exe "suddenly" refuses to load your Plugin without any error message or indications, chances are high, that you are loading an updated version with a former/unchanged ID from a previous version!
