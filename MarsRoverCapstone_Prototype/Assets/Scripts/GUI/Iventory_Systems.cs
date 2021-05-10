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
    public List<GameObject> panelImages;
    public List<GameObject> CurrentInventory;
    public List<GameObject> allMaterials;
    public GameObject Material;
    

 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //setting the object active could probably be done in the collision detection script.

   void AddToInventory()
    {
        CurrentInventory.Add(Material);

        //testing
        if (gameObject.tag == "Material")
        {
            panelImages[0].SetActive(true);
        }
    }

    //Deactivate all panel images and clear inventory list.
    void ClearInventory()
    {
        foreach(GameObject panel in panelImages)
        {
            panel.SetActive(false);
        }

        CurrentInventory.Clear();
    }

    void SaveInventory()
    {
        foreach(GameObject material in CurrentInventory)
        {
            allMaterials.Add(material);
        }

        CurrentInventory.Clear();

        Debug.Log(allMaterials.Count);
    }
}
