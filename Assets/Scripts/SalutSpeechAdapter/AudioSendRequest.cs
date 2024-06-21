using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

public class AudioSendRequest
{
    public string accessToken { get; private set; }

    public string pathContent { get; set; }

    public AudioSendRequest(string accessToken, string filePath)
    {
        this.accessToken = accessToken;
        pathContent = filePath;
    }
}
