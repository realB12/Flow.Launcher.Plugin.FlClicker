# This PowerShell-script publishes the latest FlClicker-vx.x.x.zip file that was created with the
# previous run of the local-version.ps1 script to my personal Flow.Launcher.Plugin.FlClicker named
# GitHub Repo and marks it with a "v0.x.y" named Release-Tag. 
#
# Make sure you are setting the correct semVer version number below. For a list of already used 
# version numbers check the FlClicker_Release_Notes.md in the /docs folder. 

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