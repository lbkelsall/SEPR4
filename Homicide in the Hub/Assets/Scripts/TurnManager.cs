﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class TurnManager{
		
	private int playerTurn = 1;

	private int numOfPlayers = 1;

	private bool playerSwitched = false;

	private int actionsCap;

	private int actionCounter;

	public float timer;

	private float timeCap;

	private GameState[] states;

	public TurnManager(int maxActions, float maxTime, int numOfPlayers){
		this.actionsCap = maxActions;
		this.timeCap = maxTime;
		this.numOfPlayers = numOfPlayers;
	}

	private void CyclePlayers(){
		states [playerTurn-1].Save ();
		playerTurn += 1;
		if (playerTurn > numOfPlayers) {
			playerTurn = 1;
		} 
		timer = 0.0f;
		actionCounter = 0;
		states [playerTurn-1].Load ();

	}

	public void IncrementActionCounter(){
		actionCounter++;
	}

	public void EndTurnCheck(){
		playerSwitched = false;
		Debug.Log (timer);
		if ((actionCounter >= actionsCap) || (timer >= timeCap)) {
			playerSwitched = true;
			CyclePlayers ();
			 
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

	public void IncrementTimer() {
		timer += Time.deltaTime;
	}

}



