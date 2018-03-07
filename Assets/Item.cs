using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
/// <summary>Contains basic Item info.</summary>
public class Item {


	/// <summary>The name of the item.</summary>
	public string itemName = "Bag of Gold";

	/// <summary>The amount of gold the item contains.</summary>
	public int itemGold = 5; 


	//Default constructor (FOR TESTING ONLY)


	public Item (string itemNameIN, int itemGoldIN)
	{
		itemName = itemNameIN;
		itemGold = itemGoldIN;
	}



		/// <summary>Prints an item drop message when triggered.</summary>
	public void GrabLoot(Player playerInst, Enemy enemyInst)
		{
		playerInst.playerGold += itemGold;	
			//print the message.
		Debug.Log("After a long battle, "+ playerInst.playerName + " defeats the " +enemyInst.enemyName+"! It drops a " + itemName + ", worth " + itemGold + " GP.");

		}

}
