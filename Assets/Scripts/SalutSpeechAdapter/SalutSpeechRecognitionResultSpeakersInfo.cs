using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UnityEngine;

public class SalutSpeechRecognitionResultSpeakersInfo
{
    [JsonPropertyName("speaker_id")]
    public int speakerId { get; set; }
    [JsonPropertyName("main_speaker_confidence")]
    public float mainSpeakerConfidence { get; set; }
}
