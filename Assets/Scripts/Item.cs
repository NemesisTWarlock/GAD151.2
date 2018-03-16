using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]//Displays this classes' variables in the Unity Editor.

/// <summary>
/// Basic item info.
/// </summary>
public class Item
{


    /// <summary>
    /// The name of the item.
    /// </summary>
    public string name;

    /// <summary>
    /// The amount of gold the item contains.
    /// </summary>
    public int gold;


    //Default constructor for Item objects - Debug, should never show up
    public Item()
    {
        name = "Bag of Gold";
        gold = 5;
    }

    /// <summary>
    /// Custom constructor for Item objects.
    /// </summary>
    /// <param name="enemyInst">Enemy inst.</param>
    public Item(Enemy enemyInst)
    {
        name = enemyInst.item;
        gold = enemyInst.gold;
    }

    /// <summary>
    /// Adds the item's gold value to the player
    /// </summary>
    /// <param name="playerInst">Player instance.</param>
    /// <param name="enemyInst">Enemy instance.</param>
    public void GrabLoot(Player playerInst, Enemy enemyInst)
    {
        //add the item's gold value to the player's
        playerInst.gold += gold;	
        //print the message.
        Debug.Log("After a long battle, " + playerInst.name + " defeats the " + enemyInst.name + "! It drops a " + name + ", worth " + gold + " GP.");

    }

}
