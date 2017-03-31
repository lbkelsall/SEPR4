using UnityEngine;
using System.Collections;

public class TurnManager{
		
	private int playerTurn = 1;

	private int numOfPlayers = 1;

	private int actionsCap;

	private int actionCounter;

	private float timer;

	private float timeCap;

	private GameState[] states;

	public TurnManager(int maxActions, float maxTime, int numOfPlayers){
		this.states = states; 
		this.actionsCap = maxActions;
		this.timeCap = maxTime;
		this.numOfPlayers = numOfPlayers;
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

	public void IncrementActionCounter(){
		actionCounter++;
	}

	public void EndTurnCheck(){
		if ((actionCounter >= actionsCap) || (timer >= timeCap)) {
			CyclePlayers ();
			Debug.Log ("Cycle Players");
		}
	}

	public int GetPlayerTurn(){
		return playerTurn;
	}

	public void SetPlayerTurn(int turn){
		this.playerTurn = turn;
	}

	public void SetStates(GameState[] states){
		this.states = states;
	}
}

