using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UnityEngine;

public class SalutSpeechRecognition
{
    [JsonPropertyName("results")]
    public List<SalutSpeechRecognitionResult> results { get; set; }
}
