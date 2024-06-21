using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UnityEngine;

public class SalutSpeechRecognitionResultInsightInfo
{
    [JsonPropertyName("csi")]
    public SalutSpeechRecognitionResultInsightInfoCsi[] csi { get; set; }
    
}
