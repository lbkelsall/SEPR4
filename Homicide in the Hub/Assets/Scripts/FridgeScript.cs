//Used when the player clicks on the fridge to open the fridge scene if the player has not solved the puzzle before otherwise load the factory. 
//__NEW_FOR_ASSESSMENT_4__(START)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FridgeScript : MonoBehaviour {

	//Loads the factory if the riddle has been solved, if not it loads the riddle area
	void OnMouseDown(){
		if (GameMaster.instance.GetRiddleStatus () == true) {
			print ("Will take to factory, need to set!");
			SceneManager.LoadScene ("Factory");
		} else {
			SceneManager.LoadScene ("Fridge");
		}
	}
}
//__NEW_FOR_ASSESSMENT_4__(END)