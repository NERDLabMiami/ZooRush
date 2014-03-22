using UnityEngine;
using System.Collections;

/** Controls user interaction with the splash screen.
 *	@author Ebtissam Wahman
 */
public class StartMenu : MonoBehaviour
{
	public DirectToScene playButton;
	public DirectToScene instructionsButton;

	void Start ()
	{
		if (PlayerPrefs.GetInt ("Music") != 0) {
			audio.mute = false;
		} else {
			audio.mute = true;
		}

		Application.targetFrameRate = 60;

		if (PlayerPrefs.GetInt ("Levels Unlocked", 0) > 1) {
			playButton.sceneName = "LevelSelect";
		} else {
			if (PlayerPrefs.GetInt ("Level1-TutorialStory", 0) == 0) {
				playButton.sceneName = "CharacterSelect";
			} else {
				playButton.sceneName = "Level1-Tutorial";
			}

		}
	}
}
