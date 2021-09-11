using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    /*
     * collision stuff should be dealt with in it's own class but for testing purposes i'm writing it all here. 
     * damage is being taken twice, thinking its because TakeDamage() gets called in the collision check
     * and then again right after.
    */
    public static GUI_DeathScreen DeathScreen => FindObjectOfType<GUI_DeathScreen>();

    public static int health;
    public static float damage;
    public static GameObject hazard;
    public static GameObject player;
    
    // Start is called before the first frame update
    public void Start()
    {
        health = 100;
        player = this.gameObject;
    }

    //Deals damage to the player based on the passed in damage amount. 
    public static void TakeDamage(int damage)
    {
        int newHealth = health - damage;
        health = newHealth;

        healthCheck();
    }

    //Checks the players current health
    private static void healthCheck()
    {
        if (health <= 0)
        {
            Destroy(player);
            DeathScreen.Display();
        }
    }

    // Update is called once per frame
    public static void Update()
    {
        
    }
}
