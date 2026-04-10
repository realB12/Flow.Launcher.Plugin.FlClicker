# xCODE Comments Folder

## Principles of SourceCode Documentation
**This folder is internal documentation only for developers** and it therefore never is an integrated part of the SourceCode or the final product! 

xCode-Files contain instructions, technical hints, tips, background information, recomendations, ideas for improvement etc. that are only relevant to developers and therefdore must not be shared with the end-user or other project stakeholders. 

This folder contains MarkDown (*.md) formatted CodeComment-files that in their entity mimic the directory structures and filenames of the VSC managed SourceCode/Project-folder. 

So, for instance, the *.vscode/launch.json* file under the ProjectRoot is commented with a corresponding *.vscode/launch.json.md* file in this xCODE Folder. 

However, be aware, that not every tiny file is commented this way. When not, the file is just missing in the xCode folder (but can be added anytime later, should it make sense). 

**This folder is documentation only**. Whereas it is included into the GitHub Repo is must not be parsed for builds and deployments. 

## Starting Points
Same as for the **SourceCode** the Starting point of this Code-Doc is the **[Main.cs](Main.cs.md)-file**. 

Run/Debug/Deploy Configuration start with the [launcher.json](.vscode/launcher.json.md) documentation 

## Core Files
```plaintext

+--.vscode
   +--launch.json   (VSC standard: run/debug configuration start)
+--bin              (VSC standard: the compiled binaries such as dlls etc. )
+--my_scripts       (my personal location for PowerShell Deployment Scripts
   +--debug.ps1     (Powershell Script to install the new built Plugin in a live FloLauncher.exe in debug mode)
   +--release.ps1   (Powershell Script generate the optimized (lean) production code)   
+--obj              (VSC standard: don't touch)
+- xCode            (my Code Documentation folder)
+--Flow.Launcher.Plugin.FlClicker.csproj  (VSC standard: Project Configuration File)
+--Flow.Launcher.Plugin.FlClicker.sln     (VSC standard: Project Solution File)
+--Main.cs          (VSC standard: MainProgram and therefore Single Point of entry into the SourceCode)
+--plugin.json      (FlowLauncher Standard: GitHub Configuration for Production Releases). 

```