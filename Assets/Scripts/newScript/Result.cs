using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Result : MonoBehaviour
{

    public TextMeshProUGUI cointext;
    public TextMeshProUGUI coinvalue;
    public TextMeshProUGUI coinchoose;
    public TextMeshProUGUI amount;
    public TextMeshProUGUI status;

   public void SetResultData(string cr,string cc,string am)
    {
        coinvalue.text = cr.Substring(0,1);
        cointext.text = cr;
        coinchoose.text = "'You had choosen " + cc;
        if (cr == cc)
        {
            status.text = "You Win";
            amount.text = "You earned " + am;
        }
        else
        {
            status.text = "You Lose";
            amount.text = "You losed " + am;
        }
    }
}
