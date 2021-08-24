using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ParentObject : MonoBehaviour
{
    public GameObject Camera;
    public GameObject PlayerCharacter;
    public static GameObject staticCamera;
    public static GameObject staticPlayer;

    private void Start()
    {
        staticCamera = Camera;
        staticPlayer = PlayerCharacter;
    }
}
