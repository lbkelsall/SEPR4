﻿//Placed on the letter prefabs to allow them to be clicked, dragged and dropped into drop areas. 
//__NEW_FOR_ASSESSMENT_4__(START)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LetterScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{

	Vector2 position;
	Transform parent;

	//When the letter has just started being dragged store the current position and parent
	public void OnBeginDrag(PointerEventData eventData){
		position = this.transform.position;
		parent = this.transform.parent;
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	//Move the letter to the mouse position while dragging
	public void OnDrag(PointerEventData eventData){
		this.transform.position = eventData.position;
	}

	//Click released so set the position and parent. If dropped in a drop area, the drop area will
	//have updated position and parent to itself, if they will be set to the last known position and parent.
	public void OnEndDrag(PointerEventData eventData){
		this.transform.position = position;
		this.transform.SetParent (parent);
		GetComponent<CanvasGroup> ().blocksRaycasts = true;

		//Reordering the elements in the drop area into the order dropped
		Transform[] siblings = parent.GetComponentsInChildren<Transform> (true);
		for (int i = 0; i < (siblings.Length - 1); i++) {
			if (eventData.position.x < siblings [0].position.x) {
				gameObject.transform.SetSiblingIndex (0);
			} else if (eventData.position.x > siblings [siblings.Length-1].position.x) {
				gameObject.transform.SetSiblingIndex (siblings.Length);
			} else if ((eventData.position.x > siblings [i].position.x) && (eventData.position.x < siblings [i + 1].position.x)) {
				gameObject.transform.SetSiblingIndex (i);
			} 
		}
	}

	//Called by the drop area to set the position and parent of the letter to the drop area
	public void SetPosition(Vector2 position, GameObject parent){
		this.position = position;
		this.parent = parent.transform;
	}
}
//__NEW_FOR_ASSESSMENT_4__(END)