using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Arena - Creates the Player and enemy classes, and handles combat and item/gold gain.
/// </summary>
public class Arena : MonoBehaviour
{



    //Create new instances of the player, enemy and item classes
    public Player player1 = new Player();

    //A list of enemy stats
    public Enemy[] enemyStats;

    //Make a List for the enemy
    List<Enemy> enemyList = new List<Enemy>();

    //Player Death bool
    [HideInInspector]
    public bool playerIsDead = false;

    //bools for post-combat step
    [HideInInspector]
    public bool defeatedEnemy = false;

    //Open the Shop
    Shop openShop = new Shop();








    // Use this for initialization
    void Start()
    {

        //close the shop (for flow control)
        openShop.leftShop = true;

        //Set the player's Max Health
        player1.maxHealth = player1.playerHealth;

        //Game Start text
        Debug.Log("Controls:");
        Debug.Log("Left Click: Attack Enemy");
        Debug.Log("Right Click: Flee");
        Debug.Log("Shift Key: Use a Healing Herb");
		
        //Spawn the first Enemy
        SpawnEnemy();


    }

    //Updates each frame
    void Update()
    {


        //Check Player Input

        //While In Combat
        if (openShop.leftShop == true)
        {
            //Left CLick to Fight Enemy
            if (Input.GetMouseButtonDown(0))
            {
                Fight(player1, enemyList[0]);
            }
			
            //Right CLick to Reset Combat
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log(player1.playerName + " Flees from the combat, to find a better fight.");
                player1.fleeCount++;
                //Reset Combat
                enemyList.Clear();
                SpawnEnemy();
            }
        }
			
        //Shift Key to heal
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
			
            if (player1.playerHerbs == 0)
            {
                Debug.Log(player1.playerName + "does not have any Healing Herbs!");
            }
            else
            {
                player1.Heal();
            }
        }
			
        //Healing Herb Purchase
        if (Input.GetKeyDown(KeyCode.Alpha1) && openShop.leftShop == false || Input.GetKeyDown(KeyCode.Keypad1) && openShop.leftShop == false)
        {
            openShop.BuyHerb(player1);
        }
			
        //Damage Boost Purchase
        if (Input.GetKeyDown(KeyCode.Alpha2) && openShop.leftShop == false || Input.GetKeyDown(KeyCode.Keypad2) && openShop.leftShop == false)
        {
            openShop.BuyDamageBoost(player1);
        }
			
        //Leave Shop
        if (Input.GetKeyDown(KeyCode.Space) && openShop.leftShop == false)
        {
            LeaveShop(player1);
        }
		
	

        //Check wether the player's dead or not
        CheckHealth(player1);


        //If player has defeated an enemy, Do Post Combat Step
        if (defeatedEnemy == true)
        {
            PostCombat(player1, enemyList[0]);
        }

    }


    void Fight(Player playerInst, Enemy enemyInst)
    {	
        //if the player's health isn't 0, attack the enemy
        if (playerInst.playerHealth > 0)
        {
            //Combat Step
            playerInst.Attack(enemyInst);

            //if Enemy isn't dead, atack them back
            if (enemyInst.enemyHealth > 0)
            {
                enemyInst.Attack(playerInst);
            }
        }

        //if they have defeated the enemy, toggle the defeatedenemy bool
        if (enemyInst.enemyHealth == 0)
        {
            defeatedEnemy = true;
            playerInst.killCount++;
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
            enemyList.Add(new Enemy(enemyStats[Random.Range(0, enemyStats.Length)]));
            enemyList[0].Encounter();
        }
        else //this *should* Never happen!
        {			
            Debug.Log("How the heck is there more than one monster on the field? Lemme fix that...");
            //clear the list and restart combat
            enemyList.Clear();
            Debug.Log("Fixed. Restarting Combat...");
            SpawnEnemy();			
        }
    }


    /// <summary>
    /// Check's the Player's health, and cleanly stops the game when they die.
    /// </summary>
    /// <param name="playerInst">Selected Player instance.</param>
    void CheckHealth(Player playerInst)
    {   
        //if the player's dead, jump back to Update(); (which should cleanly loop)
        if (playerIsDead)
            return;

        //when the player's health hits Zero, declare the player is dead, yo!
        if (playerInst.playerHealth == 0)
        {
            Debug.Log("Alas, " + playerInst.playerName + " has Fallen.");
            Debug.Log("GAME OVER");
            Debug.Log("Final Stats:");
            Debug.Log("Number of Enemies Defeated: " + playerInst.killCount);
            Debug.Log("Number of times fleed from combat: " + playerInst.fleeCount);
            playerIsDead = true;
        }
		
    }

    void PostCombat(Player playerInst, Enemy enemyInst)
    {
        //Start Post-Combat/Shop step when enemy defeated
        if (defeatedEnemy == true)
        {
            //Loot Step

            //create a temporary item with the enemy's item stats
            Item tempItem = new Item(enemyInst);
            //pick up the loot
            tempItem.GrabLoot(playerInst, enemyInst);
            //destroy the temporary item
            tempItem = null;
				
            //Shop Step	

            //Open the shop
            openShop.leftShop = false;				
            //Visit the Shop
            openShop.VisitShop(playerInst);

            //Final step

            //Prepare the player or the next monster
            defeatedEnemy = false;

				
	
        }	

    }

    public void LeaveShop(Player playerInst)
    {
        Debug.Log(playerInst.playerName + "Leaves the shop, and encounters a new enemy!");
        openShop.leftShop = true;
        enemyList.Clear();
        SpawnEnemy();
    }

}

