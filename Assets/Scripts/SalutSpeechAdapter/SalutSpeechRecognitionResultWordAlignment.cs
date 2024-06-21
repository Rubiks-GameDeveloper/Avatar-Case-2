using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UnityEngine;

public class SalutSpeechRecognitionResultWordAlignment
{
    [JsonPropertyName("word")]
    public string word { get; set; }
    [JsonPropertyName("start")]
    public string start { get; set; }
    [JsonPropertyName("end")]
    public string end { get; set; }
}
