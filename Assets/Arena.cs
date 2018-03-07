using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates instances of the player, item, and enemy classes, and triggers their functions.
/// </summary>
public class Arena : MonoBehaviour {



	//Create new instances of the player, enemy and item classes
	public Player player1 = new Player ();
	public Enemy enemy1 = new Enemy ();



	// Use this for initialization
	void Start () 
	{
		player1.Greet ();
		enemy1.Encounter ();
		player1.Attack (enemy1);
		enemy1.Attack (player1);
		Item Item1 = new Item ("Gem",enemy1);
		Item1.GrabLoot (player1, enemy1);

		//Enemy enemy2 = new Enemy ("Dracky", 10, 10, 20);
		//enemy2.Encounter ();

	}

		
	}

