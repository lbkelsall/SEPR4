//Shows the GUI displaying the current player turn for 3 seconds and removes user input for wait period. 
//__NEW_FOR_ASSESSMENT_4__(START)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;

public class PlayerTurnGUI : MonoBehaviour {

	public GameObject playerTurnPanel;
	public Text multiplayerTimerText;
	public GameObject multiplayerPanel;
	private bool multiplayerGame = false;

	// Use this for initialization
	void Start () {
		//If multiplayer
		if (GameObject.Find ("Multiplayer Manager Object") != null) {
			if (MultiplayerManager.instance.GetTurnManager ().HasPlayerSwitched ()) {
				StartCoroutine ("ShowPlayerTurn");
			}
			multiplayerPanel.SetActive (true);
			multiplayerGame = true;
			MultiplayerManager.instance.GameActive (true);

		} else {
			multiplayerPanel.SetActive (false);
			multiplayerGame = false;
		}
	}
	
	public void Update(){
		//Update multiplayer timer
		if (multiplayerGame){
			multiplayerTimerText.text = MultiplayerManager.instance.GetTurnManager ().GetTimer ().ToString();
		}
	}

	private IEnumerator ShowPlayerTurn(){		

		//Before Waiting
		Cursor.lockState = CursorLockMode.Locked;	//Restricts mouse movement
		InputManager1 inputManager = gameObject.GetComponent <InputManager1> ();	//Disable map, notebook etc
		if (inputManager != null) {
			inputManager.enabled = false;
		}
		Time.timeScale = 0;							//Stops timers
		playerTurnPanel.SetActive (true);			//Shows player turn
		Text playerTurnText = playerTurnPanel.transform.GetChild (1).GetComponent<Text> ();
		playerTurnText.text = "Player " + MultiplayerManager.instance.GetTurnManager ().GetPlayerTurn ();
		Camera.main.GetComponent<Blur> ().enabled = true;	//Blur camera

		//Waiting
		yield return new WaitForSecondsRealtime (3);

		//After waiting
		playerTurnPanel.SetActive (false);					//Remove player turn GUI
		Camera.main.GetComponent<Blur> ().enabled = false; //Unblur camera
		Time.timeScale = 1;							//Reenable timer						
		Cursor.lockState = CursorLockMode.None;		//enables mouse movement
		if (inputManager != null) {					//enable map, notebook etc
			inputManager.enabled = true;
		}
	}
}
//__NEW_FOR_ASSESSMENT_4__(END)
