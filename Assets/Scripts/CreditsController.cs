using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
/// <summary>
/// Controls clickable URLs for the credits.
/// </summary>
public class CreditsController : MonoBehaviour
{

    /// <summary>
    /// Opens the Specified URL on Click
    /// </summary>
    /// <param name="linkURL">The Link URL</param>
    public void OpenURL(string linkURL)
    {
        Application.OpenURL(linkURL);
    }

}
