using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Riddler : MonoBehaviour {

	private string riddle = "Say my name and I disapeear. What am I?";
	private string correctAnswer = "Silence";

	public Text questionText;
	public GameObject letterPrefab;
	public GameObject centreSpawnPoint;
	public float spawnPointPadding;
	public GameObject dropArea;

	void Start () {
		questionText.text = riddle;
		CreateAnswerMagnets (correctAnswer);
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
				Debug.Log ("Correct Answer!");
			
			//Incorrect Answer
			} else {
				Debug.Log ("InCorrect Answer!");
			}

		//Submit button pressed with no letters added to the drop area
		} else {
			Debug.Log ("No letters added!");
		}
	}
}
