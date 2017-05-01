//Placed on the drop areas of the riddle menu to allow letters to be dragged in the area and dropped from one to another
//__NEW_FOR_ASSESSMENT_4__(START)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropAreaScript : MonoBehaviour, IDropHandler {

	//When a letter is dropped over a drop area this is function is called 
	//to set the parent to the drop area and the position to the mouse position
	public void OnDrop(PointerEventData eventData){
		LetterScript ls = eventData.pointerDrag.GetComponent <LetterScript> ();
		if (ls != null) {
			ls.SetPosition (eventData.position, this.gameObject);
		}
	}
}
//__NEW_FOR_ASSESSMENT_4__(END)
