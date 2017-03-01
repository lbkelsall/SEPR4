using UnityEngine;
using System.Collections.Generic;

public class NonPlayerCharacter : Character {
	//Subclass to Character and defines the unique variables and methods that apply to Non-playable characters
	//These included but are not limited to Captain Bluebottle, Jesse Ranger etc

	//__Variables__
	private bool isMurderer = false;
	private VerbalClue verbalClue = null;
	private GameObject prefab;
	private List<string> weaknesses;
	private List<string> questioningResponses;
	// Use this for initialization

	//__Constructor__
	//Inherits characterID, sprite and nickname from the Character class. All passed variablkes are stored in this instance
	public NonPlayerCharacter (string characterID, Sprite sprite, string nickname, GameObject prefab, List<string> weaknesses, List<string> questioningResponces1) :  base(characterID, sprite, nickname) {
		this.prefab = prefab;
		this.weaknesses = weaknesses;
		questioningResponses = questioningResponces1;
	}

	//__Methods__
	public bool IsMurderer(){
		return isMurderer;
	}

	//Setters
	public void SetAsMurderer(){
		isMurderer = true;
	}

	public void setVerbalClue (VerbalClue clue) {
		verbalClue = clue;
	}

	//Accessors
	public VerbalClue getVerbalClue () {
		return verbalClue;
	}

	public GameObject GetPrefab(){
		return this.prefab;
	}
		
	public List<string> GetWeaknesses(){
		return this.weaknesses;
	}

	public string GetResponse(string questioningStyle){
		//Get the responce relevant to the selected questioning style
		switch(questioningStyle){
		//Chase Hunter Questioning Styles
		case ("Forceful"):
			return questioningResponses [0];
		case ("Condescending"):
			return questioningResponses [1];
		case ("Intimidating"):
			return questioningResponses [2];
		case ("ForcefulButBlocked"):
			return questioningResponses [9];
		case ("CondescendingButBlocked"):
			return questioningResponses [9];
		case ("IntimidatingButBlocked"):
			return questioningResponses [9];
		//TODO: ADD MORE RESPONSES FOR EACH QUESTION TYPE


		//Johnny Chase Questioning Styles
		case ("Coaxing"):
			return questioningResponses [3];
		case ("Wisecracking"):
			return questioningResponses [4];
		case ("Rushed"):
			return questioningResponses [5];
		case ("CoaxingButBlocked"):
			return questioningResponses [9];
		case ("WisecrackingButBlocked"):
			return questioningResponses [9];
		case ("RushedButBlocked"):
			return questioningResponses [9];
		//TODO: ADD MORE RESPONSES FOR EACH QUESTION TYPE

		//Adam Founder Questioning Styles
		case ("Inquisitive"):
			return questioningResponses [6];
		case ("Kind"):
			return questioningResponses [7];
		case ("Inspiring"):
			return questioningResponses [8];
		case ("InquisitiveButBlocked"):
			return questioningResponses [9];
		case ("KindButBlocked"):
			return questioningResponses [9];
		case ("InspiringButBlocked"):
			return questioningResponses [9];
		//TODO: ADD MORE RESPONSES FOR EACH QUESTION TYPE

		default: 
			return "..."; //Used if null

		}
	}
}