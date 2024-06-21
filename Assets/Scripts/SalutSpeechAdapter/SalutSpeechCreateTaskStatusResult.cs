using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UnityEngine;

public class SalutSpeechCreateTaskStatusResult
{
    [JsonPropertyName("id")]
    public string taskId { get; set; }
    [JsonPropertyName("created_at")]
    public string createdAtTime { get; set; }
    [JsonPropertyName("updated_at")]
    public string updatedAtTime { get; set; }
    [JsonPropertyName("status")]
    public string taskStatus { get; set; }
    [JsonPropertyName("error")]
    public string erronInfo { get; set; }
    [JsonPropertyName("response_file_id")]
    public string responseFileId { get; set; }
}
