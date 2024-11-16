using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinToss : MonoBehaviour
{
    public float _rotate = 5f;
    public UpandDown up;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (up.getflag()) transform.Rotate(new Vector3(_rotate * Time.deltaTime, 0f, 0f));
        else
        {
            transform.rotation = Quaternion.identity;
            //DataSaver.Instance.getcoinValue = 1;
           
        }
    }
}
