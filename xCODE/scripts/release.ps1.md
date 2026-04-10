dotnet publish "src/Flow.Launcher.Plugin.FlClicker" `
-c Release -r win-x64 --no-self-contained `
-o "bin/Release/win-x64"

# Compressing the Target-Binaries into a *.zip file to be published on the FlowLauncher GitHub Repo
Compress-Archive `
  -LiteralPath "bin/Release/win-x64" `
  -DestinationPath "bin/Release/win-x64/Flow.Launcher.Plugin.FlClicker.zip"  `
  -Force