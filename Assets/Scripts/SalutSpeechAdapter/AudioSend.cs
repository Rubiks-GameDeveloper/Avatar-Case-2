using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GigaChatAdapter;
using UnityEngine;

public class AudioSend
{
    public AudioSendRequest lastRequest { get; private set; }
    
    public AudioSendResponse lastResponse { get; private set; }

    public async Task<AudioSendResponse> SendRequest(string accessToken, string filePath)
    {
        AudioSendRequest request = null;

        request = new AudioSendRequest(accessToken, filePath);

        lastRequest = request;
        return await SendRequestToServer(request);
    }

    private async Task<AudioSendResponse> SendRequestToServer(AudioSendRequest sendRequest)
    {
        HttpClient client = new HttpClient();
        
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer { sendRequest.accessToken }");

        var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(sendRequest.pathContent));
        //fileContent.Headers.ContentType = new MediaTypeHeaderValue("audio/x-pcm;bit=16;rate=XXX");

        //Debug.Log(fileContent.ReadAsByteArrayAsync().Result[0] + fileContent.ReadAsByteArrayAsync().Result[1] + fileContent.ReadAsByteArrayAsync().Result[2]); ;
        
        var response = await client.PostAsync("https://smartspeech.sber.ru/rest/v1/data:upload", fileContent);

        AudioSendResponse result = new AudioSendResponse(response);
        
        //Debug.Log(result.ErrorText);
        
        lastResponse = result;

        return result;
    }
}
