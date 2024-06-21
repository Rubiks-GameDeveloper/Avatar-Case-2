using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UnityEngine;

public class SalutSpeechCreateTaskStatus
{
    [JsonPropertyName("status")]
    public int httpStatus { get; set; }
    [JsonPropertyName("result")]
    public SalutSpeechCreateTaskStatusResult result { get; set; }
}
