using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //Get the Player's Data
    [HideInInspector]
    public Player player;

    public Enemy enemy;

    //declare the Player text fields
    Text playerNameText;
    Text playerHPText;
    Text playerGPText;

    //Declare the Enemy Text Fields
    Text enemyNameText;
    Text enemyHPText;

    //Declare the Monster Sprite
    Image enemySprite;

    // Use this for initialization
    void Start()
    {   
        //Get the UI Text components
        playerNameText = GameObject.Find("PlayerNameText").GetComponent<Text>();
        playerHPText = GameObject.Find("HPText").GetComponent<Text>();
        playerGPText = GameObject.Find("GPText").GetComponent<Text>();
        enemyNameText = GameObject.Find("EnemyName").GetComponent<Text>();
        enemyHPText = GameObject.Find("EnemyHP").GetComponent<Text>();



        //Get the MonsterSprite Object
        enemySprite = GameObject.Find("EnemySprite").GetComponent<Image>();

        //Set the Initial Values
        playerNameText.text = player.name.ToString(); 
        playerHPText.text = "HP: " + player.health.ToString();
        playerGPText.text = "GP: " + player.gold.ToString();
        enemySprite.sprite = Resources.Load<Sprite>("Characters/Slime");
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
    /// Updates the monster sprite.
    /// </summary>
    /// <param name="enemyInst">Enemy instance.</param>
    public void UpdateEnemySprite(Enemy enemyInst)
    {
        //change the currently active sprite
        enemySprite.sprite = enemyInst.enemySprite;


    }
	
    //Enemy UI functions

    public void UpdateEnemyName(Enemy enemyInst)
    {
        enemyNameText.text = enemyInst.name;
    }

    public void UpdateEnemyHP(Enemy enemyInst)
    {
        enemyHPText.text = "HP: " + enemyInst.health.ToString();
    }



}
