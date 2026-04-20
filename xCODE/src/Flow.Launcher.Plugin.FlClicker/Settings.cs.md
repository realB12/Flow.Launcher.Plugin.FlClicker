# Settings.cs

* **[Settings Control.xaml.cs](SettingsControl.xaml.cs.md)**: the xmal-code-behind file that reads the seetings into the SettingsControl. 
* **[Settings Control.xaml](SettingsControl.xaml.md)**: Defines the layout of the SettingsControl. 

----

This Settings.cs file is **just a standard helper-file.** **Do not change!!**

It just stores the clickUp provided Access-Token and list-ID when they are read from either the Settings.json file (default) or the Plugin-Manager configuraton when the Plugin is called the first time. 

ATTENTION: do not change this code and espcially do not write the Token and ID values into this file, becazse this helper-file is just used as an EMPTY container by the Flow-Core at startup to finally contain the values that are either read from the Settings.json file or from the Flow's Plugin-Configuration settings (inserted when the Plugin is installed/managed in the FlowLauncher's Plugin-Manager!) */

```csharp
using Flow.Launcher.Plugin;
using System.Text.Json.Serialization;

/// <summary>
/// Stores the ClickUp token and list ID configuration for the plugin.
/// </summary>
public class Settings {
  /// <summary>
  /// Gets or sets the ClickUp API token.
  /// </summary>
  [JsonPropertyName("ClickUpToken")]
  public string ClickUpToken { get; set; } = "";

  /// <summary>
  /// Gets or sets the ClickUp list ID.
  /// </summary>
  [JsonPropertyName("ListId")]
  public string ListId { get; set; } = "";
}// Settings class
```
