using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Iventory_Systems : MonoBehaviour
{
    /* class that deals with inventory mechanics.
     * 
     */

    //public variables
   
    public List<GameObject> currentInventory;
    public List<GameObject> savedInventory;
    public GameObject item;
    

 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdatePanelSlots()
    {

    }

   

   void AddToInventory()
    {
        //adding material gameobject to the current inventory list
        currentInventory.Add(item);
 
    }

//clear inventory upon death 
    public void ClearInventory()
    {
       
        currentInventory.Clear();

        //remove images from buttons
    }

    void SaveInventory()
    {
        foreach(GameObject item in currentInventory)
        {
            savedInventory.Add(item);
        }

        currentInventory.Clear();

        Debug.Log(savedInventory.Count);
    }
}
