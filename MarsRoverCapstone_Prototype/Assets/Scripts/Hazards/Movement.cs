using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
   
    public Vector3 newPosition;
    public GameObject positionMarker;
    public GameObject positionMarkerB;

    void Start()
    {

        newPosition = positionMarker.transform.position;
        StartCoroutine(LerpPosition(newPosition, 15));
       
    }

    IEnumerator LerpPosition(Vector3 targetPos, float duration)
    {
        float time = 0;
        Vector3 startPos = positionMarkerB.transform.position;

        while(time < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, time / duration);
            time += Time.deltaTime;

            if (time >= duration)
            {
                StartCoroutine(LerpPosition(newPosition, 15));
            }
            yield return 1;
        }

       

        transform.position = targetPos;
    }


    void Update()
    {

        

    }

   
}
