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

	//A list of enemy stats
	public Enemy[] enemyStats;

	//Make a List for the enemy
	List<Enemy>enemyList = new List<Enemy>();

	//Player Death bool
	bool playerIsDead = false;

	//First Combat text







	// Use this for initialization
	void Start () 
	{

		//Set the player's Max Health
		player1.maxHealth = player1.playerHealth;

		//Game Start text
		Debug.Log ("Controls:");
		Debug.Log ("Left Click: Attack Enemy");
		Debug.Log ("Right Click: Flee");
		Debug.Log ("Shift Key: Use a Healing Herb");
		
		//Spawn the first Enemy
		SpawnEnemy ();


	}

	//Updates each frame
	void Update()
	{

	//Check Player Input

		//Left CLick
		if(Input.GetMouseButtonDown(0))
		{
			Fight(player1, enemyList[0]);
		}

		//Right CLick
		if (Input.GetMouseButtonDown (1)) 
		{
			Debug.Log (player1.playerName+" Flees from the combat, to find a better fight.");
			//Reset Combat
			enemyList.Clear();
			SpawnEnemy ();
		}


		//Shift Key
		if (Input.GetKeyDown(KeyCode.LeftShift)||Input.GetKeyDown(KeyCode.RightShift)) 
		{
		
			if (player1.playerHerbs == 0) 
			{
				Debug.Log (player1.playerName + "does not have any Healing Herbs!");
			} 
			else 
			{
				player1.Heal ();

			}
		}



		//Then Check wether the player's dead or not
		Checkhealth(player1);

	}


	void Fight(Player playerInst, Enemy enemyInst)
	{	
		//if the player's health isn't 0, attack the enemy
		if (playerInst.playerHealth > 0) 
		{
				//Combat Step
				playerInst.Attack (enemyInst);

				//Check if the attack has killed the enemy
				if (enemyInst.enemyHealth == 0) 
				{

				//Loot Step

					//create a temporary item with the enemy's item stats
					Item tempItem = new Item (enemyInst);
					//pick up the loot
					tempItem.GrabLoot (playerInst, enemyInst);
					//destroy the temporary item
					tempItem = null;
					//destroy the enemy object
					enemyList.Clear ();

				//Shop Step

					//Create a temporary shop 
					Shop itemShop = new Shop();
					//Visit the shop
					itemShop.VisitShop(playerInst);
					

					if(itemShop.playerLeftShop == true)
						{
							//Destroy the Temporary shop
							itemShop = null;

					//Encounter Step

							//spawn a new enemy
							SpawnEnemy ();
						}
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
			enemyList.Add (new Enemy (enemyStats[Random.Range(0, enemyStats.Length)]));
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

