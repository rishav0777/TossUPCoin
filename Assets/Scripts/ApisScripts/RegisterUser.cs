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

public class RegisterUser : MonoBehaviour
{
    private string url;
    private string userName;


    public class Result
    {
        public string userName { get; set; }
        public string _id { get; set; }
        public DateTime timestamp { get; set; }
        public int __v { get; set; }
        public string token { get; set; }
    }

    public class Response
    {
        public int status { get; set; }
        public string message { get; set; }
        public Result result { get; set; }
    }

    private void Start()
    {
        Register();
    }

    public void Register()
    {
        url = StaticData.baseUrl + StaticData.adduserUrl;
        userName = SystemInfo.deviceUniqueIdentifier;
        int val = UnityEngine.Random.Range(100000, 9999999);
        // userName = val.ToString();
        if (PlayerPrefs.HasKey("token"))
        {
            DataSaver1.Instance.token = PlayerPrefs.GetString("token");
        }
        else StartCoroutine(Registrations(url,userName));
       // StartCoroutine(Registrations(url, "kr"));
    }


    IEnumerator Registrations(string url, string balance)
    {
        string jsonData = $"{{\"userName\": \"{balance}\"}}";

        if (!string.IsNullOrEmpty(jsonData))
        {
            using (UnityWebRequest request = UnityWebRequest.Post(url, ""))
            {
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");
                yield return request.SendWebRequest();
                var response = request.result;
                try
                {
                    if (request.result != UnityWebRequest.Result.Success) Debug.Log(request.error);
                    else if (request.result == UnityWebRequest.Result.Success)
                    {
                        
                        var json = request.downloadHandler.text;
                        Debug.Log(json.ToString());
                        Response data = JsonConvert.DeserializeObject<Response>(json.ToString());
                        print("Successfully adduser " +data.message);

                        PlayerPrefs.SetString("token" , data.result.token);
                        DataSaver1.Instance.token = data.result.token;
                        DataSaver1.Instance._id = data.result._id;
                        
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
