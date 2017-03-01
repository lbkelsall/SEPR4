// Here is a precise URL of the executable on the team website
// http://wedunnit.me/webfiles/ass3/HomicideInTheHub-Win.zip

using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Classes;
using Debug = UnityEngine.Debug;

public class GameMaster : MonoBehaviour {
	/* Initialises all of the objects required generate the mystery and the game world except the detectives and verbal clues. 
	 * Distibutes the clues and characters throughout the scenes.
	*/
	public Scenario scenario;

	//Arrays
	public static GameMaster instance = null;
	static public Scene[] scenes;
	public Item[] itemClues;
	public VerbalClue[] verbalClues;
	public NonPlayerCharacter[] characters;
	public List<Clue> relevantClues;
	private MurderWeapon[] murderWeapons;
	private PlayerCharacter playerCharacter;

	//NPC Sprites
	//Made public to allow for dragging and dropping of Sprites
	public Sprite pirateSprite;
	public Sprite mimesSprite;
	public Sprite millionaireSprite;
	public Sprite cowgirlSprite;
	public Sprite romanSprite;
	public Sprite wizardSprite;
	public Sprite chubbieSprite;
	//ADDITION BY WEDUNNIT
	public Sprite reginaldSprite;
	//ADDITION BY WEDUNNIT
	public Sprite hemanSprite;
	//ADDITION BY WEDUNNIT
	public Sprite scientistSprite;
	//ADDITION BY WEDUNNIT

	//NPC Prefabs
	//Made public to allow for dragging and dropping of prefabs
	public GameObject piratePref;
	public GameObject mimesPref;
	public GameObject millionarePref;
	public GameObject cowgirlPref;
	public GameObject romanPref;
	public GameObject wizardPref;
	public GameObject chubbiePref;
	//ADDITION BY WEDUNNIT
	public GameObject hemanPref;
	//ADDITION BY WEDUNNIT
	public GameObject scientistPref;
//ADDITION BY WEDUNNIT
	public GameObject reginaldPref;
	//ADDITION BY WEDUNNIT


	//Item Sprite decleratation
	//Made public to allow for dragging and dropping of sprites
	public Sprite cutlassSprite;
	public Sprite poisonSprite;
	public Sprite garroteSprite;
	public Sprite knifeSprite;
	public Sprite laserGunSprite;
	public Sprite leadPipeSprite;
	public Sprite westernPistolSprite;
	public Sprite wizardStaffSprite;
	public Sprite beretSprite;
	public Sprite footprintsSprite;
	public Sprite glovesSprite;
	public Sprite wineSprite;
	public Sprite shatteredGlassSprite;
	public Sprite shrapnelSprite;
	public Sprite smellyDeathSprite;
	public Sprite spellbookSprite;
	public Sprite tripwireSprite;
	public Sprite whistleSprite;
	//ADDITION BY WEDUNNIT
	public Sprite toastSprite;
	//ADDITION BY WEDUNNIT
	public Sprite staplerSprite;
	//ADDITION BY WEDUNNIT
	public Sprite seaweedSprite;
	//ADDITION BY WEDUNNIT
	public Sprite sandwitchSprite;
	//ADDITION BY WEDUNNIT
	public Sprite purseSprite;
	//ADDITION BY WEDUNNIT
	public Sprite plungerSprite;
	//ADDITION BY WEDUNNIT
	public Sprite monocleSprite;
	//ADDITION BY WEDUNNIT
	public Sprite featherSprite;
	//ADDITION BY WEDUNNIT
	public Sprite chefHatSprite;
	//ADDITION BY WEDUNNIT
	public Sprite glassesSprite;
	//ADDITION BY WEDUNNIT
	public Sprite dumbbellSprite;
	//ADDITION BY WEDUNNIT


	//Item Prefabs
	//Made public to allow for dragging and dropping of prefabs
	public GameObject cutlassPrefab;
	public GameObject poisonPrefab;
	public GameObject garrotePrefab;
	public GameObject knifePrefab;
	public GameObject laserGunPrefab;
	public GameObject leadPipePrefab;
	public GameObject westernPistolPrefab;
	public GameObject wizardStaffPrefab;
	public GameObject beretPrefab;
	public GameObject footprintsPrefab;
	public GameObject glovesPrefab;
	public GameObject winePrefab;
	public GameObject shatteredGlassPrefab;
	public GameObject shrapnelPrefab;
	public GameObject smellyDeathPrefab;
	public GameObject spellbookPrefab;
	public GameObject tripwirePrefab;
	public GameObject whistlePrefab;
	//ADDITION BY WEDUNNIT
	public GameObject toastPrefab;
	//ADDITION BY WEDUNNIT
	public GameObject staplerPrefab;
	//ADDITION BY WEDUNNIT
	public GameObject seaweedPrefab;
	//ADDITION BY WEDUNNIT
	public GameObject sandwichPrefab;
	//ADDITION BY WEDUNNIT
	public GameObject pursePrefab;
	//ADDITION BY WEDUNNIT
	public GameObject plungerPrefab;
	//ADDITION BY WEDUNNIT
	public GameObject monoclePrefab;
	//ADDITION BY WEDUNNIT
	public GameObject featherPrefab;
	//ADDITION BY WEDUNNIT
	public GameObject chefHatPrefab;
	//ADDITION BY WEDUNNIT
	public GameObject glassesPrefab;
	//ADDITION BY WEDUNNIT
	public GameObject dumbbellPrefab;
	//ADDITION BY WEDUNNIT

	//initialise all the speech lists    //ADDITION BY WEDUNNIT
	private List<string> hemanResponses;
	private List<string> pirateResponses = new List<string>();
	private List<string> mimeResponses; 
	private List<string> millionaireResponses;
	private List<string> cowgirlResponses;
	private List<string> romanResponses;
	private List<string> wizardResponses;
	private List<string> chubbieResponses;
	private List<string> reginaldResponses;
	private List<string> scientistResponses;


	private NonPlayerCharacter murderer;

	//Relevant Clues
	private List<Item> relevant_items;
	private List<VerbalClue> relevant_verbal_clues;

	public TextAsset speechFile;
	private JSONObject speechData;

	/// <summary>
	/// The score of the player in the current game.
	/// </summary>
	private float gameScore;	//ADDITION BY WEDUNNIT

	//Sets as a Singleton
	void Awake() {  //Makes this a singleton class on awake
		if(instance == null) { //Does an instance already exist?
			instance = this;	//If not set instance to this
		} else if(instance != this) { //If it already exists and is not this then destroy this
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject); //Set this to not be destroyed when reloading scene
	}
		
	/// <summary>
	/// Update is a Unity function that runs every frame; this uses it to decrease score by the time between frames.
	/// </summary>
	void Update(){		//ADDITION BY WEDUNNIT
		if (gameScore > 0) {
			Penalise (Time.deltaTime);
		} else {
			gameScore = 0;
		}
	}

	void Start() {
		//Initialises Variables
		//Response Arrays
		speechData = new JSONObject(speechFile.text);
		Debug.Log("Accessing JSON: ");
		foreach(var character in speechData.keys)
		{

		    if (character == "Pirate")
		    {
		        pirateResponses = SpeechHandler.AccessData(speechData, character).ToList().ToList();
		        Debug.Log(pirateResponses[0]);
		    }
		    else if (character == "Mime")
		    {
		        mimeResponses = SpeechHandler.AccessData(speechData, character).ToList().ToList();
				Debug.Log("HERE!!!!!!!!!!!!!!!" + mimeResponses[0]);
		    }
		    else if (character == "Millionaire")
		    {
		        millionaireResponses = SpeechHandler.AccessData(speechData, character).ToList();
		    }
		    else if (character == "Cowgirl")
		    {
		        cowgirlResponses = SpeechHandler.AccessData(speechData, character).ToList();
		    }
		    else if (character == "Roman")
		    {
		        romanResponses = SpeechHandler.AccessData(speechData, character).ToList();
		    }
		    else if (character == "Wizard")
		    {
		        wizardResponses = SpeechHandler.AccessData(speechData, character).ToList();
		    }
		    else if (character == "Chubbie")
		    {
		        chubbieResponses = SpeechHandler.AccessData(speechData, character).ToList();
		    }
		    else if (character == "HeMan")
		    {
		        hemanResponses = SpeechHandler.AccessData(speechData, character).ToList();
		    }
		    else if (character == "Reginald")
		    {
		        reginaldResponses = SpeechHandler.AccessData(speechData, character).ToList();
		    }
		    else if (character == "Scientist")
		    {
		        scientistResponses = SpeechHandler.AccessData(speechData, character).ToList();
		    }
		}

		//Weaknesses
		List<string> pirateWeaknesses = new List<string> { "Forceful", "Wisecracking", "Kind" };
		List<string> mimeWeaknesses = new List<string> { "Intimidating", "Coaxing", "Inspiring" };
		List<string> millionaireWeaknesses = new List<string> { "Forceful", "Rushed", "Kind" };
		List<string> cowgirlWeaknesses = new List<string> { "Condescending", "Wisecracking", "Inspiring" };
		List<string> romanWeaknesses = new List<string> { "Condescending", "Coaxing", "Inquisitive" };
		List<string> wizardWeaknesses = new List<string> { "Intimidating", "Rushed", "Inquisitive" };
		List<string> chubbieWeaknesses = new List<string> { "Condescending", "Wizecracking", "Kind" };	//ADDITION BY WEDUNNIT
		List<string> scientistWeaknesses = new List<string> { "Forceful", "Coaxing", "Inspiring" };		//ADDITION BY WEDUNNIT
		List<string> reginaldWeaknesses = new List<string> { "Forceful", "Coaxing", "Inspiring" };			//ADDITION BY WEDUNNIT
		List<string> hemanWeaknesses = new List<string> { "Condescending", "Rushed", "Kind" };				//ADDITION BY WEDUNNIT



		//Defining NPC's
		NonPlayerCharacter reginald = new NonPlayerCharacter("Reginald Montgomery IV", reginaldSprite, "Reginald M IV", reginaldPref, reginaldWeaknesses, reginaldResponses);	//ADDITION BY WEDUNNIT
		NonPlayerCharacter pirate = new NonPlayerCharacter("Captain Bluebottle", pirateSprite, "Salty Seadog", piratePref, pirateWeaknesses, pirateResponses);
		NonPlayerCharacter mimes = new NonPlayerCharacter("The Mime Twins", mimesSprite, "mimes", mimesPref, mimeWeaknesses, mimeResponses);
		NonPlayerCharacter millionaire = new NonPlayerCharacter("Sir Worchester", millionaireSprite, "Money Bags", millionarePref, millionaireWeaknesses, millionaireResponses);
		NonPlayerCharacter cowgirl = new NonPlayerCharacter("Jesse Ranger", cowgirlSprite, "Outlaw", cowgirlPref, cowgirlWeaknesses, cowgirlResponses);
		NonPlayerCharacter roman = new NonPlayerCharacter("Celcius Maximus", romanSprite, "Legionnaire", romanPref, romanWeaknesses, romanResponses);
		NonPlayerCharacter wizard = new NonPlayerCharacter("Randolf the Deep Purple", wizardSprite, "Dodgy Dealer", wizardPref, wizardWeaknesses, wizardResponses);
		NonPlayerCharacter chubbie = new NonPlayerCharacter("Tinky Wobbly", chubbieSprite, "Telechubbie", chubbiePref, chubbieWeaknesses, chubbieResponses);					//ADDITION BY WEDUNNIT
		NonPlayerCharacter heman = new NonPlayerCharacter("HisMan", hemanSprite, "Superhero", hemanPref, hemanWeaknesses, hemanResponses);										//ADDITION BY WEDUNNIT
		NonPlayerCharacter scientist = new NonPlayerCharacter("Dr Emmanuel Brown", scientistSprite, "Mad scientist", scientistPref, scientistWeaknesses, scientistResponses);	//ADDITION BY WEDUNNIT



		//Defining Scenes
		Scene controlRoom = new Scene("Control Room");
		Scene kitchen = new Scene("Kitchen");
		Scene lectureTheatre = new Scene("Lecture Theatre");
		Scene lakehouse = new Scene("Lakehouse");
		Scene islandOfInteraction = new Scene("Island of Interaction");
		Scene roof = new Scene("Roof");
		Scene atrium = new Scene("Atrium");
		Scene undergroundLab = new Scene("Underground Lab");

		//Defining Items
		MurderWeapon cutlass = new MurderWeapon(cutlassPrefab, "Cutlass", "A worn and well used cutlass", cutlassSprite, "SD");
		MurderWeapon poison = new MurderWeapon(poisonPrefab, "Empty Poison Bottle", "This had poison in once ", poisonSprite, "SD");
		MurderWeapon garrote = new MurderWeapon(garrotePrefab, "Garrote", "Used for strangling someone... to death!", garroteSprite, "SD");
		MurderWeapon knife = new MurderWeapon(knifePrefab, "Knife", "A sharp tool meant for cutting meat", knifeSprite, "SD");
		MurderWeapon laserGun = new MurderWeapon(laserGunPrefab, "Laser Gun", "It's still warm, implying it has been recently fired", laserGunSprite, "SD");
		MurderWeapon leadPipe = new MurderWeapon(leadPipePrefab, "Lead Pipe", "It's a bit battered with a few dents on the side", leadPipeSprite, "SD");
		MurderWeapon westernPistol = new MurderWeapon(westernPistolPrefab, "Western Pistol", "The gunpowder residue implies it has been recently fired", westernPistolSprite, "SD");
		MurderWeapon wizardStaff = new MurderWeapon(wizardStaffPrefab, "Wizard Staff", "The gems still seem to be glow as if it has been used recently", wizardStaffSprite, "SD");

		Item beret = new Item(beretPrefab, "Beret", "A hat most stereotypically worn by the French", beretSprite);
		Item footprints = new Item(footprintsPrefab, "Bloody Footprints", "Bloody footprints most likely left by the murderer", footprintsSprite);
		Item gloves = new Item(glovesPrefab, "Bloody Gloves", "Bloody gloves most likely used by the murderer", glovesSprite);
		Item wine = new Item(winePrefab, "Fine Wine", "An expensive vintage that's close to 100 years old", wineSprite);
		Item shatteredGlass = new Item(shatteredGlassPrefab, "Shattered Glass", "Broken glass shards spread quite close together", shatteredGlassSprite);
		Item shrapnel = new Item(shrapnelPrefab, "Shrapnel", "Shrapnel from an explosion or gun being fired", shrapnelSprite);
		Item smellyDeath = new Item(smellyDeathPrefab, "Smelly Ashes", "All that remains of the victim", smellyDeathSprite);
		Item spellbook = new Item(spellbookPrefab, "Spellbook", "A spellbook used by those who practise in the magic arts", spellbookSprite);
		Item tripwire = new Item(tripwirePrefab, "Tripwire", "A used tripwire most likely used to immobilize the victim", tripwireSprite);
		Item chefHat = new Item(chefHatPrefab, "Chef's Hat", "A clean and fresh smelling hat, worn by chefs.", chefHatSprite); 		//ADDITION BY WEDUNNIT
		Item whistle = new Item(whistlePrefab, "Whistle", "A bright, shiny whistle that's as clean as... well.", whistleSprite); 	//ADDITION BY WEDUNNIT
		Item toast = new Item(toastPrefab, "Toast", "A slice of well buttered toast. It's slightly warm.", toastSprite); 			//ADDITION BY WEDUNNIT
		Item stapler = new Item(staplerPrefab, "Stapler", "A bright red stapler, with no staples in it.", staplerSprite); 			//ADDITION BY WEDUNNIT
		Item seaweed = new Item(seaweedPrefab, "Seaweed", "Oceanman, take me by the hand lead me to the land, that you understand.", seaweedSprite); 		//ADDITION BY WEDUNNIT
		Item sandwitch = new Item(sandwichPrefab, "Sandwich", "A ham sandwich with cheese and lettuce on white bread.", sandwitchSprite); //ADDITION BY WEDUNNIT
		Item purse = new Item(pursePrefab, "Fancy Purse", "A finely made, hand crafted, now-empty purse.", purseSprite); 			//ADDITION BY WEDUNNIT
		Item plunger = new Item(plungerPrefab, "Plunger", "A toilet plunger. It hasn't been used recently.", plungerSprite); 		//ADDITION BY WEDUNNIT
		Item monocle = new Item(monoclePrefab, "Monocle", "A finely made monocle, complete with gold chain.", monocleSprite); 		//ADDITION BY WEDUNNIT
		Item feather = new Item(featherPrefab, "Feather", "A goose feather, apparently freshly plucked.", featherSprite); 			//ADDITION BY WEDUNNIT
		Item glasses = new Item(glassesPrefab, "Safety Glasses", "Safety glasses good for keeping splinters and acid out of the eyes.", glassesSprite); 			//ADDITION BY WEDUNNIT
		Item dumbbell = new Item(dumbbellPrefab, "Dumbbbell", "Dumbbells; the source of pure strength.", dumbbellSprite); 			//ADDITION BY WEDUNNIT

		murderWeapons = new MurderWeapon[8] { cutlass, poison, garrote, knife, laserGun, leadPipe, westernPistol, wizardStaff };
		itemClues = new Item [21] {
			beret,
			footprints,
			gloves,
			wine,
			shatteredGlass,
			shrapnel,
			smellyDeath,
			spellbook,
			tripwire,
			whistle,
			chefHat,
			toast,
			stapler,
			seaweed,
			sandwitch,
			purse,
			plunger,
			monocle,
			feather,
			dumbbell,
			glasses
		};
		characters = new NonPlayerCharacter[10] {
			pirate,
			mimes,
			millionaire,
			cowgirl,
			roman,
			wizard,
			heman,
			chubbie,
			scientist,
			reginald
		};
		scenes = new Scene[8] { atrium, lectureTheatre, lakehouse, controlRoom, kitchen, islandOfInteraction, roof, undergroundLab };
	}

	void AssignNPCsToScenes(NonPlayerCharacter[] characters, Scene[] scenes) {
		int sceneCounter = 0;
		Shuffler shuffler = new Shuffler();
		shuffler.Shuffle(characters);
		shuffler.Shuffle(scenes);
		foreach(NonPlayerCharacter character in characters) { 		//For every character in the randomly shuffled array
			scenes[sceneCounter].AddNPCToArray(character);		//Assign a character to a scene
			sceneCounter += 1;										//Increment sceneCounter
			if(sceneCounter >= scenes.Length) {					//If the counter is above the number of scenes cycle to the first scene.  
				sceneCounter = 0;
			}
		}

	}

	void AssignItemsToScenes(Item[] items, Scene[] scenes) {
		int sceneIndex = 0;
		Shuffler shuffler = new Shuffler();
		shuffler.Shuffle(items);
		shuffler.Shuffle(scenes);
		foreach(Item item in items) {
			scenes[sceneIndex].AddItemToArray(item);
			sceneIndex++;
			if(sceneIndex > scenes.Length) {				
				sceneIndex = 0;
			}
		}
	}

	public void CreateNewGame(PlayerCharacter detective) { //Called when the player presses play
		//Reset values from a previous playthough
		ResetNotebook();
		ResetAll(scenes);
		//Create a Scenario
		scenario = new Scenario(murderWeapons, itemClues, characters);
		scenario.chooseMotive();
		string motive = scenario.getMotive();
		murderer = scenario.chooseMurderer();
		scenario.chooseWeapon();
		MurderWeapon weapon = scenario.getWeapon();
		scenario.CreateVerbalClues(motive, weapon, murderer); 
		scenario.BuildCluePools(motive, murderer, weapon);
		scenario.DistributeVerbalClues(murderer);
		itemClues = scenario.getItemCluePool().ToArray();
		characters = scenario.getNPCs();
		verbalClues = scenario.getVerbalCluePool().ToArray();
		relevant_items = scenario.getRelevantItems();
		relevant_verbal_clues = scenario.getRelevantVerbalClues();
		relevantClues = scenario.getRelevantClues(); 
		//Assign To rooms
		AssignNPCsToScenes(characters, scenes);					//Assigns NPCS to scenes
		AssignItemsToScenes(itemClues, scenes);					//Assigns Items to scenes
		playerCharacter = detective;

		gameScore = 1000;	//ADDITION BY WEDUNNIT
	}
		
	/// <summary>
	/// Decreases the player's score by the given amount.
	/// Raises error if penalty is less than 0.
	/// </summary>
	/// <param name="penalty">Penalty.</param>
	public void Penalise (float penalty){	//ADDITION BY WEDUNNIT
		Debug.Assert(penalty >= 0, "Penalise called with penalty less than 0");
		gameScore -= penalty;
	}

	/// <summary>
	/// Increases score by bonus, raises error if bonus less than 0, also turns score green.
	/// </summary>
	/// <param name="bonus">The amount the score should be incresaed by.</param>
	public void GainScore(float bonus){		//ADDITION BY WEDUNNIT
		Debug.Assert (bonus >= 0, "gainScore called with bonus less than 0");
		gameScore += bonus;
		LevelManager ActiveManager = FindObjectOfType<LevelManager>();
		ActiveManager.OnScoreIncrease();
	}
		
	/// <summary>
	/// Returns the score of the player, as an int.
	/// </summary>
	/// <returns>The score of the player.</returns>
	public int GetScore (){	//ADDITION BY WEDUNNIT
		int intScore = (int)gameScore;
		return intScore;
	}


	public PlayerCharacter GetPlayerCharacter() {
		return playerCharacter;
	}

	public Scene GetScene(string sceneName) {
		for(int i = 0; i < scenes.Length; i++) {
			if(scenes[i].GetName() == sceneName) {
				return scenes[i];
			} 
		}
		return null;
	}

	public void ResetAll(Scene[] scenes) {
		foreach(Scene scene in scenes) {
			scene.ResetScene();
		}

	}

	public List<Item> GetRelevantItems() {
		return this.relevant_items;
	}

	public List<VerbalClue> GetRelevantVerbalClues() {
		return this.relevant_verbal_clues;
	}

	public string GetMurderer() {
		return this.murderer.getCharacterID();
	}

	private void ResetNotebook() {
		NotebookManager.instance.ResetSelectedClues();
		NotebookManager.instance.logbook.Reset();	//Reset logbook
		NotebookManager.instance.inventory.Reset();	//Reset inventory
		NotebookManager.instance.UpdateNotebook();
	}

	/// <summary>
	/// Unblocks all characters to allow interrogation if they have been accused.
	/// Called when any clue is added to the logbook
	/// </summary>
	public void UnblockAllCharacters() {
		foreach(Character character in characters) {
			character.AllowCharacterQuestioning();
		}
	}

		
}
