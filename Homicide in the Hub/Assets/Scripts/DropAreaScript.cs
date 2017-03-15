using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropAreaScript : MonoBehaviour, IDropHandler {

	public void OnDrop(PointerEventData eventData){


		LetterScript ls = eventData.pointerDrag.GetComponent <LetterScript> ();
		if (ls != null) {
			ls.SetPosition (eventData.position, this.gameObject);
		}
	
	}
}
