using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using UnityEngine;

public enum AudioCodec
{
    MP3,
    OGG
}

public class AudioCreateTaskRequest
{
    public string accessToken { get; private set; }
    public string requestFileId { get; private set; }
    public StringContent requestData { get; private set; }

    public AudioCreateTaskRequest(string accessToken, string requestFileId, AudioCodec codec, string language,
        bool profanityFilter, int hypothesesCount)
    {
        this.accessToken = accessToken;
        this.requestFileId = requestFileId;
        var reqData = $@"{{
            ""options"": {{
                ""language"": ""{language}"",
                ""content-type"": ""audio/mpeg"",
                ""audio_encoding"": ""MP3"",
                ""hypotheses_count"": 1,
                ""enable_profanity_filter"": {profanityFilter},
                ""max_speech_timeout"": ""20s"",
                ""channels_count"": {hypothesesCount},
                ""no_speech_timeout"": ""7s"",
                ""hints"": {{
                    ""words"": [""привет"", ""день""],
                    ""enable_letters"": true,
                    ""eou_timeout"": ""3s""
                }},
                ""speaker_separation_options"": {{
                    ""enable"": true, 
                    ""enable_only_main_speaker"": true, 
                    ""count"": 2
                }}
            }},
            ""request_file_id"": ""{requestFileId}""
        }}";
        
        var requestSettings = $@"{{
            ""options"": {{
                ""language"": ""ru-RU"",
                ""audio_encoding"": ""PCM_S16LE"",
                ""sample_rate"": 48000,
                ""hypotheses_count"": 1,
                ""enable_profanity_filter"": false,
                ""max_speech_timeout"": ""20s"",
                ""channels_count"": 1,
                ""no_speech_timeout"": ""7s"",
                ""hints"": {{
                    ""enable_letters"": true,
                    ""eou_timeout"": ""3s""
                }},
                ""speaker_separation_options"": {{
                    ""enable"": true, 
                    ""enable_only_main_speaker"": true, 
                    ""count"": 2
                }}
            }},
            ""request_file_id"": ""{requestFileId}""
        }}";

        requestData = new StringContent(requestSettings, Encoding.UTF8, "application/json");
    }
}
