using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogController : MonoBehaviour
{


    /// <summary>
    /// The text template Object.
    /// </summary>
    [SerializeField]
    private GameObject textTemplate;

    /// <summary>
    /// A list of objects currently in the Battle Log (For Text Limiting)
    /// </summary>
    private List<GameObject> battleLogItems;


    void Start()
    {
        //set the Text Template
        textTemplate = GameObject.Find("LogText");

        //Create the battle log object list
        battleLogItems = new List<GameObject>();
    }

    /// <summary>
    /// Creates a new text object and adds it to the combat log.
    /// </summary>
    /// <param name="newTextString">New text string.</param>
    public void AddText(string newTextString)
    {
        //Text limiter, removes older items from the list
        if (battleLogItems.Count == 6)
        {
            //get the top item from the list
            GameObject tempItem = battleLogItems[0];

            //Destroy it and remove it from the list
            Destroy(tempItem.gameObject);
            battleLogItems.Remove(tempItem);
        }

        //instantiate the object
        GameObject newText = Instantiate(textTemplate) as GameObject;
        //Enable the object
        newText.SetActive(true);

        //set the text of the object
        newText.GetComponent<BattleLogItem>().SetText(newTextString);

        //set the parent of the new object to the previous object
        newText.transform.SetParent(textTemplate.transform.parent, false);

        //add the object to the list
        battleLogItems.Add(newText.gameObject);

    }

}
