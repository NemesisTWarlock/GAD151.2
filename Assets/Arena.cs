using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Arena - Creates the Player and enemy classes, and handles combat and item/gold gain.
/// </summary>
public class Arena : MonoBehaviour 
{



	//Create new instances of the player, enemy and item classes
	public Player player1 = new Player ();

	//Make a List for the enemy
	public List<Enemy>enemyList = new List<Enemy>();

	//Player Death bool
	bool playerIsDead = false;


	// Use this for initialization
	void Start () 
	{
		//Spawn the first Enemy.
		SpawnEnemy ();
	}

	//Updates each frame
	void Update()
	{
		//When Left mouse button is clicked, Fight opponent
		if(Input.GetMouseButtonDown(0))
		{
			Fight(player1, enemyList[0]);
		}

		//Then Check wether the player's dead or not
		Checkhealth(player1);
	}


	void Fight(Player playerInst, Enemy enemyInst)
	{	
		//if the player's health isn't 0, attack the enemy
		if (playerInst.playerHealth > 0) 
		{
			playerInst.Attack (enemyInst);

				//Check if the attack has killed the enemy
				if (enemyInst.enemyHealth == 0) 
				{
					//create a temporary item with the enemy's item stats
					Item tempItem = new Item (enemyInst);
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

	/// <summary>
	/// Manages enemy instance spawning.
	/// </summary>
	void SpawnEnemy()
	{
		//Sanity check -- If there's zero monsters on the battlefield...
		if (enemyList.Count == 0) 
		{
			enemyList.Add (new Enemy ());
			enemyList [0].Encounter ();
		} 
		else //this *should* Never happen!
		{			
			Debug.Log ("How the heck is there more than one monster on the field? Lemme fix that...");
			//clear the list and restart combat
			enemyList.Clear();
			Debug.Log ("Fixed. Restarting Combat...");
			SpawnEnemy ();			
		}
	}

	/// <summary>
	/// Check's the Player's health, and cleanly stops the game when they die.
	/// </summary>
	/// <param name="playerInst">Selected Player instance.</param>
	void Checkhealth(Player playerInst)
	{   
		//if the player's dead, jump back to Update(); (which should cleanly loop)
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

