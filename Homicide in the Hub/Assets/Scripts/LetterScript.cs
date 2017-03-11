using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LetterScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{

	Vector2 position;

	public void OnBeginDrag(PointerEventData eventData){
		position = this.transform.position;
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData){
		this.transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData){
		this.transform.position = position;
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}
}