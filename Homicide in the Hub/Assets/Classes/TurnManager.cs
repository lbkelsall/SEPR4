using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class TurnManager{
		
	private int playerTurn = 1;

	private int numOfPlayers = 2;

	private bool playerSwitched = false;

	private int actionsCap;

	private int actionCounter;

	public float timer;

	private float timeCap;

	private GameState[] states;

	public TurnManager(int maxActions, float maxTime, int numOfPlayers){
		this.actionsCap = maxActions;
		this.timer = maxTime;
		this.timeCap = maxTime;
		this.numOfPlayers = numOfPlayers;
	}


	private void CyclePlayers(){
		states [playerTurn-1].Save ();
		playerTurn += 1;
		if (playerTurn > numOfPlayers) {
			playerTurn = 1;
		} 
		timer = timeCap;
		actionCounter = 0;
		states [playerTurn-1].Load ();
	}

	public void IncrementActionCounter(){
		actionCounter++;
	}

	public void EndTurnCheck(){
		playerSwitched = false;
		if ((actionCounter >= actionsCap) || (timer <= 0f)) {
			playerSwitched = true;
			CyclePlayers (); //Comment this out when unit testing this method
		} 
	}

	public int GetPlayerTurn(){
		return playerTurn;
	}

	public void SetPlayerTurn(int turn){
		this.playerTurn = turn;
	}

	public void SetStates(GameState[] states, int numOfPlayers){
		this.states = new GameState[numOfPlayers];
		this.states = states;
	}

	public bool HasPlayerSwitched(){
		return playerSwitched;
	}

	public void DecrementTimer() {
		timer -= Time.deltaTime;
	}

	public int GetTimer(){
		return (int) timer;
	}


	//Used for testing
	public int GetActionCounter(){
		return actionCounter;
	}

	public GameState[] GetStates(){
		return states;
	}

	

}



