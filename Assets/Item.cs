using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]//Displays this classes' variables in the Unity Editor.

/// <summary>
/// Basic item info.
/// </summary>
public class Item {


	/// <summary>
	/// The name of the item.
	/// </summary>
	public string itemName;

	/// <summary>
	/// The amount of gold the item contains.
	/// </summary>
	public int itemGold; 


	//Default constructor (FOR TESTING ONLY)
	public Item()
	{
		itemName = "Bag of Gold";
		itemGold = 5;
	}


	public Item (Enemy enemyInst)
	{
		itemName = enemyInst.enemyItem;
		itemGold = enemyInst.enemyGold;
	}

/// <summary>
/// Adds the item's gold value to the player
/// </summary>
/// <param name="playerInst">Player inst.</param>
/// <param name="enemyInst">Enemy inst.</param>
	public void GrabLoot(Player playerInst, Enemy enemyInst)
		{
		playerInst.playerGold += itemGold;	
			//print the message.
		Debug.Log("After a long battle, "+ playerInst.playerName + " defeats the " +enemyInst.enemyName+"! It drops a " + itemName + ", worth " + itemGold + " GP.");

		}

}
