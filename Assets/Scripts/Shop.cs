using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Controls Shop Functionality.
/// </summary>
public class Shop
{
    

    //Declare shop bool
    public bool inCombat;

    //Setup Shop Prices
    public int herbPrice = 10;
    public int damageBoostPrice = 200;
    public int healthBoostPrice = 200;


    /// <summary>
    /// Displays the shop text when called.
    /// </summary>
    /// <param name="playerInst">Player instance.</param>
    public void VisitShop(Player playerInst, BattleLogController battleLog)
    {
                
        //Shop Text
        battleLog.AddText(playerInst.name + " Enters the Shop.");
    }


    /// <summary>
    /// Purchases a Herb.
    /// </summary>
    /// <param name="playerInst">Player instance.</param>
    public void BuyHerb(Player playerInst, BattleLogController battleLog)
    {
        //if the player doesn't have enough GP...
        if (playerInst.gold < herbPrice)
        {
           
            battleLog.AddText("Not enough GP!");
        }
        else
        {
            //pay the cost of the item
            playerInst.gold -= herbPrice;
            //update the UI

            //give the player the item
            playerInst.herbs++;
            battleLog.AddText(playerInst.name + " now has " + playerInst.herbs + " Healing Herbs.");
        }
    }

    /// <summary>
    /// Purchases a Damage booster.
    /// </summary>
    /// <param name="playerInst">Player instance.</param>
    public void BuyDamageBoost(Player playerInst, BattleLogController battleLog)
    {
        //if the player doesn't have enough GP...
        if (playerInst.gold < damageBoostPrice)
        {
            battleLog.AddText("Not enough GP!");
        }
        else
            
        {	             
            //pay the cost of the item
            playerInst.gold -= damageBoostPrice;	
            //boost the player's damage
            playerInst.DamageBoost(battleLog);	
        }	
    }

    /// <summary>
    /// Purchases a Maximum HP booster.
    /// </summary>
    /// <param name="playerInst">Player instance.</param>
    public void BuyMaxhealthBoost(Player playerInst, BattleLogController battleLog)
    {
        //if they don't have enough GP...
        if (playerInst.gold < healthBoostPrice)
        {
            battleLog.AddText("Not enough GP!");
        }
        else
        {   
            //pay the cost itf the item
            playerInst.gold -= healthBoostPrice;
            //boost the player's max health
            playerInst.MaxHealthBoost(battleLog);   
        }
    }


}
