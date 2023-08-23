using System.Collections;
using UnityEngine;
using Newtonsoft.Json;
using System;
using UnityEngine.Networking;

namespace WeatherForeCasting
{
    public class CommHandler : MonoBehaviour
    {
        private bool deserializationFailed = false;

        public bool DeserializationFailed { get => deserializationFailed;}

        public T Deserialize<T>(object obj)
        {
            try
            {
                deserializationFailed = false;
                return JsonConvert.DeserializeObject<T>(obj.ToString());
            }
            catch (Exception ex)
            {
                deserializationFailed = true;
                Debug.Log("!!!!!!!!!Error Occured while deserialization!!!!!!!!!");
                Debug.Log("Reason " + ex.Message);
                return default(T);
            }
        }

        public IEnumerator GetJsonData(string url, Action<string> callback)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("Error while fetching JSON data: " + webRequest.error);
                    callback?.Invoke(null);
                }
                else
                {
                    callback?.Invoke(webRequest.downloadHandler.text);
                }
            }
        }
    }
}
