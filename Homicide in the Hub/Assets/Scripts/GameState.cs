using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState {

	private PlayerCharacter detective;
	private Scene currentScene;
	private List<Item> items;
	private List<VerbalClue> verbalClues; 
	private Dictionary<NonPlayerCharacter, string> NPCs = new Dictionary<NonPlayerCharacter, string> ();
	private int failedAccusations;
	private int score;
	private float time;

	public GameState (PlayerCharacter detective) {
		this.detective = detective; 
	}

	public void Save() {
		items = NotebookManager.instance.inventory.GetInventory ();
		verbalClues = NotebookManager.instance.logbook.GetLogbook (); 
	}

	public void Load() {
		NotebookManager.instance.inventory.setInventory (items);
		NotebookManager.instance.logbook.SetLogbook (verbalClues);  
	}

}