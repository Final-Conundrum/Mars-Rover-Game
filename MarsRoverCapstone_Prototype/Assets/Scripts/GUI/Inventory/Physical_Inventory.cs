using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physical_Inventory : MonoBehaviour
{
    public Inventory_Systems inventory;
    public Display_Inventory displayInventory;

   public void OnTriggerEnter(Collider other)
    {
        var item = other.gameObject.GetComponent<Mineral_Pick_Up>();
      
        if (item)
        {
            inventory.AddItem(item.item, 1);
            //Destroy(other.gameObject);
            displayInventory.UpdateDisplay();

        }
    }

    //reset inventory to 0 when exiting play mode. 
    private void OnApplicationQuit()
    {
        inventory.itemContainer.Clear();
    }
}
