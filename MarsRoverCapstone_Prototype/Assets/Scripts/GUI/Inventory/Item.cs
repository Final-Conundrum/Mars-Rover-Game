using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//type of item
public enum MineralType
{
    Aragonite,
    Other,
    Default
}
public abstract class Item : ScriptableObject
{
    //scriptable objects exist outside of the game world

    //will hold the display of item
    public GameObject prefab;
    public MineralType type;

    [TextArea(15,20)]
    public string description;

  
}
