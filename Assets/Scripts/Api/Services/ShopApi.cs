using System;
using System.Collections;
using System.Collections.Generic;
using Api.Responses;
using InventorySystem.Item;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace Api.Services
{
    public class ShopApi
    {
        public void SendGetAllGameItemsRequest(Action<List<ItemModel>> onSuccess, Action<string> onError)
        {
            var url = Endpoints.API_URL + Endpoints.GET_ALL_GAME_ITEMS_URL;

            WebApi.Instance.StartCoroutine(SendGetItemsRequest(url, onSuccess, onError));
        }

        public void SendTryBuyItemRequest(int itemId, Action<ItemModel, string> onSuccess, Action<string> onError)
        {
            var url = Endpoints.API_URL + Endpoints.BUY_ITEM_URL + itemId;

            WebApi.Instance.StartCoroutine(SendBuyItemRequest(url, onSuccess, onError));
        }
        
        private IEnumerator SendGetItemsRequest(string url,
            Action<List<ItemModel>> onSuccess, Action<string> onError)
        {
            var webRequest = UnityWebRequest.Get(url);
            webRequest.SetRequestHeader("Authorization", $"Bearer {WebApi.Instance.JwtToken}");

            yield return webRequest.SendWebRequest();

            if (!IsRequestResultSuccess(webRequest, onError, out var response))
            {
                yield break;
            }

            onSuccess?.Invoke(response.GameItems);
            webRequest.Dispose();
        }

        private IEnumerator SendBuyItemRequest(string url,
            Action<ItemModel, string> onSuccess, Action<string> onError)
        {
            var webRequest = UnityWebRequest.Put(url, "");
            webRequest.SetRequestHeader("Authorization", $"Bearer {WebApi.Instance.JwtToken}");

            yield return webRequest.SendWebRequest();

            if (!IsRequestResultSuccess(webRequest, onError, out var response))
            {
                yield break;
            }
            
            onSuccess?.Invoke(response.Item, response.Content);
            webRequest.Dispose();
        }
        
        private bool IsRequestResultSuccess(UnityWebRequest webRequest, Action<string> onError,
            out ShopResponse response)
        {
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                var message = $"{webRequest.error} : {webRequest.downloadHandler.text}";
                onError?.Invoke(message);
                response = null;
                return false;
            }

            var jsonResponse = webRequest.downloadHandler.text;
            response = JsonConvert.DeserializeObject<ShopResponse>(jsonResponse);
            return true;
        }
    }
}