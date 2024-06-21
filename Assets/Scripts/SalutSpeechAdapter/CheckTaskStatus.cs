using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class CheckTaskStatus
{
    public async Task<AudioCreateTaskResponse> SendCheckStatusRequest(string accessToken, string taskId)
    {
        HttpClient client = new HttpClient();

        var url = $"https://smartspeech.sber.ru/rest/v1/task:get?id={taskId}";
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
        var res = await client.GetAsync(url);

        AudioCreateTaskResponse result = new AudioCreateTaskResponse(res);
        return result;
    }
}
