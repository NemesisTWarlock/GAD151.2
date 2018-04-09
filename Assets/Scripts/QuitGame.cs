using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls Game Exit
/// </summary>
public class QuitGame : MonoBehaviour {

/// <summary>
/// Quits the game when called.
/// </summary>
	public void Quit ()
	{
		Application.Quit ();
	}

}
