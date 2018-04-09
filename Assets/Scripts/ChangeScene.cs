using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Scene Control Class
/// </summary>
public class ChangeScene : MonoBehaviour {

/// <summary>
/// Loads the selected scene when called.
/// </summary>
/// <param name="sceneNumber">The Scene Number</param>
	public void LoadScene(int sceneNumber)
	{
		SceneManager.LoadScene(sceneNumber);
	}

}
