# Directory.Build.props

**Redirects the /bin and /obj from the /src folder into the /artefacts folder**. 

## In Detail

Currently this Solution-level configuration file contains the following xml-code: 

```xml
<Project>
  <PropertyGroup>
    <ArtifactsRoot>$(MSBuildThisFileDirectory)artifacts\</ArtifactsRoot>

    <BaseOutputPath>$(ArtifactsRoot)bin\$(MSBuildProjectName)\$(Configuration)\</BaseOutputPath>
    
    <BaseIntermediateOutputPath>$(ArtifactsRoot)obj\$(MSBuildProjectName)\$(Configuration)\</BaseIntermediateOutputPath>
    
    <MSBuildProjectExtensionsPath>$(BaseIntermediateOutputPath)</MSBuildProjectExtensionsPath>
  </PropertyGroup>
</Project>
```

It advises the dotnet build process to **compile all binaries including /bin and /obj folders into an /artefacts folder under the project root**. 

This **keeps the */src*-folder clean** from binaries resp. prevents the "eternal" */bin*- and */obj*-folders that otherwise would normally (by default) be automatically created after every *dotnet publish*-call. 

## Practical advice
For your Flow Launcher plugin project in VS Code, I’d use Directory.Build.props at the repo root so your source folders stay clean and every project follows the same output layout. 

With this **Directory.Build.props**-file in this github-repo-root every project under the repo uses the same output layout automatically. With the configuration above, your outputs will land under a central artifacts folder, separated by project name and build configuration, which aligns with current .NET guidance for common output layouts.

## Resulting structure:


```plaintext
Flowcharter/
├─ Directory.Build.props
├─ scripts/
│  └─ clean-artifacts.ps1
├─ artifacts/
│  ├─ bin/
│  │  └─ Flow.Launcher.Plugin.Flowcharter/
│  │     ├─ Debug/
│  │     └─ Release/
│  └─ obj/
│     └─ Flow.Launcher.Plugin.Flowcharter/
│        ├─ Debug/
│        └─ Release/
└─ src/
   └─ Flow.Launcher.Plugin.Flowcharter/
```

That keeps source folders clean and separates Debug and Release outputs cleanly.


## Cleanup Script
To delete the such created artefacts folder (which contains only files that have been generated from the source) create the follwoing  scripts\clean-artifacts.ps1 Powerschell script:

```powershell
param(
    [switch]$All,
    [string]$Configuration
)

$ErrorActionPreference = "Stop"

$RepoRoot = Split-Path -Parent $PSScriptRoot
$ArtifactsRoot = Join-Path $RepoRoot "artifacts"

if (-not (Test-Path $ArtifactsRoot)) {
    Write-Host "No artifacts folder found: $ArtifactsRoot"
    return
}

if ($All -or [string]::IsNullOrWhiteSpace($Configuration)) {
    Write-Host "Deleting entire artifacts folder..."
    Remove-Item -Path $ArtifactsRoot -Recurse -Force
    Write-Host "Deleted: $ArtifactsRoot"
    return
}

$Targets = @(
    (Join-Path $ArtifactsRoot "bin\*\$Configuration"),
    (Join-Path $ArtifactsRoot "obj\*\$Configuration")
)

foreach ($Target in $Targets) {
    Get-ChildItem -Path $Target -Directory -ErrorAction SilentlyContinue | ForEach-Object {
        Write-Host "Deleting $($_.FullName)"
        Remove-Item -Path $_.FullName -Recurse -Force
    }
}

Write-Host "Cleanup complete for configuration: $Configuration"
```

This script either deletes the whole shared artifacts tree or only the Debug or Release subfolders, which is safer than recursively deleting from the repo root.