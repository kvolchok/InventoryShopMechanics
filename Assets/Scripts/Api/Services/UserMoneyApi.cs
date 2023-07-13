using System;
using System.Collections;
using System.Text;
using Api.Requests;
using Api.Responses;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace Api.Services
{
    public class UserMoneyApi
    {
        public void SendAddMoneyRequest(int amount, Action<UserMoneyResponse> onSuccess, Action<string> onError)
        {
            var url = Endpoints.API_URL + Endpoints.ADD_MONEY_URL;

            var userMoneyRequest = new UserMoneyRequest
            {
                Amount = amount
            };

            WebApi.Instance.StartCoroutine(SendRequest(url, userMoneyRequest, onSuccess, onError));
        }
        
        private IEnumerator SendRequest(string url, UserMoneyRequest request,
            Action<UserMoneyResponse> onSuccess, Action<string> onError)
        {
            var jsonRequest = JsonConvert.SerializeObject(request);
            
            var webRequest = UnityWebRequest.Post(url, jsonRequest);
            webRequest.SetRequestHeader("Authorization", $"Bearer {WebApi.Instance.JwtToken}");
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.uploadHandler = new UploadHandlerRaw(Encoding.ASCII.GetBytes(jsonRequest));

            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                if (webRequest.downloadHandler.data == null)
                {
                    onError?.Invoke(webRequest.error);
                    yield break;
                }

                var message = Encoding.ASCII.GetString(webRequest.downloadHandler.data);
                onError?.Invoke(message);
                yield break;
            }

            var jsonResponse = webRequest.downloadHandler.text;
            var response = JsonConvert.DeserializeObject<UserMoneyResponse>(jsonResponse);
            
            onSuccess?.Invoke(response);
            webRequest.Dispose();
        }
    }
}