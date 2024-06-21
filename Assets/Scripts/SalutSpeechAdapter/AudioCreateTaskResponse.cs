using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using UnityEngine;

public class AudioCreateTaskResponse
{
    public HttpResponseMessage httpResponse { get; private set; }
    
    public SalutSpeechCreateTaskStatus salutSpeechCreateTaskStatus { get; private set; }
    public string ErrorText { get; private set; }
    public bool RequestSuccess { get; private set; }
    public AudioCreateTaskResponse(HttpResponseMessage httpMessage)
    {
        httpResponse = httpMessage;
        var responseValue = httpMessage.Content.ReadAsStringAsync().Result;

        if (httpResponse.StatusCode == HttpStatusCode.OK)
        {
            RequestSuccess = true;
            //Debug.Log(httpResponse.StatusCode == (HttpStatusCode)200);
            salutSpeechCreateTaskStatus = JsonSerializer.Deserialize<SalutSpeechCreateTaskStatus>(responseValue);
        }
        else
        {
            RequestSuccess = false;
            ErrorText = string.IsNullOrEmpty(responseValue) ? "See HttpResponse for more info" : responseValue;
        }
    }
}
