using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UnityEngine;

public class SalutSpeechFileIdResponse
{
    [JsonPropertyName("status")]
    public int httpStatus { get; set; }
    [JsonPropertyName("result")]
    public SalutSpeechFileIdResult result { get; set; }
}
