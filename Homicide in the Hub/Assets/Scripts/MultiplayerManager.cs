// Takes parameters specified by the player, and uses them to create all the instances of other classes needed for multiplayer.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MultiplayerManager : MonoBehaviour {

	public Dropdown numOfPlayersDropdown;
	private TurnManager turnManager;
	private bool gameStarted = false;

	private int numOfPlayers;
	List<PlayerCharacter> detectives = new List<PlayerCharacter>();

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

	//Called when the player submits the number of players.
	//Creates a new instance of TurnManager, using some parameters given by the player.
	public void Setup() {
		numOfPlayers = numOfPlayersDropdown.value + 2;
		turnManager = new TurnManager (3, 21.0f,numOfPlayers);

		SceneManager.LoadScene("Character Selection");

	}

	// Updates the multiplayer turn timer, and then checks if the current player's turn should end.
	public void Update() {
		if (turnManager != null) {
			if (gameStarted) {
				turnManager.DecrementTimer ();
				turnManager.EndTurnCheck ();
			}
		}
	}

	//Accessors
	public TurnManager GetTurnManager(){
		return turnManager;
	}

	public int GetNumOfPlayers(){
		return numOfPlayers;
	}
		
	public List<PlayerCharacter> GetDetectives(){
		return detectives;
	}

	//Setters
	public void SetStates(){
		GameState[] states = new GameState[numOfPlayers];
		for (int i = 0; i < states.Length; i++) {
			states [i] = new GameState (detectives[i]);
		}

		turnManager.SetStates (states, numOfPlayers);
	}

	public void GameActive(bool active){
		gameStarted = active;
	}

	public void AddDetective(PlayerCharacter detective){
		detectives.Add (detective);
	}
}
