using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataHolder : MonoBehaviour
{
    public GameObject id, select, point, result, amount;


    private void Start()
    {
       /* id = GameObject.Find("id");
        select = GameObject.Find("select");
        result = GameObject.Find("'result");
        point = GameObject.Find("point");
        amount = GameObject.Find("amount");*/

    }

    public void SetDataValues(string i,string s,string p,string re,string am)
    {
        id.GetComponent<TextMeshProUGUI>().text = i.Substring(0,11);
        select.GetComponent<TextMeshProUGUI>().text = s;
        point.GetComponent<TextMeshProUGUI>().text = p;
        result.GetComponent<TextMeshProUGUI>().text = re;
        amount.GetComponent<TextMeshProUGUI>().text = am;
        if (s != re) amount.GetComponent<TextMeshProUGUI>().text = "-"+am;
    }

}
