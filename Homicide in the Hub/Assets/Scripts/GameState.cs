using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState {

	private PlayerCharacter detective;
	private string currentScene = "Atrium";
	private List<Item> items = new List<Item>();
	private List<VerbalClue> verbalClues = new List<VerbalClue>(); 
	private Dictionary<NonPlayerCharacter, string> NPCs = new Dictionary<NonPlayerCharacter, string> ();
	private int failedAccusations = 0;
	private float score = 1000;
	private float time;

	public GameState (PlayerCharacter detective) {
		this.detective = detective; 
		score = GameMaster.instance.GetScore ();
	}

	public void Save() {
		items = NotebookManager.instance.inventory.GetInventory ();
		verbalClues = NotebookManager.instance.logbook.GetLogbook ();
		currentScene = SceneManager.GetActiveScene ().name;
		score = (float)GameMaster.instance.GetScore ();
		//Need to add failed accusations
	}

	public void Load() {
		NotebookManager.instance.inventory.SetInventory (items);
		NotebookManager.instance.logbook.SetLogbook (verbalClues);
		GameMaster.instance.SetPlayerCharacter (detective);
		GameMaster.instance.SetScore (score);
		SceneManager.LoadScene (currentScene);
	}

}