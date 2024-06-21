using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using UnityEngine;

public class AudioSendResponse
{
    public HttpResponseMessage httpResponse { get; private set; }
    
    public SalutSpeechFileIdResponse salutSpeechFileIdResponse { get; private set; }
    public string ErrorText { get; private set; }
    public bool RequestSuccess { get; private set; }
    public AudioSendResponse(HttpResponseMessage httpMessage)
    {
        httpResponse = httpMessage;
        var responseValue = httpMessage.Content.ReadAsStringAsync().Result;

        if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
        {
            RequestSuccess = true;
            Debug.Log(responseValue);
            salutSpeechFileIdResponse = JsonSerializer.Deserialize<SalutSpeechFileIdResponse>(responseValue);
        }
        else
        {
            RequestSuccess = false;
            ErrorText = string.IsNullOrEmpty(responseValue) ? "See HttpResponse for more info" : responseValue;
        }
    }
}
