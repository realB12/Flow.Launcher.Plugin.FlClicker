using System.Collections.Generic;
using System.Text.Json.Serialization;

/* This file models the response from ClickUp’s list-tasks endpoint, where the
 * returned tasks are inside the tasks property. */
public class GetTasksResponse {
  [JsonPropertyName("tasks")]
  public List<ClickUpTask> Tasks { get; set; } = new();
}