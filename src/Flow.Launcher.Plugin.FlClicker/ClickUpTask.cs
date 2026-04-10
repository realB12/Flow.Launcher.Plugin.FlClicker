using System.Text.Json.Serialization;

/* This file models the ClickUp task data you actually use in the plugin UI,
 * including ID, title, URL, and status. */
public class ClickUpTask {
  [JsonPropertyName("id")]
  public string? Id { get; set; }

  [JsonPropertyName("name")]
  public string? Name { get; set; }

  [JsonPropertyName("url")]
  public string? Url { get; set; }

  [JsonPropertyName("status")]
  public ClickUpStatus? Status { get; set; }
}

public class ClickUpStatus {
  [JsonPropertyName("status")]
  public string? Status { get; set; }
}
