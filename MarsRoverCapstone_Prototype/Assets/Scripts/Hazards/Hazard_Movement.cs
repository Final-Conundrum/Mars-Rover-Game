using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hazard_Movement : MonoBehaviour
{
    /* Make the Hazard travel around a zone to emulate a 'dust storm' 
     * Rotate the object while moving it along a set path. 
     * 
     * Come back and randomise the spawn points for storm path and then iterate list to move from point to point. 
     */




    GameObject hazard;
    public GameObject checkPointPrefab;
    public List<GameObject> points = new List<GameObject>();

    public int rotationSpeed;
    public float moveSpeed;
    public int pointsToSpawn;
    int min, max;
    
    Coroutine moveIE;
  



    // Start is called before the first frame update
    void Start()
    {
        hazard = this.gameObject;
        moveSpeed = 5.0f;
        PlacePoints();
        StartCoroutine(MoveObject());
        
    }

    void PlacePoints()
    {
        for(int i = 0; i < pointsToSpawn; i++)
        {
            Instantiate(checkPointPrefab, GeneratePoints(), Quaternion.identity);
            points.Add(checkPointPrefab);
            Debug.Log(points.Count);
        }
    }

    Vector3 GeneratePoints()
    {
        int x, y, z;
        x = Random.Range(min = -20, max = 20);
        y = 1;
        z = Random.Range(min = -30, max = 30);
        return new Vector3(x, y, z);

    }

    // Update is called once per frame
    void Update()
    {
   
    }

    IEnumerator MoveObject()
    {
        GameObject[] pointArray = points.ToArray();

        for (int i = 0; i < pointArray.Length; i++)
        {
            moveIE = StartCoroutine(Moving(i));
            Debug.Log("coroutine started!");
            yield return moveIE;
        }
    }

    IEnumerator Moving(int currentPos)
    {
        GameObject[] pointArray = points.ToArray();

        while (hazard.transform.position != pointArray[currentPos].transform.position)
        {
            hazard.transform.position = Vector3.MoveTowards(hazard.transform.position, pointArray[0].transform.position, moveSpeed * Time.deltaTime);
        }
        yield return null;
    }






    //OLD CODE - DOES NOT WORK 

    void GetNextTarget()
    {
        //int nextTarget = Random.Range(0, checkPoints.Length);
        //nextTargetPos = checkPoints[nextTarget];
    }

    //the 'zone' will be made up of multiple checkpoints. the hazard will move between these points at random.
    void MoveTowardsPoints()
    {
        
        //OLD CODE BELOW
        //foreach (Transform point in checkPoints)
        //{
            
        //    moveSpeed = moveSpeed * Time.deltaTime;
        //    hazard.transform.position = Vector3.MoveTowards(transform.position, nextTargetPos.position, moveSpeed);

        //    if (point.gameObject.tag == "hazardPoints")
        //    {
        //        Debug.Log("We made it here");
        //        GetNextTarget();
        //    }
        }
    }


    



