using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>Contains Basic player Info.</summary>
public class Player {



	/// <summary>The name of the player.</summary>
	public string playerName = "Erdrick";

	/// <summary>The player's total health.</summary>
	public int playerHealth = 50;

	/// <summary>The player's total damage output.</summary>
	public int totalDamage = 5;

	/// <summary>The amount of GP the player is carrying.</summary>
	public int playerGold = 0;

	//Default Constructor
	public Player()
	{
	}

		/// <summary>Prints a greeting to the Debug Log.</summary>
		public void Greet()
		{
				//print the greeting.
			Debug.Log("Hail! My name is " + playerName+"!");
		}

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
