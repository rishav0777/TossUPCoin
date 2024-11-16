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

public class TopStatus : MonoBehaviour
{
    public class TossHistoryItem
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

    public class TossHistoryResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public List<TossHistoryItem> tossHistory { get; set; }
    }

    private TossHistoryResponse historyResponse;

    private string url;
    private List<GameObject> dataColl = new List<GameObject>();

    private void Start()
    {
        //GetmyHistory();
    }
    public Result result;
    public void GetmyHistory()
    {
        url = StaticData.baseUrl + StaticData.getcointosshistoryUrl;
        StartCoroutine(myRegistrations(url));
    }
    IEnumerator myRegistrations(string url)
    {
        Debug.Log("token " + DataSaver1.Instance.token);
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + DataSaver1.Instance.token);

            yield return request.SendWebRequest();
            var response = request.result;
            try
            {
                if (request.result != UnityWebRequest.Result.Success) setMyData();
                else if (request.result == UnityWebRequest.Result.Success)
                {
                    var json = request.downloadHandler.text;
                    Debug.Log("json" + json.ToString());

                    historyResponse = JsonConvert.DeserializeObject<TossHistoryResponse>(json.ToString());
                    Debug.Log("message " + historyResponse.message);
                    setMyData();
                    
                }
            }
            catch (Exception e)
            {
                print("exception " + e);
            }
        }
    }



    public GameObject DataPrefabH, DataPrefabT;
    public GameObject DataHolder;
    public GameObject resultPanel;
    public void setMyData()
    {
        for (int j = 0; j < dataColl.Count; j++) Destroy(dataColl[j]);
        for (int j = 0; j < historyResponse.tossHistory.Count; j++)
        {
            GameObject data;
            if (historyResponse.tossHistory[j].result.Substring(0,1)=="H")
               data = GameObject.Instantiate(DataPrefabH);
            else data = GameObject.Instantiate(DataPrefabT);
            data.transform.SetParent(DataHolder.transform);
            data.transform.localScale = new Vector3(0.1789588f, 1.636992f, 0.39009f);
            data.transform.SetAsFirstSibling();

            
            Invoke("show", 4f);
            dataColl.Add(data);
        }
    }

    public void show()
    {
        resultPanel.SetActive(true);
        result.SetResultData(historyResponse.tossHistory[historyResponse.tossHistory.Count-1].result,
            historyResponse.tossHistory[historyResponse.tossHistory.Count - 1].selectFlipCoin, 
            historyResponse.tossHistory[historyResponse.tossHistory.Count - 1].selectPrice);
    }
}

