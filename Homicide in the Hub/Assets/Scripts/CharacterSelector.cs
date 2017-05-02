﻿// Here is a precise URL of the executable on the team website
// http://www-users.york.ac.uk/~phj501/Executable_4.zip

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour {
	/*Handles the character selection screen
	 * -Instantiates and defines the detectives
	 * -Passes the chosen detective to the GameMaster
	 * -Handles GUI of the character selector screen
	 */


	//Detecive variable declaration
	//Public to allow for drag and drop in inspector
	public Sprite chaseHunterSprite;
	public Sprite johnnySlickSprite;
	public Sprite adamFounderSprite;
	private PlayerCharacter chaseHunter;
	private PlayerCharacter johnnySlick;
	private PlayerCharacter adamFounder;

	//Querstioning Styles for detectives
	private string[] chaseHunterQuestioningStyles = new string[3] {"Forceful","Intimidating","Condescending"};
	private string[] johnnySlickQuestioningStyles = new string[3] {"Wisecracking","Rushed","Coaxing"};
	private string[] adamFounderQuestioningStyles = new string[3] {"Inquisitive","Kind","Inspiring"};

	PlayerCharacter[] detectives;

	//GUI References
	//Public to allow for drag and drop in inspector
	public Text GUIName;
	public Image GUIImage;
	public Text GUIQuestioningStyle;
	public Text GUIDescription;
	private int detectiveCounter = 0;
	public Text playerNumberReference;


	// Use this for initialization
	void Start () {
		//__NEW_FOR_ASSESSMENT_4__(START)
		if (GameObject.Find ("Multiplayer Manager Object") != null) {
			MultiplayerManager multiplayerManager = GameObject.Find ("Multiplayer Manager Object").GetComponent<MultiplayerManager> ();
			TurnManager turnManager = multiplayerManager.GetTurnManager ();
			playerNumberReference.text = "Player " + turnManager.GetPlayerTurn () + " Selection"; 
		} else {
			playerNumberReference.text = "Select your Detective";
		}
		//__NEW_FOR_ASSESSMENT_4__(END)


		//Initalise detectives
		GameObject GlobalScripts = (GameObject)Instantiate(Resources.Load("GlobalScripts")); //ADDITION BY WEDUNNIT - - new scenario
		GlobalScripts.name = "GlobalScripts"; //ADDITION BY WEDUNNIT - - removes (clone) from name
		GameObject NoteBookCanvas = (GameObject) Instantiate(Resources.Load("NotebookCanvas")); // ADDITON BY WEDUNNIT
		NoteBookCanvas.name = "NotebookCanvas"; //ADDITION BY WEDUNNIT - - removes (clone) from name

		chaseHunter = new PlayerCharacter ("Chase Hunter", chaseHunterSprite, "The Loose Cannon", "Aggressive", chaseHunterQuestioningStyles, "An ill tempered detective who will do whatever it takes to get to the bottom of a crime." );
		johnnySlick = new PlayerCharacter ("Johnny Slick", johnnySlickSprite, "The Greaseball", "Wisecracking",johnnySlickQuestioningStyles, "A witty detective who finds the comedic value in everything... even death apparently." );
		adamFounder = new PlayerCharacter ("Adam Founder", adamFounderSprite, "Good Cop", "By the Book", adamFounderQuestioningStyles,"A by the book cop who uses proper detective techniques to solve mysteries" );
		detectives =  new PlayerCharacter[3] {chaseHunter, johnnySlick, adamFounder};
		ChangeDetective(); //Sets first detective to be viewed in GUI
	}

	//Called when right button is pressed
	public void CycleUpDetectives(){
		detectiveCounter += 1;
		if (detectiveCounter >= 3) {
			detectiveCounter = 0;
		}
		ChangeDetective();
	}

	//Called when left button is pressed
	public void CycleDownDetectives(){
		detectiveCounter -= 1;
		if (detectiveCounter <= -1) {
			detectiveCounter = 2;
		}
		ChangeDetective();
	}

	//Updates the GUI with the selected detective
	private void ChangeDetective(){
		GUIName.text = detectives[detectiveCounter].getCharacterID ();
		GUIImage.sprite =  detectives[detectiveCounter].getSprite ();
		GUIQuestioningStyle.text =  "Questioning Style: "+ detectives[detectiveCounter].GetOverallQuestioningStyle();
		GUIDescription.text = detectives [detectiveCounter].GetDescription ();
	}

	//Called when the play button is pressed
	public void SelectDetective(){

		//__NEW_FOR_ASSESSMENT_4__(START)
		//Singleplayer
		if (GameObject.Find ("Multiplayer Manager Object") == null) {
			GameMaster.instance.CreateNewGame (detectives [detectiveCounter]);
			SceneManager.LoadScene ("Atrium");
		
		
		//Multiplayer
		} else {
			MultiplayerManager multiplayerManager = GameObject.Find ("Multiplayer Manager Object").GetComponent<MultiplayerManager> ();
			TurnManager turnManager = multiplayerManager.GetTurnManager ();

			multiplayerManager.AddDetective (detectives [detectiveCounter]);
			//If all detectives selected.
			if (turnManager.GetPlayerTurn () == multiplayerManager.GetNumOfPlayers ()) {
				multiplayerManager.SetStates ();
				turnManager.SetPlayerTurn (1);
				GameMaster.instance.CreateNewGame (multiplayerManager.GetDetectives ()[0]);
				SceneManager.LoadScene ("Atrium");
			
			//If not all detectives entered
			} else {
				turnManager.SetPlayerTurn (turnManager.GetPlayerTurn () + 1);
				SceneManager.LoadScene ("Character Selection");
			}
		}
		//__NEW_FOR_ASSESSMENT_4__(END)
	}

	public PlayerCharacter GetDetective() {
		return detectives [detectiveCounter];
	}



}