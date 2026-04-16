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
