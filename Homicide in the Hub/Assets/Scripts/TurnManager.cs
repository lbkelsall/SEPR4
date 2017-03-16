using UnityEngine;
using System.Collections;

public class TurnManager{
		
	private int playerTurn;

	private int numOfPlayers;

	private int actionsCap;

	private int actionCounter;

	private float timer;

	private float timeCap;

	private GameState[] states;

	public TurnManager(GameState[] states, int maxActions, float maxTime){
		this.states = states; 
		this.actionsCap = maxActions;
		this.timeCap = maxTime;
	}

	private void CyclePlayers(){
		states [playerTurn].Save ();

		if (playerTurn == numOfPlayers) {
			playerTurn = 0;
		} else {
			playerTurn++;
		}
		states [playerTurn].Load ();
		timer = 0.0f;
		actionCounter = 0;
	}

	private void IncrementActionCounter(){
		actionCounter++;
	}

	public void EndTurn(){
		if ((actionCounter >= actionsCap) || (timer >= timeCap)) {
			CyclePlayers ();
		}
	}


}

