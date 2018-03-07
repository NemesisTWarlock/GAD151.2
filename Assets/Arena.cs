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
				Item tempItem = new Item();
				//Drop the loot
				tempItem.DropLoot(enemyInst);
				//pick up the loot
				tempItem.GrabLoot(playerInst, enemyInst);
				//destroy the temporary item
				tempItem = null;
				//destroy the enemy object
				enemyList.Clear();
				//spawn a new one
				SpawnEnemy();
			}
		}
	}
		
	void SpawnEnemy()
	{
		enemyList.Add (new Enemy());
		enemyList[0].Encounter();
	}

	void Checkhealth(Player playerInst)
	{    
		if (playerIsDead) return;
		if (playerInst.playerHealth == 0) 
		{
			Debug.Log ("You Dead, yo!");
			playerIsDead = true;
		}
		
	}

}

