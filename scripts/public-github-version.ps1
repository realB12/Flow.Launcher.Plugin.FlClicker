# This script publishes the latest FlClicker-vx.x.x.zip file
# to the GitHub Repo and marks it with a Release Tag. 

param(
    [Parameter(Mandatory = $true)]
    [string]$Version,

    [string]$Repo = "realB12/Flow.Launcher.Plugin.FlClicker",
    [string]$PluginName = "FlClicker"
)

$ErrorActionPreference = "Stop"

$ZipPath = Resolve-Path "artifacts\release\$PluginName-v$Version.zip"
$Tag = "v$Version"
$Title = "$PluginName - $Version"

git add .
git commit -m "Created new productive Release: $PluginName-v$Version" 2>$null
git tag $Tag
git push
git push origin $Tag

# GitHub CLI (gh) is a command-line tool that lets you interact with GitHub directly from your terminal.
# if not yet installed, install it with >winget install --id GitHub.cli 
gh release create $Tag $ZipPath `
--repo $Repo `
--title $Title `
--notes "Release $Version"