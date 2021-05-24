using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral_Pick_Up : MonoBehaviour
{
    /*This can be thought of as the 'physical' version of the scriptable object (Item.cs)
     * This will be the item that players interact with in the scene.
     * 
     */
   
    //Reference to the scriptable object (Item)
    public Item item;
    Slot_Controller slotController;
    public static Mineral_Pick_Up mineralPickUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendToInventory()
    {
        Inventory_Systems.instance.AddToInventory(item);
        slotController.UpdateInfo();
        
    }
}
