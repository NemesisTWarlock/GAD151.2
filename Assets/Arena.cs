using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Arena - Creates the Player and enemy classes, and handles combat and item/gold gain.
/// </summary>
public class Arena : MonoBehaviour 
{
	//DEBUG: Shop Bool
	public bool playerLeftShop = false;


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
<<<<<<< HEAD

				//Shop Step

//					//Create a temporary shop 
//					Shop itemShop = new Shop();
//					//Visit the shop
//					itemShop.VisitShop(playerInst);					
//
//					if(itemShop.playerLeftShop == true)
//						{
//							//Destroy the Temporary shop
//							itemShop = null;

				//DEBUG: Shop Step

				VisitShop (playerInst);


					if (playerLeftShop == true)
						{
							//Encounter Step

								//spawn a new enemy
								SpawnEnemy ();
						}
=======
					//spawn a new one
					SpawnEnemy ();
>>>>>>> parent of bf0b388... Implement Shop System
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

<<<<<<< HEAD


//DEBUG: Shop Visiting
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


=======
>>>>>>> parent of bf0b388... Implement Shop System
}

