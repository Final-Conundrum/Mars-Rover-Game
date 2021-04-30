using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_MovingObject : MonoBehaviour
{
    // Transform positions object travels between
    public Transform pointA;
    public Transform pointB;

    public bool delay = false;
    [SerializeField] private string currentTarget;

    // Attributes
    public float speed;
    public float delayTimer;

    private float addTime;

    // Start is called before the first frame update
    void Start()
    {
        if (currentTarget == "")
        {
            currentTarget = "A";
        }

        addTime = delayTimer;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        // Choose direction to move towards
        switch (currentTarget)
        {
            case "A":
                GoToPos(pointA);
                break;
            case "B":
                GoToPos(pointB);
                break;
        }
    }

    public virtual void GoToPos(Transform target)
    {
        if (transform.position != target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // Set timer
            delayTimer = Time.time + addTime;
        }
        else
        {
            switch (currentTarget)
            {
                case "A":
                    // Setup delay so that platform doesn't automatically change direction
                    if (Time.time > delayTimer && delay == true)
                    {
                        currentTarget = "B";
                    }
                    else if (delay == false)
                    {
                        currentTarget = "B";
                    }
                    break;
                case "B":
                    // Setup delay so that platform doesn't automatically change direction
                    if (Time.time > (delayTimer - (addTime / 2)) && delay == true)
                    {
                        currentTarget = "A";
                    }
                    else if (delay == false)
                    {
                        currentTarget = "A";
                    }
                    break;
            }
        }
    }
}
