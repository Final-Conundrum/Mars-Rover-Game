using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_PersistentLevel : MonoBehaviour
{
    private static GameObject instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = gameObject;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
