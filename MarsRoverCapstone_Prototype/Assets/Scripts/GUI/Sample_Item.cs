using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sample_Item : ScriptableObject
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
    public void Use()
    {

    }
}
