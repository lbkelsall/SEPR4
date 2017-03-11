using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Riddler : MonoBehaviour {

	private string riddle = "What am I?";
	private string correctAnswer = "Silence";

	public Text questionText;
	public GameObject letterPrefab;
	private float[] spawnRegion = {-351.0f,322.0f,-295.0f,238.0f};
	public GameObject centreSpawnPoint;

	// Use this for initialization
	void Start () {
		questionText.text = riddle;
		CreateAnswerMagnets (correctAnswer);

	}


	private void CreateAnswerMagnets(string correctAnswer){
		Shuffler shuffler = new Shuffler();
		char[] answerCharArray = correctAnswer.ToCharArray ();
		shuffler.Shuffle<char>(answerCharArray);
		for (int i = 0; i < correctAnswer.Length; i++) {
			float xSpawnPoint = Random.Range (spawnRegion [0], spawnRegion [1]);
			float ySpawnPoint = Random.Range (spawnRegion [2], spawnRegion [3]);
			GameObject letter = Instantiate (letterPrefab, centreSpawnPoint.transform.position,Quaternion.identity);
			letter.transform.SetParent (centreSpawnPoint.transform);
			letter.transform.localPosition = new Vector2 (xSpawnPoint, ySpawnPoint);
			Text letterText = letter.GetComponent<Text> ();
			letterText.text = answerCharArray [i].ToString ();
			Color colour = new Color(Random.value, Random.value, Random.value, 1.0f);
			letterText.color = colour;
		}
	}

}
