using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physical_Inventory : MonoBehaviour
{
    public Inventory_Systems _inventory;
    public static Inventory_Systems inventory;
    public Display_Inventory _displayInventory;
    public static Display_Inventory displayInventory;

    public static Mineral_Pick_Up currentMineralInteraction;

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
        }*/

        if(c.gameObject.tag == "Aragonite" || c.gameObject.tag == "Feldspar" || c.gameObject.tag == "Random")
        {
            currentMineralInteraction = c.gameObject.GetComponent<Mineral_Pick_Up>();
        }
    }

    public static void AddToInventory()
    {
        inventory.AddItem(currentMineralInteraction.item, 1);
        displayInventory.UpdateDisplay();
        
    }

    //reset inventory to 0 when exiting play mode. 
    private void OnApplicationQuit()
    {
        inventory.itemContainer.Clear();
    }
}
