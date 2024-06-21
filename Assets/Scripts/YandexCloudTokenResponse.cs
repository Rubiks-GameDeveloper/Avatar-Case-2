using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UnityEngine;

public class YandexCloudTokenResponse
{
    [JsonPropertyName("iamToken")] 
    public string[] token;
}
