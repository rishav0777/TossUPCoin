using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toandfro : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveDistance = 5.0f; // Distance to move back and forth
    public float moveSpeed = 2.0f;    // Speed of movement

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    public bool x, y, z;

    private void OnEnable()
    {

        initialPosition = transform.position;
        targetPosition = initialPosition + Vector3.right * moveDistance;
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
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            Vector3 temp = initialPosition;
            initialPosition = targetPosition;
            targetPosition = temp;
        }
    }
}
