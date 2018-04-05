using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(AudioSource))]

/// <summary>
/// Arena - Handles Combat, input, and Music sytsems
/// </summary>
public class Arena : MonoBehaviour
{
    //Flow control Bools

    /// <summary>
    /// Wether the player is dead
    /// </summary>
    [HideInInspector]
    public bool playerIsDead = false;

    /// <summary>
    /// Wether the player has defeated an enemy
    /// </summary>
    [HideInInspector]
    public bool defeatedEnemy = false;

    /// <summary>
    /// Wether the game has begun
    /// </summary>
    [HideInInspector]
    public bool gameStart = false;

    /// <summary>
    /// wether the music is about to change.
    /// </summary>
    [HideInInspector]
    public bool isMusicChanging = false;

    //GameObjects

    /// <summary>
    /// Music AudioSource.
    /// </summary>
    [HideInInspector]
    public AudioSource BGM;

    /// <summary>
    /// Sound Effect AudioSource.
    /// </summary>
    [HideInInspector]
    public AudioSource SFX;
    //The battleLog Controller
    public BattleLogController battleLog;

    // Arrays and Lists


    /// <summary>
    /// Audio Library array.
    /// </summary>
    //[HideInInspector]
    public AudioClip[] audioLibrary;

    /// <summary>
    /// A list of Enemy names and stats/Random Encounter table.
    /// </summary>
//    [HideInInspector]
    public Enemy[] enemyStats;

    //Make a List for the enemy
    List<Enemy> currentEnemy = new List<Enemy>();


    //Create Objects

    /// <summary>
    /// The player object
    /// </summary>
    public Player player1 = new Player();

    /// <summary>
    /// The Shop object
    /// </summary>
    Shop openShop = new Shop();







    // Use this for initialization
    void Start()
    {


        //Cue the Music!
        isMusicChanging = true;
        ChangeBGM(audioLibrary[0]);

        //close the shop (for flow control)
        openShop.inCombat = true;

        //Set the player's Max Health
        player1.maxHealth = player1.health;



        //Set the UI Controller
        battleLog = GameObject.Find("UIController").GetComponent<BattleLogController>();

    }

    //Updates each frame
    void Update()
    {
        if (gameStart == false)
        {
            //do nothing while waiting for initial input   
        }

        //When the player hits the spacebar to start the game...
        if (Input.GetKeyDown(KeyCode.Space) && !gameStart)
        {
            //Start the Game
            gameStart = true;
            //Spawn the first Enemy
            SpawnEnemy();
        }

        ///once the game has begun...
        if (gameStart == true)
        {

            //While In Combat...

            if (openShop.inCombat)
            {
                //Switch to Combat Music
                ChangeBGM(audioLibrary[1]);

                //Check for Player Input

                //Left CLick to Fight Enemy
                if (Input.GetMouseButtonDown(0))
                {
                    SFX.PlayOneShot(audioLibrary[4]);
                    Fight(player1, currentEnemy[0]);
                }

                //Right CLick to Reset Combat
                if (Input.GetMouseButtonDown(1) && !playerIsDead)
                {
                    //play SFX
                    SFX.PlayOneShot(audioLibrary[7]);

                    battleLog.AddText(player1.name + " Flees from the combat, to find a better fight.");
                    player1.fleeCount++;
                    //Reset Combat
                    currentEnemy.Clear();
                    SpawnEnemy();
                }

                //if the player isn't Dead...
                if (!playerIsDead)
                {
                    //shift key to use a herb to heal
                    if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                    {
                        //if the player's out of herbs, tell them as much
                        if (player1.herbs == 0)
                        {
                            battleLog.AddText(player1.name + "is out of Healing Herbs!");
                        }

                        //if the player's already at max health play the buzzer sound
                        else if (player1.health == player1.maxHealth)
                        {
                            SFX.PlayOneShot(audioLibrary[8]);
                            player1.Heal(battleLog);
                        }
                        //otherwise, play the item use sound
                        else
                        {
                            SFX.PlayOneShot(audioLibrary[5]);
                            player1.Heal(battleLog);
                        }
                    }
                }
            }


            //While in the shop...   

            if (!openShop.inCombat)
            {
                //Healing Herb Purchase option
                if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
                {
                    //play buzzer if they don't have enough GP
                    if (player1.gold < openShop.herbPrice)
                    {
                        SFX.PlayOneShot(audioLibrary[8]);
                        openShop.BuyHerb(player1, battleLog);

                    }
                    //otherwise, play the item purchase sound
                    else
                    {
                        SFX.PlayOneShot(audioLibrary[6]);
                        openShop.BuyHerb(player1, battleLog);
                    }
                }

                //Damage Boost Purchase option
                if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
                {
                    //play buzzer if they don't have enough GP
                    if (player1.gold < openShop.damageBoostPrice)
                    {
                        SFX.PlayOneShot(audioLibrary[8]);
                        openShop.BuyDamageBoost(player1, battleLog);
                    }
                    //otherwise, play the item purchase sound
                    else
                    {
                        SFX.PlayOneShot(audioLibrary[6]);
                        openShop.BuyDamageBoost(player1, battleLog);
                    }
                }

                //Max health boost purchase option
                if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
                {
                    //play buzzer if they don't have enough GP
                    if (player1.gold < openShop.damageBoostPrice)
                    {
                        SFX.PlayOneShot(audioLibrary[8]);
                        openShop.BuyMaxhealthBoost(player1, battleLog);
                    }
                    //otherwise, play the item purchase sound
                    else
                    {
                        SFX.PlayOneShot(audioLibrary[6]);
                        openShop.BuyMaxhealthBoost(player1, battleLog);
                    }
                }

                //Leave Shop option
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    LeaveShop(player1);
                }
            }



            //Check wether the player's dead or not
            CheckHealth(player1);


            //If player has defeated an enemy, Do Post Combat Step
            if (defeatedEnemy == true)
            {
                PostCombat(player1, currentEnemy[0]);
            }
        }

    }

    /// <summary>
    /// Covers combat mechanics.
    /// </summary>
    /// <param name="playerInst">Player instance.</param>
    /// <param name="enemyInst">Enemy instance.</param>
    void Fight(Player playerInst, Enemy enemyInst)
    {
        //if the player's health isn't 0
        if (playerInst.health > 0)
        {
            //attack the enemy
            playerInst.Attack(enemyInst, battleLog);

            //if Enemy isn't dead, atack them back
            if (enemyInst.health > 0)
            {
                enemyInst.Attack(playerInst, battleLog);
            }
        }

        //if they have defeated the enemy, toggle the defeatedenemy bool and increase their kill count
        if (enemyInst.health == 0)
        {
            defeatedEnemy = true;
            playerInst.killCount++;
        }
    }

    void FightOnClick()
    {
        Fight(player1, currentEnemy[0]);
    }

    void ItemOnClick()
    {
        //if the player isn't Dead...
        if (!playerIsDead)
        {
            //if the player's out of herbs, tell them as much
            if (player1.herbs == 0)
            {
                battleLog.AddText(player1.name + "is out of Healing Herbs!");
            }

            //if the player's already at max health play the buzzer sound
            else if (player1.health == player1.maxHealth)
            {
                SFX.PlayOneShot(audioLibrary[8]);
                player1.Heal(battleLog);
            }
            //otherwise, play the item use sound
            else
            {
                SFX.PlayOneShot(audioLibrary[5]);
                player1.Heal(battleLog);
            }
        }
    }


    /// <summary>
    /// Manages enemy instance spawning.
    /// </summary>
    void SpawnEnemy()
    {
        //Sanity check -- If there's zero monsters on the battlefield...
        if (currentEnemy.Count == 0)
        {
            //Change the music track
            isMusicChanging = true;
            ChangeBGM(audioLibrary[1]);

            //pick a monster from the random encounter table then run the encounter function
            currentEnemy.Add(new Enemy(enemyStats[Random.Range(0, enemyStats.Length)]));
            currentEnemy[0].Encounter(battleLog);

        }
        else //this *should* Never happen!
        {
            battleLog.AddText("How the heck is there more than one monster on the field? Lemme fix that...");
            //clear the list and restart combat
            currentEnemy.Clear();
            battleLog.AddText("Fixed. Restarting Combat...");
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
        if (playerInst.health == 0)
        {
            //play Death Music
            isMusicChanging = true;
            ChangeBGM(audioLibrary[3]);

            //Death Text
            battleLog.AddText("Alas, " + playerInst.name + " has Fallen.");
            battleLog.AddText("GAME OVER");
            battleLog.AddText("Final Stats:");
            battleLog.AddText("Number of Enemies Defeated: " + playerInst.killCount);
            battleLog.AddText("Number of times fleed from combat: " + playerInst.fleeCount);
            playerIsDead = true;
        }

    }

    /// <summary>
    /// Gives the player gold/items, and opens the shop.
    /// </summary>
    /// <param name="playerInst">Player instance.</param>
    /// <param name="enemyInst">Enemy instance.</param>
    void PostCombat(Player playerInst, Enemy enemyInst)
    {

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

            //Start the Shop Music
            isMusicChanging = true;
            ChangeBGM(audioLibrary[2]);
            //Open the shop
            openShop.inCombat = false;
            //Visit the Shop
            openShop.VisitShop(playerInst, battleLog);

            //Final step

            //Prepare the player or the next monster
            defeatedEnemy = false;
        }

    }

    /// <summary>
    /// Runs when exiting the shop.
    /// </summary>
    /// <param name="playerInst">Player instance.</param>
    public void LeaveShop(Player playerInst)
    {
        //leave the shop
        battleLog.AddText(playerInst.name + "Leaves the shop, and encounters a new enemy!");
        openShop.inCombat = true;
        //reset combat
        currentEnemy.Clear();
        SpawnEnemy();
    }




    /// <summary>
    /// Changes the background music.
    /// </summary>
    /// <param name="Music">Music AudioClip</param>
    public void ChangeBGM(AudioClip Music)
    {
        //When we're ready to change the music track...
        if (isMusicChanging == true)
        {
            //Stop the currently playing music
            BGM.Stop();
            //change the audioclip
            BGM.clip = Music;
            //make sure it loops
            BGM.loop = true;
            //play that funky music
            BGM.Play();
            //Make sure this doesn't happen every frame (flow control)
            isMusicChanging = false;
        }
    }




}
