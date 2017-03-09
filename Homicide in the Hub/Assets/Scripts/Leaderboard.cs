// Here is a precise URL of the executable on the team website
// http://wedunnit.me/webfiles/ass3/HomicideInTheHub-Win.zip

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Linq;


public class Leaderboard : MonoBehaviour
{
	//CLASS ADDITION BY WEDUNNIT
	/// <summary>
	/// The text object that displays the scores of the leaderboard.
	/// </summary>
    public Text scoreGUI;	//dragged in unity editor
	/// <summary>
	/// The text object that displays the names of the players on the leaderboard.
	/// </summary>
    public Text nameGUI;

	private List<KeyValuePair<string,int>> scoreList = new List<KeyValuePair<string,int>>();	//List of pairs


	/// <summary>
	/// Gets the scores from file & stores them in scoreList.
	/// </summary>
	private void GetScores(){
		KeyValuePair<string,int> scorePair = new KeyValuePair<string,int> ();
		using (StreamReader sr = new StreamReader("leaderboard.txt"))
		{
			int score;
			string name;
			while (sr.EndOfStream == false){
				name = sr.ReadLine();
				score = int.Parse(sr.ReadLine());
				scorePair = new KeyValuePair<string,int> (name, score);
				//print (scorePair.Key);
				//print (scorePair.Value);
				scoreList.Add (scorePair);
			}
			sr.Close();
		}
		scoreList = scoreList.OrderByDescending(x => x.Value).ToList();	//sorts list based on value using linq
	}

	/// <summary>
	/// Shows the scores on leaderboard.
	/// </summary>
	private void ShowScores(){
		string scoreText = "";	//string to be showin in textbox
		string nameText = "";
		for (int i = 0; i < scoreList.Count; i++) {
			scoreText = scoreText + scoreList [i].Value + "\r\n";
			nameText = nameText + scoreList [i].Key + "\r\n";
		}
		if (scoreGUI != null) {
			scoreGUI.text = scoreText;
		}
		if (nameGUI != null) {
			nameGUI.text = nameText;
		}
	}

    // Use this for initialization
    void Start()
    {
		GetScores ();
		ShowScores ();
    }
 }

