// Here is a precise URL of the executable on the team website
// http://wedunnit.me/webfiles/ass3/HomicideInTheHub-Win.zip

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
	//One LevelManager per level
	//Assigns NPCs and items provided by GameMaster in its list of scene to the spawnpoints in the rooms.
	//Also sets the detective sprite to the selected detective.

	//__Variables__
	//Public to drag and drop in inspector
	private PlayerCharacter detective;
	public GameObject playerObject;
	private SpriteRenderer playerSpriteRenderer;
	public GameObject[] characterSpawnPoints;
	public GameObject[] itemSpawnPoints;
	public GameObject scoreText;

	//Used to change the scaling of characters and items per room
	public float characterScaling = 1;
	public float itemScaling = 1;

	private bool isGreen; //ADDITION BY WEDUNNIT
	private float greenTime; //ADDITION BY WEDUNNIT


	void Start() {
		//Assign correct detective
		playerSpriteRenderer = playerObject.GetComponent<SpriteRenderer> ();
		detective = GameMaster.instance.GetPlayerCharacter(); 
		playerSpriteRenderer.sprite = detective.getSprite ();

		//Get Scene in scene
		string sceneName = SceneManager.GetActiveScene().name;
		Scene scene = GameMaster.instance.GetScene(sceneName);
		AssignCharactersToSpawnPoints (scene);
		AssignItemsToSpawnPoints (scene);
	}

	void Update(){		//ADDITION BY WEDUNNIT
		scoreText.GetComponent<Text>().text = GameMaster.instance.GetScore().ToString();
		if (isGreen) {
			greenTime -= Time.deltaTime;
			if (greenTime <= 0) {
				isGreen = false;
				scoreText.GetComponent<Text> ().color = new Color (1F, 1F, 1F);
			}
		}
	}

	public void OnScoreIncrease(){	//ADDITION BY WEDUNNIT
		isGreen = true;
		greenTime = 3;
		scoreText.GetComponent<Text> ().color = new Color (0F, 1F, 0F);
	}

	//Spawns characters in character spawnpoints
	private void AssignCharactersToSpawnPoints(Scene scene){
		int spawnPointCounter = 0;
		if (scene.GetCharacters().Count > 0){ //Checks if there are characters to spawn
			foreach (NonPlayerCharacter character in scene.GetCharacters()) {
				GameObject prefab = Instantiate (character.GetPrefab (), characterSpawnPoints [spawnPointCounter].transform.position, Quaternion.identity) as GameObject; //Spawns the character prefab at the position of the given spawnpoint
				prefab.transform.localScale *= characterScaling; 			//Scales the character relative to characterScaling
				spawnPointCounter += 1;
				CharacterInteraction characterInteraction = prefab.GetComponent<CharacterInteraction> ();
				characterInteraction.SetCharacter (character);				//Tells the prefab which character it is
			}
		}

	}

	//Spawns items in item spawnpoints
	private void AssignItemsToSpawnPoints(Scene scene){
		int itemSpawnPointCounter = 0;
		if (scene.GetItems ().Count > 0) {//Checks if there are items to spawn
			foreach (Item item in scene.GetItems()) {
				if (!NotebookManager.instance.inventory.GetInventory ().Contains (item)) {
					GameObject prefab = Instantiate (item.GetPrefab (), itemSpawnPoints [itemSpawnPointCounter].transform.position, Quaternion.identity) as GameObject; //Spawns the item prefab at the position of the given spawnpoint
					prefab.transform.localScale *= itemScaling; 		//Scales the item relative to itemScaling
					itemSpawnPointCounter += 1;
					ItemScript itemScript = prefab.GetComponent<ItemScript> ();
					itemScript.SetItem (item);							//Tells the prefab which item it is
				}
			}
		}
	}
}
