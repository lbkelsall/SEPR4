using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Scenario
	{

	// Arrays to store elements passed to Scenario class from GameMaster
	private MurderWeapon[] weapons;
	private Item[] item_clues;
	private VerbalClue[] verbal_clues;
	private NonPlayerCharacter[] npcs;

	// Intialising variables which will be returned to GameMaster
	private List<Item> item_clue_pool = new List<Item> ();
	private List<VerbalClue> verbal_clue_pool = new List<VerbalClue> ();
	private List<Item> relevant_item_clues = new List<Item> ();
	private List<VerbalClue> relevant_verbal_clues = new List<VerbalClue> ();
	private MurderWeapon weapon;
	private string motive;

	/// <summary>
	/// A convenience property to store the name of the murderer.
	/// </summary>
	private string murdererName; //ADDITION BY WEDUNNIT

	private string[] motives = { "homewrecker", "loanshark", "promotion", "unfriended", "blackmail", "avenge_friend", "avenge_pet" };

	// Constructor for Scenario
	public Scenario (MurderWeapon[] murder_weapons, Item[] items, NonPlayerCharacter[] characters)
	{
		weapons = murder_weapons;
		item_clues = items;
		npcs = characters;
	}

	// Chooses a random weapon from the MurderWeapon array 'weapons'
	public void chooseWeapon() {
		Shuffler shuffler = new Shuffler ();
		shuffler.Shuffle (weapons);
		weapon = weapons [0];
	}
			
	// Chooses a random motive from the string[] 'motives' initialised above
	public void chooseMotive() {
		Shuffler shuffler = new Shuffler ();
		shuffler.Shuffle (motives);
		motive = motives [0];
	}

	// Chooses and returns a random murderer from the NonPlayerCharacter[] 'npcs'
	public NonPlayerCharacter chooseMurderer() {
		Shuffler shuffler = new Shuffler ();
		shuffler.Shuffle (npcs);
		NonPlayerCharacter murderer = npcs [0];
		murderer.SetAsMurderer ();
		return murderer;
	}
	/// <summary>
	/// Returns List of all Non murderer NPCs' nicknames
	/// </summary>
	private string getRandomNonMurderingNPCName(string murdererName){ //ADDITION BY WEDUNNIT
		List<string> nonMurderingNPCNames = new List<string> ();
		foreach (Character NPC in npcs) {
			if(NPC.getNickname() != murdererName){
				nonMurderingNPCNames.Add (NPC.getNickname());
			}
		}

		return nonMurderingNPCNames [Random.Range (0, nonMurderingNPCNames.Count)];
	}


	private string getMotiveClause(string motive){ //REFACTORED BY WEDUNNIT
		string motiveClause = ".";
		switch (motive) {
		case "homewrecker":
			string partner_gender;
			int binary = Random.Range (0, 1);
			if (binary == 0) {
				partner_gender = "wife";
			} else {
				partner_gender = "husband";
			}
			motiveClause = "because the victim slept with one of their " + partner_gender + "s.";  //UPDATED BY WEDUNNIT
			return motiveClause;
		case "loanshark":
			motiveClause = "because the victim was a bit of a loanshark..."; 	//UPDATED BY WEDUNNIT
			return motiveClause;
		case "promotion":
			motiveClause = "because they were in competition professionally. The victim got the promotion...";	//UPDATED BY WEDUNNIT
			return motiveClause;
		case "unfriended":
			motiveClause = "because of scome shenanigans on Facebook.";	//UPDATED BY WEDUNNIT
			return motiveClause;
		case "blackmail":
			motiveClause = "because the victim was a stalker, and knew their darkest secrets.";	//UPDATED BY WEDUNNIT
			return motiveClause;
		case "avenge_friend":
			motiveClause = "because I think there was something to do with a friend's death."; 	//UPDATED BY WEDUNNIT
			return motiveClause;
		case "avenge_pet":
			string species;
			int rand = Random.Range (0, 4);
			if (rand == 0) {
				species = "parrot";
			} else if (rand == 1) {
				species = "chihuahua";
			} else if (rand == 2) {
				species = "iguana";
			} else if (rand == 3) {
				species = "goldfish";
			} else {
				species = "fox";
			}
			string cause_of_death;
			rand = Random.Range (0, 7);
			if (rand == 0) {
				cause_of_death = "starvation";
			} else if (rand == 1) {
				cause_of_death = "loneliness";
			} else if (rand == 2) {
				cause_of_death = "a broken heart";
			} else if (rand == 3) {
				cause_of_death = "boredom";
			} else if (rand == 4) {
				cause_of_death = "injuries sustained in the Hub"; 	//ADDITION BY WEDUNNIT
			} else if (rand == 5) {
				cause_of_death = "unspecified injuries";			//ADDITION BY WEDUNNIT
			} else if (rand == 6) {
				cause_of_death = "death";							//ADDITION BY WEDUNNIT		
			} else {
				cause_of_death = "electrocution";
			}
			motiveClause = " because the victim was looking after either " + getRandomNonMurderingNPCName (murdererName) + " or " + murdererName + "'s " + species + " when it " +
			"died of " + cause_of_death + ", I can't remember.";  
			return motiveClause;
		default:
			throw new System.ArgumentOutOfRangeException ("No match for motive");
		}
	}

	// given a murderer, weapon and motive, creates a VerbalClue[] containing 6 relevant verbal clues
	public void CreateVerbalClues(string motive, MurderWeapon weapon, NonPlayerCharacter murderer) { //UPDATED BY WEDUNNIT

		murdererName = murderer.getNickname (); //UPDATED BY WEDUNNIT
		string weapon_name = weapon.getID ();

		VerbalClue disposing_of_weapon = new VerbalClue ("Disposing of a Weapon", "I saw "+murdererName+" trying to " +
			"dispose of a "+weapon_name+". Or was it "+ getRandomNonMurderingNPCName (murdererName)+"? I can't remember."); //UPDATED BY WEDUNNIT

		VerbalClue old_friends = new VerbalClue ("Old Friends", "The victim fell out with with " + getRandomNonMurderingNPCName (murdererName) + " and "+murdererName+" a long time ago "+ getMotiveClause(motive)); //UPDATED BY WEDUNNIT

		VerbalClue old_enemies = new VerbalClue ("Old Enemies", "Rumour is that the victim had an unpleasant past with "+murdererName+"."); //UPDATED BY WEDUNNIT

		VerbalClue last_seen_with = new VerbalClue ("Last Seen With", "I saw the victim talking with "+getRandomNonMurderingNPCName(murdererName)+" and " +murdererName+" just a few minutes before their body was discovered."); //UPDATED BY WEDUNNIT

		VerbalClue altercation = new VerbalClue ("An Altercation", "There once was an altercation between" + getRandomNonMurderingNPCName (murdererName) + " and "+murdererName+", " + getMotiveClause(motive)); //UPDATED BY WEDUNNIT

		VerbalClue changed_story = new VerbalClue ("Stories Have Changed", murdererName+ " told me they last saw the victim before 8pm, but told "+getRandomNonMurderingNPCName(murdererName)+" that they didnt speak to the victim at all..."); //UPDATE BY WEDUNNIT

		verbal_clues = new VerbalClue[6] {
			old_friends,
			altercation,
			disposing_of_weapon,
			old_enemies,
			last_seen_with,
			changed_story
		};
	}

	// Creates two lists of clues, one containing 3 verbal clues (two relevant, one useless) and one containing 6 item clues (2 relevent, 4 useless).
	public void BuildCluePools(string motive, NonPlayerCharacter murderer, MurderWeapon weapon) {

		// one of the relevant item clues is the murder weapon, there is only ever one MurderWeapon item present in the game.
		item_clue_pool.Add (weapon);
		relevant_item_clues.Add (weapon);

		int verbalClueWithMotive = Random.Range (0, 2); // UPDATED BY WEDUNNIT either 'old friends' or 'altercation' for the first verbal clue
		verbal_clue_pool.Add (verbal_clues [verbalClueWithMotive]);			//ADDITION BY WEDUNNIT
		relevant_verbal_clues.Add (verbal_clues [verbalClueWithMotive]);	//ADDITION BY WEDUNNIT

		int verbalClueWithWeapon = Random.Range (2, 6); // UPDATED BY WEDUNNIT any of the remaining verbal clues for the second verbal clue
		verbal_clue_pool.Add (verbal_clues [verbalClueWithWeapon ]);
		relevant_verbal_clues.Add (verbal_clues [verbalClueWithWeapon ]);

		switch (murdererName) {	//REFACTORED BY WEDUNNIT
		case "Salty Seadog":
			item_clue_pool.Add (item_clues [4]); // shattered glass
			relevant_item_clues.Add (item_clues [4]);
			break;
		case "mimes":
			item_clue_pool.Add (item_clues [0]); // beret
			relevant_item_clues.Add (item_clues [0]);
			break;
		case "Money Bags":
			item_clue_pool.Add (item_clues [2]); // gloves
			relevant_item_clues.Add (item_clues [2]);
			break;
		case "Outlaw":
			item_clue_pool.Add (item_clues [8]); // tripwire
			relevant_item_clues.Add (item_clues [8]);
			break;
		case "Legionnaire":
			item_clue_pool.Add (item_clues [3]); // wine
			relevant_item_clues.Add (item_clues [3]);
			break;
		case "Dodgy Dealer":
			item_clue_pool.Add (item_clues [7]); // spellbook
			relevant_item_clues.Add (item_clues [7]);
			break;
		case "Superhero":
			item_clue_pool.Add (item_clues [19]); 		//Dumbbells 	//ADDITION BY WEDUNNIT
			relevant_item_clues.Add (item_clues [19]); 	//ADDITION BY WEDUNNIT
			break;
		case "Mad scientist":
			item_clue_pool.Add (item_clues [18]); 		// glasses	//ADDITION BY WEDUNNIT
			relevant_item_clues.Add (item_clues [18]);	//ADDITION BY WEDUNNIT
			break;
		case "Telechubbie":
			item_clue_pool.Add (item_clues [15]); 	//purse			//ADDITION BY WEDUNNIT
			relevant_item_clues.Add (item_clues [15]);	//ADDITION BY WEDUNNIT
			break;
		case "Reginald M IV":
			item_clue_pool.Add (item_clues [17]); 	//monocle			//ADDITION BY WEDUNNIT
			relevant_item_clues.Add (item_clues [17]);		//ADDITION BY WEDUNNIT
			break;
		default:
			throw new System.ArgumentException (murdererName + " does not have any clues associated with them.");
		}

		// add the 4 irrelevant verbal clues
		while (item_clue_pool.Count() < 6) {
			int index = Random.Range (0, item_clues.Count()-1);
			if (item_clue_pool.Contains (item_clues [index]) == false) {
				item_clue_pool.Add (item_clues [index]);
			}
		}
			
		// add 'red herring' verbal clue
		int weapon_index = Random.Range(0,weapons.Count()-1);
		string red_herring_weapon = weapons [weapon_index].getID ();
		while (red_herring_weapon == weapon.getID() ) {
			weapon_index = Random.Range(0,weapons.Count()-1);
			red_herring_weapon = weapons [weapon_index].getID ();
		}
		int npc_index = Random.Range(0,npcs.Count()-1);
		string red_herring_character = npcs [npc_index ].getCharacterID  ();
		while (red_herring_character == murderer.getCharacterID () ) {
			npc_index = Random.Range(0,npcs.Count()-1);
			red_herring_character = npcs [npc_index ].getCharacterID ();
		}


		// create and then choose one of two irrelevant verbal clues
		VerbalClue police_failure = new VerbalClue ("Lack of Evidence", "The police think the victim was killed using a " + red_herring_weapon + ", but they can’t find evidence of one.");

		VerbalClue shifty_looking = new VerbalClue ("Looking Shifty", "I think I saw "+red_herring_character+" acting suspiciously.");

		VerbalClue[] red_herring_verbal_clues = new VerbalClue[2] { police_failure, shifty_looking };
		int herring_index = Random.Range (0,1);
		verbal_clue_pool.Add (red_herring_verbal_clues [herring_index]);
	}

	// distribute the 3 verbal clues among the NPC characters in NonPlayerCharacter[] 'npcs'
	public void DistributeVerbalClues(NonPlayerCharacter murderer) {
		int index = 0;
		List<NonPlayerCharacter> npcs_list = npcs.ToList (); 
		npcs_list.Remove (murderer);
		while (index < verbal_clue_pool.Count()) {
			NonPlayerCharacter character = npcs_list [Random.Range (0, npcs_list.Count ())];
			if (character.getVerbalClue() == null) {
				character.setVerbalClue (verbal_clue_pool [index]);
				verbal_clue_pool [index].SetOwner (character); 
				index++;
			}
		}
		npcs_list.Add (murderer);
		npcs = npcs_list.ToArray (); 
	}

	// Setters and Accessors
	public List<Item> getItemCluePool () {
		return item_clue_pool; 
	}

	public List<VerbalClue> getVerbalCluePool () {
		return verbal_clue_pool;
	}

	public MurderWeapon getWeapon () {
		return weapon;
	}

	public string getMotive() {
		return motive;
	}

	public NonPlayerCharacter[] getNPCs () {
		return npcs;
	}

	public List<Item> getRelevantItems () {
		return relevant_item_clues;
	}

	public List<VerbalClue> getRelevantVerbalClues () {
		return relevant_verbal_clues; 
	}

	public List<Clue> getRelevantClues () {
		List<Clue> relevant_clues = new List<Clue> ();

		for (int i = 0; (relevant_verbal_clues.Count) > i; i++) {
			relevant_clues.Add (relevant_verbal_clues [i]);
		}
		for (int i = 0; (relevant_item_clues.Count) > i; i++) {
			relevant_clues.Add (relevant_item_clues [i]);
		}
		return relevant_clues;
	}
}