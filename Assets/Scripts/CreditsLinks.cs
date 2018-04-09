using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls clickable URLs for the credits.
/// </summary>
public class CreditsLinks : MonoBehaviour

{
	/// <summary>
	/// Link1 to Link5 all open their respective links in the default web browser when called.
	/// </summary>
	public void Link1()
	{
		Application.OpenURL("https://opengameart.org/users/charlesgabriel");
	}

		public void Link2()
	{
		Application.OpenURL("http://twitter.com/RumLockerArt");
	}

		public void Link3()
	{
		Application.OpenURL("https://wingless-seraph.net/en/index");
	}

			public void Link4()
	{
		Application.OpenURL("http://twitter.com/OtterAfterDawn");
	}
}
