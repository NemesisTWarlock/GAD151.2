using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]//Displays Player stats in the Unity Editor.


/// <summary>
/// Basic player info.
/// </summary>
public class Player
{

    /// <summary>
    /// The name of the player.
    /// </summary>
    public string name;

    /// <summary>
    /// The player's Current health.
    /// </summary>
    public int health;

    /// <summary>
    /// The total damage.
    /// </summary>
    public int damage;

    /// <summary>
    /// The total Gold the player is carrying.
    /// </summary>
    public int gold;

    /// <summary>
    /// How many Healing Herbs the player is carrying.
    /// </summary>
    public int herbs;

    /// <summary>
    /// The Player's Max Health. (Hidden)
    /// </summary>
    [HideInInspector]
    public int maxHealth;

    /// <summary>
    ///Number of times the player has Fleed from combat. (Hidden) 
    /// </summary>
    [HideInInspector]
    public int fleeCount;

    /// <summary>
    /// Number of times the player has defeated an enemy. (Hidden)
    /// </summary>
    [HideInInspector]
    public int killCount;


    /// <summary>
    /// Default Constructor for Player objects.
    /// </summary>
    public Player()
    {
        name = "Erdrick";
        health = 100;
        damage = 5;
        gold = 0;
        herbs = 2;
    }


    /// <summary>
    /// Attack the specified enemy instance.
    /// </summary>
    /// <param name="enemyInst">Enemy instance.</param>
    public void Attack(Enemy enemyInst, BattleLogController battleLog)
    {	
        //subtract weapon damage from enemy health (use Mathf.Max to make sure the enemy Health doesn't fall below 0)	
        enemyInst.health = Mathf.Max(0, enemyInst.health - damage);
        //Update the UI

        battleLog.AddText(name + " Hits the " + enemyInst.name + " for " + damage + " damage, reducing it's health to " + enemyInst.health + "!");
    }

    /// <summary>
    /// Heals the player by using a Healing Herb when called.
    /// </summary>
    public void Heal(BattleLogController battleLog)
    {
        //setup healing range
        int healAmount = Random.Range(25, 50);

        //if already at max health...
        if (health == maxHealth)
        {
            battleLog.AddText("Already at Max health!");
            return;
        }
        else
        {
            //heal the player
            health += Mathf.Min(healAmount, maxHealth);
            //remove a herb 
            herbs--;


          
            //print text
            battleLog.AddText(name + " Uses a Healing Herb, healing them for " + healAmount + "!");
            battleLog.AddText(name + " has " + herbs + " herbs left.");
        }
    }


    /// <summary>
    /// Increases the player's damage output when called.
    /// </summary>
    public void DamageBoost(BattleLogController battleLog)
    {
        //setup Damage boost range
        int dmgBoostAmount = Random.Range(2, 10);
        //Boost Damage
        damage += dmgBoostAmount;
        //Print Text
        battleLog.AddText(name + " increases their total damage output by " + dmgBoostAmount + " to " + damage + "!");


    }

    /// <summary>
    /// Boosts the player's Maximum health when called. 
    /// </summary>
    public void MaxHealthBoost(BattleLogController battleLog)
    {
        //setup HP Boost range
        int hpBoostAmount = Random.Range(5, 20);
        //Boost Max HP
        maxHealth += hpBoostAmount;
        //Print Text
        battleLog.AddText(name + " increases their health by " + hpBoostAmount + " to " + maxHealth + "!");
    }


}
