using System;
using System.Collections;
using System.Collections.Generic;
using Api.Responses;
using InventorySystem.Item;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace Api.Services
{
    public class InventoryApi
    {
        public void SendGetAllUserItemsRequest(Action<Dictionary<ItemModel, int>> onSuccess, Action<string> onError)
        {
            var url = Endpoints.API_URL + Endpoints.GET_USER_ITEMS_URL;

            WebApi.Instance.StartCoroutine(SendGetItemsRequest(url, onSuccess, onError));
        }
        
        public void SendDeleteItemByIdRequest(int itemId, Action<string> onSuccess, Action<string> onError)
        {
            var url = Endpoints.API_URL + Endpoints.DELETE_ITEM_URL + itemId;

            WebApi.Instance.StartCoroutine(SendDeleteItemRequest(url, onSuccess, onError));
        }
        
        private IEnumerator SendGetItemsRequest(string url,
            Action<Dictionary<ItemModel, int>> onSuccess, Action<string> onError)
        {
            var webRequest = UnityWebRequest.Get(url);
            webRequest.SetRequestHeader("Authorization", $"Bearer {WebApi.Instance.JwtToken}");

            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                var message = $"{webRequest.error} : {webRequest.downloadHandler.text}";
                onError?.Invoke(message);
                yield break;
            }

            var jsonResponse = webRequest.downloadHandler.text;
            var response = JsonConvert.DeserializeObject<InventoryResponse>(jsonResponse);

            onSuccess?.Invoke(response.UserItems);
            webRequest.Dispose();
        }
        
        private IEnumerator SendDeleteItemRequest(string url, Action<string> onSuccess, Action<string> onError)
        {
            var webRequest = UnityWebRequest.Delete(url);
            webRequest.SetRequestHeader("Authorization", $"Bearer {WebApi.Instance.JwtToken}");
            webRequest.downloadHandler = new DownloadHandlerBuffer();

            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                var data = webRequest.downloadHandler.text;
                var inventoryResponse = JsonConvert.DeserializeObject<InventoryResponse>(data);
                var errorMessage = $"Request failed. Error: {inventoryResponse.Content}";
                onError?.Invoke(errorMessage);
                yield break;
            }
            
            var jsonResponse = webRequest.downloadHandler.text;
            var response = JsonConvert.DeserializeObject<InventoryResponse>(jsonResponse);

            onSuccess?.Invoke(response.Content);
            webRequest.Dispose();
        }
    }
}