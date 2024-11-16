using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Photon.Pun;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine.SceneManagement;

public class PhotonConnections1 : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject NextGameObject;
    [SerializeField] private GameObject CurrentGameObject;
    [SerializeField] private TextMeshProUGUI debug;
    private float stime;

    private bool gameFlag = false;
    private void Start()
    {
        stime=Time.time;
         DataSaver1.Instance.TossMode = 0;
        if (DataSaver1.Instance.TossMode == 0)
        {
            Debug.Log("Connecting to Photon Master Server");
            PhotonNetwork.ConnectUsingSettings();
            
        }
        else
        {
            StartCoroutine(JoinGame());
        }
    }
    private void Update()
    {
        if (gameFlag)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == PlayerPrefs.GetInt("PlayerCount"))
                StartCoroutine(JoinGame());
            else if((Time.time-stime)>10){
                DataSaver1.Instance.TossMode = 1;
                StartCoroutine(JoinGame());
            }
        }
        else if((Time.time-stime)>10){
             DataSaver1.Instance.TossMode = 1;
            StartCoroutine(JoinGame());
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server");
        PhotonNetwork.JoinLobby();// new Photon.Realtime.TypedLobby(DataSaver.Instance.contestId,Photon.Realtime.LobbyType.Default));
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Connected to Photon lobby");
        //debug.text = debug.text + "lobbby";
        PhotonNetwork.JoinRandomRoom();
        //PhotonNetwork.CreateRoom("rishav");
        base.OnJoinedLobby();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed");
        //debug.text = debug.text + "OnJoinRandomFailed";
        string RoomName = UnityEngine.Random.Range(11111, 99999).ToString();
        if (DataSaver1.Instance.GetRoomId() == "" || DataSaver1.Instance.GetRoomId() == null) { } else { RoomName = DataSaver1.Instance.GetRoomId(); }
        DataSaver1.Instance.SetRoomId(RoomName);
        PhotonNetwork.CreateRoom(RoomName);
        base.OnJoinRandomFailed(returnCode, message);
    }



    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom()");
        PhotonNetwork.JoinRandomRoom();
        base.OnCreatedRoom();
    }

    public override void OnJoinedRoom()
    {
        //debug.text = debug.text+"OnJoinedRoom()" + PhotonNetwork.CurrentRoom.Name + " " + PhotonNetwork.CurrentRoom.PlayerCount;
        Debug.Log("OnJoinedRoom()" + PhotonNetwork.CurrentRoom.Name + " " + PhotonNetwork.CurrentRoom.PlayerCount);
        gameFlag = true;

        base.OnJoinedRoom();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRoomFailed");
        //StartCoroutine(JoinGame());
        base.OnJoinRoomFailed(returnCode, message);
    }

    public void OnClick()
    {

    }



    IEnumerator JoinGame()
    {
        // Debug.Log("Join Game()");
        //contestJoining.ContestJoined();
        yield return new WaitForSeconds(3.0f);
        NextGameObject.SetActive(true);
        CurrentGameObject.SetActive(false);
    }





}
