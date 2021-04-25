using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    // collision stuff should be dealt with in it's own class but for testing purposes i'm writing it all here. 

    public int health;
    int damage = 10;
    public GameObject hazard;
    
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hazard")
        {
            health = health - damage;
            Debug.Log("nono don't touch me there, this is my nono square");
            healthCheck(health);
        }
        Debug.Log("You took a hit! your current Health is:  " + health);
    }

    private void healthCheck(int health)
    {
        if (health == 0)
        {
            Destroy(this.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
