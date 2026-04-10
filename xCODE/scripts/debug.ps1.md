<# debug.ps1

This ShellScript 
1. builds/compiles a new version *.dll and related files that toghether make the FlClicker-Plugin 
   to be run in the FlowLauncher App

2. It Stops an eventually running FlowLauncher instance

3. copies the such created new plugin files from the developer path to the actual FlowLauncher's plugin folder

4. Restarts the FlowLauncher-Window again after a few seconds so that you can test the new plugin life from 
   its commandline

you can run this script from the VS Code Terminal as follows:

> Powershell.exe -executionpolicy remotesigned -File "C:\me\REPO\PRJ\FLClicker\40 DEV\VSC\debug.ps1"

Mind the enclosing " " for the file-path so that the "40 DEV" PathPartial is properly recognized!


#>

# v-- creates a debuggable dll for the win-x64 platform
#     The command must refer to the *.csproj file WITHOUT! addin the *.csproj extention. 
dotnet publish src/Flow.Launcher.Plugin.FlClicker -c Debug -r win-x64 --no-self-contained

dotnet publish "src/Flow.Launcher.Plugin.FlClicker" `
-c Debug -r win-x64 --no-self-contained `
-o "bin\Debug\win-x64"

$AppDataFolder = [Environment]::GetFolderPath("ApplicationData")
$flowLauncherExe = "$env:LOCALAPPDATA\FlowLauncher\Flow.Launcher.exe"

if (Test-Path $flowLauncherExe) {
    Stop-Process -Name "Flow.Launcher" -Force -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 2

    if (Test-Path "$AppDataFolder\FlowLauncher\Plugins\FlClicker") {
        Remove-Item -Recurse -Force "$AppDataFolder\FlowLauncher\Plugins\FlClicker"
    }

    Copy-Item "bin\Debug\win-x64" "$AppDataFolder\FlowLauncher\Plugins\publish" -Recurse -Force
    Rename-Item -Path "$AppDataFolder\FlowLauncher\Plugins\publish" -NewName "FlClicker"

    Start-Sleep -Seconds 2
    Start-Process $flowLauncherExe
} else {
    Write-Host "Flow.Launcher.exe not found. Please install Flow Launcher first"
}
