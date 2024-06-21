using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UnityEngine;

public class SalutSpeechRecognitionResultBackendInfo
{
    [JsonPropertyName("model_name")]
    public string modelName { get; set; }
    [JsonPropertyName("model_version")]
    public string modelVersion { get; set; }
    [JsonPropertyName("server_version")]
    public string serverVersion { get; set; }
}
