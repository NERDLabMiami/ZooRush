using UnityEngine;
using System.Collections;

/** Controls user interaction with the splash screen.
 *	@author Ebtissam Wahman
 */
public class StartMenu : MonoBehaviour
{
	public Button play;

	void Start ()
	{
//		if (PlayerPrefs.GetInt ("Music") != 0) {
//			audio.mute = false;
//		} else {
//			audio.mute = true;
//		}

		BreadCrumbs.previousScene = Application.loadedLevelName;

		if (PlayerPrefs.GetInt ("Levels Unlocked", 1) > 1) { //Default value is 1 for level 1
			play.SceneName = "LevelSelect";
		} else {
			if (PlayerPrefs.GetInt ("Level1-TutorialStory", 0) == 0) {
				play.SceneName = "CharacterSelect";
			} else {
				play.SceneName = "Level1-Tutorial";
			}

		}
	}
}
