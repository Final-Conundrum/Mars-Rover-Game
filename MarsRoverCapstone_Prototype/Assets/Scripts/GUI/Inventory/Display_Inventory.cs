using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Display_Inventory : MonoBehaviour
{
    //Useful note: using scriptable object structure we don't have to go through the player to find the inventory,
    //instead we can just jump straight to the inventory system.

    public Inventory_Systems inventory;

    //Inventory Panel Boundary points 
    public int x_Start;
    public int y_Start;

    //public variables
    public int x_Space_Between_Items;
    public int number_Of_Colums;
    public int y_Space_Between_Items;

  

    Dictionary<Inventory_Slots, GameObject> itemsDisplayed = new Dictionary<Inventory_Slots, GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        //move this, we only want to call this when we want to update our display.
        //UpdateDisplay();
    }

    public void CreateDisplay()
    {
        for(int i = 0; i < inventory.itemContainer.Count; i++)
        {
            //set position of icon in world space
            var obj = Instantiate(inventory.itemContainer[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            
            //set local position of icon
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            //Get the TM Pro component and set it's value to amount of an item.
            obj.GetComponentInChildren <TextMeshProUGUI>().text = inventory.itemContainer[i].amount.ToString("n0");

            //Add to the dictionary
            itemsDisplayed.Add(inventory.itemContainer[i], obj);
        }
    }

    // i = which position in the inventory is the item
    public Vector3 GetPosition(int i)
    {
        return new Vector3(x_Start + (x_Space_Between_Items * (i % number_Of_Colums)), y_Start + (-y_Space_Between_Items * (i/number_Of_Colums)), 0f);
    }

    public void UpdateDisplay()
    {
        for(int i = 0; i < inventory.itemContainer.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(inventory.itemContainer[i]))
            {
                //if the item is in the inventory:
                itemsDisplayed[inventory.itemContainer[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.itemContainer[i].amount.ToString("n0");
            }else
            {
                var obj = Instantiate(inventory.itemContainer[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.itemContainer[i].amount.ToString("n0");
                itemsDisplayed.Add(inventory.itemContainer[i], obj);
            }
        }
    }
}
