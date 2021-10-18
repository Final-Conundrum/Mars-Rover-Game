using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <SUMMARY> 
 * Gets the disance between the player and the first mineral of the game. It then calls our GUI_InfoPanel. OnApproachNotification method to display a message to the player. 
 * 
 **/
public class Player_Distance_Between_Objects : MonoBehaviour
{
    public Transform target; // set this to all mineral transforms
    Transform player;
    GUI_infoPanel InfoPanel => FindObjectOfType<GUI_infoPanel>();

    [SerializeField]
    float distance;

    [SerializeField]
    float triggerPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        player = this.transform; 
    }

    // Update is called once per frame
    void Update()
    {
        GetDistance();
        DistanceTrigger();
    }

    void GetDistance()
    {
        //Returns the distance between player's position and the target's position
        distance = Vector3.Distance(player.position, target.position);
        
    }

    //void GetHalfwayPoint()
    //{
    //    //divide the distance by 2 to get the mid-way point
    //    triggerPosition = distance / 2; 
    //}

    public void DistanceTrigger()
    {
        if(distance <= 25.0f)
        {
            InfoPanel.OnApproachNotification();
        }
        if (distance < 15.0f || distance == 0)
        {
            InfoPanel.infoPanel.SetActive(false);
        }
    }
}
