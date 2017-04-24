using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class Riddler : MonoBehaviour {

	private string riddle = "Speak my name and I disappear, what am I?";
	private string correctAnswer;

	public Text questionText;
	public GameObject letterPrefab;
	public GameObject centreSpawnPoint;
	public float spawnPointPadding;
	public GameObject dropArea;

	public GameObject secretEntrance; 
	public GameObject emptyFridge;
	public GameObject riddleGUI;
	public GameObject backButton;

	public GameObject playerTurnPanel;
	public Text multiplayerTimerText;
	public GameObject multiplayerPanel;
	private bool multiplayerGame = false;

	void Start () {
		riddle = GameMaster.instance.GetRiddle () [0];
		correctAnswer = GameMaster.instance.GetRiddle () [1];
		questionText.text = riddle;
		CreateAnswerMagnets (correctAnswer);


		//If multiplayer
		if (GameObject.Find ("Multiplayer Manager Object") != null) {
			if (MultiplayerManager.instance.GetTurnManager ().HasPlayerSwitched ()) {
				StartCoroutine ("ShowPlayerTurn");
			}
			multiplayerPanel.SetActive (true);
			multiplayerGame = true;
			MultiplayerManager.instance.GameHasStarted ();

		} else {
			multiplayerPanel.SetActive (false);
			multiplayerGame = false;
		}
	}

	//Instantiates the 'magnets', 
	private void CreateAnswerMagnets(string correctAnswer){
		Shuffler shuffler = new Shuffler();
		char[] answerCharArray = correctAnswer.ToCharArray ();
		shuffler.Shuffle<char>(answerCharArray);				//Generates a random order for the characters
		for (int i = 0; i < correctAnswer.Length; i++) {

			//Instantiate and reposition
			GameObject letter = Instantiate (letterPrefab, centreSpawnPoint.transform.position,Quaternion.identity);
			letter.transform.SetParent (centreSpawnPoint.transform);
			letter.transform.localPosition = GenerateSpawnPoint (centreSpawnPoint);

			//Assign a letter
			Text letterText = letter.GetComponent<Text> ();
			letterText.text = answerCharArray [i].ToString ();

			//Assign Random Colour
			Color colour = new Color(Random.value, Random.value, Random.value, 1.0f);
			letterText.color = colour;
		}
	}

	//Generates a random spawn point bounded by the dimensions of the given region
	private Vector2 GenerateSpawnPoint(GameObject spawnCentre){
		RectTransform rt = spawnCentre.GetComponent<RectTransform>();
		float width = rt.rect.width;
		float height = rt.rect.height;

		float xSpawnPoint = Random.Range (-(width /2) + spawnPointPadding, (width /2) - spawnPointPadding);
		float ySpawnPoint = Random.Range (-(height/2) + spawnPointPadding, (height/2) - spawnPointPadding);
		Vector2 spawnPoint = new Vector2(xSpawnPoint, ySpawnPoint);
		return spawnPoint;
	}

	//Executed when the submit button is pressed
	public void CheckIfCorrect(){
		if (dropArea.transform.childCount > 0) {
			Text[] letters = dropArea.GetComponentsInChildren<Text> (true);
			List<string> answerEnteredList = new List<string>();
			foreach (Text letter in letters) {
				answerEnteredList.Add (letter.text);
			}
			string strAnswerEntered = string.Join ("",answerEnteredList.ToArray ());

			//Correct Answer
			if (strAnswerEntered == correctAnswer) {
				GameMaster.instance.SetRiddleStatus (true);
				ShowHiddenEntrance ();

				if (GameObject.Find ("Multiplayer Manager Object") != null)  {
					MultiplayerManager.instance.GetTurnManager().IncrementActionCounter(); // For turn switching in multiplayer.
				}
			
			//Incorrect Answer
			} else {
				GameMaster.instance.SetRiddleStatus (false);
				ShowEmptyFridge ();
			}

		//Submit button pressed with no letters added to the drop area
		} else {
			GameMaster.instance.SetRiddleStatus (false);
			ShowEmptyFridge ();
		}
	}

	private void ShowHiddenEntrance(){
		secretEntrance.SetActive (true);
		riddleGUI.SetActive (false);
		backButton.SetActive (true);
	}

	private void ShowEmptyFridge(){
		emptyFridge.SetActive (true);
		riddleGUI.SetActive (false);
		backButton.SetActive (true);
	}

	public void Update(){
		//Update multiplayer timer
		if (multiplayerGame){
			multiplayerTimerText.text = MultiplayerManager.instance.GetTurnManager ().GetTimer ().ToString();
		}
	}

	private IEnumerator ShowPlayerTurn(){
		Time.timeScale = 0;
		playerTurnPanel.SetActive (true);
		Text playerTurnText = playerTurnPanel.transform.GetChild (1).GetComponent<Text> ();
		playerTurnText.text = "Player " + MultiplayerManager.instance.GetTurnManager ().GetPlayerTurn ();
		Camera.main.GetComponent<Blur> ().enabled = true;
		yield return new WaitForSecondsRealtime (3);
		playerTurnPanel.SetActive (false);
		Camera.main.GetComponent<Blur> ().enabled = false;
		Time.timeScale = 1;
	}
}
