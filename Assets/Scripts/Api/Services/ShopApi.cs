using System;
using System.Collections;
using Api.Responses;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace Api.Services
{
    public class ShopApi
    {
        public void SendGetAllGameItemsRequest(Action<ShopResponse> onSuccess, Action<string> onError)
        {
            var url = Endpoints.API_URL + Endpoints.GET_ALL_GAME_ITEMS_URL;
            
            var webRequest = UnityWebRequest.Get(url);

            WebApi.Instance.StartCoroutine(SendRequest(webRequest, onSuccess, onError));
        }

        public void SendTryBuyItemRequest(int itemId, Action<ShopResponse> onSuccess, Action<string> onError)
        {
            var url = Endpoints.API_URL + Endpoints.BUY_ITEM_URL + itemId;
            
            var webRequest = UnityWebRequest.Put(url, "");

            WebApi.Instance.StartCoroutine(SendRequest(webRequest, onSuccess, onError));
        }
        
        private IEnumerator SendRequest(UnityWebRequest webRequest,
            Action<ShopResponse> onSuccess, Action<string> onError)
        {
            webRequest.SetRequestHeader("Authorization", $"Bearer {WebApi.Instance.JwtToken}");

            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                var message = $"{webRequest.downloadHandler.text}";
                onError?.Invoke(message);
                yield break;
            }

            var jsonResponse = webRequest.downloadHandler.text;
            var response = JsonConvert.DeserializeObject<ShopResponse>(jsonResponse);

            onSuccess?.Invoke(response);
            webRequest.Dispose();
        }
    } 
}