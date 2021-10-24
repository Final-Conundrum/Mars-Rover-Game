using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physical_Inventory : MonoBehaviour
{
    public Inventory_Systems _inventory;
    public static Inventory_Systems inventory;
    public Display_Inventory _displayInventory;
    public static Display_Inventory displayInventory;

    //public static Mineral_Pick_Up currentMineralInteraction;
    public static MiniGame_PIXLMineral currentPIXLInteraction;
    public static MiniGame_DrillLocation currentDrillInteraction;
    public static MiniGame_RIMFAXLocation currentRIMFAXInteraction;

    public void Start()
    {
        inventory = _inventory;
        displayInventory = _displayInventory;
    }
    public void OnTriggerEnter(Collider c)
    {
        /*
        var item = other.gameObject.GetComponent<Mineral_Pick_Up>();
      
        if (item)
        {
            inventory.AddItem(item.item, 1);
            displayInventory.UpdateDisplay();
        }

        if(c.gameObject.tag == "Aragonite" || c.gameObject.tag == "Feldspar" || c.gameObject.tag == "Random")
        {
            currentMineralInteraction = c.gameObject.GetComponent<Mineral_Pick_Up>();
        }
        */

        if(c.gameObject.tag == "PIXLMineral")
        {
            currentPIXLInteraction = c.gameObject.GetComponent<MiniGame_PIXLMineral>();
        }

        if(c.gameObject.tag == "RIMFAX")
        {
            currentRIMFAXInteraction = c.gameObject.GetComponent<MiniGame_RIMFAXLocation>();
        }

        if (c.gameObject.tag == "Drill")
        {
            currentDrillInteraction = c.gameObject.GetComponent<MiniGame_DrillLocation>();
        }
    }

    public static void AddToInventory(string minigameType)
    {
        switch (minigameType) {
            case "PIXL":
                inventory.AddItem(currentPIXLInteraction.inventoryItem, 1);
                break;
            case "RIMFAX":
                inventory.AddItem(currentRIMFAXInteraction.inventoryItem, 1);

                break;
            case "Drill":
                inventory.AddItem(currentDrillInteraction.inventoryItem, 1);

                break;
        }
        displayInventory.UpdateDisplay();
    }

    //reset inventory to 0 when exiting play mode. 
    private void OnApplicationQuit()
    {
        ClearInventory();
    }
    public void ClearInventory()
    {
        inventory.itemContainer.Clear();
    }
}
