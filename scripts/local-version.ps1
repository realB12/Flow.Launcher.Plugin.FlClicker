# local-version.ps1
# =================
# This PowerShell-script compiles the current sourceCode into a publishable "FlClicker-v0.x.y.zip" 
# named distribution package to be found in the /artifacts/release subfolder and which will server 
# as the input for the subsequent "publish-github-verions.ps1" script that will upload it to the
# GitHub Repo and publish is as a new "v0.x.y"-named  Release. 
#
# Additionally, to support local testing, this script copies all required binaries and related 
# resource files (from teh Languages and Images folders) into the locally installed FlowLauncher
# app folder, after which the FlowLauncher.exe is stopped and restarted, so that the new version 
# can be readily tested from the FlowLauncher's command-line: such as "click version" to get 
# the current, hardcoded version from the Main.cs


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
