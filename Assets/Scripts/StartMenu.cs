using UnityEngine;
using System.Collections;

/** Controls user interaction with the splash screen.
 *	@author Ebtissam Wahman
 */
public class StartMenu : MonoBehaviour
{
	void Start ()
	{
		if (PlayerPrefs.GetInt ("Music") != 0) {
			audio.mute = false;
		} else {
			audio.mute = true;
		}

		BreadCrumbs.previousScene = Application.loadedLevelName;

		if (PlayerPrefs.GetInt ("Levels Unlocked", 1) > 1) { //Default value is 1 for level 1
			BreadCrumbs.nextScene = "LevelSelect";
		} else {
			if (PlayerPrefs.GetInt ("Level1-TutorialStory", 0) == 0) {
				BreadCrumbs.nextScene = "CharacterSelect";
			} else {
				BreadCrumbs.nextScene = "Level1-Tutorial";
			}

		}
	}
}
