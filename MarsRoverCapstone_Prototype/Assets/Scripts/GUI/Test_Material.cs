using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a class attirubute, allows us to create Test_Materials in the inspector.
[CreateAssetMenu (fileName = "new Test_Material", menuName = "Items/Test_Material")]
public class Test_Material : Item
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Override replaces the Use() method on the Item Script.
    //This is useful if we plan to make different objects (items) to different things. 
    public override void Use()
    {
        //something cool here
    }
}
