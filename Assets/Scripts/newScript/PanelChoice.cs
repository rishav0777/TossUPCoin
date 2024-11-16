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

public class PanelChoice : MonoBehaviour
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


    public GameObject everyone, my;
    public GameObject DataPrefab;
    public GameObject DataHolder;
    public GameObject noRecord;

    private List<GameObject> dataColl = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GetHistory();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChoosePanel(int val)
    {
        if (val == 0)
        {
            everyone.transform.GetChild(0).gameObject.SetActive(true);
            my.transform.GetChild(0).gameObject.SetActive(false);
            everyone.GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 255);
            my.GetComponent<TextMeshProUGUI>().color = new Color32(96, 96, 96, 255);
            GetHistory();
        }
        else if (val == 1)
        {
            everyone.transform.GetChild(0).gameObject.SetActive(false);
            my.transform.GetChild(0).gameObject.SetActive(true);
            everyone.GetComponent<TextMeshProUGUI>().color = new Color32(96, 96, 96, 255);
            my.GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 255);
            GetmyHistory();
        }
    }

    public void setData()
    {
        for (int j = 0; j < dataColl.Count; j++) Destroy(dataColl[j]);
        noRecord.SetActive(false);
        for (int j = 0; j < mydata.toss.Count; j++)
        {
            GameObject data = GameObject.Instantiate(DataPrefab);
            data.transform.SetParent(DataHolder.transform);
            data.transform.localScale = new Vector3(1, 1, 1);
            data.GetComponent<DataHolder>().SetDataValues
                (mydata.toss[j]._id, mydata.toss[j].selectFlipCoin.Substring(0, 1),
                mydata.toss[j].points.ToString(), mydata.toss[j].result.Substring(0, 1), mydata.toss[j].selectPrice.ToString());
            dataColl.Add(data);

        }
        if (mydata.toss.Count == 0)
        {
            for (int j = 0; j < dataColl.Count; j++) dataColl[j].SetActive(false);
            noRecord.SetActive(true);
        }
    }









    private string url;
    private TossResponse mydata;
    public void GetHistory()
    {
        url = StaticData.baseUrl + StaticData.lastTenTossResultUrl;
        StartCoroutine(Registrations(url));
    }

    public void GetmyHistory()
    {
        url = StaticData.baseUrl + StaticData.getcointosshistoryUrl;
        StartCoroutine(myRegistrations(url));
    }

    IEnumerator Registrations(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
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
                    Debug.Log("json" + json.ToString());

                    mydata = JsonConvert.DeserializeObject<TossResponse>(json.ToString());
                    Debug.Log("message " + mydata.message + " " + mydata.toss[0]._id);
                    setData();
                }
            }
            catch (Exception e)
            {
                print("exception " + e);
            }
        }

    }







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

    
    public void setMyData()
    {
        for (int j = 0; j < dataColl.Count; j++) Destroy(dataColl[j]);
        noRecord.SetActive(false);
        for (int j = 0; j < historyResponse.tossHistory.Count; j++)
        {
            GameObject data = GameObject.Instantiate(DataPrefab);
            data.transform.SetParent(DataHolder.transform);
            data.transform.localScale = new Vector3(1, 1, 1);
            
            data.GetComponent<DataHolder>().SetDataValues
                (historyResponse.tossHistory[j]._id, historyResponse.tossHistory[j].selectFlipCoin.Substring(0, 1),
                historyResponse.tossHistory[j].points.ToString(), historyResponse.tossHistory[j].result.Substring(0, 1),
                historyResponse.tossHistory[j].selectPrice.ToString());
            dataColl.Add(data);



        }
        if (historyResponse.tossHistory.Count == 0)
        {
            for (int j = 0; j < dataColl.Count; j++) dataColl[j].SetActive(false);
            noRecord.SetActive(true);
        }
    }
}
