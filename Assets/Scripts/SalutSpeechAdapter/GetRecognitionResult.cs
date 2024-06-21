using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class GetRecognitionResult
{
    public string accessToken { get; private set; }
    public string requestFileId { get; private set; }
    
    public async Task<GetRecognitionResultResponse> SendRequestToGetOutput(string accessToken, string requestFileId)
    {
        this.accessToken = accessToken;
        this.requestFileId = requestFileId;

        HttpClient client = new HttpClient();
        
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
        var result =
           await client.GetAsync("https://smartspeech.sber.ru/rest/v1/data:download?response_file_id=" + requestFileId);

        GetRecognitionResultResponse response = new GetRecognitionResultResponse(result);

        return response;
    }

}
