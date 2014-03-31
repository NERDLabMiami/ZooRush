using UnityEngine;
using System.Collections;

/** Sets up all necessary player prefs if they have not yet been set.
 *	@author Ebtissam Wahman
 */
public class GameSetup : MonoBehaviour
{

	void Awake ()
	{
		Application.targetFrameRate = 60;

		//INPUT RELATED VALUES
		if (!Input.simulateMouseWithTouches) {
			Input.simulateMouseWithTouches = true;
		}
		
		//CHARACTER BASED VALUES
		if (!PlayerPrefs.HasKey ("Character Selection")) { //Character we will be playing in the game.
			PlayerPrefs.SetInt ("Character Selection", 0); //Default is the boy
		}
		
		//SOUND RELATED VALUES
		if (!PlayerPrefs.HasKey ("Music")) { //Music Settings
			PlayerPrefs.SetInt ("Music", 1); //Default is 1 = "ON"
		}
		if (!PlayerPrefs.HasKey ("Sound")) { //Sound Settings
			PlayerPrefs.SetInt ("Sound", 1); //Default is 1 = "ON"
		}
		
		if (!PlayerPrefs.HasKey ("Volume")) { //Volume Settings
			PlayerPrefs.SetFloat ("Volume", 1.0f);	//Default is 100%
		}

		if (!PlayerPrefs.HasKey ("Levels Unlocked")) { //Number of Levels unlocked
			PlayerPrefs.SetInt ("Levels Unlocked", 1); //Default is 1 for tutorial, level 1
		}
		
	}

}
