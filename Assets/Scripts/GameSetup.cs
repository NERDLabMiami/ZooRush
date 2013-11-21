using UnityEngine;
using System.Collections;

/** Sets up all necessary player prefs if they have not yet been set.
 *	@author Ebtissam Wahman
 */
public class GameSetup : MonoBehaviour
{

	void Start ()
	{
		//INPUT RELATED VALUES
		if (!Input.simulateMouseWithTouches) {
			Input.simulateMouseWithTouches = true;
		}
		
		//CHARACTER BASED VALUES
		if (!PlayerPrefs.HasKey ("Character Selection")) { //Character we will be playing in the game.
			PlayerPrefs.SetInt ("Character Selection", 0); //Default is the boy
		}
		
		//SOUND RELATED VALUES
		if (!PlayerPrefs.HasKey ("Music")) { // set default values for music as ON
			PlayerPrefs.SetString ("Music", "ON");
		}
		
		if (!PlayerPrefs.HasKey ("Sound")) {// set default values for sound as ON
			PlayerPrefs.SetString ("Sound", "ON");
		}
		
		if (!PlayerPrefs.HasKey ("Volume")) {
			PlayerPrefs.SetFloat ("Volume", 1.0f);
		}
		
	}
	
	void Update ()
	{
	
	}
}
