// Here is a precise URL of the executable on the team website
// http://wedunnit.me/webfiles/ass3/HomicideInTheHub-Win.zip

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVScript : MonoBehaviour {
	private int image;
	private int version;
	private float time;
	private int frameCounter;
	// Use this for initialization
	void Start () {
		image = Random.Range (0, 6); //select random scene
		time = Time.time;
		version = 0; //versions to add flickering
		frameCounter = 0;
		this.GetComponent<SpriteRenderer> ().sprite = Resources.Load <Sprite>("Sprites/cctv" + image + version);
	}
	
	// Update is called once per frame
	void Update () {
		if (frameCounter > 3) { //only change version every 3rd frame
			if (version == 0) {
				version = 1;
			} else {
				version = 0;
			}
			frameCounter = 0;
		}

		this.GetComponent<SpriteRenderer> ().sprite = Resources.Load <Sprite>("Sprites/cctv" + image + version);

		if ((Time.time - time) > 5) { //swap scenes every 5 seconds
			time = Time.time;
			image = Random.Range (0, 7);
		}

		frameCounter++;
	}
}
