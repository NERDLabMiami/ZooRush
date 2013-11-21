using UnityEngine;
using System.Collections;

/** Sets up all necessary player prefs if they have not yet been set.
 *	@author Ebtissam Wahman
 */
public class GameSetup : MonoBehaviour
{

	void Start ()
	{
		if (!PlayerPrefs.HasKey ("Character Selection")) { //Character we will be playing in the game.
			PlayerPrefs.SetInt ("Character Selection", 0); //Default is the boy
		}
		if (!Input.simulateMouseWithTouches) {
			Input.simulateMouseWithTouches = true;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
