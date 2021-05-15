using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Player_Stats 
{
    /*
     * collision stuff should be dealt with in it's own class but for testing purposes i'm writing it all here. 
     * damage is being taken twice, thinking its because TakeDamage() gets called in the collision check
     * and then again right after.
    */

    public static int health;
    public static int damage = 10;
    public static GameObject hazard;
    public static GameObject player;
    
    // Start is called before the first frame update
    public static void Start()
    {
        health = 100;
    }

    
    //testing collision method. doesn't need to be here once collision script is running.
   public static void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hazard")
        {
            TakeDamage();
        }
        Debug.Log("You took a hit! your current Health is:  " + health);
    }

    public static void TakeDamage()
    {
        int newHealth = health - damage;
        health = newHealth;

        healthCheck(newHealth);
    }


    private static void healthCheck(int health)
    {
        //this will need to go into the collision script, can't use Destroy() in static class
    }

    // Update is called once per frame
    public static void Update()
    {
        
    }
}
