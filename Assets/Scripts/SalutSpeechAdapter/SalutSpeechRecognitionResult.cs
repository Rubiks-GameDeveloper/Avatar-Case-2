using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UnityEngine;

public class SalutSpeechRecognitionResult
{
    [JsonPropertyName("text")]
    public string text { get; set; }
    [JsonPropertyName("normalized_text")]
    public string normalizedText { get; set; }
    // [JsonPropertyName("start")]
    // public string start { get; set; }
    // [JsonPropertyName("end")]
    // public string end { get; set; }
    // [JsonPropertyName("word_alignments")]
    // public IEnumerable<SalutSpeechRecognitionResultWordAlignment> wordAlignments { get; set; }
    //
    // [JsonPropertyName("eou")]
    // public bool eou { get; set; }
    //
    // [JsonPropertyName("emotions_result")]
    // public IEnumerable<SalutSpeechRecognitionResultEmotions> emotionsResult { get; set; }
    //
    // [JsonPropertyName("processed_audio_start")]
    // public string processedAudioStart { get; set; }
    // [JsonPropertyName("processed_audio_end")]
    // public string processedAudioEnd { get; set; }
    //
    // [JsonPropertyName("backend_info")]
    // public IEnumerable<SalutSpeechRecognitionResultBackendInfo> backendInfo { get; set; }
    //
    // [JsonPropertyName("channel")]
    // public int channel { get; set; }
    //
    // [JsonPropertyName("speaker_info")]
    // public IEnumerable<SalutSpeechRecognitionResultSpeakersInfo> speakerInfo { get; set; }
    //
    // [JsonPropertyName("eou_reason")]
    // public string eouReason { get; set; }
    //
    // [JsonPropertyName("insight_result")]
    // public IEnumerable<SalutSpeechRecognitionResultInsightInfo> insightResult { get; set; }
}
