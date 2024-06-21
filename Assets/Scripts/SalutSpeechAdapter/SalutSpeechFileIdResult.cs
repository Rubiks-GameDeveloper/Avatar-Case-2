using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UnityEngine;

public class SalutSpeechFileIdResult
{
    [JsonPropertyName("request_file_id")]
    public string requestFileId { get; set; }
}
