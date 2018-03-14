﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]//Displays Player stats in the Unity Editor.


/// <summary>
/// Basic player info.
/// </summary>
public class Player
{

    /// <summary>
    /// The name of the player.
    /// </summary>
    public string playerName;

    /// <summary>
    /// The player's Current health.
    /// </summary>
    public int playerHealth;

    /// <summary>
    /// The total damage.
    /// </summary>
    public int totalDamage;

    /// <summary>
    /// The total Gold the player is carrying.
    /// </summary>
    public int playerGold;

    /// <summary>
    /// How many Healing Herbs the player is carrying.
    /// </summary>
    public int playerHerbs;

    /// <summary>
    /// The Player's Max Health. (Hidden)
    /// </summary>
    [HideInInspector]
    public int maxHealth;

    /// <summary>
    ///Number of times the player has Fleed from combat. (Hidden) 
    /// </summary>
    [HideInInspector]
    public int fleeCount;

    /// <summary>
    /// Number of times the player has defeated an enemy. (Hidden)
    /// </summary>
    [HideInInspector]
    public int killCount;




    /// <summary>
    /// Default Constructor for the <see cref="Player"/>class.
    /// </summary>
    public Player()
    {
        playerName = "Erdrick";
        playerHealth = 100;
        totalDamage = 5;
        playerGold = 0;
        playerHerbs = 5;
    }


    /// <summary>
    /// Attack the specified enemy instance.
    /// </summary>
    /// <param name="enemyInst">Enemy instance.</param>
    public void Attack(Enemy enemyInst)
    {	
        //use Mathf.Max to make sure the enemy Health doesn't fall below 0	
        enemyInst.enemyHealth = Mathf.Max(0, enemyInst.enemyHealth - totalDamage);
        Debug.Log(playerName + " Hits the " + enemyInst.enemyName + " for " + totalDamage + " damage, reducing it's health to " + enemyInst.enemyHealth + "!");
    }

    /// <summary>
    /// Heals the player by using a Healing Herb when called.
    /// </summary>
    public void Heal()
    {
        //setup healing range
        int healAmount = Random.Range(25, 50);

        if (playerHealth == maxHealth)
        {
            Debug.Log("Already at Max health!");
            return;
        }
        else
        {
            //heal the player
            playerHealth = Mathf.Max(healAmount, maxHealth);
            //remove a herb (YES, "A" HERB THANKYOU VERY MUCH I WILL DIE ON THIS COMMENTED HILL)
            playerHerbs--;
            //print text
            Debug.Log(playerName + " Uses a Healing Herb, healing them for " + healAmount + "!");
            Debug.Log(playerName + "'s HP: " + playerHealth);
            Debug.Log(playerName + " has " + playerHerbs + " herbs left.");
        }
    }


    /// <summary>
    /// Increases the player's damage output when called.
    /// </summary>
    public void DamageBoost()
    {
        //setup Damage boost range
        int dmgBoostAmount = Random.Range(5, 20);
        //Boost Damage
        totalDamage += dmgBoostAmount;
        //Print Text
        Debug.Log(playerName + " increases their total damage output by " + dmgBoostAmount + " to " + totalDamage + "!");


    }

    /// <summary>
    /// Boosts the player's Maximum health when called. 
    /// </summary>
    public void MaxHealthBoost()
    {
        //setup HP Boost range
        int hpBoostAmount = Random.Range(10, 50);
        //Boost Max HP
        maxHealth += hpBoostAmount;
        //Print Text
        Debug.Log(playerName + " increases their health by " + hpBoostAmount + " to " + maxHealth + "!");
    }


}