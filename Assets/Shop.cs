using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls Shop Functionality.
/// </summary>
public class Shop {

	public bool playerLeftShop = false;

	public void VisitShop(Player playerInst)
	{
		//Setup Shop Prices
		int herbPrice = Random.Range (5,50);
		int damageBoostPrice = Random.Range (100, 500);

		//Setup Shop Exit Bool


		//Shop Text
		Debug.Log ("Welcome to the Shop!");
		Debug.Log (playerInst.playerName + " has " + playerInst.playerGold + "GP.");
		Debug.Log ("Press 1 to purchase a Healing Herb for "+ herbPrice+ "GP.");
		Debug.Log ("Press 2 to Purchase a Damage Boost");
		Debug.Log ("Press SPACE to leave the shop.");

		//Healing Herb Purchase
		if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
		{
			if (playerInst.playerGold < herbPrice)
			{
				Debug.Log ("You do not have enough GP!");
			} 
				else
				{
					playerInst.playerGold -= herbPrice;
					playerInst.playerHerbs++;
				}
		}

		//Damage Boost Purchase
		if(Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
		{
			if (playerInst.playerGold < damageBoostPrice)
			{
				Debug.Log ("You do not have enough GP!");
			} 
				else
					{					
						playerInst.DamageBoost ();	
					}
		}

		//Leave Shop
				if (Input.GetKeyDown(KeyCode.Space))
		{
			playerLeftShop = true;
			return;
		}
	}

}
