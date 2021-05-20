using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot_Controller : MonoBehaviour
{
    /*
     * This script stires the item corresponding to the slot it is attached to.
     * Will display the items name and icon and allow the player to interact with the item by clicking the slot.
     */

    public Item item;

    public void Use()
    {
        if (item)
        {
            Debug.Log("You Clicked: " + item.itemName);
        }else
        {
            Debug.Log("Uh oh, there's nothing here!");
        }
    }

    //Update the text and image
    public void UpdateInfo()
    {
        Text displayText = transform.Find("Text").GetComponent<Text>();
        Image displayImage = transform.Find("Image").GetComponent<Image>();

        if (item)
        {
            displayText.text = item.itemName;
            displayImage.sprite = item.icon;
        }else
        {
            displayText.text = "";
            displayImage.sprite = null;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
