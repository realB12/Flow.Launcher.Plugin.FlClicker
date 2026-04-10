# launch.json

> User -> clicks **PF5** -> .vscode **launcher.json** -> my_scripts/**debug.ps1**

The **launch.json file** in the root's **.vscode subfolder** is the standard configuration file **the VSC IDE is looking for the command/batch-files to be executed, when the developer clicks PF5** (or selects Run/Debug from a the VSC IDE's menu).  

## Current Configuration

```json
{
    "version": "0.2.0",
    "configurations": [
        {
            "name": "Debug FlowLauncher Plugin",
            "type": "PowerShell",
            "request": "launch",
            "script": "C:/me/REPO/PRJ/FLClicker/40 DEV/VSC/Flow.Launcher.Plugin.FlClicker/my_scripts/debug.ps1"
        }
    ]
}
```

**What it does:** 

When the User clicks PF5 or selects Run/Debug from the Menu this file will call and **execute the indicated debug.ps1 PowerShell Script from the [my_scripts folder](../my_scripts/my_scripts.md)**. 

## Further information
For details about how and why I am manging all build-stuff from this single configuration file, see my generic guide upon [Running and Debugging SourceCode in VSC](../../../../../../../WORK/ENTITY/SE/MS/VSC/Run%20and%20Debug/_Running%20and%20Debugging%20SourceCode.md)
