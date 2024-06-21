using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using UnityEngine;

public class GetRecognitionResultResponse
{
    public HttpResponseMessage httpResponse { get; private set; }
    
    public List<SalutSpeechRecognition> salutSpeechRecognition { get; private set; }
    public string ErrorText { get; private set; }
    public bool RequestSuccess { get; private set; }
    public GetRecognitionResultResponse(HttpResponseMessage httpMessage)
    {
        httpResponse = httpMessage;
        var responseValue = httpMessage.Content.ReadAsStringAsync().Result;

        if (httpResponse.StatusCode == HttpStatusCode.OK)
        {
            RequestSuccess = true;
            Debug.Log(responseValue);
            salutSpeechRecognition = JsonSerializer.Deserialize<List<SalutSpeechRecognition>>(responseValue);
        }
        else
        {
            RequestSuccess = false;
            ErrorText = string.IsNullOrEmpty(responseValue) ? "See HttpResponse for more info" : responseValue;
        }
    }
}
