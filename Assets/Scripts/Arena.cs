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

    // wether the first enemy has spawned
    bool gameStart = false;

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

    /// <summary>
    /// The Battle Log Controller.
    /// </summary>
    public BattleLogController battleLog;


    public UIController UI;

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
    public Player player = new Player();

    /// <summary>
    /// The Shop object
    /// </summary>
    Shop openShop = new Shop();




    // Use this for initialization
    void Start()
    {

        //Set the Combat state
        openShop.inCombat = true;

        //Set the player's Max Health
        player.maxHealth = player.health;

        //Set the UI Controllers
        UI = GameObject.Find("UIController").GetComponent<UIController>();
        battleLog = GameObject.Find("BattleLogController").GetComponent<BattleLogController>();



    }

    /// <summary>
    /// Updates Each Frame.
    /// </summary>
    void Update()
    {
        //Basically, Start(), but for UI Elements, since they don't like the normal start() Function for some reason.
        if (!gameStart)
        {
            //Disable the Game over UI Object
            UI.gameOverUI.SetActive(false);
            //Toggle the Combat UI
            UI.ToggleShopUI(openShop, this);
            //Update the player's stats
            UI.UpdatePlayer(player);
            //Spawn the first enemy         
            SpawnEnemy();
            //Make this run only once
            gameStart = true;
        }
        //While In Combat...

        if (openShop.inCombat)
        {
            //Switch to Combat Music
            ChangeBGM(audioLibrary[1]);
        }

        //Check wether the player's dead or not
        CheckHealth(player);
        //Check the player's XP for Levelling up
        CheckXP(player);



        //If player has defeated an enemy, Go to PostCombat State
        if (defeatedEnemy == true)
        {
            PostCombat(player, currentEnemy[0]);
        }

        //Hit F12 to take a Screenshot
        if (Input.GetKeyDown(KeyCode.F12))
        {
            ScreenCapture.CaptureScreenshot("Screenshot.png");
        }


    }

    /// <summary>
    /// Covers combat mechanics.
    /// </summary>
    /// <param name="playerInst">Player instance.</param>
    /// <param name="enemyInst">Enemy instance.</param>
    void Fight(Player playerInst, Enemy enemyInst)
    {
        if (!defeatedEnemy)
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
    }

    /// <summary>
    /// (ButtonClick) Fight enemy when clicked.
    /// </summary>
    public void FightOnClick()
    {
        if (openShop.inCombat)
        {
            SFX.PlayOneShot(audioLibrary[4]);
            Fight(player, currentEnemy[0]);
            UI.UpdatePlayer(player);
            UI.UpdateEnemyHP(currentEnemy[0]);
        }
    }
    /// <summary>
    /// (ButtonClick) Use Item when clicked.
    /// </summary>
    public void ItemOnClick()
    {
        if (openShop.inCombat)
        {
            //if the player isn't Dead...
            if (!playerIsDead)
            {
                //if the player's out of herbs, tell them as much
                if (player.herbs == 0)
                {
                    battleLog.AddText(player.name + " is out of Healing Herbs!");
                }

                //if the player's already at max health play the buzzer sound
                else if (player.health == player.maxHealth)
                {
                    SFX.PlayOneShot(audioLibrary[8]);
                    player.Heal(battleLog);
                }
                //otherwise, play the item use sound
                else
                {
                    SFX.PlayOneShot(audioLibrary[5]);
                    player.Heal(battleLog);
                }
            }
        }

    }
    /// <summary>
    /// (ButtonClick) Flee From Combat when clicked.
    /// </summary>
    public void FleeOnClick()
    {
        //if in the combat state and not in the dead state...
        if (openShop.inCombat && !playerIsDead)
        {
            //play SFX
            SFX.PlayOneShot(audioLibrary[7]);

            battleLog.AddText(player.name + " Flees from the combat, to find a better fight.");
            player.fleeCount++;
            //Reset Combat
            currentEnemy.Clear();
            UI.ToggleSprite();
            SpawnEnemy();
        }
    }

    /// <summary>
    /// (ButtonClick) Leaves the shop when clicked. 
    /// </summary>
    public void LeaveShopOnClick()
    {
        if (!openShop.inCombat)
        {
            LeaveShop(player);
        }
    }

    /// <summary>
    /// (ButtonClick) Tries to buy a herb when clicked.
    /// </summary>
    public void BuyHerbOnClick()
    {
        if (!openShop.inCombat)
        {
            if (player.gold < openShop.herbPrice)
            {
                SFX.PlayOneShot(audioLibrary[8]);
                openShop.BuyHerb(player, battleLog, UI);

            }
            //otherwise, play the item purchase sound
            else
            {
                SFX.PlayOneShot(audioLibrary[6]);
                openShop.BuyHerb(player, battleLog, UI);
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

            //pick a monster from the random encounter table at random, then add it to the currentEnemy list
            currentEnemy.Add(new Enemy(enemyStats[Random.Range(0, enemyStats.Length)]));
            // updates the Enemy UI with the New Data
            UI.ToggleSprite();
            UI.UpdateEnemy(currentEnemy[0]);

            //Run the currentEnemy's Encounter function
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
            UI.ToggleGameOverScreen();
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
            //Give the player XP for the fight
            RewardXP(playerInst, enemyInst);
            //create a temporary item with the enemy's item stats
            Item tempItem = new Item(enemyInst);
            //pick up the loot
            tempItem.GrabLoot(playerInst, enemyInst, battleLog);
            //Update the Player UI
            UI.UpdatePlayerGP(playerInst);
            //destroy the temporary item
            tempItem = null;

            //Shop Step	

            //Start the Shop Music
            isMusicChanging = true;
            ChangeBGM(audioLibrary[2]);
            //Start the Shop Gamestate
            openShop.inCombat = false;
            UI.ToggleSprite();
            //Visit the Shop
            UI.ToggleShopUI(openShop, this);
            openShop.VisitShop(playerInst, battleLog);
            defeatedEnemy = false;


        }

    }

    /// <summary>
    /// Runs when exiting the shop.
    /// </summary>
    /// <param name="playerInst">Player instance.</param>
    public void LeaveShop(Player playerInst)
    {

        defeatedEnemy = false;
        //leave the shop      
        openShop.inCombat = true;
        UI.ToggleShopUI(openShop, this);
        battleLog.AddText(playerInst.name + " Leaves the shop, and encounters a new enemy!");
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
    /// <summary>
    /// Rewards the player XP, and Updates the UI accordingly.
    /// </summary>
    /// <param name="playerInst"></param>
    /// <param name="enemyInst"></param>
    void RewardXP(Player playerInst, Enemy enemyInst)
    {
        //Reward the player with XP
        playerInst.experience += enemyInst.experience;
        //Write to the Battle Log
        battleLog.AddText(playerInst.name + " gains " + enemyInst.experience.ToString() + " XP!");
        UI.UpdatePlayerXP(playerInst);
    }
    void CheckXP(Player playerInst)
    {
        if (playerInst.experience >= 100)
        {
            //Level up the player
            LevelUp(playerInst);
            //Reset the XP counter
            playerInst.experience = 0;
        }
    }
    /// <summary>
    /// Levels up the player, Writes to the battle log, and Updates the player UI.
    /// </summary>
    /// <param name="playerInst">Player instance.</param>
    /// <param name="battleLog">Battle Log.</param>
    public void LevelUp(Player playerInst)
    {
        //increase level number
        playerInst.level++;
        //Write Congratulatory Text
        battleLog.AddText(playerInst.name + " Has Gained a Level!");
        battleLog.AddText(playerInst.name + " is now Level " + playerInst.level.ToString() + ".");
        //Increase the player's Max health
        playerInst.MaxHealthBoost(battleLog);
        //heal the player to Max HP
        playerInst.health = playerInst.maxHealth;
        //increase the player's Damage
        playerInst.DamageBoost(battleLog);
        //Update the Player's UI
        UI.UpdatePlayer(playerInst);
    }

}
