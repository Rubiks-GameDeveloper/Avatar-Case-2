using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UnityEngine;

public class SalutSpeechRecognitionResultInsightInfoCsi
{
    [JsonPropertyName("positive")]
    public float positive { get; set; }
    [JsonPropertyName("negative")]
    public float negative { get; set; }
    [JsonPropertyName("prediction")]
    public float prediction { get; set; }
}
