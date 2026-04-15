<# compile.ps1

This ShellScript is COMPILATION ONLY
I builds/compiles the current code and would indicate erros when such exist. 
It does not copy a new version to the FlowLauncher.exe nur does it start/stop its execution. 

#>

# v-- creates a debuggable dll for the win-x64 platform
#     The command must refer to the *.csproj file WITHOUT! addin the *.csproj extention. 
dotnet publish src/Flow.Launcher.Plugin.FlClicker -c Debug -r win-x64 --no-self-contained