using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class TossupPhotonData : MonoBehaviourPunCallbacks
{
    public WhowillToss whowill;
    // Start is called before the first frame update
    [PunRPC]
    void PhotonPlate(int step)
    {
        Debug.Log("photonplate "+step);
        whowill.plate = step;
    }


    [PunRPC]
    void PhotonOutput(int step)
    {
        Debug.Log("photonoutput " + step);
        DataSaver1.Instance.getcoinValue = step;
    }
}
