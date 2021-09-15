using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mineral_Pick_Up : MonoBehaviour
{

    //The game object our item will represent
    public Item item;

    // Additions made by Dallas for prompts and info
    public TMP_Text mineralName;

    private void Start()
    {
        mineralName.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            mineralName.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            mineralName.enabled = false;
        }
    }

    //    /*This can be thought of as the 'physical' version of the scriptable object (Item.cs)
    //     * This will be the item that players interact with in the scene.
    //     * 
    //     */

    //    //Reference to the scriptable object (Item)
    //    Item item => FindObjectOfType<Item>();
    //    Slot_Controller slotController => FindObjectOfType<Slot_Controller>();
    //    Inventory_Systems instance => FindObjectOfType<Inventory_Systems>();

    //    public string Text = "Aragonite";
    //    public Sprite Icon;

    //    public static Mineral_Pick_Up mineralPickUp;

    //    // Start is called before the first frame update
    //    void Start()
    //    {

    //    }

    //    // Update is called once per frame
    //    void Update()
    //    {

    //    }

    //    public void SendToInventory()
    //    {

    //        //slotController.UpdateInfo();
    //    }
}
