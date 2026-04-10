using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Json;


/* This file wraps HttpClient, uses System.Net.Http.Json for task creation, and
 * uses System.Text.Json to deserialize task-list responses. The personal token
 * should be sent in the Authorization header directly.*/

/// <summary>
/// Client for interacting with the ClickUp API.
/// </summary>
public class ClickUpClient {
  private readonly HttpClient _httpClient;
  private readonly JsonSerializerOptions _jsonOptions;

  /// <summary>
  /// Class Constructor
  /// Initializes a new instance of the ClickUpClient class.
  /// </summary>
  /// <param name="token">The API token for authentication.</param>
  public ClickUpClient(string token) {
    _httpClient = new HttpClient() { BaseAddress = new Uri(
                                         "https://api.clickup.com/api/v2/") };
    _httpClient.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue(token);

    _jsonOptions =
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
  } // class Constructor

  /// <summary>
  /// Creates a new task in the specified ClickUp list.
  /// </summary>
  /// <param name="listId">The ID of the list where the task will be
  /// created.</param> <param name="taskName">The name of the task.</param>
  /// <param name="description">The description of the task.</param>
  /// <param name="cancellationToken">A cancellation token to cancel the
  /// operation.</param> <returns>The response containing the created task
  /// details, or null if deserialization fails.</returns>
  public async Task<CreateTaskResponse>
  CreateTaskAsync(string listId, string taskName, string description,
                  CancellationToken cancellationToken = default) {
    var payload = new { name = taskName, description = description };

    var json = JsonSerializer.Serialize(payload);
    var content = new StringContent(json, Encoding.UTF8, "application/json");

    var response = await _httpClient.PostAsync(
        $"https://api.clickup.com/api/v2/list/{listId}/task", content,
        cancellationToken);

    var body = await response.Content.ReadAsStringAsync(cancellationToken);

    response.EnsureSuccessStatusCode();

    return JsonSerializer.Deserialize<CreateTaskResponse>(body);
  }

  /// <summary>
  /// Releases the resources used by the ClickUpClient.
  /// </summary>
  public void Dispose() { _httpClient.Dispose(); }

  /*===================================================================================================*/
  public async Task<List<ClickUpTask>> GetTasksAsync(string listId,
                                                     bool includeClosed,
                                                     int page,
                                                     CancellationToken token) {
    string includeClosedValue = includeClosed.ToString().ToLowerInvariant();
    string requestUri =
        $"list/{listId}/task?include_closed={includeClosedValue}&page={page}";

    using var response = await _httpClient.GetAsync(requestUri, token);
    response.EnsureSuccessStatusCode();

    string json = await response.Content.ReadAsStringAsync(token);
    var result =
        JsonSerializer.Deserialize<GetTasksResponse>(json, _jsonOptions);

    return result?.Tasks ?? new List<ClickUpTask>();
  }

  public async Task<List<ClickUpTask>>
  GetAllTasksAsync(string listId, bool includeClosed, CancellationToken token) {
    var allTasks = new List<ClickUpTask>();
    int page = 0;

    while (true) {
      var pageTasks = await GetTasksAsync(listId, includeClosed, page, token);

      if (pageTasks == null || pageTasks.Count == 0) {
        break;
      }

      allTasks.AddRange(pageTasks);

      if (pageTasks.Count < 100) {
        break;
      }

      page++;
    }

    return allTasks;
  }
} // ClickUpClient Class