using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splashAnimation1 : MonoBehaviour
{
    public GameObject objectToDeactivate;
    public GameObject objectToActivate;

    public float transitionDuration = 2.0f;

    private bool isTransitioning = false;
    public float finalScale = 1.0f;

    private void Start()
    {
        //if (isTransitioning) return;
        Debug.Log("splashAnimation Start");
        StartCoroutine(TransitionObjects());
    }
   

    private IEnumerator TransitionObjects()
    {
        isTransitioning = true;

        // Deactivate the objectToDeactivate
       // objectToDeactivate.SetActive(false);

        // Set the initial and final positions/scales for the animation
        Vector3 initialPosition = objectToActivate.transform.position;
        Vector3 finalPosition = objectToActivate.transform.position; // Change this to your desired final position
        float initialScale = 0.1f;
        

        float elapsedTime = 0;

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;

            // Interpolate position and scale
            objectToActivate.transform.position = Vector3.Lerp(initialPosition, finalPosition, t);
            objectToActivate.transform.localScale = Vector3.Lerp(Vector3.one * initialScale, Vector3.one * finalScale, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isTransitioning = false;
    }

}
