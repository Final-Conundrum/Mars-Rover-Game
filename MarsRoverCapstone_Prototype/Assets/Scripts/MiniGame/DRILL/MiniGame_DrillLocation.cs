using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame_DrillLocation : MonoBehaviour
{
    // Added to DrillLocation object:
    // Interacts with player and holds Item variable added to UI inventory

    private bool completed;
    public Item inventoryItem;

    private void Awake()
    {
        if (completed)
        {
            Destroy(gameObject);
        }
    }

    public bool CompleteThis()
    {
        return completed;
    }
}
