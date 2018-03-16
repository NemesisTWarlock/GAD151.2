using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Controls Shop Functionality.
/// </summary>
public class Shop
{
    

    //Declare shop bool
    public bool leftShop;

    //Setup Shop Prices
    public int herbPrice = 10;
    public int damageBoostPrice = 200;
    public int healthBoostPrice = 200;


    /// <summary>
    /// Displays the shop text when called.
    /// </summary>
    /// <param name="playerInst">Player instance.</param>
    public void VisitShop(Player playerInst)
    {
                
        //Shop Text
        Debug.Log("Welcome to the Shop!");
        Debug.Log(playerInst.name + " has " + playerInst.gold + "GP.");
        Debug.Log("Press 1 to purchase a Healing Herb for " + herbPrice + "GP.");
        Debug.Log("Press 2 to Purchase a Damage Boost for " + damageBoostPrice + "GP.");
        Debug.Log("Press 3 to purcase a Maximum Health boost for " + healthBoostPrice + "GP.");
        Debug.Log("Press SPACE to leave the shop.");

    }


    /// <summary>
    /// Purchases a Herb.
    /// </summary>
    /// <param name="playerInst">Player instance.</param>
    public void BuyHerb(Player playerInst)
    {
        //if the player doesn't have enough GP...
        if (playerInst.gold < herbPrice)
        {
           
            Debug.Log("Not enough GP!");
        }
        else
        {
            //pay the cost of the item
            playerInst.gold -= herbPrice;
            //give the player the item
            playerInst.herbs++;
            Debug.Log(playerInst.name + " now has " + playerInst.herbs + " Healing Herbs.");
        }
    }

    /// <summary>
    /// Purchases a Damage booster.
    /// </summary>
    /// <param name="playerInst">Player instance.</param>
    public void BuyDamageBoost(Player playerInst)
    {
        //if the player doesn't have enough GP...
        if (playerInst.gold < damageBoostPrice)
        {
            Debug.Log("Not enough GP!");
        }
        else
            
        {	             
            //pay the cost of the item
            playerInst.gold -= damageBoostPrice;	
            //boost the player's damage
            playerInst.DamageBoost();	
        }	
    }

    /// <summary>
    /// Purchases a Maximum HP booster.
    /// </summary>
    /// <param name="playerInst">Player instance.</param>
    public void BuyMaxhealthBoost(Player playerInst)
    {
        //if they don't have enough GP...
        if (playerInst.gold < healthBoostPrice)
        {
            Debug.Log("Not enough GP!");
        }
        else
        {   
            //pay the cost itf the item
            playerInst.gold -= healthBoostPrice;
            //boost the player's max health
            playerInst.MaxHealthBoost();   
        }
    }


}
