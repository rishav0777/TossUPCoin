using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WhowillToss : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public GameObject tossOption;
   
    public int plate { get; set; }
    public GameObject nextGameobject;
    public GameObject CurrentGameobject;
    void Start()
    {
        if (DataSaver1.Instance.TossMode == 0)
        {
            if (PhotonNetwork.IsMasterClient) DataSaver1.Instance.master = 0;
            else DataSaver1.Instance.master = 1;
        }
        plate = -1;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DataSaver1.Instance.TossMode == 0)
        {
            if ((DataSaver1.Instance.master == 0 && PhotonNetwork.IsMasterClient) )
            {
                tossOption.SetActive(true);
            }
            else
            {
                tossOption.SetActive(false);
            }

            if (!PhotonNetwork.IsMasterClient && plate!=-1)
            {
                Debug.Log("move " + plate);
                DataSaver1.Instance.coinValue = 1-plate;
                plate = -1;
                Game();
            }
        }
    }

    public void Game()
    {
        nextGameobject.SetActive(true);
        CurrentGameobject.SetActive(true);
    }

    public void ChangeMaster(int mode)
    {
        DataSaver1.Instance.master = mode;
    }


}
