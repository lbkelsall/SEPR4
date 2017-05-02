// Here is a precise URL of the executable on the team website
// http://www-users.york.ac.uk/~phj501/Executable_4.zip

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TraverseRooms : MonoBehaviour {
	//Used on the map to load the appropriate level.
	//Placed on the child objects of the map defining the hitboxes (Polygon Collider 2D) 

	public string level;	//Public to allow for changing in inspector.

	//When the area on the map is clicked load the respective level
	void OnMouseDown() {
		SceneManager.LoadScene(level);
	}
}
