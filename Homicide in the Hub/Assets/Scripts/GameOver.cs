
// Here is a precise URL of the executable on the team website
// http://wedunnit.me/webfiles/ass3/HomicideInTheHub-Win.zip

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameOver : MonoBehaviour {
	// CLASS ADDITION BY WEDUNNIT

	/// <summary>
	/// The text object that contains the player's score.
	/// </summary>
	public Text scoreText;
	/// <summary>
	/// The text object that the player writes their name to.
	/// </summary>
	public Text nameField;
	/// <summary>
	/// The final score of the player.
	/// </summary>
	private int endScore;

	/// <summary>
	/// Initialise this instance.
	/// </summary>
	void Start () {
		GameMaster gMaster = FindObjectOfType<GameMaster> ();	// Find the current Game Master object
		endScore = gMaster.GetScore ();							// Get the player's score
		Text actualText = scoreText.GetComponent<Text> ();		// Get the text component of the text box...
		actualText.text = "Your score: " + endScore;
		Destroy(GameObject.Find("GlobalScripts")); // As we no longer need the GlobalScripts and NotebookCanvas objects...
		Destroy(GameObject.Find("NotebookCanvas")); // We can now get rid of them.
	}

	/// <summary>
	/// Closes the screen and returns to the main menu.
	/// </summary>
	public void CloseScreen(){
		string UserInput = nameField.text;			// Fetch the user's name from the field.
		if (UserInput == "") {						// If it's blank, assign it a dummy value.
			UserInput = "Some Unnamed Detective";
		}
		using (StreamWriter sw = new StreamWriter ("leaderboard.txt", true)) {
			sw.WriteLine (UserInput);				// Write the name and score to leaderboard.txt.
			sw.WriteLine (endScore.ToString ());
		}
		SceneManager.LoadScene ("Main Menu");		// Then return to main menu.
	}
}
