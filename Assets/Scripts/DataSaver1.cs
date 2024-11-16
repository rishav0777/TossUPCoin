using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaver1 : MonoBehaviour
{
    private static DataSaver1 instance;

    public static DataSaver1 Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public string roomId="";
    public int playerCount=0;
    public int mode = 0;
    public string prefabid="id";
    public string prefabfirstName = "firstName";
    public string prefablastName = "lastName";
    public string prefabToken = "token";

    public string _id { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string contestIdJoined { get; set; }
    public string token { get; set; }
    public int deposite { get; set; }
    public int bonus { get; set; }
    public int firstPrize { get; set; }
    public int secondPrize { get; set; }
    public int thirdPrize { get; set; }
    public string contestId { get; set; }
    public int entryFee { get; set; }


    public  string GetRoomId() { return roomId; }
    public void SetRoomId(string id) { roomId = id; }
    public int GetPlayerCount() { return playerCount; }
    public void SetPlayerCount(int p) { playerCount = p; }
    public int GetMode() { return mode; }
    public void SetMode(int p) { mode = p; }




    public int coinValue { get; set; }
    public int getcoinValue { get; set; }
    public int BetAmount { get; set; }
    public int TossMode { get; set; }
    public int master { get; set; }

    public string selectedtoss { get; set; }
    public int selectedprice { get; set; }
    public string resultCoin { get; set; }
}
