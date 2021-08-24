using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Inventory",  menuName = "Inventory System/ Inventory")]
public class Inventory_Systems : ScriptableObject
{

    /*The reason for having the inventory as a scriptable object is so that we can have multiple player inventories.
     * Example: This allows us to have a 'tutorial inventory' filled with items that we may not want the player to have in the main game.
     * 
     */
     //A list of inventory slots
    public List<Inventory_Slots> itemContainer = new List<Inventory_Slots>();
    
    public void AddItem(Item _item, int _amount)
    {
        bool hasItem = false;

        //loop through all items to see if we have it already
        for (int i = 0; i < itemContainer.Count; i++)
        {
            if(itemContainer[i].item == _item)
            {
                itemContainer[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            //Add an item to the inventory if we do not have the item already.
            itemContainer.Add(new Inventory_Slots(_item, _amount));
        }
    }

    ///* OLD class that deals with inventory interactions within the scene
    // * NOTE: List<Item> is the list of scriptable objects (the Item script)
    // */

    ////public variables
    //public static Inventory_Systems instance;
    //public List<Item> currentInventory = new List<Item>();
    //public List<Item> savedInventory = new List<Item>();

    //public GameObject player;
    //public GameObject inventoryPanel;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    instance = this;
    //    //UpdatePanelSlots();
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    ////public void UpdatePanelSlots()
    ////{
    ////    int index = 0;
    ////    //iterate through the panel's child objects
    ////    foreach (Transform child in inventoryPanel.transform)
    ////    {
    ////        Slot_Controller slot = child.GetComponent<Slot_Controller>();

    ////        if (index < currentInventory.Count)
    ////        {
    ////            slot.item = currentInventory[index];
    ////        }
    ////        else
    ////        {
    ////            slot.item = null;
    ////        }
    ////        slot.UpdateInfo();
    ////        index++;
    ////    }

    ////}


    //public void AddToInventory(Item item)
    //{
    //    //Add to inventory if the inventory isn't full
    //    if (currentInventory.Count < 6)
    //    {
    //        currentInventory.Add(item);
    //    }
    //    else
    //    {
    //        //Add pop-up telling player that inventory is full
    //        Debug.Log("Inventory is full!");
    //    }
    //   // UpdatePanelSlots();
    //}

    ////remove an item from inventory (Mostly testing purposes)
    //public void RemoveItem(Item item)
    //{
    //    currentInventory.Remove(item);
    //   // UpdatePanelSlots();
    //}

    ////clear inventory upon death 
    //public void ClearInventory()
    //{

    //    currentInventory.Clear();
    //  //  UpdatePanelSlots();

    //}

    //void SaveInventory()
    //{
    //    foreach (Item items in currentInventory)
    //    {
    //        savedInventory.Add(items);
    //    }

    //    currentInventory.Clear();

    //    Debug.Log(savedInventory.Count);
    //}
}
[System.Serializable]
public class Inventory_Slots
{
    //Second iteration 
    public Item item;
    public int amount;

    public Inventory_Slots(Item _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }

    //adding an amount of an object to inventory.
    public void AddAmount(int value)
    {
        amount += value;
    }
}

