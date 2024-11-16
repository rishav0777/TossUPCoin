using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.Text.RegularExpressions;
using System;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class CoinTossHistory : MonoBehaviour
{

    public class TossResult
    {
        public string _id { get; set; }
        public string userId { get; set; }
        public string selectFlipCoin { get; set; }
        public string selectPrice { get; set; }
        public int points { get; set; }
        public string result { get; set; }
        public DateTime timestamp { get; set; }
        public int __v { get; set; }
    }

    public class TossResponse
    {
        public string message { get; set; }
        public List<TossResult> toss { get; set; }
    }




   

    private string url;
    public void GetHistory()
    {
        url = StaticData.baseUrl + StaticData.lastTenTossResultUrl;
        StartCoroutine(Registrations(url));
    }

    IEnumerator Registrations(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            /* byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

             request.uploadHandler = new UploadHandlerRaw(bodyRaw);*/
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + DataSaver1.Instance.token);

            yield return request.SendWebRequest();
            var response = request.result;
            try
            {
                if (request.result != UnityWebRequest.Result.Success) Debug.Log(request.error);
                else if (request.result == UnityWebRequest.Result.Success)
                {
                    var json = request.downloadHandler.text;
                    Debug.Log(json.ToString());

                    TossResponse data = JsonConvert.DeserializeObject<TossResponse>(json.ToString());
                   
                }
            }
            catch (Exception e)
            {
                print("exception " + e);
            }
        }

    }


    


}
