using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MakeToss : MonoBehaviour
{

    public GameObject midPanel;
    public TMP_InputField inputAmount;
    public TextMeshProUGUI _timer;
    public int _time = 15;
    public int tossChoosen { get; set; }
    public int amountChoosen { get; set; }

    public GameObject Coin;
    public GameObject hide1, hide2;
    private bool tossup = false;

    public cointossApi cointossApi;
    
    // Start is called before the first frame update
    void Start()
    {
        DataSaver1.Instance.selectedprice = 500;
        tossChoosen = -1;
        amountChoosen = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (tossup)
        {
            currentAngle += swingSpeed * direction * Time.deltaTime;
            Coin.transform.rotation = Quaternion.Euler(0f, currentAngle, 0f);
        }
    }

    public void MakeReady(int val)
    {
        if (val == 0) DataSaver1.Instance.selectedtoss = "Heads";
        else if(val==1) DataSaver1.Instance.selectedtoss = "Tails";
        tossChoosen = val;
        midPanel.SetActive(true);
        hide1.SetActive(true);
        hide2.SetActive(true);
        _time = 15;
        StartCoroutine(Timer());
    }

    public void ChooseAmount(int val)
    {
        amountChoosen = val;
        inputAmount.text = val.ToString();
        DataSaver1.Instance.selectedprice = val;
    }
    public void Confirm()
    {
        midPanel.SetActive(false);
        _time = 15;
        cointossApi.flipApi();
        StartCoroutine(Toss());
    }


    public float swingAngle = 45f;
    public float swingSpeed = 80f;

    public float currentAngle = 0f;
    public int direction = 1;

    
    
    IEnumerator Toss()
    {
        tossup = true;
        StartCoroutine(Flipping());
        yield return new WaitForSeconds(5f);
        
        tossup = false;
        Coin.transform.rotation = Quaternion.identity;
        if (DataSaver1.Instance.resultCoin == "Heads")
        {
            Coin.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "H";
        }
        else
        {
            Coin.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "T";
        }
        hide1.SetActive(false);
        hide2.SetActive(false);
        StopAllCoroutines();
    }

    IEnumerator Timer()
    {
        _timer.text = _time.ToString()+".00";
        yield return new WaitForSeconds(1);
        _time--;
        if (_time >= 0) StartCoroutine(Timer());
        else Confirm();
    }

    public float fliptime = 1f;
    IEnumerator Flipping()
    {
        if(Coin.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == "T")
            Coin.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "H";
        else Coin.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "T";
        yield return new WaitForSeconds(fliptime);
        if (tossup) StartCoroutine(Flipping());
        
    }




}
