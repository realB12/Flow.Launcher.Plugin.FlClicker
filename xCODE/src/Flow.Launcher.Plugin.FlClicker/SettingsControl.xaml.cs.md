# SettingsControl.xaml.cs

This file contains the C#-code behind the SettingsControl.xaml-file. 
It reade the default values from the [Settings.json](Settings.json.md) and displays them in the [Settings Control.xaml](SettingsControl.xaml.md) defined Control where the such changed values are overwritten and locally saved in the local Settings.json of the currently active FlClickerPlugin version. 

Together the two files define the Layout (.xmal) and functionality (.xaml.cs) of the **FlClicker Plugin's Settings Panel**: 

![Settings Panel](./zPIC/SettingsPanel.png)

## The Code
```csharp
using System;
using System.Windows;
using System.Windows.Controls;

public partial class SettingsControl : UserControl {
  private readonly Settings _settings;
  private readonly Action _saveAction;

  public SettingsControl(Settings settings, Action saveAction) {
    InitializeComponent();

    _settings = settings;
    _saveAction = saveAction;

    ClickUpTokenTextBox.Text = _settings.ClickUpToken;
    ListIdTextBox.Text = _settings.ListId;
  }

  private void SaveButton_Click(object sender, RoutedEventArgs e) {
    _settings.ClickUpToken = ClickUpTokenTextBox.Text?.Trim() ?? "";
    _settings.ListId = ListIdTextBox.Text?.Trim() ?? "";

    _saveAction.Invoke();

    MessageBox.Show("ClickUp settings saved.", "Flow Launcher Plugin",
                    MessageBoxButton.OK, MessageBoxImage.Information);
  }
}
```

