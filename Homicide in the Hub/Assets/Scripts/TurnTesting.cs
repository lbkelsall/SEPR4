﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTesting : MonoBehaviour {

	TurnManager turnManager;

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
