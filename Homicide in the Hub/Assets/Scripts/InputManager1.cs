// Here is a precise URL of the executable on the team website
// http://wedunnit.me/webfiles/ass3/HomicideInTheHub-Win.zip

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputManager1 : MonoBehaviour {
	//Handles the input keys 'I' and 'M' pause menu and the map and notebook icons

	//States if the relative menu is open
	private bool isMapvisible = false;
	private bool isMenuvisible = false;
	private bool isNotebookvisible = false;

	public GameObject map;
	public GameObject pauseMenu; 
	public GameObject cluePanel; 	//ADDITION BY WEDUNNIT
	private GameObject notebookMenu;
	public GameObject detective; 
	private PlayerMovement playerMovement;
	private bool mapIconPressed = false;
	private bool notebookIconPressed = false;
	private bool pauseIconPressed = false; //ADDITION BY WEDUNNIT

	void Start () {
		//Ensures the player can move at the start
		playerMovement = detective.GetComponent<PlayerMovement>();
		Time.timeScale = 1; 
		playerMovement.enabled = true;
		notebookMenu = GameObject.Find("NotebookCanvas").transform.GetChild(0).gameObject;
	}

	//Every frame
	void Update () {

		//Map
		if (!isMenuvisible && !isNotebookvisible) {					//If other menus are not open
			if (Input.GetKeyDown (KeyCode.M) || mapIconPressed) { 	//If M key pressed or UI icon pressed
				isMapvisible = !isMapvisible;						//Toggle visibiltiy
				if (isMapvisible) {	
					StopGame (map);									//Pause game if map is visble
				} else {
					ResumeGame (map);								//Resume game if map is not visible
				}
				mapIconPressed = false;								//Reset icon being pressed
			}
		}

		//Pause Menu
		if (!isMapvisible && !isNotebookvisible) {					//If other menus are not open
			if (Input.GetKeyDown (KeyCode.Escape) || pauseIconPressed) {	// EDITED BY WEDUNNIT
				isMenuvisible = true;								//Toggle visibiltiy
				StopGame (pauseMenu);								//Pause game if is visble
				pauseIconPressed = false;							//ADDITION BY WEDUNNIT
			}

		}

		//Notebook
		if (!isMapvisible && !isMenuvisible) {								//If other menus are not open
			if (Input.GetKeyDown (KeyCode.I) || notebookIconPressed) {		//If ESC key pressed
				isNotebookvisible = !isNotebookvisible;						//Toggle visibiltiy
				if (isNotebookvisible) {
					NotebookManager.instance.UpdateNotebook ();				//Update notebook
					StopGame (notebookMenu);								//Pause game if is visble
				} else {
					ResumeGame (notebookMenu);								//Resume game if map is not visible
				}
				notebookIconPressed = false;
			}

		}

	}

	/// <summary>
	/// Sets CluePanel active and fills with clue information.
	/// </summary>
	/// <param name="item">Item.</param>
	public void ShowCluePanel(Item item){ //ADDITION BY WEDUNNIT
		cluePanel.SetActive(true);
		cluePanel.transform.GetChild(0).transform.GetChild(0).GetComponent<Text> ().text = item.getID (); //gets Title parent object, then its child (Text) 
		cluePanel.transform.GetChild(1).transform.GetChild(0).GetComponent<Text> ().text = item.getDescription ();
		cluePanel.transform.GetChild(2).GetComponent<Image> ().sprite = item.GetSprite ();

	}

	void StopGame(GameObject menu){
		//Stops ingame time and playermovement
		Time.timeScale = 0; 
		playerMovement.enabled = false;
		menu.SetActive (true);
	}
		
	public void ResumeGame(GameObject menu){
		//Resumes ingame time and playermovement
		Time.timeScale = 1; 
		pauseIconPressed = false;		//ADDITION BY WEDUNNIT
		isMenuvisible = false;			//ADDITION BY WEDUNNIT
		playerMovement.enabled = true;
		menu.SetActive (false);
	}

	public void PauseIconPressed(){			//ADDITION BY WEDUNNIT
		//Called when pause icon is pressed //ADDITION BY WEDUNNIT
		pauseIconPressed = true;			//ADDITION BY WEDUNNIT
	}										//ADDITION BY WEDUNNIT


	public void MapIconPressed(){
		//Called when map icon is pressed
		mapIconPressed = true;
	}

	public void NotebookIconPressed(){
		//Called when Notepad icon is pressed
		notebookIconPressed = true;
	}


}
