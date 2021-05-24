using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a class attirubute, allows us to create Test_Materials in the inspector.
[CreateAssetMenu (fileName = "new Mineral", menuName = "Inventory System/Items/Default")]
public class DefaultObject : Item
{

    public void Awake()
    {
        type = MineralType.Default;
    }
   
}
