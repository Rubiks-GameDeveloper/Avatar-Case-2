using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Net.Http;
using UnityEngine;
using GigaChatAdapter;
using GigaChatAdapter.Auth;

public class GigaChatTest : MonoBehaviour
{
    private Completion _completion;
    // Start is called before the first frame update
    void Start()
    {
        _completion = new Completion();
        
        //Request();
    }

    private async void Request()
    {
        string _authData = "OGVlZWNjYTMtNGUwOC00MGZmLTkwOTMtMDJiNmVlMjEwMzFkOjNmMzAyMzA5LTZhN2UtNDhlMi04YTBmLWZhMTg4NzI0ZjA1NQ==";
        Authorization auth = new Authorization(_authData, RateScope.GIGACHAT_API_PERS);

        var autoResult = await auth.SendRequest();
        Debug.Log(autoResult.AuthorizationSuccess);

        await auth.UpdateToken();

        if (autoResult.AuthorizationSuccess)
        {
            var completion = new Completion();

            var prompt = "В чём выгода или польза интерактивного аватара городской среды Лиса - любителя природы?";

            await auth.UpdateToken();

            var result =
                await completion.SendRequest(auth.LastResponse.GigaChatAuthorizationResponse?.AccessToken, prompt);

            if (result.RequestSuccessed)
            {
                Debug.Log(result.GigaChatCompletionResponse.Choices.LastOrDefault().Message.Content);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
