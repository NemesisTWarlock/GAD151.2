using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // the Text Component of the button
    private Text myText;
    //the text when hovering over the button
	[TextArea]//Allows for multiple line text input
    public string hoverText;
    //the text when not hovering over the button
	[TextArea]
    public string normalText;

    void Start()
    {
        //get the Text component of the button
        myText = GetComponentInChildren<Text>();
    }
    //when the cursor is hovering over the button...
    public void OnPointerEnter(PointerEventData eventData)
    {
		hoverText.Replace("\\n", "\n"); 
        //change the text to the hovertext
	        myText.text = hoverText;
    }
    // when the cursor is *not* hovering over the button...
    public void OnPointerExit(PointerEventData eventData)
    {
        //change the text to the normaltext
        myText.text = normalText;
    }


}