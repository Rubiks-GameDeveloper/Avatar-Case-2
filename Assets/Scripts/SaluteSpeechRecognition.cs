using System.Threading.Tasks;
using UnityEngine;
using Authorization = GigaChatAdapter.Authorization;

public class SaluteSpeechRecognition : MonoBehaviour
{
    // Start is called before the first frame update

    public async Task<string> SendAudioForRecognition(Authorization auth, string pathToFile)
    {
        await auth.SendRequest();
        
        //Debug.Log(auth.LastResponse.AuthorizationSuccess);
        
        if (!auth.LastResponse.AuthorizationSuccess) return auth.LastResponse.ErrorTextIfFailed;
        var accessToken = auth.LastResponse.GigaChatAuthorizationResponse?.AccessToken;
        if (accessToken == null) return auth.LastResponse.ErrorTextIfFailed;
        
        AudioSend sender = new AudioSend();
        await sender.SendRequest(accessToken, pathToFile);

        //Create task for recognition
        AudioCreateTask tasker = new AudioCreateTask();
        await tasker.SendCreateTaskRequest(accessToken,
            sender.lastResponse.salutSpeechFileIdResponse.result.requestFileId, AudioCodec.MP3, "ru-RU", false, 1);

        //Start loop for status checking
        var taskResponse = await CheckTaskStatus(tasker.lastResponse, accessToken);
        if (taskResponse.salutSpeechCreateTaskStatus.result.taskStatus != "DONE") return null;
        
        var recognition = new GetRecognitionResult();
        var recognitionResult = await recognition.SendRequestToGetOutput(accessToken,
            taskResponse.salutSpeechCreateTaskStatus.result.responseFileId);

        return recognitionResult.salutSpeechRecognition[0].results[0].normalizedText;
    }
    private async Task<AudioCreateTaskResponse> CheckTaskStatus(AudioCreateTaskResponse lastCheckResponse,
        string accessToken)
    {
        var newCheckResponse = lastCheckResponse;
        while (newCheckResponse.salutSpeechCreateTaskStatus.result.taskStatus != "DONE")
        {
            await Task.Delay(2000);
            //Debug.Log(newCheckResponse.salutSpeechCreateTaskStatus.result.taskStatus);
            //Debug.Log(newCheckResponse.salutSpeechCreateTaskStatus.result.erronInfo);
            global::CheckTaskStatus checkTaskStatus = new CheckTaskStatus();
            newCheckResponse = await checkTaskStatus.SendCheckStatusRequest(accessToken,
                newCheckResponse.salutSpeechCreateTaskStatus.result.taskId);
            if (newCheckResponse.salutSpeechCreateTaskStatus.result.taskStatus != "RUNNING")
            {
                break;
            }
        }

        //Debug.Log("Task done!");
        return newCheckResponse;
    }
}