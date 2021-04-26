using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    public GameObject player => FindObjectOfType<Player_Movement>().gameObject;

    public Vector3 perspectiveOffset = new Vector3 (0f, 15f, 15f);
    public Vector3 orthoOffset = new Vector3(8f, 22f, 7f);

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        switch (Camera.main.orthographic)
        {
            case true:
                transform.position = new Vector3(player.transform.position.x + orthoOffset.x, 
                    player.transform.position.y + orthoOffset.y, 
                    player.transform.position.z + orthoOffset.z);

                break;

            case false:
                transform.position = new Vector3(player.transform.position.x + perspectiveOffset.x, 
                    player.transform.position.y + perspectiveOffset.y, 
                    player.transform.position.z + perspectiveOffset.z);

                break;

        }
    }
}
