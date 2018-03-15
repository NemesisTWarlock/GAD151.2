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

    int herbPrice = 50;
    int damageBoostPrice = 100;


    public void VisitShop(Player playerInst)
    {
        
        
        //Shop Text
        Debug.Log("Welcome to the Shop!");
        Debug.Log(playerInst.playerName + " has " + playerInst.playerGold + "GP.");
        Debug.Log("Press 1 to purchase a Healing Herb for " + herbPrice + "GP.");
        Debug.Log("Press 2 to Purchase a Damage Boost for " + damageBoostPrice + "GP.");
        Debug.Log("Press SPACE to leave the shop.");

    }



    public void BuyHerb(Player playerInst)
    {
        if (playerInst.playerGold < herbPrice)
        {
            Debug.Log("Not enough GP!");
        }
        else
        {
            playerInst.playerGold -= herbPrice;
            playerInst.playerHerbs++;
            Debug.Log(playerInst.playerName + " now has " + playerInst.playerHerbs + " Healing Herbs.");
        }
    }

    public void BuyDamageBoost(Player playerInst)
    {
        if (playerInst.playerGold < damageBoostPrice)
        {
            Debug.Log("Not enough GP!");
        }
        else
        {					
            playerInst.DamageBoost();	
        }	
    }


}
