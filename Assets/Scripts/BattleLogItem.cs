using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleLogItem : MonoBehaviour {

   /// <summary>
   /// Sets the string Input for combat log items.
   /// </summary>
   /// <param name="textIN">String input</param>
    public void SetText (string textIN)
    {
        GetComponent<Text>().text = textIN;
    }


}
