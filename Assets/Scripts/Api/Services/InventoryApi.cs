using System;
using System.Collections;
using Api.Responses;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace Api.Services
{
    public class InventoryApi
    {
        public void SendGetAllUserItemsRequest(Action<InventoryResponse> onSuccess, Action<string> onError)
        {
            var url = Endpoints.API_URL + Endpoints.GET_USER_ITEMS_URL;

            var webRequest = UnityWebRequest.Get(url);

            WebApi.Instance.StartCoroutine(SendRequest(webRequest, onSuccess, onError));
        }

        public void SendDeleteItemByIdRequest(int itemId,
            Action<InventoryResponse> onSuccess, Action<string> onError)
        {
            var url = Endpoints.API_URL + Endpoints.DELETE_ITEM_URL + itemId;

            var webRequest = UnityWebRequest.Delete(url);
            webRequest.downloadHandler = new DownloadHandlerBuffer();

            WebApi.Instance.StartCoroutine(SendRequest(webRequest, onSuccess, onError));
        }

        private IEnumerator SendRequest(UnityWebRequest webRequest,
            Action<InventoryResponse> onSuccess, Action<string> onError)
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
            var response = JsonConvert.DeserializeObject<InventoryResponse>(jsonResponse);

            onSuccess?.Invoke(response);
            webRequest.Dispose();
        }
    }
}