// Here is a precise URL of the executable on the team website
// http://wedunnit.me/webfiles/ass3/HomicideInTheHub-Win.zip

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class QuestioningScript : MonoBehaviour {
	//Used when the player selects the questioning option but also to set the correct sprites in the scene when entering it
	//Handles the GUI portion of this part of interrogation.
	//We have chosen to use 'interogation' when refering to questioning or accusing a character


	//__Variables__
	//public to allow drag and drop in inspector
	private PlayerCharacter detective; 
	private NonPlayerCharacter character;
	public GameObject detectiveGameObject;
	public GameObject characterGameObject;
	public Text characterName; //ADDITION BY WEDUNNIT

	public Text[] detectiveStylesText = new Text[3]; //Where Left-most button is 1 and rightmost is 3
	public Text clueSpeech;			//Where the clue text is written to

	void Start () {
		//Get detective and character from singletons
		detective = GameMaster.instance.GetPlayerCharacter(); 
		character = InterrogationScript.instance.GetInterrogationCharacter ();

		//Change sprites to correct characters
		SpriteRenderer detectiveSR = detectiveGameObject.GetComponent<SpriteRenderer> ();
		detectiveSR.sprite = detective.getSprite ();

		SpriteRenderer characterSR = characterGameObject.GetComponent<SpriteRenderer> ();
		characterSR.sprite = character.getSprite ();

		characterName.text = character.getCharacterID (); //ADDITION BY WEDUNNIT

		for (int i = 0; i < 3; i++) {				//Set Text in Style buttons to Styles of chosen detective
			detectiveStylesText [i].text = detective.GetQuestioningStyles () [i];
		}
	}

	public void QuestionCharacter(int reference){
		//reference passes the question style reference i.e leftmost button=0, middle=1, rightmost=2
		string response;
		string choice; 
		choice = GetQuestioningChoice (reference);
		if (character.CanBeQuestionned ()) {	//ADDITION BY WEDUNNIT
			List<string> weaknesses;
			weaknesses = character.GetWeaknesses ();

			if ((weaknesses.Contains (choice)) && (character.getVerbalClue () != null)) { //If chosen style is a weakness to the character
				VerbalClue clue = character.getVerbalClue ();
				if (!NotebookManager.instance.logbook.GetLogbook ().Contains (character.getVerbalClue ())) { //If the logbook doesnt contain the clue already
					response = "Clue Added: " + clue.getDescription (); 					//Change the responce and add to the logbook
					NotebookManager.instance.logbook.AddVerbalClueToLogbook (clue);
				    GameMaster.instance.UnblockAllCharacters ();	//ADDITION BY WEDUNNIT
				    NotebookManager.instance.UpdateNotebook ();
				} else {
					response = "Clue Already Obtained";			//Otherwise state the clue is already in the logbook
				}
			} else {	
				response = character.GetResponse (choice);		//Otherwise just give the responce
			}
		}else{													//If the character blocks questioning //ADDITION BY WEDUNNIT
			response = character.GetResponse (choice + "ButBlocked");	//Gets relevent reply //ADDITION BY WEDUNNIT
		}

		clueSpeech.text = response; 	//Update the UI Text with the appropriate repsonce
	}




	private string GetQuestioningChoice(int reference){
		string choice = detective.GetQuestioningStyles () [reference];
		return choice;
	}

}
