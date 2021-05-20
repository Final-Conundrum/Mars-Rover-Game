using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Systems : MonoBehaviour
{
    /* class that deals with inventory interactions within the scene
     * NOTE: List<Item> is the list of scriptable objects (the Item script)
     */

    //public variables
    public static Inventory_Systems instance;
    public List<Item> currentInventory = new List<Item>();
    public List<Item> savedInventory = new List<Item>();

    public GameObject player;
    public GameObject inventoryPanel;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //UpdatePanelSlots();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdatePanelSlots()
    {
        int index = 0;
        //iterate through the panel's child objects
        foreach (Transform child in inventoryPanel.transform)
        {
            Slot_Controller slot = child.GetComponent<Slot_Controller>();

            if (index < currentInventory.Count)
            {
                slot.item = currentInventory[index];
            }
            else
            {
                slot.item = null;
            }
            slot.UpdateInfo();
            index++;
        }

    }


    public void AddToInventory(Item item)
    {
        //Add to inventory if the inventory isn't full
        if (currentInventory.Count < 6)
        {
            currentInventory.Add(item);
        }
        else
        {
            //Add pop-up telling player that inventory is full
            Debug.Log("Inventory is full!");
        }
        UpdatePanelSlots();
    }

    //remove an item from inventory (Mostly testing purposes)
    public void RemoveItem(Item item)
    {
        currentInventory.Remove(item);
        UpdatePanelSlots();
    }

    //clear inventory upon death 
    public void ClearInventory()
    {

        currentInventory.Clear();
        UpdatePanelSlots();

    }

    void SaveInventory()
    {
        foreach (Item items in currentInventory)
        {
            savedInventory.Add(items);
        }

        currentInventory.Clear();

        Debug.Log(savedInventory.Count);
    }
}

