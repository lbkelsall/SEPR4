using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MultiplayerManager : MonoBehaviour {

	public Text numOfPlayersText;
	TurnManager turnManager;
	GameState[] states;
	int players;

	//Sets as a Singleton
	public static MultiplayerManager instance = null;
	void Awake() {  //Makes this a singleton class on awake
		if(instance == null) { //Does an instance already exist?
			instance = this;	//If not set instance to this
		} else if(instance != this) { //If it already exists and is not this then destroy this
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject); //Set this to not be destroyed when reloading scene
	}
		
	public void Setup() {
		players = int.Parse (numOfPlayersText.text.Trim ());
		SceneManager.LoadScene("Character Selection");

		CharacterSelector characterSelector = FindObjectOfType<CharacterSelector> ();
		for (int i = 0; i < players; i++) {
			PlayerCharacter detective = characterSelector.GetDetective ();
			states [i] = new GameState (detective);
		}
	}

	public void Start() {

		turnManager = new TurnManager (states, 3, 20.0f);

	}

	public void Update() {

		turnManager.EndTurn ();
	}



}
