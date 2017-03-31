using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FridgeScript : MonoBehaviour {

	// Use this for initialization
	void OnMouseDown(){
		if (GameMaster.instance.GetRiddleStatus () == true) {
			print ("Will take to factory, need to set!");
			SceneManager.LoadScene ("Factory");
		} else {
			SceneManager.LoadScene ("Fridge");
		}
	}
}
