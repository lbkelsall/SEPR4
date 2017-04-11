// Here is a precise URL of the executable on the team website
// http://wedunnit.me/webfiles/ass3/HomicideInTheHub-Win.zip

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {
	//Functions used are called on button presses. Functions assigned on a button are assigned in the inspector


	public void LoadScene(string scene){
		//Loads the given scene, and removes scenario objects if the game is quit.
		if(scene == "Main Menu" && (GameObject.Find("GlobalScripts") != null) && (GameObject.Find("NotebookCanvas") != null)){	//ADDITION BY WEDUNNIT
			Destroy(GameObject.Find("GlobalScripts")); 	//ADDITION BY WEDUNNIT
			Destroy(GameObject.Find("NotebookCanvas")); //ADDITION BY WEDUNNIT
		}												//ADDITION BY WEDUNNIT

		if (scene == "Main Menu" && (GameObject.Find ("Multiplayer Manager Object") != null)) {
			Destroy(GameObject.Find("Multiplayer Manager Object")); 
			Destroy(GameObject.Find("Turn Testing"));
		}

		SceneManager.LoadScene(scene);
	}

	public void QuitGame(){
		//Closes the game
		Application.Quit ();
	}

	public void LoadPreviousScene(){
		//Loads the previously stored scene in InterrogationScript.
		//Only used in the interogation room.
		string previousScene = InterrogationScript.instance.GetReturnScene ();
		SceneManager.LoadScene (previousScene);
	}
		
	public void ClickedOnFridge(){
		if (GameMaster.instance.GetRiddleStatus () == true) {
			print ("Load factory, currently not set");
			SceneManager.LoadScene ("Factory");
		} else {
			SceneManager.LoadScene ("Fridge");
		}
	}

	public void CharacterSelectionBack(){
		if (GameObject.Find ("Multiplayer Manager Object") != null) {
			Destroy (GameObject.Find ("Multiplayer Manager Object"));
			SceneManager.LoadScene ("Multiplayer Setup");
		} else {
			LoadScene ("Main Menu");
		}
	}
}
