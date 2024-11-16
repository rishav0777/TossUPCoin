using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggleee : MonoBehaviour
{
    public GameObject image1;
    public GameObject image2;

    public void MakeToggle()
    {
        if (image1.activeInHierarchy)
        {
            image1.SetActive(false);
            image2.SetActive(true);

        }
        else if (image2.activeInHierarchy)
        {
            image2.SetActive(false);
            image1.SetActive(true);
        }
    }

    public void CheckToggle()
    {
        if (image1.activeInHierarchy)
        {
            image1.SetActive(false);

        }
        else if (!image1.activeInHierarchy)
        {
            image1.SetActive(true);

        }
    }


    public Sprite sprite1;
    public Sprite sprite2;

    public GameObject currency;
    public GameObject custom;

    public GameObject currencyP;
    public GameObject customP;
    public void SwitchButton(int val)
    {
        if (val == 0)
        {
            currency.GetComponent<Image>().sprite = sprite1;
            custom.GetComponent<Image>().sprite = sprite2;

            currencyP.SetActive(true);
            customP.SetActive(false);
        }
        else if (val == 1)
        {
            currency.GetComponent<Image>().sprite = sprite2;
            custom.GetComponent<Image>().sprite = sprite1;

            currencyP.SetActive(false);
            customP.SetActive(true);
        }

    }
   
}
