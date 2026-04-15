# launch.json

* **[Running and Debugging SourceCode in VSC](../../../../../../../WORK/ENTITY/SE/MS/VSC/Run%20and%20Debug/_Running%20and%20Debugging%20SourceCode.md)** 
* **[Deployment Guide](../../../../DEV_MAN/07%20DEPLOY/_FlClicker_Deployment%20Guide.md)** 
* **[debug.ps1](../scripts/debug.ps1.md)**: error checking only
* **[local version.ps1](../scripts/local-version.ps1.md)**: create a new version/release package
* **[public github version.ps1](../scripts/public-github-version.ps1.md)**: create an official GitHub Release
---

The **launch.json file** in the root's **.vscode subfolder** is the standard configuration file **the VSC IDE is looking for the command/batch-files to be executed, when the developer clicks PF5** in Run-View (or selects Run/Debug from a the VSC IDE's menu).  

> User -> clicks **PF5** -> .vscode **launcher.json** -> my_scripts/**debug.ps1**

## Current Configuration

```json
{
    "version": "0.2.0",
    "configurations": [
        {
            "name": "FC: Debug",
            "type": "PowerShell",
            "request": "launch",
            "script": "C:/me/REPO/PRJ/FLClicker/40 DEV/VSC/Flow.Launcher.Plugin.FlClicker/scripts/debug.ps1"
        },
        {
            "name": "FC: Local Version",
            "type": "PowerShell",
            "request": "launch",
            "script": "C:/me/REPO/PRJ/FLClicker/40 DEV/VSC/Flow.Launcher.Plugin.FlClicker/scripts/local-version.ps1"
        },
        {
            "name": "FC: Public Github Version",
            "type": "PowerShell",
            "request": "launch",
            "script": "C:/me/REPO/PRJ/FLClicker/40 DEV/VSC/Flow.Launcher.Plugin.FlClicker/scripts/public-github-version.ps1"
        }
    ]
}
```

**What it does:** 

When the User clicks PF5 or selects Run/Debug from the Menu this file will call and **execute the such matched PowerShell Script from the [/scripts folder](../my_scripts/my_scripts.md)**. 

## Further information
For details about how and why I am manging all deployment from this single configuration file, see my generic guide upon [Running and Debugging SourceCode in VSC](../../../../../../../WORK/ENTITY/SE/MS/VSC/Run%20and%20Debug/_Running%20and%20Debugging%20SourceCode.md) and for Deployment refer to the [Deployment Guide](../../../../DEV_MAN/07%20DEPLOY/_FlClicker_Deployment%20Guide.md) or read the comments for the following PowerShellScripts: 

* **[debug.ps1](../scripts/debug.ps1.md)**: error checking only
* **[local version.ps1](../scripts/local-version.ps1.md)**: create a new version/release package
* **[public github version.ps1](../scripts/public-github-version.ps1.md)**: create an official GitHub Release