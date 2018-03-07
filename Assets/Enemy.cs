using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
/// <summary>Contains basic Enemy info.</summary>
public class Enemy {  
    
	/// <summary>The Enemy's name.</summary>
	public string enemyName = "Slime";

	/// <summary>The Enemy's Current health.</summary>
	public int enemyHealth = 10;

	/// <summary>The Enemy's total Damage output.</summary>
	public int enemyDamage = 2;

	/// <summary>The amount of GP the enemy is carrying.</summary>
	public int enemyGold = 5;	


	//Default Constructor
	public Enemy ()
	{

	}

	//Custom Constructor
	public Enemy ( string enemyNameIN, int enemyHealthIN, int enemyDamageIN, int enemyGoldIN )
	{
		
		//set custom attributes
		enemyName = enemyNameIN;
		enemyHealth = enemyHealthIN;
		enemyDamage = enemyDamageIN;
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
