using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState {

	private PlayerCharacter detective;
	private string currentScene = "Atrium";
	private List<Item> items = new List<Item>();
	private List<VerbalClue> verbalClues = new List<VerbalClue>(); 
	private Dictionary<NonPlayerCharacter, bool> NPCLockStatus = new Dictionary<NonPlayerCharacter, bool> ();
	private int failedAccusations = 0;
	private float score = 1000f;
	private float time;
	private string sceneToReturnTo;
	private NonPlayerCharacter interrogationCharacter;
	private bool riddleStatus;

	public GameState (PlayerCharacter detective) {
		this.detective = detective;
	}

	public void Save() {

		//character locked states
		foreach (NonPlayerCharacter character in GameMaster.instance.GetCharacters() ) {
			if (NPCLockStatus.ContainsKey (character) == false) {
				NPCLockStatus.Add (character, character.CanBeQuestionned ());
			} else {
				NPCLockStatus [character] = character.CanBeQuestionned ();
			}
		}

		items = NotebookManager.instance.inventory.GetInventory ();
		verbalClues = NotebookManager.instance.logbook.GetLogbook ();
		currentScene = SceneManager.GetActiveScene ().name;
		score = (float)GameMaster.instance.GetScore ();
		sceneToReturnTo = InterrogationScript.instance.GetReturnScene ();
		interrogationCharacter = InterrogationScript.instance.GetInterrogationCharacter ();
		riddleStatus = GameMaster.instance.GetRiddleStatus ();
		//Need to add failed accusations and if riddle solved
	}

	public void Load() {

		foreach (KeyValuePair <NonPlayerCharacter, bool> status in NPCLockStatus ) {
			if (status.Value == true) {
				status.Key.AllowCharacterQuestioning ();
			} else {
				status.Key.BlockCharacterQuestioning ();
			}
		}
		
		NotebookManager.instance.inventory.SetInventory (items);
		NotebookManager.instance.logbook.SetLogbook (verbalClues);
		GameMaster.instance.SetPlayerCharacter (detective);
		GameMaster.instance.SetScore (score);
		SceneManager.LoadScene (currentScene);
		InterrogationScript.instance.SetReturnScene (sceneToReturnTo);
		InterrogationScript.instance.SetInterrogationCharacter (interrogationCharacter);
		GameMaster.instance.SetRiddleStatus (riddleStatus);
	}

}