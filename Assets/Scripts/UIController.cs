using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //Get the Player's Data
    [HideInInspector]
    public Player player;
    [HideInInspector]
    public Enemy enemy;

    //declare the Player text fields
    Text playerNameText;
    Text playerHPText;
    Text playerGPText;
    Text playerLevelText;
    Text playerXPText;
    Text playerItemText;

    //Declare the Enemy Text Fields
    Text enemyNameText;
    Text enemyHPText;

    //Declare the Monster Sprite
    Image enemySprite;

    //declare the Shop and Command UI Objects
    GameObject commandUI;
    GameObject shopUI;

    GameObject pauseUI;

    //Declare the Game Over Screen
    [HideInInspector]
    public GameObject gameOverUI;

    // Use this for initialization
    void Start()
    {
        //Get the Player's Text Components
        playerNameText = GameObject.Find("PlayerNameText").GetComponent<Text>();
        playerHPText = GameObject.Find("HPText").GetComponent<Text>();
        playerGPText = GameObject.Find("GPText").GetComponent<Text>();
        playerLevelText = GameObject.Find("LevelText").GetComponent<Text>();
        playerXPText = GameObject.Find("XPNum").GetComponent<Text>();
        playerItemText = GameObject.Find("ItemNum").GetComponent<Text>();

        //get the Enemies' Text Components
        enemyNameText = GameObject.Find("EnemyName").GetComponent<Text>();
        enemyHPText = GameObject.Find("EnemyHP").GetComponent<Text>();



        //Get the MonsterSprite Object
        enemySprite = GameObject.Find("EnemySprite").GetComponent<Image>();

        //Set the Initial Values
        playerNameText.text = player.name.ToString();
        playerHPText.text = "HP: " + player.health.ToString();
        playerGPText.text = "GP: " + player.gold.ToString();
        enemySprite.enabled = false;

        //Get the Toggleable UI Objects
        commandUI = GameObject.Find("CommandUI");
        shopUI = GameObject.Find("ShopUI");
        gameOverUI = GameObject.Find("GameOverUI");
        pauseUI = GameObject.Find("PauseUI");




    }


    //Player UI Functions

    /// <summary>
    /// Updates the player's HP.
    /// </summary>
    /// <param name="playerInst">Player instance.</param>
    public void UpdatePlayerHP(Player playerInst)
    {
        playerHPText.text = "HP: " + playerInst.health.ToString();
    }

    /// <summary>
    /// Updates the player's GP.
    /// </summary>
    /// <param name="playerInst">Player instance.</param>
    public void UpdatePlayerGP(Player playerInst)
    {
        playerGPText.text = "GP: " + playerInst.gold.ToString();
    }

    /// <summary>
    /// Updates the player's item count.
    /// </summary>
    /// <param name="playerInst">Player instance.</param>
    public void UpdatePlayerItem(Player playerInst)
    {
        playerItemText.text = playerInst.herbs.ToString();
    }

    /// <summary>
    /// Updates the players XP.
    /// </summary>
    /// <param name="playerInst"></param>
    public void UpdatePlayerXP(Player playerInst)
    {
        playerXPText.text = playerInst.experience.ToString();
    }

    /// <summary>
    /// Updates the player's Level.
    /// </summary>
    /// <param name="playerInst"></param>
    public void UpdatePlayerLevel(Player playerInst)
    {
        playerLevelText.text = "Lv." + playerInst.level.ToString();
    }

    /// <summary>
    /// Updates all Player UI Elements at once.
    /// </summary>
    /// <param name="playerInst">The Player instance.</param>
    public void UpdatePlayer(Player playerInst)
    {
        UpdatePlayerGP(playerInst);
        UpdatePlayerHP(playerInst);
        UpdatePlayerItem(playerInst);
        UpdatePlayerLevel(playerInst);
        UpdatePlayerXP(playerInst);
    }

    //Enemy UI functions

    /// <summary>
    /// Updates the enemy sprite.
    /// </summary>
    /// <param name="enemyInst">Enemy instance.</param>
    public void UpdateEnemySprite(Enemy enemyInst)
    {
        //change the currently active sprite
        enemySprite.sprite = enemyInst.enemySprite;


    }
    /// <summary>
    /// Updates the Enemy name.
    /// </summary>
    /// <param name="enemyInst">Enemy Instance.</param>
    public void UpdateEnemyName(Enemy enemyInst)
    {
        enemyNameText.text = enemyInst.name;
    }
    /// <summary>
    /// Updates Enemy HP.
    /// </summary>
    /// <param name="enemyInst">Enemy Instance.</param>
    public void UpdateEnemyHP(Enemy enemyInst)
    {
        enemyHPText.text = "HP: " + enemyInst.health.ToString();
    }

    /// <summary>
    /// Updates all Enemy UI Elements at once.
    /// </summary>
    /// <param name="enemyInst">Enemy Isntance.</param>
    public void UpdateEnemy(Enemy enemyInst)
    {
        UpdateEnemyName(enemyInst);
        UpdateEnemyHP(enemyInst);
        UpdateEnemySprite(enemyInst);
    }

    //UI Window Toggles

    /// <summary>
    /// Toggles the Shop UI
    /// </summary>
    /// <param name="shop">Shop Instance</param>
    /// <param name="arena">Arena Instance</param>
    public void ToggleShopUI(Shop shop, Arena arena)
    {
        if (shop.inCombat)
        {
            shopUI.SetActive(false);
            commandUI.SetActive(true);
        }
        else if (!shop.inCombat)
        {
            shopUI.SetActive(true);
            commandUI.SetActive(false);
        }
        else if (arena.playerIsDead)
        {
            shopUI.SetActive(false);
            commandUI.SetActive(false);
        }
    }

    /// <summary>
    /// Toggles the Game Over Screen when called.
    /// </summary>
    public void ToggleGameOverScreen()
    {
        gameOverUI.SetActive(true);
    }

    /// <summary>
    /// Toggles the Enemy Sprite on and off.
    /// </summary>
    public void ToggleSprite()
    {
        if (enemySprite.enabled == true)
        {
            enemySprite.enabled = false;
        }
        else enemySprite.enabled = true;
    }

    public void TogglePauseScreen(Shop shop, Arena arena)
    {

        if (arena.gamePaused)
        {
            if (shop.inCombat)
            {
                //disable the command UI
                commandUI.SetActive(false);
                //enable the Pause UI
                pauseUI.SetActive(true);
            }
            else if (!shop.inCombat)
            {
                //disable the Shop UI
                shopUI.SetActive(false);
                //Enable the pause UI
                pauseUI.SetActive(true);

            }
            else if (arena.playerIsDead)
            {
                //Do nothing, cause the game over screen's already up
            }
        }
        else
        {
            if (shop.inCombat)
            {
                //enable the command UI
                commandUI.SetActive(true);
                //disable the Pause UI
                pauseUI.SetActive(false);
            }
            else if (!shop.inCombat)
            {
                //enable the Shop UI
                shopUI.SetActive(true);
                //disable the pause UI
                pauseUI.SetActive(false);

            }
            else if (arena.playerIsDead)
            {
                //Do nothing, cause the game over screen's already up
            }
        }
    }
}