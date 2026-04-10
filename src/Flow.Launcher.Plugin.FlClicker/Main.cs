using Flow.Launcher.Plugin;
using System; // for string handling
using System.Collections.Generic;
using System.IO;
using System.Linq; // for skipping string parts
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

/// <summary>
/// Main plugin class for ClickUp integration with Flow Launcher.
/// Implements IAsyncPlugin for calling the ClickUp API asynchronously so that
/// errors can be handled and displayed to the user. So the request is handled
/// cleanly with await instead of fire-and-forget. Flow Launcher’s docs describe
/// IAsyncPlugin with InitAsync and QueryAsync, and the query returns a list of
/// results.
/// </summary>
public class Main : IAsyncPlugin {
  private PluginInitContext _context = null!;
  private Settings _settings = new();
  private ClickUpClient? _client;

  // vv-- The version and Text that will appear when you prompt "click version"
  private const string myCURRENT_VERSION = "04.10.22:01";
  private const string myCURRENT_VERSION_DESCRIPTION =
      "Basic core function implementation";

  /// <summary>
  /// Mandatory Init Function that runs once when the Plugin is activated with
  /// the "click" keyword: Initializes the plugin with the given context and
  /// loads settings.
  /// </summary>
  /// <param name="context">The plugin initialization context.</param>
  public async Task InitAsync(PluginInitContext context) {
    _context = context;
    // reading the Settings.json file with the Token and List ID
    LoadSettings();

    // creating a new ClickUpClient instance with injected TokenID
    if (!string.IsNullOrWhiteSpace(_settings.ClickUpToken)) {
      _client = new ClickUpClient(_settings.ClickUpToken);
    } // if

    /* await Task.CompletedTask means “wait for a task that is already
     * finished.” What it does:
     * - Task.CompletedTask returns a Task that has already completed
     * successfully.
     * - await on it does not really pause the method.
     * - The code continues immediately after that line.*/
    await Task.CompletedTask;
  } // InitAsync()

  /// <summary>
  /// Processes user queries and returns a list of results.
  /// </summary>
  /// <param name="query">The user query.</param>
  /// <param name="token">The cancellation token.</param>
  /// <returns>A list of results to display.</returns>
  /// This function is mandatory for implementing the IAsyncPlugin Interface
  /// The herein called Result.AsyncAction functionis awaits execution and
  /// returns a boolen, where true hides the launcher (makes it disappear) and
  /// false keeps it open to display results.
  public async Task<List<Result>> QueryAsync(Query query,
                                             CancellationToken token) {
    var input = query.Search?.Trim() ?? "";

    if (string.IsNullOrWhiteSpace(input)) {
      return myReturnAsyncInfo(
          "Waiting for Input",
          "Add a command such as \"add\" or \"get\" followed by parameters");
    }

    var myInputParts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var myCommand = myInputParts[0].ToLowerInvariant();
    // var payload = myInputParts.Length > 1 ? string.Join(" ",
    // myInputParts.Skip(1)) : "";

    if (myCommand == "version") {
      return myReturnAsyncInfo($"Current version = {myCURRENT_VERSION}",
                               myCURRENT_VERSION_DESCRIPTION);
    }

    /* Handling ClickUp API initialization issues */
    if (_client == null || string.IsNullOrWhiteSpace(_settings.ListId)) {
      return myReturnAsyncInfo(
          "ClickUp settings missing or not valid",
          "Open the plugin settings and enter your Personal Token and List ID");
    }

    /* Command Dispatching */
    switch (myCommand) {
    case "add": {
      string myPayload =
          myInputParts.Length > 1 ? string.Join(" ", myInputParts.Skip(1)) : "";

      return HandleAdd(myPayload, token);
    } // case "add"

    case "get": {
      if (myInputParts.Length == 1) {
        return myReturnAsyncInfo(
            "Usage: click get all | click get <id>",
            "Type \"all\" to fetch all tasks or provide a task ID");
      }

      string getOption = myInputParts[1].ToLowerInvariant();

      switch (getOption) {
      case "all":
        return await HandleGetAllResultsAsync(token);

      default:
        return myReturnAsyncInfo("Unknown get option",
                                 "Use \"all\" or provide a specific task ID");
      }
    } // case "get"

    default:
      return myReturnAsyncInfo("Invalid ClickUp command",
                               $"{myCommand} is an unrecognized command");
    } /* Command Dispatching */
  } // QueryAsync()

  /* Helper function that generates the Result record */
  private static List<Result> myReturnAsyncInfo(string title,
                                                string subTitle) => new() {
    new Result { Title = title, SubTitle = subTitle }
  };

  /* "add"-Handler
  The add branch gives a preview result and only performs the API call when
  selected.   */
  private List<Result> HandleAdd(string payload, CancellationToken token) {
    if (string.IsNullOrWhiteSpace(payload)) {
      return myReturnAsyncInfo("Waiting for Input",
                               "Please type the task you want to add");
    }

    return new List<Result> { new Result {
      Title = $"Create ClickUp task: {payload}",
      SubTitle = "Press Enter to create this task",
      AsyncAction =
          async
              _ => {
                try {
                  var created = await _client.CreateTaskAsync(
                      _settings.ListId, payload, "Created from Flow Launcher",
                      token);

                  if (created != null) {
                    _context.API.ShowMsg(
                        "ClickUp", $"Created task: {created.Name ?? payload}");
                    return true;
                  } // if

                  _context.API.ShowMsg("ClickUp", "Task creation failed");
                  return false;
                } catch (Exception ex) {
                  _context.API.ShowMsg("ClickUp error", ex.Message);
                  return false;
                } // try
              }
    } };
  } // "add"-Handler

  /* "get"-Handler */
  private async Task<List<Result>>
  HandleGetAllResultsAsync(CancellationToken token) {
    try {
      var tasks = await _client!.GetAllTasksAsync(
          _settings.ListId, includeClosed: false, token: token);

      if (tasks == null || tasks.Count == 0) {
        return myReturnAsyncInfo("No tasks found",
                    "The ClickUp list does not contain any open tasks");
      }

      return tasks
          .Select(task => new Result {
            Title = task.Name ?? "(no title)",
            SubTitle =
                $"ID: {task.Id} | Status: {task.Status?.Status ?? "unknown"}",
            AsyncAction =
                async
                    _ => {
                      try {
                        if (!string.IsNullOrWhiteSpace(task.Url)) {
                          var psi = new ProcessStartInfo(
                              task.Url) { UseShellExecute = true };

                          Process.Start(psi);
                          return true;
                        }

                        _context.API.ShowMsg("ClickUp",
                                             "Task URL not available");
                        return false;
                      } catch (Exception ex) {
                        _context.API.ShowMsg("ClickUp error", ex.Message);
                        return false;
                      }
                    }
          })
          .ToList();
    } catch (Exception ex) {
      return myReturnAsyncInfo("ClickUp error", ex.Message);
    }
  }

  private void LoadSettings() {
    var settingsPath = Path.Combine(
        _context.CurrentPluginMetadata.PluginDirectory, "Settings.json");

    if (File.Exists(settingsPath)) {
      var json = File.ReadAllText(settingsPath);
      _settings = JsonSerializer.Deserialize<Settings>(json) ?? new Settings();
    }
  } // LoadSettings()

  /// <summary>
  /// Disposes the ClickUp client resource.
  /// </summary>
  public void Dispose() { _client?.Dispose(); }

} // class Main