using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
/// <summary>Contains basic Enemy info.</summary>
public class Enemy {  
    
	/// <summary>The Enemy's name.</summary>
	public string enemyName;

	/// <summary>The Enemy's Current health.</summary>
	public int enemyHealth;

	/// <summary>The Enemy's total Damage output.</summary>
	public int enemyDamage;

	/// <summary>The amount of GP the enemy is carrying.</summary>
	public int enemyGold;	

	public string enemyItem;


	//Default Constructor
	public Enemy ()
	{
		enemyName = "Slime";
		enemyHealth = 10;
		enemyDamage = 2;
		enemyItem = "Slime Goo";
		enemyGold = 5;


	}

	//Custom Constructor
	public Enemy ( string enemyNameIN, int enemyHealthIN, int enemyDamageIN, string enemyItemIN, int enemyGoldIN )
	{
		
		//set custom attributes
		enemyName = enemyNameIN;
		enemyHealth = enemyHealthIN;
		enemyDamage = enemyDamageIN;
		enemyItem = enemyItemIN;
		enemyGold = enemyGoldIN;



	}

	/// <summary>Prints a message when triggered.</summary>
	public void Encounter()
	{
			Debug.Log ("A "+ enemyName + " draws near!");
	}

	public void Attack(Player playerInst)
	{
		//use Mathf.Max to make sure the player's Health doesn't fall below 0	
		playerInst.playerHealth = Mathf.Max(0, playerInst.playerHealth - enemyDamage);
		Debug.Log(enemyName + " Hits " + playerInst.playerName + " for "+ enemyDamage + ", reducing it's health to " + playerInst.playerHealth + "!");
	}


}
