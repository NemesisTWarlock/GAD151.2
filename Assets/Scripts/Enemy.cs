using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]//Makes Enemy stats viewable in the Unity Editor.
/// <summary>
/// Contains basic Enemy info.
/// </summary>
public class Enemy
{
    
    /// <summary>
    /// The Enemy's name.
    /// </summary>
    public string name;

    /// <summary>
    /// The Enemy's Current health.
    /// </summary>
    public int health;

    /// <summary>
    /// The Enemy's total Damage output.
    /// </summary>
    public int damage;

    /// <summary>
    /// The name of the item the Enemy is Carrying.
    /// </summary>
    public string item;

    /// <summary>
    /// The amount of GP the enemy is carrying.
    /// </summary>
    public int gold;

    /// <summary>
    /// Default Constructor for the <see cref="Enemy"/> class.
    /// </summary>
    public Enemy()
    {		
        name = "Slime";
        health = 5;
        damage = 5;
        item = "Slime Goo";
        gold = 10;
    }

    /// <summary>
    /// Custom constructor for the <see cref="Enemy"/> class.
    /// </summary>
    public Enemy(string nameIN, int healthIN, int damageIN, string itemIN, int goldIN)
    {		
        //set custom attributes
        name = nameIN;
        health = healthIN;
        damage = damageIN;
        item = itemIN;
        gold = goldIN;
    }

    // Copy constructor
    public Enemy(Enemy enemyIN)
    {
        name = enemyIN.name;
        health = enemyIN.health;
        damage = enemyIN.damage;
        item = enemyIN.item;
        gold = enemyIN.gold;
    }

    /// <summary>
    /// Prints a message declaring combat has begun to the Debug Log.
    /// </summary>
    public void Encounter()
    {
        Debug.Log("A " + name + " draws near!");
    }

    /// <summary>
    /// Attack the specified player instance.
    /// </summary>
    /// <param name="playerInst">player instance to attack.</param>
    public void Attack(Player playerInst)
    {
        //use Mathf.Max to make sure the player's Health doesn't fall below 0	
        playerInst.health = Mathf.Max(0, playerInst.health - damage);
        Debug.Log(name + " Hits " + playerInst.name + " for " + damage + " damage, reducing their health to " + playerInst.health + "!");
    }
}
