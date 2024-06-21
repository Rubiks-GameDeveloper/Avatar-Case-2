using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using GigaChatAdapter;
using GigaChatAdapter.Auth;
using GigaChatAdapter.Completions;
using jp.gulti.ColorBlind;
using NAudio;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using YaCloudKit.TTS;
using Authorization = GigaChatAdapter.Authorization;
using Directory = System.IO.Directory;
using File = System.IO.File;
using JsonSerializer = System.Text.Json.JsonSerializer;

public class AvatarGigachatContainer : MonoBehaviour
{
    [SerializeField] private List<AvatarVariations> avatars;
    [SerializeField] private AvatarType avatarType;
    [SerializeField] private ColorBlindnessSimulator colorBlindnessSimulator;

    [SerializeField] private SaluteSpeechRecognition recognition;

    [SerializeField] private string startRequest;
    private List<GigaChatMessage> history = new List<GigaChatMessage>();
    private AvatarVariations avatar;
    [SerializeField] private TextMeshProUGUI gigachatAnswer;

    private Authorization authGigachat;
    private AuthorizationResponse authResponseGigachat;

    [SerializeField] private Color stopColor;
    [SerializeField] private Color startColor;

    [SerializeField] private Button stopButton;
    [SerializeField] private Image circle;
    [SerializeField] private GameObject stopIcon;
    [SerializeField] private GameObject startIcon;

    private AudioSource audioSource;
    private void Start()
    {
        Application.targetFrameRate = 120;
        
        recognition = GetComponent<SaluteSpeechRecognition>();
        audioSource = GetComponent<AudioSource>();
        
        avatar = avatars.Find(a => a.avatarType == avatarType);

        avatar.history = new List<GigaChatMessage>();
        
        InitializeAuthentication();
    }
    public void StartSession()
    {
        Request(startRequest);
    }
    public void EndSession()
    {
        avatar.history.Clear();
        audioSource.Stop();
    }
    private async void InitializeAuthentication()
    {
        authGigachat = new Authorization("OGVlZWNjYTMtNGUwOC00MGZmLTkwOTMtMDJiNmVlMjEwMzFkOmEzODc4ZDNlLTlhZTYtNDIyMi1hNmNlLTg1Yjg5YTAwNGVkZQ==", RateScope.GIGACHAT_API_PERS);
        authResponseGigachat = await authGigachat.SendRequest();
    }
    public async void StartTextRecognition()
    {

        Authorization auth = new Authorization("MzM0NTU1YWUtNDNjNC00OGVkLTkyMWUtYjkzMzE4NjRiZDVmOmNmNDY2MDNlLTZhNDMtNGM2Yi05YzcyLWYxMTEzMjM5NDQ2OA==", RateScope.SALUTE_SPEECH_PERS, Guid.NewGuid());
        
        await auth.SendRequest();
        
        var resultText = await recognition.SendAudioForRecognition(auth, Application.dataPath + "/RecordedAudio/recordedAudio.wav");
        
        Request(resultText);
    }

    public void ChangeAccessibility()
    {
        if (colorBlindnessSimulator.BlindIntensity == 0)
            colorBlindnessSimulator.BlindIntensity = 1;
        else
            colorBlindnessSimulator.BlindIntensity = 0;
    }
    private async void TTSRequest(string text)
    {
        //string oauthToken = "y0_AgAAAAAWpxnxAATuwQAAAAD-cJMnAABoSHslzSJOJbJILH9bx6dRiDm6EA";
        //string apiUrl = "https://iam.api.cloud.yandex.net/iam/v1/tokens";

        // // Создаем HttpClient
        // using (var httpClient = new HttpClient())
        // {
        //     // Подготавливаем данные для отправки
        //     var postData = new
        //     {
        //         yandexPassportOauthToken = oauthToken
        //     };
        //
        //     // Сериализуем данные в формат JSON
        //     var json = Newtonsoft.Json.JsonConvert.SerializeObject(postData);
        //
        //     // Отправляем POST запрос
        //     var content = new StringContent(json, Encoding.UTF8, "application/json");
        //     var response = await httpClient.PostAsync(apiUrl, content);
        //
        //     // Проверяем успешность запроса
        //     if (response.IsSuccessStatusCode)
        //     {
        //         // Получаем ответ в виде строки
        //         IEnumerable<string> responseString = response.Headers.First().Value;;
        //
        //         //var token = JsonSerializer.Deserialize<YandexCloudTokenResponse>(responseString);
        //
        //         string iamToken = null;
        //         foreach (var part in responseString)
        //         {
        //             iamToken += part;
        //         }
        //         
        //         Debug.Log(iamToken);
        //     }
        //     else
        //     {
        //         Debug.Log($"Ошибка: {response.StatusCode}");
        //     }
        // }

        IYandexTts client = new YandexTtsClient(new YandexTtsConfig("t1.9euelZrHlpKalJKVkM-Ly8rNkZSRke3rnpWai8mamcvJmZTLjMySmoyMkY3l8_cJcHVP-e9JQkVP_d3z90kec0_570lCRU_9zef1656Vmo6cjp7IjonIjo-ZzMmMkJ2K7_zF656Vmo6cjp7IjonIjo-ZzMmMkJ2K.7FC9Np2AP8aFKxtL2U2uNAVB676XPL3jn25iLomu2ZpKo6ghgh5ISX_NoUZyMrBruiFkGr2oa_JkXMbu0wrbDQ", "b1g7cf9s7mspiqlruvqm") { LoggingEnabled = true });

        VoiceParameters voiceParameters = new VoiceParameters(VoiceName.Zahar);
        voiceParameters.Emotion = VoiceEmotion.Good;
        voiceParameters.Speed = "1.2";
        voiceParameters.Language = VoiceLanguage.Russian;

        var result = await client.TextToSpeechAsync(text, voiceParameters, AudioFormat.Mp3);

        Directory.CreateDirectory(Application.dataPath + "/TTSYandex");

        var path = Path.Combine(Application.dataPath, "TTSYandex");
        
        await File.WriteAllBytesAsync(path + "/speech.mp3", result.Content);
        
        StartCoroutine(ConvertMP3ToAudioCLipAndPlay(path));
    }
    private IEnumerator ConvertMP3ToAudioCLipAndPlay(string path)
    {
        WWW www = new WWW("file://" + path + "/speech.mp3");
        yield return www;

        AudioClip clip = www.GetAudioClip(false, false, AudioType.MPEG);

        audioSource.Stop();
        
        audioSource.clip = clip;
        
        audioSource.Play();
    }
    private async void Request(string prompt)
    {
        await authGigachat.UpdateToken();
        
        Debug.Log(prompt);
        
        if (authResponseGigachat.AuthorizationSuccess)
        {
            var completion = new Completion();
            
            completion.History.AddRange(avatar.history);
            foreach (var item in completion.History)
            {
                Debug.Log(item.Content);
            }

            CompletionSettings set = new CompletionSettings("GigaChat-Pro", 1.5f, 0, 1, 1024);

            var result =
                await completion.SendRequest(authResponseGigachat.GigaChatAuthorizationResponse?.AccessToken, prompt, true, set);

            if (result.RequestSuccessed)
            {
                var res = result.GigaChatCompletionResponse?.Choices.First().Message.Content;
                // if (res!.Contains('<'))
                //     res = res.Remove(res.IndexOf('<'), res.IndexOf('>'));
                avatar.history.Add(result.GigaChatCompletionResponse?.Choices.First().Message);
                gigachatAnswer.text = res;
                
                TTSRequest(gigachatAnswer.text);
            }
        }
    }
}
