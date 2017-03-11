using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropAreaScript : MonoBehaviour, IDropHandler {

	public void OnDrop(PointerEventData eventData){

		eventData.pointerDrag.transform.parent = gameObject.transform;

	}
}
