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
    public int herbPrice = 50;


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
    public void BuyHerb(Player playerInst, BattleLogController battleLog, UIController UI)
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

            //Update the UI
            UI.UpdatePlayerGP(playerInst);

            //give the player the item
            playerInst.herbs++;
            battleLog.AddText(playerInst.name + " now has " + playerInst.herbs + " Healing Herbs.");
        }
    }
}
