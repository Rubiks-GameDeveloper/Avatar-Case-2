using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class AudioCreateTask
{
    public AudioCreateTaskRequest lastRequest { get; private set; }
    public AudioCreateTaskResponse lastResponse { get; private set; }

    public async Task<AudioCreateTaskResponse> SendCreateTaskRequest(string accessToken, string requestFileId, AudioCodec codec, string language,
        bool profanityFilter, int hypothesesCount)
    {
        AudioCreateTaskRequest request = null;
        request = new AudioCreateTaskRequest(accessToken, requestFileId, codec, language, profanityFilter,
            hypothesesCount);

        lastRequest = request;
        
        return await SendCreateTaskRequestToServer(request);
    }

    private async Task<AudioCreateTaskResponse> SendCreateTaskRequestToServer(AudioCreateTaskRequest request)
    {
        HttpClient client = new HttpClient();
        
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + request.accessToken);

        var response = await client.PostAsync("https://smartspeech.sber.ru/rest/v1/speech:async_recognize",
            request.requestData);

        AudioCreateTaskResponse result = new AudioCreateTaskResponse(response);
        lastResponse = result;
        
        return result;
    }
}
