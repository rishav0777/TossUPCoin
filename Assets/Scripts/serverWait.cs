using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class serverWait : MonoBehaviour
{
    public float _rotate = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, _rotate * Time.deltaTime));
    }
}
