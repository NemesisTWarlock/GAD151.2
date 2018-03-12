using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]//Makes Enemy stats viewable in the Unity Editor.
/// <summary>
/// Contains basic Enemy info.
/// </summary>
public class Enemy {  
    
	/// <summary>
	/// The Enemy's name.
	/// </summary>
	public string enemyName;

	/// <summary>
	/// The Enemy's Current health.
	/// </summary>
	public int enemyHealth;

	/// <summary>
	/// The Enemy's total Damage output.
	/// </summary>
	public int enemyDamage;

	/// <summary>
	/// The name of the item the Enemy is Carrying.
	/// </summary>
	public string enemyItem;

	/// <summary>
	/// The amount of GP the enemy is carrying.
	/// </summary>
	public int enemyGold;	

	/// <summary>
	/// Default Constructor for the <see cref="Enemy"/> class.
	/// </summary>
	public Enemy ()
	{		
		enemyName = "Slime";
		enemyHealth = 5;
		enemyDamage = 5;
		enemyItem = "Slime Goo";
		enemyGold = 10;
	}

	/// <summary>
	/// Custom constructor for the <see cref="Enemy"/> class.
	/// </summary>
	public Enemy ( string enemyNameIN, int enemyHealthIN, int enemyDamageIN, string enemyItemIN, int enemyGoldIN )
	{		
		//set custom attributes
		enemyName = enemyNameIN;
		enemyHealth = enemyHealthIN;
		enemyDamage = enemyDamageIN;
		enemyItem = enemyItemIN;
		enemyGold = enemyGoldIN;
	}

	// Copy constructor
	public Enemy(Enemy enemyIN){
		enemyName = enemyIN.enemyName;
		enemyHealth = enemyIN.enemyHealth;
		enemyDamage = enemyIN.enemyDamage;
		enemyItem = enemyIN.enemyItem;
		enemyGold = enemyIN.enemyGold;
	}

	/// <summary>
	/// Prints a message declaring combat has begun to the Debug Log.
	/// </summary>
	public void Encounter()
	{
			Debug.Log ("A "+ enemyName + " draws near!");
	}

	/// <summary>
	/// Attack the specified player instance.
	/// </summary>
	/// <param name="playerInst">player instance to attack.</param>
	public void Attack(Player playerInst)
	{
		//use Mathf.Max to make sure the player's Health doesn't fall below 0	
		playerInst.playerHealth = Mathf.Max(0, playerInst.playerHealth - enemyDamage);
		Debug.Log(enemyName + " Hits " + playerInst.playerName + " for "+ enemyDamage + ", reducing it's health to " + playerInst.playerHealth + "!");
	}
}
