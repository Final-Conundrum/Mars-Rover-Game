using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_ElevationIndicator : MonoBehaviour
{
    Player_Movement MovementScript => GetComponent<Player_Movement>();

    public GameObject positionIndicator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PosIndicator(Player_Movement.elevation - 1);

        // Modify Visibility
        if (Player_Movement.grounded == true)
        {
            positionIndicator.SetActive(false);
        }
        else
        {
            positionIndicator.SetActive(true);
        }

        // Modify colour
        /*
        if(MovementScript.takeFallDamage)
        {
            positionIndicator.GetComponent<TextMeshPro>().color = new Color(200, 0, 0);
        }
        else
        {
            positionIndicator.GetComponent<TextMeshPro>().color = new Color(0, 200, 0);
        }*/
    }

    public void PosIndicator(float Ypos)
    {
        try
        {
            positionIndicator.transform.position = new Vector3(
                gameObject.transform.position.x, 
                gameObject.transform.position.y - Ypos, 
                gameObject.transform.position.z);
        }
        catch (MissingReferenceException e)
        {
            Debug.Log("Player is missing, position indicator has stopped functioning... " + e);
            Destroy(this.gameObject);
        }
    }
}
