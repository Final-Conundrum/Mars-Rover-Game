using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Player_ParentObject : MonoBehaviour
{
    // Added to Player_ParentObject:
    // Sets static objects for the player object and camera

    public GameObject Camera;
    public GameObject PlayerCharacter;
    public static GameObject staticCamera;
    public static GameObject staticPlayer;
    public static ConstraintSource cameraSource;

    private void Awake()
    {
        staticCamera = Camera;
        staticPlayer = PlayerCharacter;
        cameraSource.sourceTransform = staticCamera.transform;
        cameraSource.weight = 1;
    }
}
