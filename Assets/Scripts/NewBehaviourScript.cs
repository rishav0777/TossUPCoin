using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
 
           public float swingAngle = 45f;
    public float swingSpeed = 2f;

    public float currentAngle = 45f;
    public int direction = 1;
    public UpandDown up;

    void Update()
    {

        
        // Update the angle and direction
        currentAngle += swingSpeed * direction * Time.deltaTime;
       


        if (currentAngle >= swingAngle || currentAngle <= -swingAngle)
        {
            direction *= -1;
        }

        Debug.Log(transform.rotation);
        // Rotate the object
       if(up.getflag()) transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
    }
}

