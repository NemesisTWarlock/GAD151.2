using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]//Displays Player stats in the Unity Editor.


/// <summary>
/// Basic player info.
/// </summary>
public class Player {

	/// <summary>
	/// The name of the player.
	/// </summary>
	public string playerName;

	/// <summary>
	/// The player's health.
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
	/// Default Constructor for the <see cref="Player"/>class.
	/// </summary>
	public Player()
	{
		playerName = "Erdrick";
		playerHealth = 50;
		totalDamage = 5;
		playerGold = 0;
	}

// <summary>Prints a greeting to the Debug Log.(deprecated)</summary>
//		public void Greet()
//		{
//				//print the greeting.
//			Debug.Log("Hail! My name is " + playerName+"!");
//		}

	/// <summary>
	/// Attack the specified enemy instance.
	/// </summary>
	/// <param name="enemyInst">Enemy instance.</param>
	public void Attack(Enemy enemyInst)
	{	
		//use Mathf.Max to make sure the enemy Health doesn't fall below 0	
		enemyInst.enemyHealth = Mathf.Max(0, enemyInst.enemyHealth - totalDamage);
		Debug.Log(playerName + " Hits the " + enemyInst.enemyName + " for "+ totalDamage + ", reducing it's health to " + enemyInst.enemyHealth + "!");
	}



}
