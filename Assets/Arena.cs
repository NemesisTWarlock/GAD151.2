using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates instances of the player, item, and enemy classes, and triggers their functions.
/// </summary>
public class Arena : MonoBehaviour 
{



	//Create new instances of the player, enemy and item classes
	public Player player1 = new Player ();
	//make a list of enemies
	List<Enemy>enemyList = new List<Enemy>();
	//player death bool
	bool playerIsDead = false;



	// Use this for initialization
	void Start () 
	{
		SpawnEnemy ();
	}

	//Updates each frame
	void Update()
	{
		//when mouse button clicked, Fight opponent
		if(Input.GetMouseButtonDown(0))
		{
			Fight(player1, enemyList[0]);
		}

		//then Check wether the player's dead or not
		Checkhealth(player1);
	}


	void Fight(Player playerInst, Enemy enemyInst)
	{	//if the player's health isn't 0, attack the enemy
		if (playerInst.playerHealth > 0) 
		{
			playerInst.Attack (enemyInst);

			//Check if the attack has killed the enemy
			if (enemyInst.enemyHealth == 0) 
			{
				//create a temporaty item
				Item tempItem = new Item ();
				//Drop the loot
				tempItem.DropLoot (enemyInst);
				//pick up the loot
				tempItem.GrabLoot (playerInst, enemyInst);
				//destroy the temporary item
				tempItem = null;
				//destroy the enemy object
				enemyList.Clear ();
				//spawn a new one
				SpawnEnemy ();
			}
			//if not, attack the player back
			else 
			{
				enemyInst.Attack (playerInst);
			}
		}
	}
		
	void SpawnEnemy()
	{
		enemyList.Add (new Enemy());
		enemyList[0].Encounter();
	}

	/// <summary>
	/// Check's the Player's health, and cleanly stops the game when they die.
	/// </summary>
	/// <param name="playerInst">Selected Player instance.</param>
	void Checkhealth(Player playerInst)
	{   
		//if the player's dead, jump back to Update(); (which should cleanly loop nothing)
		if (playerIsDead) return;

		//when the player's health hits Zero, declare the player is dead, yo!
		if (playerInst.playerHealth == 0) 
		{
			Debug.Log ("Alas,"+playerInst.playerName+" has Fallen.");
			Debug.Log("GAME OVER");
			playerIsDead = true;
		}
		
	}

}

