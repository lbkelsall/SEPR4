using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTesting : MonoBehaviour {

	TurnManager turnManager;


	//Sets as a Singleton
	public static TurnTesting instance = null;
	void Awake() {  //Makes this a singleton class on awake
		if(instance == null) { //Does an instance already exist?
			instance = this;	//If not set instance to this
		} else if(instance != this) { //If it already exists and is not this then destroy this
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject); //Set this to not be destroyed when reloading scene
	}

	// Use this for initialization
	void Start(){
		if (MultiplayerManager.instance != null) {
			turnManager = MultiplayerManager.instance.GetTurnManager ();
		}
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.C)) {
			turnManager.IncrementActionCounter ();
		}

	}
}
