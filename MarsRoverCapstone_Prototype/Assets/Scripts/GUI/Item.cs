using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : ScriptableObject
{
    //scriptable objects exist outside of the game world

    public string itemName;
    public Sprite icon;
    

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //'using' items will enable the player to drop them off at the safe points.
    //virtual means to use the extended class's version if one exists, otherwise use this.
    public virtual void Use()
    {
        //Drop off items
    }
}
