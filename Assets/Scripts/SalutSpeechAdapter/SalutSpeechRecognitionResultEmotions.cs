using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UnityEngine;

public class SalutSpeechRecognitionResultEmotions
{
    [JsonPropertyName("positive")]
    public float positive { get; set; }
    [JsonPropertyName("neutral")]
    public float neutral { get; set; }
    [JsonPropertyName("negative")]
    public float negative { get; set; }
}
