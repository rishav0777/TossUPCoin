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

public class cointossApi : MonoBehaviour
{
    private string url;
    private string flipcoin;
    private int price;

    public class User
    {
        public string _id { get; set; }
        public string userName { get; set; }
        public DateTime timestamp { get; set; }
        public int __v { get; set; }
        public string token { get; set; }
    }
    public class FlipCoinResult
    {
        public User userId { get; set; }
        public string selectFlipCoin { get; set; }
        public string selectPrice { get; set; }
        public int points { get; set; }
        public string result { get; set; }
        public string _id { get; set; }
        public DateTime timestamp { get; set; }
        public int __v { get; set; }
    }

    public class FlipCoinResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public FlipCoinResult result { get; set; }
    }


    public TopStatus topStatus;

    public void flipApi()
    {
        flipcoin = DataSaver1.Instance.selectedtoss;
        price=DataSaver1.Instance.selectedprice;
        url = StaticData.baseUrl + StaticData.cointossUrl;
        StartCoroutine(Registrations(url, flipcoin,price));
    }


    IEnumerator Registrations(string url, string flip,int price)
    {
        string jsonData = $"{{\"selectFlipCoin\": \"{flip}\",\"selectPrice\": \"{price}\"}}";

        if (!string.IsNullOrEmpty(jsonData))
        {
            using (UnityWebRequest request = UnityWebRequest.Post(url, ""))
            {
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
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
                        FlipCoinResponse data = JsonConvert.DeserializeObject<FlipCoinResponse>(json.ToString());
                        DataSaver1.Instance.resultCoin = data.result.result;
                        print("Successfully adduser " + data.message);
                        topStatus.GetmyHistory();
                    }
                }
                catch (Exception e)
                {
                    print(e);
                }
            }
        }




    }
}
