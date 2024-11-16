using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class winner : MonoBehaviour
{
    public TextMeshProUGUI win, coin, choose, earn;
    public Image coinIMage;
    public Sprite Head, Tail;
    // Start is called before the first frame update
    void Start()
    {
      //  WinnerStatus();
    }
    private void OnEnable()
    {
        WinnerStatus();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void WinnerStatus()
    {
        Debug.Log("DataSaver.Instance.getcoinValue "+ DataSaver1.Instance.getcoinValue);
        if (DataSaver1.Instance.getcoinValue == 1) { coin.text = "TAil"; coinIMage.sprite = Tail; }
        else{ coin.text = "heads!"; coinIMage.sprite = Head; }
        if (DataSaver1.Instance.coinValue == 1) choose.text = "you had chosen TAil!";
        else choose.text = "you had chosen heads!";
        if (DataSaver1.Instance.coinValue == DataSaver1.Instance.getcoinValue)
        {
            win.text = "you have won!\ncongrats!!";
            earn.text = "you earned : " + DataSaver1.Instance.BetAmount.ToString();
        }
        else
        {
            win.text = "you have Lossed!\nBetter Luck nexttime";
            earn.text = "you lossed : " + DataSaver1.Instance.BetAmount.ToString();
        }
    }
}
