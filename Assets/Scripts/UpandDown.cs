using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class UpandDown : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public float moveDistance = 5.0f; // Distance to move back and forth
    public float moveSpeed = 2.0f;    // Speed of movement

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    public bool x, y, z;
    public bool flag, fl = true;
    public Sprite Hcoin,Tcoin,toss;
    public int k=0;

    private void OnEnable()
    {
        Debug.Log("choose coin ");
        k = Random.Range(0, 2);
        if (DataSaver1.Instance.TossMode == 0 && DataSaver1.Instance.master == 0)
        {
            photonView.RPC("PhotonOutput", RpcTarget.Others, k);
        }
        transform.gameObject.GetComponent<Image>().sprite = toss;
        initialPosition = transform.position;
        targetPosition = initialPosition + Vector3.up * moveDistance;
    }

    private void Start()
    {
       
       
    }

    private void Update()
    {
       // initialPosition = transform.position;
       // targetPosition = initialPosition + Vector3.right * moveDistance;
        float step = moveSpeed * Time.deltaTime;
        if(x)targetPosition.x = transform.position.x;
        if(y)targetPosition.y = transform.position.y;
        if (z) targetPosition.z = transform.position.z;
        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        // If the target position is reached, swap it with the initial position
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f && flag)
        {
            Vector3 temp = initialPosition;
            initialPosition = targetPosition;
            targetPosition = temp;
            flag = false;
        }
        else if (Vector3.Distance(transform.position, targetPosition) < 0.01f && !flag)
        {
            if (DataSaver1.Instance.TossMode == 0 && DataSaver1.Instance.master == 0)
            {
                k=DataSaver1.Instance.getcoinValue;
            }

            if (k==1)transform.gameObject.GetComponent<Image>().sprite = Tcoin;
            else transform.gameObject.GetComponent<Image>().sprite = Hcoin;
            fl = false;
            transform.rotation = Quaternion.identity;
            //Debug.Log("value of k " + k+" "+ DataSaver.Instance.getcoinValue);
            DataSaver1.Instance.getcoinValue = k;
           
            StartCoroutine(Result());
        }

    }
    public GameObject winner;
    public GameObject current;
    IEnumerator Result()
    {
        yield return new WaitForSeconds(2.0f);
        flag = true;fl = true;
        winner.SetActive(true);
        current.SetActive(false);
        
    }

    public bool getflag() { return fl; }
}
